using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000135 RID: 309
	// (Invoke) Token: 0x0600089E RID: 2206
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SessionSearchOnFindCallbackInternal(IntPtr data);
}
