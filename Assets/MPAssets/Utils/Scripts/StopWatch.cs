using UnityEngine;
using System.Collections;

namespace MPAssets {
	public class StopWatch {
		#region ENUM
		public enum STATUS {
			STOPPED,
			RUNNING,
			PAUSED
		}
		#endregion

		#region VARIABLES
		private STATUS _status = STATUS.STOPPED;

		private float _startTime = 0.0f;

		private float _pauseStartTime = 0.0f;
		private float _pausedTime = 0.0f;
		private float _pauseDelta = 0.0f;
		#endregion

		#region API
		public void Run() {
			switch (_status) {
				case STATUS.STOPPED:
					_startTime = Time.realtimeSinceStartup;
					_pauseDelta = 0;
					break;
				case STATUS.PAUSED:
					_pauseDelta += Time.realtimeSinceStartup - _pauseStartTime;
					_status = STATUS.RUNNING;
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
			_pausedTime = (_pauseStartTime - _startTime) - _pauseDelta;
			_status = STATUS.PAUSED;
		}

		public float CheckTime() {
			float time = -1;
			switch (_status) {
				case STATUS.RUNNING:
					time = (Time.realtimeSinceStartup - _startTime) - _pauseDelta;
					break;
				case STATUS.PAUSED:
					time = _pausedTime;
					break;
				case STATUS.STOPPED:
					time = 0;
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