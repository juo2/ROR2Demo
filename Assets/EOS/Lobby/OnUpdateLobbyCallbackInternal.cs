using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000396 RID: 918
	// (Invoke) Token: 0x0600165D RID: 5725
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateLobbyCallbackInternal(IntPtr data);
}
