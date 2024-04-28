using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200021A RID: 538
	// (Invoke) Token: 0x06000E0F RID: 3599
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryPresenceCompleteCallbackInternal(IntPtr data);
}
