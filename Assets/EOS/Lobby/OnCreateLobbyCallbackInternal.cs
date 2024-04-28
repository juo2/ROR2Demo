using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000376 RID: 886
	// (Invoke) Token: 0x060015DD RID: 5597
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCreateLobbyCallbackInternal(IntPtr data);
}
