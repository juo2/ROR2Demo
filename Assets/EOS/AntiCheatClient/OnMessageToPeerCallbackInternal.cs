using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005C1 RID: 1473
	// (Invoke) Token: 0x060023A1 RID: 9121
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnMessageToPeerCallbackInternal(IntPtr data);
}
