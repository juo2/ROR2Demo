using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x0200049B RID: 1179
	// (Invoke) Token: 0x06001CA1 RID: 7329
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCustomInviteAcceptedCallbackInternal(IntPtr data);
}
