using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037A RID: 890
	// (Invoke) Token: 0x060015ED RID: 5613
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinLobbyAcceptedCallbackInternal(IntPtr data);
}
