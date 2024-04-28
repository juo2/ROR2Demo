using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Logging
{
	// Token: 0x020002EC RID: 748
	// (Invoke) Token: 0x060012DA RID: 4826
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void LogMessageFuncInternal(IntPtr message);
}
