using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000386 RID: 902
	// (Invoke) Token: 0x0600161D RID: 5661
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyMemberStatusReceivedCallbackInternal(IntPtr data);
}
