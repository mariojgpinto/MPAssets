using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace MPAssets {
	public class SocketServerWorker {
		#region VARIABLES
		SocketServer server;
		TcpClient client;
		Socket socket;

		public int id;

		//bool _isConnected = false;
		public bool isConnected {
			set {
				//_isConnected = value;
			}
			get {
				return (socket != null) ? socket.Connected : false;
			}
		}

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

		const int bufferSize = 1024;
		byte[] mainBuffer = new byte[bufferSize];

		//bool bigIncoming = false;
		byte[] imageBuffer = new byte[10000000];

		#endregion

		#region SETUP
		public SocketServerWorker(SocketServer _server, TcpClient _client, int _id) {
			server = _server;
			id = _id;
			client = _client;

			socket = client.Client;

			Log.AddToDebug(id + "|SocketServerWorker Created - " + ((System.Net.IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
		}

		public void RunWorker() {
			Log.AddToDebug(id + "|SocketServerWorker RunWorker");

			isConnected = true;

			writeThread = new Thread(Send_Threaded);
			writeThread.Start();

			readThread = new Thread(Read_Threaded);
			readThread.Start();

			mainThread = new Thread(ManageConnection_thread);
			mainThread.Start();
		}

		public bool IsConnected() {
			try {
				return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
			}
			catch (SocketException) { return false; }
		}
		#endregion

		#region MANAGE
		void ManageConnection_thread() {
			while (isConnected) {
				if (!IsConnected()) {
					Close();
				}

				mainWaitInLine.WaitOne(1000);
			}
		}

		public void Close(bool removeFromServer = true) {
			if (socket != null) {
				try {
					socket.Shutdown(SocketShutdown.Both);
					socket.Close();
				}
				catch (Exception e) {
					Log.AddToLog("SocketServerWorker", "Close()", "Error closing socket", e.ToString());
				}
			}

			if (removeFromServer)
				server.RemoveClient(this);

			Log.AddToLog(id + "| Client Closed");

			isConnected = false;

			if (mainThread != null)
				mainThread.Abort();

			if (writeThread != null)
				writeThread.Abort();

			if (readThread != null)
				readThread.Abort();
		}

		#endregion

		#region SEND
		public void SendInfo_text(string text) {
			Log.AddToDebug(id + "|SocketServerWorker SendMessageRequest: " + text);
			infoToSend.Enqueue(new COMData_text(text));

			writeWaitInLine.Set();
		}

		public void SendInfo_image(byte[] data, int width, int height) {
			Log.AddToDebug(id + "|SocketServerWorker SendImageRequest: " + data.Length);
			infoToSend.Enqueue(new COMData_image(data, width, height));

			writeWaitInLine.Set();
		}

		public void SendInfo_audio(byte[] data) {
			Log.AddToDebug(id + "|SocketServerWorker SendAudioRequest: " + data.Length);
			infoToSend.Enqueue(new COMData_audio(data));

			writeWaitInLine.Set();
		}

		bool Send(COMData data) {
			if (!isConnected)
				return false;

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
					return false;
				}
			}
			catch {
				try {
					Close();
				}
				catch (Exception ex) {
					Log.AddToDebug(ex.ToString());
					return false;
				}

				return false;
			}
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

			//string header = 
			//		COMData.macroInit + 
			//		text.type + 
			//		COMData.macroSeparator + 
			//		text.data.Length + 
			//		COMData.macroEnd;

			//Log.AddToDebug(header + " - " + text.data);
			//socket.Send(System.Text.Encoding.UTF8.GetBytes (header));
			//int bytesSent = socket.Send(text.data);

			//return bytesSent == text.data.Length;
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
						Thread.Sleep(200);
						//					ConnectionLost();
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

								server.infoReceived.Enqueue(new KeyValuePair<int, COMData>(id, message));

								Log.AddToDebug("Message Received: " + message.data.Length);

								//RunEvent(OnReceive);
							}
							else {
								//RunEvent(OnReceiveFailed, new SocketArgs("Bad Text"));
								Log.AddToLog("Bad Text");
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
								Log.AddToDebug("Bad Text");
							}
						}
						else {
							if (fields[0] == COMData.TYPE.AUDIO.ToString()) {

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

			Log.AddToDebug("Message Received: " + messageSize + " - " + sizeReceived);

			if (messageSize == sizeReceived) {
				server.infoReceived.Enqueue(new KeyValuePair<int, COMData>(id, message));

				Log.AddToDebug("Message Received: " + message.data.Length);
			}
		}

		void ReceiveImage(int imageSize, int imageWidth, int imageHeight) {
			Log.AddToLog(id + "|Prepare to Resceive Image (size:" + imageSize + " width:" + imageWidth + " height:" + imageHeight + ")");
			COMData_image image = new COMData_image();
			image.imageWidth = imageWidth;
			image.imageHeight = imageHeight;
			image.size = imageSize;
			image.data = imageBuffer;


			int bytesReceived = 0;

			while (bytesReceived != imageSize) {
				int tmp = socket.Receive(imageBuffer, bytesReceived, imageSize - bytesReceived, SocketFlags.None);
				bytesReceived += tmp;
				Log.AddToLog(id + "|" + bytesReceived + " bytes received so far (" + tmp + " this time) - (" + (imageSize - bytesReceived) + " left)");
			}

			Log.AddToDebug(id + "|" + bytesReceived + " bytes received");

			if (imageSize == bytesReceived) {
				server.infoReceived.Enqueue(new KeyValuePair<int, COMData>(id, image));

				Log.AddToDebug("Image Received: " + bytesReceived);
			}
		}

		void ReceiveAudio(int audioSize) {
			COMData_audio audio = new COMData_audio();
			audio.data = new byte[audioSize];

			int sizeReceived = socket.Receive(audio.data);

			if (audioSize == sizeReceived) {
				server.infoReceived.Enqueue(new KeyValuePair<int, COMData>(id, audio));

				Log.AddToDebug("Image Received: " + audio.data.Length);
			}
		}
		#endregion
	}
}