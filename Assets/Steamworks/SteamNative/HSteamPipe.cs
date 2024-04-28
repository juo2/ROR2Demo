using System;

namespace SteamNative
{
	// Token: 0x02000141 RID: 321
	internal struct HSteamPipe
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x00033108 File Offset: 0x00031308
		public static implicit operator HSteamPipe(int value)
		{
			return new HSteamPipe
			{
				Value = value
			};
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00033126 File Offset: 0x00031326
		public static implicit operator int(HSteamPipe value)
		{
			return value.Value;
		}

		// Token: 0x04000793 RID: 1939
		public int Value;
	}
}
