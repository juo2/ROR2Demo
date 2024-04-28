using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001DF RID: 479
	// (Invoke) Token: 0x06000CBC RID: 3260
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDisconnectedCallbackInternal(IntPtr data);
}
