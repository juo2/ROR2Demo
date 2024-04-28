using System;
using System.Runtime.InteropServices;

namespace SteamNative
{
	// Token: 0x020000BE RID: 190
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardEntry_t
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x00019A68 File Offset: 0x00017C68
		internal static LeaderboardEntry_t FromPointer(IntPtr p)
		{
			if (Platform.PackSmall)
			{
				return (LeaderboardEntry_t.PackSmall)Marshal.PtrToStructure(p, typeof(LeaderboardEntry_t.PackSmall));
			}
			return (LeaderboardEntry_t)Marshal.PtrToStructure(p, typeof(LeaderboardEntry_t));
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00019AA1 File Offset: 0x00017CA1
		internal static int StructSize()
		{
			if (Platform.PackSmall)
			{
				return Marshal.SizeOf(typeof(LeaderboardEntry_t.PackSmall));
			}
			return Marshal.SizeOf(typeof(LeaderboardEntry_t));
		}

		// Token: 0x040005D7 RID: 1495
		internal ulong SteamIDUser;

		// Token: 0x040005D8 RID: 1496
		internal int GlobalRank;

		// Token: 0x040005D9 RID: 1497
		internal int Score;

		// Token: 0x040005DA RID: 1498
		internal int CDetails;

		// Token: 0x040005DB RID: 1499
		internal ulong UGC;

		// Token: 0x020001E2 RID: 482
		[StructLayout(LayoutKind.Sequential, Pack = 4)]
		internal struct PackSmall
		{
			// Token: 0x06001D1E RID: 7454 RVA: 0x00062418 File Offset: 0x00060618
			public static implicit operator LeaderboardEntry_t(LeaderboardEntry_t.PackSmall d)
			{
				return new LeaderboardEntry_t
				{
					SteamIDUser = d.SteamIDUser,
					GlobalRank = d.GlobalRank,
					Score = d.Score,
					CDetails = d.CDetails,
					UGC = d.UGC
				};
			}

			// Token: 0x04000A3E RID: 2622
			internal ulong SteamIDUser;

			// Token: 0x04000A3F RID: 2623
			internal int GlobalRank;

			// Token: 0x04000A40 RID: 2624
			internal int Score;

			// Token: 0x04000A41 RID: 2625
			internal int CDetails;

			// Token: 0x04000A42 RID: 2626
			internal ulong UGC;
		}
	}
}
