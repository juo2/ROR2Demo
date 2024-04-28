using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000055 RID: 85
	// (Invoke) Token: 0x060003EA RID: 1002
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnShowFriendsCallbackInternal(IntPtr data);
}
