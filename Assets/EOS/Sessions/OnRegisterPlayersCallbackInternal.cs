using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E5 RID: 229
	// (Invoke) Token: 0x0600070E RID: 1806
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnRegisterPlayersCallbackInternal(IntPtr data);
}
