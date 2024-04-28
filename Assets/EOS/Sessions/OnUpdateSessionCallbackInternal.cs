using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F3 RID: 243
	// (Invoke) Token: 0x06000746 RID: 1862
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateSessionCallbackInternal(IntPtr data);
}
