using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000189 RID: 393
	// (Invoke) Token: 0x06000A90 RID: 2704
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioBeforeRenderCallbackInternal(IntPtr data);
}
