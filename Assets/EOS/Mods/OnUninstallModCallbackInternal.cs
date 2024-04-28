using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002D1 RID: 721
	// (Invoke) Token: 0x06001246 RID: 4678
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUninstallModCallbackInternal(IntPtr data);
}
