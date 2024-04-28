using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200052F RID: 1327
	// (Invoke) Token: 0x06002017 RID: 8215
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeletePersistentAuthCallbackInternal(IntPtr data);
}
