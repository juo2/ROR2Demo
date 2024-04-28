using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003F6 RID: 1014
	// (Invoke) Token: 0x06001874 RID: 6260
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUpdateParentEmailCallbackInternal(IntPtr data);
}
