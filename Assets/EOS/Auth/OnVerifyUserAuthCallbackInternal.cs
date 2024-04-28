using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200053D RID: 1341
	// (Invoke) Token: 0x0600204F RID: 8271
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnVerifyUserAuthCallbackInternal(IntPtr data);
}
