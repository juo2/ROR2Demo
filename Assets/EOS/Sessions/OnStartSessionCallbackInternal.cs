using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000EF RID: 239
	// (Invoke) Token: 0x06000736 RID: 1846
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnStartSessionCallbackInternal(IntPtr data);
}
