using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E1 RID: 225
	// (Invoke) Token: 0x060006FE RID: 1790
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnJoinSessionCallbackInternal(IntPtr data);
}
