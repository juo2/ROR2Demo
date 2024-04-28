using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005C7 RID: 1479
	// (Invoke) Token: 0x060023BC RID: 9148
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPeerActionRequiredCallbackInternal(IntPtr data);
}
