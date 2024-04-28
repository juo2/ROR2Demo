using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000564 RID: 1380
	// (Invoke) Token: 0x06002179 RID: 8569
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnClientAuthStatusChangedCallbackInternal(IntPtr data);
}
