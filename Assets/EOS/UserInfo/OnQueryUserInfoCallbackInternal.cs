using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000030 RID: 48
	// (Invoke) Token: 0x06000312 RID: 786
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryUserInfoCallbackInternal(IntPtr data);
}
