using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000384 RID: 900
	// (Invoke) Token: 0x06001615 RID: 5653
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyInviteReceivedCallbackInternal(IntPtr data);
}
