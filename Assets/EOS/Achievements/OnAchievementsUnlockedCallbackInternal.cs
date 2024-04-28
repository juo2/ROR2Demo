using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000611 RID: 1553
	// (Invoke) Token: 0x06002607 RID: 9735
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void OnAchievementsUnlockedCallbackInternal(IntPtr data);
}
