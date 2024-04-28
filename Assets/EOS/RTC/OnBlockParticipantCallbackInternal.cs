using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001DD RID: 477
	// (Invoke) Token: 0x06000CB4 RID: 3252
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnBlockParticipantCallbackInternal(IntPtr data);
}
