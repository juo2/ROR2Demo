using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003F0 RID: 1008
	// (Invoke) Token: 0x0600185C RID: 6236
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryAgeGateCallbackInternal(IntPtr data);
}
