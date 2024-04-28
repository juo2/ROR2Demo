using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200061D RID: 1565
	// (Invoke) Token: 0x06002649 RID: 9801
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnQueryPlayerAchievementsCompleteCallbackInternal(IntPtr data);
}
