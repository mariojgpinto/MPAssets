using UnityEngine;
using System.Collections;


namespace MPAssets {
	public class Timer {
		#region ENUM
		public enum STATUS {
			STOPPED,
			RUNNING,
			PAUSED
		}
		#endregion

		#region VARIABLES
		public float timeout = 45.0f;

		private float _startTime = 0.0f;
		
		private float _pauseStartTime = 0;
		private float _pauseDelta = 0;

		private STATUS _status;
		#endregion

		#region API
		public void Run() {
			switch (_status) {
				case STATUS.PAUSED:
					_pauseDelta += (Time.realtimeSinceStartup - _pauseStartTime);
					break;
				case STATUS.STOPPED:
					_startTime = Time.realtimeSinceStartup;
					_pauseDelta = 0;
					break;
			}
			_status = STATUS.RUNNING;
		}

		public void Stop() {
			_startTime = 0.0f;
			_pauseDelta = 0;
			_status = STATUS.STOPPED;
		}

		public void Pause() {
			_pauseStartTime = Time.realtimeSinceStartup;
			_status = STATUS.PAUSED;
		}

		public void Restart() {
			Stop();
			Run();
		}

		public float CheckTime() {
			float time = -1;
			switch (_status) {
				case STATUS.RUNNING:
					time = timeout - ((Time.realtimeSinceStartup - _startTime) - _pauseDelta);//(_startTime + timeout - _pauseDelta) - Time.realtimeSinceStartup;

					break;
				case STATUS.PAUSED:
					time = timeout - (_pauseStartTime - _startTime) + _pauseDelta;
					break;
				case STATUS.STOPPED:
					time = timeout;
					break;
			}

			return time;
		}

		public STATUS Status() {
			return _status;
		}
		#endregion
	}
}