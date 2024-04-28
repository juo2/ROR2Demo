using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004E0 RID: 1248
	// (Invoke) Token: 0x06001E45 RID: 7749
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCreateDeviceIdCallbackInternal(IntPtr data);
}
