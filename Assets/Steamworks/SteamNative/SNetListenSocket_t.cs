using System;

namespace SteamNative
{
	// Token: 0x0200014D RID: 333
	internal struct SNetListenSocket_t
	{
		// Token: 0x060009D2 RID: 2514 RVA: 0x000332E8 File Offset: 0x000314E8
		public static implicit operator SNetListenSocket_t(uint value)
		{
			return new SNetListenSocket_t
			{
				Value = value
			};
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00033306 File Offset: 0x00031506
		public static implicit operator uint(SNetListenSocket_t value)
		{
			return value.Value;
		}

		// Token: 0x0400079F RID: 1951
		public uint Value;
	}
}
