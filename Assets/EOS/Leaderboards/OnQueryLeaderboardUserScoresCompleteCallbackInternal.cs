using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003D2 RID: 978
	// (Invoke) Token: 0x060017AF RID: 6063
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryLeaderboardUserScoresCompleteCallbackInternal(IntPtr data);
}
