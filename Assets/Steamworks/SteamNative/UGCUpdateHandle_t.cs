using System;

namespace SteamNative
{
	// Token: 0x02000156 RID: 342
	internal struct UGCUpdateHandle_t
	{
		// Token: 0x060009E4 RID: 2532 RVA: 0x00033450 File Offset: 0x00031650
		public static implicit operator UGCUpdateHandle_t(ulong value)
		{
			return new UGCUpdateHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0003346E File Offset: 0x0003166E
		public static implicit operator ulong(UGCUpdateHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x040007A8 RID: 1960
		public ulong Value;
	}
}
