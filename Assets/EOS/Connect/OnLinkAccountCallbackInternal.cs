using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004E6 RID: 1254
	// (Invoke) Token: 0x06001E5D RID: 7773
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLinkAccountCallbackInternal(IntPtr data);
}
