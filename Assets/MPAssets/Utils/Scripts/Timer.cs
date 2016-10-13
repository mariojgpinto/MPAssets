using UnityEngine;
using System.Collections;

public class Timer{
	public	bool  isRunning = false;
	public 	float timeout = 45.0f;

	private float _startTime = 0.0f;

	private bool _isPaused = false;
	private float _pauseStartTime = 0;
	private float _pauseDelta = 0;
	
	
	
	public void StartTimer(){
		_startTime = Time.realtimeSinceStartup;
		isRunning = true;

		_pauseDelta = 0;

		_isPaused = false; 
	}
	
	public void StopTimer(){
		_startTime = 0.0f;
		_pauseDelta = 0;
		isRunning = false;
	}

	public void PauseTimer(){
		_pauseStartTime = Time.realtimeSinceStartup;

		_isPaused = true;
	}

	public void RestartTimer(){
		StopTimer();
		StartTimer();
	}

	public void ResumeTimer(){
		_pauseDelta += (Time.realtimeSinceStartup - _pauseStartTime);

		_isPaused = false; 
	}
	
	public float CheckTimer(){
		if(isRunning){
			if(_isPaused){
				return timeout - (_pauseStartTime - _startTime);
			}
			else {
				return timeout - ((Time.realtimeSinceStartup - _startTime) - _pauseDelta);//(_startTime + timeout - _pauseDelta) - Time.realtimeSinceStartup;
			}
		}
		else{
			return timeout;
		}
		//return (isRunning) ? (_startTime + timeout - _pauseDelta) - Time.realtimeSinceStartup : timeout;
	}
}

