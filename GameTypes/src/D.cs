using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameTypes
{	
    public static class D
    {
		public delegate void LogHandler(string pMessage);
		public static event LogHandler onDLog;
		
        public static void isNull(object pObject) {
			if (pObject == null)
            {
				StackFrame frame = new StackFrame(1, true);
        		var message = string.Format("Line: {0}\r\nColumn: {1}\r\nWhere:{2}",
                                    frame.GetFileLineNumber(),
                                    frame.GetFileColumnNumber(),
                                    frame.GetMethod().Name);
				throw new NullReferenceException(message);
            }
		}
		
        public static void isNull(object pObject, string pMessage)
        {
            if (pObject == null)
            {
				throw new NullReferenceException(pMessage);
            }
        }
		
		public static void assert(bool pCondition)
        {
            if (!pCondition)
            {
				StackFrame frame = new StackFrame(1, true);
        		var message = string.Format("Line: {0}\r\nColumn: {1}\r\nWhere:{2}",
                                    frame.GetFileLineNumber(),
                                    frame.GetFileColumnNumber(),
                                    frame.GetMethod().Name);
				throw new Exception(message);
            }
        }
		
		public static void assert(bool pCondition, string pMessage)
        {
            if (!pCondition)
            {
				throw new Exception(pMessage);
            }
        }
		
        public static void Log( string pMessage )
        { 
			if(onDLog != null) {
				onDLog(pMessage);
			} else {
				throw new Exception(pMessage + " (to log this message instead of receiving an exception, listen to D.onDLog)");
			}
        }
		
        public static void LogError(string pMessage)
        {
			throw new Exception(pMessage);
        }
    }
}
