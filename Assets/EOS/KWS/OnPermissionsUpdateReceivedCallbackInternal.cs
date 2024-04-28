using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003EE RID: 1006
	// (Invoke) Token: 0x06001854 RID: 6228
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPermissionsUpdateReceivedCallbackInternal(IntPtr data);
}
