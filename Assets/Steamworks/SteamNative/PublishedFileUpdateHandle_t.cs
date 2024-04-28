using System;

namespace SteamNative
{
	// Token: 0x02000147 RID: 327
	internal struct PublishedFileUpdateHandle_t
	{
		// Token: 0x060009C6 RID: 2502 RVA: 0x000331F8 File Offset: 0x000313F8
		public static implicit operator PublishedFileUpdateHandle_t(ulong value)
		{
			return new PublishedFileUpdateHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x00033216 File Offset: 0x00031416
		public static implicit operator ulong(PublishedFileUpdateHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x04000799 RID: 1945
		public ulong Value;
	}
}
