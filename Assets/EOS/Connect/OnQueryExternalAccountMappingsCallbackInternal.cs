using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004EC RID: 1260
	// (Invoke) Token: 0x06001E75 RID: 7797
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryExternalAccountMappingsCallbackInternal(IntPtr data);
}
