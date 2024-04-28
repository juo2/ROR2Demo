using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000470 RID: 1136
	// (Invoke) Token: 0x06001BBD RID: 7101
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryEntitlementsCallbackInternal(IntPtr data);
}
