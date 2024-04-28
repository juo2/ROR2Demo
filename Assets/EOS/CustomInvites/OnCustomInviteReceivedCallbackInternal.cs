using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x0200049F RID: 1183
	// (Invoke) Token: 0x06001CBD RID: 7357
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCustomInviteReceivedCallbackInternal(IntPtr data);
}
