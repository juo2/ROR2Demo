using System;

namespace SteamNative
{
	// Token: 0x02000138 RID: 312
	internal struct RTime32
	{
		// Token: 0x060009A8 RID: 2472 RVA: 0x00032FA0 File Offset: 0x000311A0
		public static implicit operator RTime32(uint value)
		{
			return new RTime32
			{
				Value = value
			};
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00032FBE File Offset: 0x000311BE
		public static implicit operator uint(RTime32 value)
		{
			return value.Value;
		}

		// Token: 0x0400078A RID: 1930
		public uint Value;
	}
}
