using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A2 RID: 674
	// (Invoke) Token: 0x06001103 RID: 4355
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryNATTypeCompleteCallbackInternal(IntPtr data);
}
