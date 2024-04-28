using System;

namespace SteamNative
{
	// Token: 0x02000145 RID: 325
	internal struct HServerQuery
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x000331A8 File Offset: 0x000313A8
		public static implicit operator HServerQuery(int value)
		{
			return new HServerQuery
			{
				Value = value
			};
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000331C6 File Offset: 0x000313C6
		public static implicit operator int(HServerQuery value)
		{
			return value.Value;
		}

		// Token: 0x04000797 RID: 1943
		public int Value;
	}
}
