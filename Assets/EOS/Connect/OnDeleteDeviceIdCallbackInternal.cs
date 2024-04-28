using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004E4 RID: 1252
	// (Invoke) Token: 0x06001E55 RID: 7765
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeleteDeviceIdCallbackInternal(IntPtr data);
}
