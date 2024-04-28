using System;

namespace SteamNative
{
	// Token: 0x0200015A RID: 346
	internal struct SteamInventoryResult_t
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x000334F0 File Offset: 0x000316F0
		public static implicit operator SteamInventoryResult_t(int value)
		{
			return new SteamInventoryResult_t
			{
				Value = value
			};
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0003350E File Offset: 0x0003170E
		public static implicit operator int(SteamInventoryResult_t value)
		{
			return value.Value;
		}

		// Token: 0x040007AC RID: 1964
		public int Value;
	}
}
