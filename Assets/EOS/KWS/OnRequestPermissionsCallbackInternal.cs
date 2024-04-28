using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003F4 RID: 1012
	// (Invoke) Token: 0x0600186C RID: 6252
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRequestPermissionsCallbackInternal(IntPtr data);
}
