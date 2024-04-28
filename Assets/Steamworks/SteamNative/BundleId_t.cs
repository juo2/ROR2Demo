using System;

namespace SteamNative
{
	// Token: 0x02000133 RID: 307
	internal struct BundleId_t
	{
		// Token: 0x0600099E RID: 2462 RVA: 0x00032ED8 File Offset: 0x000310D8
		public static implicit operator BundleId_t(uint value)
		{
			return new BundleId_t
			{
				Value = value
			};
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00032EF6 File Offset: 0x000310F6
		public static implicit operator uint(BundleId_t value)
		{
			return value.Value;
		}

		// Token: 0x04000785 RID: 1925
		public uint Value;
	}
}
