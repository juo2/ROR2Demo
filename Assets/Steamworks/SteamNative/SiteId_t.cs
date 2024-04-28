using System;

namespace SteamNative
{
	// Token: 0x0200013E RID: 318
	internal struct SiteId_t
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x00033090 File Offset: 0x00031290
		public static implicit operator SiteId_t(ulong value)
		{
			return new SiteId_t
			{
				Value = value
			};
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x000330AE File Offset: 0x000312AE
		public static implicit operator ulong(SiteId_t value)
		{
			return value.Value;
		}

		// Token: 0x04000790 RID: 1936
		public ulong Value;
	}
}
