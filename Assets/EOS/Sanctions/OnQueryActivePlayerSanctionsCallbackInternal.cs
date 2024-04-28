using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000154 RID: 340
	// (Invoke) Token: 0x06000953 RID: 2387
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryActivePlayerSanctionsCallbackInternal(IntPtr data);
}
