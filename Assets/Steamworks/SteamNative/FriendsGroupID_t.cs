using System;

namespace SteamNative
{
	// Token: 0x02000143 RID: 323
	internal struct FriendsGroupID_t
	{
		// Token: 0x060009BE RID: 2494 RVA: 0x00033158 File Offset: 0x00031358
		public static implicit operator FriendsGroupID_t(short value)
		{
			return new FriendsGroupID_t
			{
				Value = value
			};
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00033176 File Offset: 0x00031376
		public static implicit operator short(FriendsGroupID_t value)
		{
			return value.Value;
		}

		// Token: 0x04000795 RID: 1941
		public short Value;
	}
}
