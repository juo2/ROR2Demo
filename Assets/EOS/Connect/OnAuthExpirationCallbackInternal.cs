using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004DE RID: 1246
	// (Invoke) Token: 0x06001E3D RID: 7741
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAuthExpirationCallbackInternal(IntPtr data);
}
