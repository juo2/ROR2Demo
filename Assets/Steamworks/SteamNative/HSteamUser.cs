using System;

namespace SteamNative
{
	// Token: 0x02000142 RID: 322
	internal struct HSteamUser
	{
		// Token: 0x060009BC RID: 2492 RVA: 0x00033130 File Offset: 0x00031330
		public static implicit operator HSteamUser(int value)
		{
			return new HSteamUser
			{
				Value = value
			};
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0003314E File Offset: 0x0003134E
		public static implicit operator int(HSteamUser value)
		{
			return value.Value;
		}

		// Token: 0x04000794 RID: 1940
		public int Value;
	}
}
