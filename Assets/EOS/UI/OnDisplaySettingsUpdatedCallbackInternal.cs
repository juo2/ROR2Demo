using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200004F RID: 79
	// (Invoke) Token: 0x060003CC RID: 972
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDisplaySettingsUpdatedCallbackInternal(IntPtr data);
}
