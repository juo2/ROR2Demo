using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005C9 RID: 1481
	// (Invoke) Token: 0x060023C4 RID: 9156
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPeerAuthStatusChangedCallbackInternal(IntPtr data);
}
