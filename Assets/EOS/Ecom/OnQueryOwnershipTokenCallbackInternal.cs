using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000476 RID: 1142
	// (Invoke) Token: 0x06001BD5 RID: 7125
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryOwnershipTokenCallbackInternal(IntPtr data);
}
