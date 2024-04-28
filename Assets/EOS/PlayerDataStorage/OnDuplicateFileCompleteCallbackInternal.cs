using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000251 RID: 593
	// (Invoke) Token: 0x06000F46 RID: 3910
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDuplicateFileCompleteCallbackInternal(IntPtr data);
}
