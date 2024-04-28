using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000562 RID: 1378
	// (Invoke) Token: 0x06002171 RID: 8561
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnClientActionRequiredCallbackInternal(IntPtr data);
}
