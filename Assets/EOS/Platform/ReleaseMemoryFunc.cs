using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005EA RID: 1514
	// (Invoke) Token: 0x060024E4 RID: 9444
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void ReleaseMemoryFunc(IntPtr pointer);
}
