using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000390 RID: 912
	// (Invoke) Token: 0x06001645 RID: 5701
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRTCRoomConnectionChangedCallbackInternal(IntPtr data);
}
