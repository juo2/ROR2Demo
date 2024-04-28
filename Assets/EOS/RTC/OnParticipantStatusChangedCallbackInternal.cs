using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001E5 RID: 485
	// (Invoke) Token: 0x06000CD4 RID: 3284
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnParticipantStatusChangedCallbackInternal(IntPtr data);
}
