using System;

namespace SteamNative
{
	// Token: 0x02000155 RID: 341
	internal struct UGCQueryHandle_t
	{
		// Token: 0x060009E2 RID: 2530 RVA: 0x00033428 File Offset: 0x00031628
		public static implicit operator UGCQueryHandle_t(ulong value)
		{
			return new UGCQueryHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00033446 File Offset: 0x00031646
		public static implicit operator ulong(UGCQueryHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x040007A7 RID: 1959
		public ulong Value;
	}
}
