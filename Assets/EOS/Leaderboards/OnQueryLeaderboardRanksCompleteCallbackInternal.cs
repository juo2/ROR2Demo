using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003CE RID: 974
	// (Invoke) Token: 0x0600179C RID: 6044
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryLeaderboardRanksCompleteCallbackInternal(IntPtr data);
}
