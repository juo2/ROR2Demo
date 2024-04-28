using System;

namespace SteamNative
{
	// Token: 0x02000159 RID: 345
	internal struct SteamItemDef_t
	{
		// Token: 0x060009EA RID: 2538 RVA: 0x000334C8 File Offset: 0x000316C8
		public static implicit operator SteamItemDef_t(int value)
		{
			return new SteamItemDef_t
			{
				Value = value
			};
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000334E6 File Offset: 0x000316E6
		public static implicit operator int(SteamItemDef_t value)
		{
			return value.Value;
		}

		// Token: 0x040007AB RID: 1963
		public int Value;
	}
}
