using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001E3 RID: 483
	// (Invoke) Token: 0x06000CCC RID: 3276
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLeaveRoomCallbackInternal(IntPtr data);
}
