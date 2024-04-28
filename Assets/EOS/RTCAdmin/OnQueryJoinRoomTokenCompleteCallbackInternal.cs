using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001BB RID: 443
	// (Invoke) Token: 0x06000BB9 RID: 3001
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryJoinRoomTokenCompleteCallbackInternal(IntPtr data);
}
