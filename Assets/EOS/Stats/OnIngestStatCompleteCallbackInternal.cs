using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x0200009B RID: 155
	// (Invoke) Token: 0x06000587 RID: 1415
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnIngestStatCompleteCallbackInternal(IntPtr data);
}
