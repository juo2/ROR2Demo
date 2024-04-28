using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000539 RID: 1337
	// (Invoke) Token: 0x0600203F RID: 8255
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryIdTokenCallbackInternal(IntPtr data);
}
