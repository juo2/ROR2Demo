using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000218 RID: 536
	// (Invoke) Token: 0x06000E07 RID: 3591
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnPresenceChangedCallbackInternal(IntPtr data);
}
