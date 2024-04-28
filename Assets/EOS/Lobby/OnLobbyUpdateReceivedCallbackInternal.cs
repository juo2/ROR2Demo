using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200038A RID: 906
	// (Invoke) Token: 0x0600162D RID: 5677
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyUpdateReceivedCallbackInternal(IntPtr data);
}
