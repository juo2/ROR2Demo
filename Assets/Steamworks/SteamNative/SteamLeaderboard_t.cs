using System;

namespace SteamNative
{
	// Token: 0x0200014A RID: 330
	internal struct SteamLeaderboard_t
	{
		// Token: 0x060009CC RID: 2508 RVA: 0x00033270 File Offset: 0x00031470
		public static implicit operator SteamLeaderboard_t(ulong value)
		{
			return new SteamLeaderboard_t
			{
				Value = value
			};
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0003328E File Offset: 0x0003148E
		public static implicit operator ulong(SteamLeaderboard_t value)
		{
			return value.Value;
		}

		// Token: 0x0400079C RID: 1948
		public ulong Value;
	}
}
