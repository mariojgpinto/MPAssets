using System;
using System.Threading;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MPAssets {
	public class SocketClient {
		#region EVENTS
		public event EventHandler<SocketArgs> OnConnect;
		public event EventHandler<SocketArgs> OnConnectionLost;
		public event EventHandler<SocketArgs> OnClose;
		public event EventHandler<SocketArgs> OnReceive;
		public event EventHandler<SocketArgs> OnReceiveFailed;
		public event EventHandler<SocketArgs> OnSendSucess;
		public event EventHandler<SocketArgs> OnSendFailed;
		#endregion

		#region VARIABLES
		//public event cenas;
		public bool run = true;

		TcpClient client;
		Socket socket;

		//bool _isConnected = false;
		public bool isConnected {
			set {
				//_isConnected = value;
			}
			get {
				return (socket != null) ? socket.Connected : false;
			}
		}

		public string host = "localhost";
		public int port = 42222;

		public string lastError;

		Thread mainThread = null;
		Thread readThread = null;
		Thread writeThread = null;

		private ManualResetEvent mainWaitInLine =
			new ManualResetEvent(false);
		//private ManualResetEvent readWaitInLine = 
		//	new ManualResetEvent(false);
		private ManualResetEvent writeWaitInLine =
			new ManualResetEvent(false);


		Queue<COMData> infoToSend = new Queue<COMData>();
		public Queue<COMData> infoReceived = new Queue<COMData>();

		const int bufferSize = 1024;
		byte[] mainBuffer = new byte[bufferSize];

		//bool bigIncoming = false;
		//byte[] auxTextBuffer = null;
		//byte[] auxImageBuffer = new byte[10000000];
		//byte[] auxAudioBuffer = null;

		#endregion

		#region SETUP
		public void RunEvent(EventHandler<SocketArgs> evt, SocketArgs args = null) {
			if (evt != null)
				evt(this, args);
		}
		public SocketClient(string _host = "localhost", int _port = 42222) {
			host = _host;
			port = _port;
		}

		public void TryToConnect() {
			Log.AddToDebug("Trying to Connect");
			mainThread = new Thread(Connect);
			mainThread.Start();
		}

		public void Connect() {
			while (run) {
				try {
					if (isConnected) {
						if (!IsConnected()) {
							CloseConnection();
						}
					}
					else {
						Log.AddToLog("Trying to Connect...");
						client = new TcpClient(host, port);
						Log.AddToLog("Connected");
						socket = client.Client;

						//isConnected = true;
						CustomClient.first = true;

						writeThread = new Thread(Send_Threaded);
						writeThread.Start();

						readThread = new Thread(Read_Threaded);
						readThread.Start();

						RunEvent(OnConnect);
					}

					mainWaitInLine.WaitOne(2000);
				}
				catch (Exception e) {
					Log.AddToDebug("setupSocket: " + e.ToString());
					lastError = e.ToString();
					//isConnected = false;
				}
			}
		}

		public void ConnectionLost() {
			//isConnected = false;

			if (readThread != null)
				readThread.Abort();

			if (writeThread != null)
				writeThread.Abort();

			if (socket != null) {
				Log.AddToDebug("socket.Shutdown");
				socket.Shutdown(SocketShutdown.Both);
				Log.AddToDebug("socket.Close");
				socket.Close();
			}

			mainWaitInLine.Set();

			RunEvent(OnConnectionLost);
		}

		void CloseConnection() {
			Log.AddToLog("Close Connection");
			//isConnected = false;

			//if(mainThread != null)
			//	mainThread.Abort();

			if (writeThread != null)
				writeThread.Abort();

			if (readThread != null)
				readThread.Abort();

			if (socket != null) {
				try {
					socket.Shutdown(SocketShutdown.Both);
					socket.Close();
				}
				catch (Exception e) {
					Log.AddToDebug(e.ToString());
				}
			}

			//RunEvent(OnClose);
		}

		public void Close() {
			Log.AddToLog("Client Close");
			run = false;

			CloseConnection();

			RunEvent(OnClose);
		}

		public bool IsConnected() {
			try {
				return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
			}
			catch (SocketException) { return false; }
		}
		#endregion

		#region SEND
		public void SendInfo_text(string text) {
			Log.AddToDebug("SendMessageRequest: " + text);
			infoToSend.Enqueue(new COMData_text(text));

			writeWaitInLine.Set();
		}

		public void SendInfo_image(byte[] data, int width, int height) {
			Log.AddToDebug("SendImageRequest: " + data.Length);
			infoToSend.Enqueue(new COMData_image(data, width, height));

			writeWaitInLine.Set();
		}

		public void SendInfo_audio(byte[] data) {
			Log.AddToDebug("SendAudioRequest: " + data.Length);
			infoToSend.Enqueue(new COMData_audio(data));

			writeWaitInLine.Set();
		}

		bool Send(COMData data) {
			if (!isConnected) {
				RunEvent(OnSendFailed);

				return false;
			}

			try {
				bool result = false;

				switch (data.type) {
					case COMData.TYPE.TEXT:
						result = SendText((COMData_text)data);
						break;
					case COMData.TYPE.IMAGE:
						result = SendImage((COMData_image)data);
						break;
					case COMData.TYPE.AUDIO:
						result = SendAudio((COMData_audio)data);
						break;
					default:
						break;
				}

				if (!result) {
					RunEvent(OnSendFailed);
					return false;
				}
			}
			catch {
				RunEvent(OnSendFailed);
				try {
					Close();
				}
				catch (Exception ex) {
					Log.AddToDebug(ex.ToString());
					return false;
				}

				return false;
			}
			RunEvent(OnSendSucess);
			return true;
		}

		bool SendText(COMData_text text) {
			string header = "";

			if (text.data.Length < 10000) {
				header = COMData.macroInit +
					text.type +
					COMData.macroSeparator +
					text.data.Length +
					COMData.macroSeparator +
					System.Text.Encoding.UTF8.GetString(text.data, 0, text.data.Length) +
					COMData.macroEnd;

				Log.AddToDebug(header + " - " + text.data);
				socket.Send(System.Text.Encoding.UTF8.GetBytes(header));
				return true;
			}
			else {
				header = COMData.macroInit +
					text.type +
					COMData.macroSeparator +
					text.data.Length +
					COMData.macroEnd;

				Log.AddToDebug(header + " - " + text.data);
				socket.Send(System.Text.Encoding.UTF8.GetBytes(header));
				int bytesSent = socket.Send(text.data);

				return bytesSent == text.data.Length;
			}
		}

		bool SendImage(COMData_image image) {
			string header =
				COMData.macroInit +
					image.type +
					COMData.macroSeparator +
					image.data.Length +
					COMData.macroSeparator +
					image.imageWidth +
					COMData.macroSeparator +
					image.imageHeight +
					COMData.macroEnd;

			Log.AddToDebug(header);
			socket.Send(System.Text.Encoding.UTF8.GetBytes(header));
			int bytesSent = socket.Send(image.data);

			return bytesSent == image.data.Length;
		}

		bool SendAudio(COMData_audio audio) {
			string header =
				COMData.macroInit +
					audio.type +
					COMData.macroSeparator +
					audio.data.Length +
					COMData.macroEnd;

			Log.AddToDebug(header);
			socket.Send(System.Text.Encoding.UTF8.GetBytes(header));
			int bytesSent = socket.Send(audio.data);

			return bytesSent == audio.data.Length;
		}

		void Send_Threaded() {
			while (isConnected) {
				if (infoToSend.Count > 0) {
					if (Send(infoToSend.Peek())) {
						infoToSend.Dequeue();
					}

					if (infoToSend.Count == 0) {
						writeWaitInLine.WaitOne(1000);
					}
				}
				else {
					writeWaitInLine.WaitOne(1000);
				}
			}
		}
		#endregion

		#region RECEIVE
		void Read_Threaded() {
			while (isConnected) {
				Log.AddToDebug("Waiting for info...");

				int bytesRead = socket.Receive(mainBuffer);

				if (bytesRead == 0) {
					if (!IsConnected()) {
						ConnectionLost();
					}
				}
				else {
					ProcessMessage(bytesRead);
				}
			}
		}

		void ProcessMessage(int bytesRead) {
			string header = Encoding.UTF8.GetString(mainBuffer, 0, bytesRead);

			string[] messageSplit = header.Split(new string[] { COMData.macroInit, COMData.macroEnd }, StringSplitOptions.RemoveEmptyEntries);

			foreach (string msg in messageSplit) {
				string[] fields = msg.Split(new string[] { COMData.macroSeparator }, StringSplitOptions.RemoveEmptyEntries);

				if (fields.Length > 0) {
					if (fields[0] == COMData.TYPE.TEXT.ToString()) {
						if (fields.Length == 2) {//								
							int stringSize = Convert.ToInt32(fields[1]);
							ReceiveMessage(stringSize);
						}
						else {
							if (fields.Length == 3) {//	
													 //int stringSize = Convert.ToInt32(fields[1]);
								COMData_text message = new COMData_text();
								//message.data = new byte[stringSize];
								message.data = System.Text.Encoding.UTF8.GetBytes(fields[2]);

								infoReceived.Enqueue(message);

								Log.AddToDebug("Message Received: " + message.data.Length);

								RunEvent(OnReceive);
							}
							else {
								RunEvent(OnReceiveFailed, new SocketArgs("Bad Text"));
							}
						}
					}
					else {
						if (fields[0] == COMData.TYPE.IMAGE.ToString()) {
							if (fields.Length >= 4) {//								
								int imageSize = Convert.ToInt32(fields[1]);
								int imageWidth = Convert.ToInt32(fields[2]);
								int imageHeight = Convert.ToInt32(fields[3]);

								ReceiveImage(imageSize, imageWidth, imageHeight);
							}
							else {
								RunEvent(OnReceiveFailed, new SocketArgs("Bad Text"));
							}
						}
						else {
							if (fields[0] == COMData.TYPE.AUDIO.ToString()) {
								RunEvent(OnReceiveFailed, new SocketArgs("AUDIO MESSAGE - NOT READY"));
							}
						}
					}
				}
			}
		}

		void ReceiveMessage(int messageSize) {
			COMData_text message = new COMData_text();
			message.data = new byte[messageSize];

			int sizeReceived = socket.Receive(message.data);

			if (messageSize == sizeReceived) {
				infoReceived.Enqueue(message);

				Log.AddToDebug("Message Received: " + message.data.Length);

				RunEvent(OnReceive);
			}
			else {
				RunEvent(OnReceiveFailed, new SocketArgs("Text: " + messageSize + " != " + sizeReceived));
			}
		}

		void ReceiveImage(int imageSize, int imageWidth, int imageHeight) {
			Log.AddToLog("Prepare to Resceive Image (size:" + imageSize + " width:" + imageWidth + " height:" + imageHeight + ")");
			COMData_image image = new COMData_image();
			image.imageWidth = imageWidth;
			image.imageHeight = imageHeight;
			image.data = new byte[imageSize];

			int bytesReceived = 0;

			while (bytesReceived != imageSize) {
				int tmp = socket.Receive(image.data, bytesReceived, imageSize - bytesReceived, SocketFlags.None);
				bytesReceived += tmp;
				Log.AddToLog(bytesReceived + " bytes received so far (" + tmp + " this time) - (" + (imageSize - bytesReceived) + " left)");
			}

			Log.AddToDebug(bytesReceived + " bytes received");
			if (imageSize == bytesReceived) {
				infoReceived.Enqueue(image);

				Log.AddToDebug("Image Received: " + image.data.Length);

				RunEvent(OnReceive);
			}
			else {
				RunEvent(OnReceiveFailed, new SocketArgs("Image: " + imageSize + " != " + bytesReceived));
			}
		}

		void ReceiveAudio(int audioSize) {
			COMData_audio audio = new COMData_audio();
			audio.data = new byte[audioSize];

			int sizeReceived = socket.Receive(audio.data);

			if (audioSize == sizeReceived) {
				infoReceived.Enqueue(audio);

				Log.AddToDebug("Image Received: " + audio.data.Length);
			}
		}

		#endregion
	}
}