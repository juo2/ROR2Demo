using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200018B RID: 395
	// (Invoke) Token: 0x06000A98 RID: 2712
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioBeforeSendCallbackInternal(IntPtr data);
}
