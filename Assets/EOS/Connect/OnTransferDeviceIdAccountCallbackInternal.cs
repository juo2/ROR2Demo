using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004F0 RID: 1264
	// (Invoke) Token: 0x06001E85 RID: 7813
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnTransferDeviceIdAccountCallbackInternal(IntPtr data);
}
