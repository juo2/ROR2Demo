using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001E1 RID: 481
	// (Invoke) Token: 0x06000CC4 RID: 3268
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinRoomCallbackInternal(IntPtr data);
}
