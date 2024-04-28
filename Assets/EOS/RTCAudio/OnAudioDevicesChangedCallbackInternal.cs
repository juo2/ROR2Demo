using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200018D RID: 397
	// (Invoke) Token: 0x06000AA0 RID: 2720
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioDevicesChangedCallbackInternal(IntPtr data);
}
