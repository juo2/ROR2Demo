using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037E RID: 894
	// (Invoke) Token: 0x060015FD RID: 5629
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnKickMemberCallbackInternal(IntPtr data);
}
