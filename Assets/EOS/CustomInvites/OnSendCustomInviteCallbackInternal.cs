using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A3 RID: 1187
	// (Invoke) Token: 0x06001CD9 RID: 7385
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSendCustomInviteCallbackInternal(IntPtr data);
}
