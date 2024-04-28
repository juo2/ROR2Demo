using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005C3 RID: 1475
	// (Invoke) Token: 0x060023A9 RID: 9129
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnMessageToServerCallbackInternal(IntPtr data);
}
