using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200041C RID: 1052
	// (Invoke) Token: 0x0600194A RID: 6474
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnFriendsUpdateCallbackInternal(IntPtr data);
}
