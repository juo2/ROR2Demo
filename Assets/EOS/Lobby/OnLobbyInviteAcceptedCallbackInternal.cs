using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000382 RID: 898
	// (Invoke) Token: 0x0600160D RID: 5645
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyInviteAcceptedCallbackInternal(IntPtr data);
}
