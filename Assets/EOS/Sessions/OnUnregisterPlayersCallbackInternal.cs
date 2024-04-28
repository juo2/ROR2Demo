using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F1 RID: 241
	// (Invoke) Token: 0x0600073E RID: 1854
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUnregisterPlayersCallbackInternal(IntPtr data);
}
