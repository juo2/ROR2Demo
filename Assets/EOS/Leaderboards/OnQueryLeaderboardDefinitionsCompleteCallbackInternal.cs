using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003CA RID: 970
	// (Invoke) Token: 0x06001789 RID: 6025
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryLeaderboardDefinitionsCompleteCallbackInternal(IntPtr data);
}
