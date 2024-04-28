using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200029E RID: 670
	// (Invoke) Token: 0x060010E7 RID: 4327
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPeerConnectionEstablishedCallbackInternal(IntPtr data);
}
