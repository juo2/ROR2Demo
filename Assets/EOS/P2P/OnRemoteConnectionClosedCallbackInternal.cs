using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A6 RID: 678
	// (Invoke) Token: 0x06001119 RID: 4377
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRemoteConnectionClosedCallbackInternal(IntPtr data);
}
