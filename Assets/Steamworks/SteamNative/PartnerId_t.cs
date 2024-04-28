using System;

namespace SteamNative
{
	// Token: 0x0200013C RID: 316
	internal struct PartnerId_t
	{
		// Token: 0x060009B0 RID: 2480 RVA: 0x00033040 File Offset: 0x00031240
		public static implicit operator PartnerId_t(uint value)
		{
			return new PartnerId_t
			{
				Value = value
			};
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0003305E File Offset: 0x0003125E
		public static implicit operator uint(PartnerId_t value)
		{
			return value.Value;
		}

		// Token: 0x0400078E RID: 1934
		public uint Value;
	}
}
