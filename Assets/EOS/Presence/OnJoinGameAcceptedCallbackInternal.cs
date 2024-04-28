using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000216 RID: 534
	// (Invoke) Token: 0x06000DFF RID: 3583
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinGameAcceptedCallbackInternal(IntPtr data);
}
