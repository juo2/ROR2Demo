using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000053 RID: 83
	// (Invoke) Token: 0x060003E2 RID: 994
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnHideFriendsCallbackInternal(IntPtr data);
}
