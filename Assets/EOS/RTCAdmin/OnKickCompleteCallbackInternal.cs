using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B9 RID: 441
	// (Invoke) Token: 0x06000BB1 RID: 2993
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnKickCompleteCallbackInternal(IntPtr data);
}
