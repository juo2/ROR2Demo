using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004E2 RID: 1250
	// (Invoke) Token: 0x06001E4D RID: 7757
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCreateUserCallbackInternal(IntPtr data);
}
