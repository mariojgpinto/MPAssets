

public class SocketArgs : System.EventArgs {
	public string message = "";

	public SocketArgs() {

	}

	public SocketArgs(string msg) {
		message = msg;
	}
}
