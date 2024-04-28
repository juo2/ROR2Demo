using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003F2 RID: 1010
	// (Invoke) Token: 0x06001864 RID: 6244
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryPermissionsCallbackInternal(IntPtr data);
}
