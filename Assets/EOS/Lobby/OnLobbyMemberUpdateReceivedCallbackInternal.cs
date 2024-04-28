using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000388 RID: 904
	// (Invoke) Token: 0x06001625 RID: 5669
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLobbyMemberUpdateReceivedCallbackInternal(IntPtr data);
}
