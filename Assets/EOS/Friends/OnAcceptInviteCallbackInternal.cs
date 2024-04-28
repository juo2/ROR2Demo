using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200041A RID: 1050
	// (Invoke) Token: 0x06001942 RID: 6466
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAcceptInviteCallbackInternal(IntPtr data);
}
