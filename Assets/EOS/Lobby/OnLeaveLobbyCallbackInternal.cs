using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000380 RID: 896
	// (Invoke) Token: 0x06001605 RID: 5637
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnLeaveLobbyCallbackInternal(IntPtr data);
}
