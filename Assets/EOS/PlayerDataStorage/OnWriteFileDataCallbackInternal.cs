using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200025F RID: 607
	// (Invoke) Token: 0x06000F7E RID: 3966
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate WriteResult OnWriteFileDataCallbackInternal(IntPtr data, IntPtr outDataBuffer, ref uint outDataWritten);
}
