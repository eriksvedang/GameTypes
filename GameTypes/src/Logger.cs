using System;

namespace GameTypes
{
	/// <summary>
	/// Use this class as a component (member) in other classes to let 
	/// them have an easy way to send log messages to whoever wants to listen.
	/// </summary>
	public class Logger
	{
		private event D.LogHandler onLog;
		
		public void Log(string pMessage) {
			if(onLog != null) {
				onLog(pMessage);
			}
		}
		
		public void AddListener(D.LogHandler pOnLog) {
			onLog += pOnLog;
		}
		
		public void RemoveListener(D.LogHandler pOnLog) {
			onLog -= pOnLog;
		}
	}
}

