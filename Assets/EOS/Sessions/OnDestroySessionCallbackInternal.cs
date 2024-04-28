using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000DB RID: 219
	// (Invoke) Token: 0x060006E6 RID: 1766
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDestroySessionCallbackInternal(IntPtr data);
}
