using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200038C RID: 908
	// (Invoke) Token: 0x06001635 RID: 5685
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPromoteMemberCallbackInternal(IntPtr data);
}
