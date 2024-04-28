using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000232 RID: 562
	// (Invoke) Token: 0x06000E93 RID: 3731
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SetPresenceCompleteCallbackInternal(IntPtr data);
}
