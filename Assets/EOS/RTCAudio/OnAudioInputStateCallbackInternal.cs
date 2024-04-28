using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200018F RID: 399
	// (Invoke) Token: 0x06000AA8 RID: 2728
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioInputStateCallbackInternal(IntPtr data);
}
