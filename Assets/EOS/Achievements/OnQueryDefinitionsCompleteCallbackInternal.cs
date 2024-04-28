using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000619 RID: 1561
	// (Invoke) Token: 0x06002636 RID: 9782
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryDefinitionsCompleteCallbackInternal(IntPtr data);
}
