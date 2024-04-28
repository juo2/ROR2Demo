using System;

namespace RoR2.Social
{
	// Token: 0x02000AC9 RID: 2761
	public struct SocialUserId
	{
		// Token: 0x06003F7F RID: 16255 RVA: 0x001063E2 File Offset: 0x001045E2
		public SocialUserId(CSteamID steamId)
		{
			this.steamId = steamId;
		}

		// Token: 0x04003DDC RID: 15836
		public readonly CSteamID steamId;
	}
}
