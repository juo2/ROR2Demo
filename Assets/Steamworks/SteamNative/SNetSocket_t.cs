using System;

namespace SteamNative
{
	// Token: 0x0200014C RID: 332
	internal struct SNetSocket_t
	{
		// Token: 0x060009D0 RID: 2512 RVA: 0x000332C0 File Offset: 0x000314C0
		public static implicit operator SNetSocket_t(uint value)
		{
			return new SNetSocket_t
			{
				Value = value
			};
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x000332DE File Offset: 0x000314DE
		public static implicit operator uint(SNetSocket_t value)
		{
			return value.Value;
		}

		// Token: 0x0400079E RID: 1950
		public uint Value;
	}
}
