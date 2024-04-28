using System;

namespace SteamNative
{
	// Token: 0x0200015C RID: 348
	internal struct CGameID
	{
		// Token: 0x060009F0 RID: 2544 RVA: 0x00033540 File Offset: 0x00031740
		public static implicit operator CGameID(ulong value)
		{
			return new CGameID
			{
				Value = value
			};
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0003355E File Offset: 0x0003175E
		public static implicit operator ulong(CGameID value)
		{
			return value.Value;
		}

		// Token: 0x040007AE RID: 1966
		public ulong Value;
	}
}
