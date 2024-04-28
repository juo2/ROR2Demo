using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000378 RID: 888
	// (Invoke) Token: 0x060015E5 RID: 5605
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDestroyLobbyCallbackInternal(IntPtr data);
}
