using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x0200015D RID: 349
	// (Invoke) Token: 0x06000992 RID: 2450
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSendPlayerBehaviorReportCompleteCallbackInternal(IntPtr data);
}
