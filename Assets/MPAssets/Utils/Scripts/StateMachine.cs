using System.Collections;

/// <summary>
/// State machine.
/// </summary>
public class StateMachine{
	public const int N_STATUS = 3;
	
	public bool[] _status;
	
	//--------------------------------------------------------------------------------
	// CONSTRUCTORS
	//--------------------------------------------------------------------------------
	
	/// <summary>
	/// Initializes a new instance of the <see cref="StateMachine"/> class.
	/// </summary>
	public StateMachine(){
		_status = new bool[N_STATUS];
		
		for (int i = 0; i < N_STATUS; ++i) {
			_status[i] = false;
		}
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="StateMachine"/> class.
	/// </summary>
	/// <param name="s1">S1.</param>
	/// <param name="s2">S2.</param>
	/// <param name="s3">S3.</param>
	public StateMachine(char s1, char s2, char s3) : this(){
		_status[0] = (s1 == '1') ? true : false;
		_status[1] = (s2 == '1') ? true : false;
		_status[2] = (s3 == '1') ? true : false;
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="StateMachine"/> class.
	/// </summary>
	/// <param name="s1">S1.</param>
	/// <param name="s2">S2.</param>
	/// <param name="s3">S3.</param>
	public StateMachine(bool s1, bool s2, bool s3) : this(){
		_status [0] = s1;
		_status [1] = s2;
		_status [2] = s3;
	}
	
	/// <summary>
	/// Initializes a new instance of the <see cref="StateMachine"/> class.
	/// </summary>
	/// <param name="sm">Sm.</param>
	public StateMachine(StateMachine sm) : this(){
		if(sm != null){
			SetAllStatus(sm._status[0],sm._status[1],sm._status[2]);
		}
	}
	
	/// <summary>
	/// Determines whether the specified <see cref="StateMachine"/> is equal to the current <see cref="StateMachine"/>.
	/// </summary>
	/// <param name="sm">The <see cref="StateMachine"/> to compare with the current <see cref="StateMachine"/>.</param>
	/// <returns><c>true</c> if the specified <see cref="StateMachine"/> is equal to the current <see cref="StateMachine"/>;
	/// otherwise, <c>false</c>.</returns>
	public bool Equals(StateMachine sm){
		if(sm == null) return false;
		
		bool result = true;
		
		for (int i = 0; i < N_STATUS; ++i) {
			result &= (_status[i] == sm._status[i]);
		}
		
		return result;
	}
	
	/// <summary>
	/// Set_all_status the specified status1, status2, status3 and status4.
	/// </summary>
	/// <param name="status1">If set to <c>true</c> status1.</param>
	/// <param name="status2">If set to <c>true</c> status2.</param>
	/// <param name="status3">If set to <c>true</c> status3.</param>
	public void SetAllStatus(bool status1, bool status2, bool status3){
		_status [0] = status1;
		_status [1] = status2;
		_status [2] = status3;
	} 
	
	/// <summary>
	/// Set_status the specified id and status.
	/// </summary>
	/// <param name="id">Identifier.</param>
	/// <param name="status">If set to <c>true</c> status.</param>
	public void SetStatus(int id, bool status){
		//System.Console.WriteLine ("KEY (" + id + ") Status: " + ((status) ? "ON" : "OFF"));
		_status [id] = status;
	}
	
	/// <summary>
	/// Get_status the specified id.
	/// </summary>
	/// <param name="id">Identifier.</param>
	public bool GetStatus(int id){
		return this._status [id];
	}
}