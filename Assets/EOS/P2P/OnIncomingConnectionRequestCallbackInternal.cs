using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000296 RID: 662
	// (Invoke) Token: 0x060010AF RID: 4271
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnIncomingConnectionRequestCallbackInternal(IntPtr data);
}
