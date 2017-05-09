using System;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MPAssets {
	public class SocketServer {
		TcpListener server = null;

		Thread mainThread;

		int idCounter = 0;
		List<SocketServerWorker> clients = new List<SocketServerWorker>();
		public Queue<KeyValuePair<int, COMData>> infoReceived = new Queue<KeyValuePair<int, COMData>>();

		string ip = "192.168.1.7";
		int port = 42222;

		public SocketServer(int _port = 42222) {
			port = _port;
		}

		#region SETUP
		public void StartServer(string _ip = "192.168.1.7") {
			ip = _ip;

			try {
				server = new TcpListener(IPAddress.Parse(ip), port);

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

		//	void UpdateClientList(){
		//		string str = clients.Count + " Clients Connected\n{";
		//
		//		for(int i = 0 ; i < clients.Count ; ++i){
		//			str += clients[i].id + ((i < (clients.Count-1)) ? "," : "");
		//		}
		//
		//		str += "}";
		//
		//		GameObject.Find("Text_Clients").GetComponent<Text>().text = str;
		//	}
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