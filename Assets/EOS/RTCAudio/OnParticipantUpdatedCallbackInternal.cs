using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000193 RID: 403
	// (Invoke) Token: 0x06000AB8 RID: 2744
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnParticipantUpdatedCallbackInternal(IntPtr data);
}
