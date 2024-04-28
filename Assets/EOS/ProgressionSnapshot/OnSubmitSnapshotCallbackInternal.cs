using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001FD RID: 509
	// (Invoke) Token: 0x06000D64 RID: 3428
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnSubmitSnapshotCallbackInternal(IntPtr data);
}
