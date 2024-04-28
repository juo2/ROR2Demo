using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000DF RID: 223
	// (Invoke) Token: 0x060006F6 RID: 1782
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinSessionAcceptedCallbackInternal(IntPtr data);
}
