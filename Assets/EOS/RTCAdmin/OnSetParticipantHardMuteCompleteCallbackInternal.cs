using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001BD RID: 445
	// (Invoke) Token: 0x06000BC1 RID: 3009
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSetParticipantHardMuteCompleteCallbackInternal(IntPtr data);
}
