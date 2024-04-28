using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002D3 RID: 723
	// (Invoke) Token: 0x0600124E RID: 4686
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateModCallbackInternal(IntPtr data);
}
