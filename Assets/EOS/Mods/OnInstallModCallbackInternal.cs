using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002CF RID: 719
	// (Invoke) Token: 0x0600123E RID: 4670
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnInstallModCallbackInternal(IntPtr data);
}
