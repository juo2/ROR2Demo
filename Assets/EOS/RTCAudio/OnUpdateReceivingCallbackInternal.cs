using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000195 RID: 405
	// (Invoke) Token: 0x06000AC0 RID: 2752
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateReceivingCallbackInternal(IntPtr data);
}
