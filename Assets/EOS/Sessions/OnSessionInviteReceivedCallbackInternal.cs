using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000ED RID: 237
	// (Invoke) Token: 0x0600072E RID: 1838
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSessionInviteReceivedCallbackInternal(IntPtr data);
}
