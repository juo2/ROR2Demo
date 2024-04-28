using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200024F RID: 591
	// (Invoke) Token: 0x06000F3E RID: 3902
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnDeleteFileCompleteCallbackInternal(IntPtr data);
}
