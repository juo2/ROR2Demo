using System;

namespace SteamNative
{
	// Token: 0x0200014B RID: 331
	internal struct SteamLeaderboardEntries_t
	{
		// Token: 0x060009CE RID: 2510 RVA: 0x00033298 File Offset: 0x00031498
		public static implicit operator SteamLeaderboardEntries_t(ulong value)
		{
			return new SteamLeaderboardEntries_t
			{
				Value = value
			};
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x000332B6 File Offset: 0x000314B6
		public static implicit operator ulong(SteamLeaderboardEntries_t value)
		{
			return value.Value;
		}

		// Token: 0x0400079D RID: 1949
		public ulong Value;
	}
}
