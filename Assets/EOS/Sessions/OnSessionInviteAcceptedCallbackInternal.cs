using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000EB RID: 235
	// (Invoke) Token: 0x06000726 RID: 1830
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSessionInviteAcceptedCallbackInternal(IntPtr data);
}
