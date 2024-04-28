using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000191 RID: 401
	// (Invoke) Token: 0x06000AB0 RID: 2736
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAudioOutputStateCallbackInternal(IntPtr data);
}
