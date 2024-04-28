using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000566 RID: 1382
	// (Invoke) Token: 0x06002181 RID: 8577
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnMessageToClientCallbackInternal(IntPtr data);
}
