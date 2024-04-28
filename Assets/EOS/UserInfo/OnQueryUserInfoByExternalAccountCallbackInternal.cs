using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002E RID: 46
	// (Invoke) Token: 0x0600030A RID: 778
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryUserInfoByExternalAccountCallbackInternal(IntPtr data);
}
