using System;

namespace SteamNative
{
	// Token: 0x0200015B RID: 347
	internal struct SteamInventoryUpdateHandle_t
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x00033518 File Offset: 0x00031718
		public static implicit operator SteamInventoryUpdateHandle_t(ulong value)
		{
			return new SteamInventoryUpdateHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00033536 File Offset: 0x00031736
		public static implicit operator ulong(SteamInventoryUpdateHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x040007AD RID: 1965
		public ulong Value;
	}
}
