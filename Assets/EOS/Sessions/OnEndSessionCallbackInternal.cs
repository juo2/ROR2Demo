using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000DD RID: 221
	// (Invoke) Token: 0x060006EE RID: 1774
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnEndSessionCallbackInternal(IntPtr data);
}
