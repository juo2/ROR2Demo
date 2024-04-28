using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000366 RID: 870
	// (Invoke) Token: 0x0600158B RID: 5515
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void LobbySearchOnFindCallbackInternal(IntPtr data);
}
