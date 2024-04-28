using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200046E RID: 1134
	// (Invoke) Token: 0x06001BB5 RID: 7093
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnCheckoutCallbackInternal(IntPtr data);
}
