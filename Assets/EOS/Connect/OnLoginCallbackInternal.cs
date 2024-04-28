using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004E8 RID: 1256
	// (Invoke) Token: 0x06001E65 RID: 7781
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLoginCallbackInternal(IntPtr data);
}
