using System;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x020002ED RID: 749
	public static class LoggingInterface
	{
		// Token: 0x060012DD RID: 4829 RVA: 0x000141D0 File Offset: 0x000123D0
		public static Result SetCallback(LogMessageFunc callback)
		{
			LogMessageFuncInternal logMessageFuncInternal = new LogMessageFuncInternal(LoggingInterface.LogMessageFuncInternalImplementation);
			Helper.AddStaticCallback("LogMessageFuncInternalImplementation", callback, logMessageFuncInternal);
			return Bindings.EOS_Logging_SetCallback(logMessageFuncInternal);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000141FC File Offset: 0x000123FC
		public static Result SetLogLevel(LogCategory logCategory, LogLevel logLevel)
		{
			return Bindings.EOS_Logging_SetLogLevel(logCategory, logLevel);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00014208 File Offset: 0x00012408
		[MonoPInvokeCallback(typeof(LogMessageFuncInternal))]
		internal static void LogMessageFuncInternalImplementation(IntPtr message)
		{
			LogMessageFunc logMessageFunc;
			if (Helper.TryGetStaticCallback<LogMessageFunc>("LogMessageFuncInternalImplementation", out logMessageFunc))
			{
				LogMessage message2;
				Helper.TryMarshalGet<LogMessageInternal, LogMessage>(message, out message2);
				logMessageFunc(message2);
			}
		}
	}
}
