using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200029A RID: 666
	// (Invoke) Token: 0x060010C8 RID: 4296
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnIncomingPacketQueueFullCallbackInternal(IntPtr data);
}
