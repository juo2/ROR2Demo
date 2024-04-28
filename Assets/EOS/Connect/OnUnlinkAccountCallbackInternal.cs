using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004F2 RID: 1266
	// (Invoke) Token: 0x06001E8D RID: 7821
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUnlinkAccountCallbackInternal(IntPtr data);
}
