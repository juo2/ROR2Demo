using System;

namespace SteamNative
{
	// Token: 0x02000135 RID: 309
	internal struct AssetClassId_t
	{
		// Token: 0x060009A2 RID: 2466 RVA: 0x00032F28 File Offset: 0x00031128
		public static implicit operator AssetClassId_t(ulong value)
		{
			return new AssetClassId_t
			{
				Value = value
			};
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00032F46 File Offset: 0x00031146
		public static implicit operator ulong(AssetClassId_t value)
		{
			return value.Value;
		}

		// Token: 0x04000787 RID: 1927
		public ulong Value;
	}
}
