using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000621 RID: 1569
	// (Invoke) Token: 0x0600265F RID: 9823
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnUnlockAchievementsCompleteCallbackInternal(IntPtr data);
}
