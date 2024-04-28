using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x020001FB RID: 507
	// (Invoke) Token: 0x06000D5C RID: 3420
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeleteSnapshotCallbackInternal(IntPtr data);
}
