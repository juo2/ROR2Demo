using System;

namespace SteamNative
{
	// Token: 0x0200013F RID: 319
	internal struct HAuthTicket
	{
		// Token: 0x060009B6 RID: 2486 RVA: 0x000330B8 File Offset: 0x000312B8
		public static implicit operator HAuthTicket(uint value)
		{
			return new HAuthTicket
			{
				Value = value
			};
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000330D6 File Offset: 0x000312D6
		public static implicit operator uint(HAuthTicket value)
		{
			return value.Value;
		}

		// Token: 0x04000791 RID: 1937
		public uint Value;
	}
}
