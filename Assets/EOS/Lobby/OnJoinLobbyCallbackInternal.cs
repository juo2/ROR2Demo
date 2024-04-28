using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037C RID: 892
	// (Invoke) Token: 0x060015F5 RID: 5621
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinLobbyCallbackInternal(IntPtr data);
}
