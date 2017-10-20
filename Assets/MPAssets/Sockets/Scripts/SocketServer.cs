using System;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MPAssets {
	public class SocketServer {
		#region  EVENTS
		public event EventHandler<SocketArgs> OnConnect {
			add {
				SocketServerWorker.OnConnect += value;
			}
			remove {
				SocketServerWorker.OnConnect -= value;
			}
		}
		public event EventHandler<SocketArgs> OnConnectionLost {
			add {
				SocketServerWorker.OnConnectionLost += value;
			}
			remove {
				SocketServerWorker.OnConnectionLost -= value;
			}
		}
		public event EventHandler<SocketArgs> OnClose {
			add {
				SocketServerWorker.OnClose += value;
			}
			remove {
				SocketServerWorker.OnClose -= value;
			}
		}
		public event EventHandler<SocketArgs> OnReceive {
			add {
				SocketServerWorker.OnReceive += value;
			}
			remove {
				SocketServerWorker.OnReceive -= value;
			}
		}
		public event EventHandler<SocketArgs> OnReceiveFailed {
			add {
				SocketServerWorker.OnReceiveFailed += value;
			}
			remove {
				SocketServerWorker.OnReceiveFailed -= value;
			}
		}
		public event EventHandler<SocketArgs> OnSendSucess {
			add {
				SocketServerWorker.OnSendSucess += value;
			}
			remove {
				SocketServerWorker.OnSendSucess -= value;
			}
		}
		public event EventHandler<SocketArgs> OnSendFailed {
			add {
				SocketServerWorker.OnSendFailed += value;
			}
			remove {
				SocketServerWorker.OnSendFailed -= value;
			}
		}
		#endregion

		#region VARIABLES
		TcpListener server = null;

		Thread mainThread;

		int idCounter = 0;
		List<SocketServerWorker> clients = new List<SocketServerWorker>();
		public Queue<KeyValuePair<int, COMData>> infoReceived = new Queue<KeyValuePair<int, COMData>>();

		string ip = "localhost";
		int port = 42222;
		#endregion

		#region SETUP
		public SocketServer(string _ip = "localhost", int _port = 42222) {
			port = _port;
		}

		public void StartServer(string _ip = "localhost", int _port = 42222) {
			//host = _host;// == "localhost" ? "127.0.0.1" : _host;
			IPHostEntry ipHostInfo = Dns.GetHostEntry(_ip);
			IPAddress ipAddress = ipHostInfo.AddressList[0];

			foreach (IPAddress ip in ipHostInfo.AddressList) {
				AddressFamily af = ip.AddressFamily;
				if (af == AddressFamily.InterNetwork) {
					ipAddress = ip;
					break;
				}
			}
			ip = ipAddress.ToString();
			//Log.AddToDebug(ip);

			port = _port;

			try {
				server = new TcpListener(ipAddress, port);

				// Start listening for client requests.
				server.Start();

				mainThread = new Thread(StartServer_threaded);
				mainThread.Start();

			}
			catch (Exception e) {
				Log.AddToLog("StartServer: " + e);
			}
		}

		void StartServer_threaded() {
			try {
				// Enter the listening loop.
				while (true) {
					Log.AddToLog("Waiting for a connection... ");

					TcpClient client = server.AcceptTcpClient();

					//OnReceiveList.Add(null);
					SocketServerWorker ssw = new SocketServerWorker(this, client, idCounter);
					clients.Add(ssw);


					Thread _thread = new Thread(() => ssw.RunWorker());
					_thread.Start();

					Log.AddToLog(idCounter++ + "|Client Connected!");
				}
			}
			catch (SocketException e) {
				Log.AddToLog("SocketException: " + e);
			}
			finally {
				// Stop listening for new clients.
				server.Stop();
			}
		}
		#endregion

		#region MANAGE
		public bool IsAlive(int id) {
			foreach (SocketServerWorker client in clients) {
				if (client.id == id) {
					return client.isConnected;
				}
			}
			return false;
		}

		public void Close() {
			if (mainThread != null)
				mainThread.Abort();

			CloseAllConnections();

			server.Stop();
		}
		public void CloseAllConnections() {
			foreach (SocketServerWorker client in clients) {
				client.Close(false);
			}

			clients.Clear();
		}

		public void RemoveClient(SocketServerWorker ssw) {
			clients.Remove(ssw);
			//int idx = clients.IndexOf(ssw);
			//clients[idx] = null;
		}
		#endregion

		#region COMMUNICATION
		public bool SendMessage(int id, string msg) {
			foreach (SocketServerWorker client in clients) {
				if (client.id == id) {
					client.SendInfo_text(msg);
					return true;
				}
			}
			return false;
		}

		public void BroadcastMessage(string msg) {
			foreach (SocketServerWorker client in clients) {
				client.SendInfo_text(msg);
			}
		}

		public void BroadcastImage(byte[] data, int width, int height) {
			foreach (SocketServerWorker client in clients) {
				client.SendInfo_image(data, width, height);
			}
		}

		public void BroadcastAudio(byte[] data) {
			foreach (SocketServerWorker client in clients) {
				client.SendInfo_audio(data);
			}
		}
		#endregion
	}
}