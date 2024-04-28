using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002C RID: 44
	// (Invoke) Token: 0x06000302 RID: 770
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryUserInfoByDisplayNameCallbackInternal(IntPtr data);
}
