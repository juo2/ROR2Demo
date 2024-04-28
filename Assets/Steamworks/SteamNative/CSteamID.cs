using System;

namespace SteamNative
{
	// Token: 0x0200015D RID: 349
	internal struct CSteamID
	{
		// Token: 0x060009F2 RID: 2546 RVA: 0x00033568 File Offset: 0x00031768
		public static implicit operator CSteamID(ulong value)
		{
			return new CSteamID
			{
				Value = value
			};
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00033586 File Offset: 0x00031786
		public static implicit operator ulong(CSteamID value)
		{
			return value.Value;
		}

		// Token: 0x040007AF RID: 1967
		public ulong Value;
	}
}
