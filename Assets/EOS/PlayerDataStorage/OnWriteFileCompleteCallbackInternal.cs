using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200025D RID: 605
	// (Invoke) Token: 0x06000F76 RID: 3958
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnWriteFileCompleteCallbackInternal(IntPtr data);
}
