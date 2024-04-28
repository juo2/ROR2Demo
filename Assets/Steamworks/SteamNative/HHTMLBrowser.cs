using System;

namespace SteamNative
{
	// Token: 0x02000157 RID: 343
	internal struct HHTMLBrowser
	{
		// Token: 0x060009E6 RID: 2534 RVA: 0x00033478 File Offset: 0x00031678
		public static implicit operator HHTMLBrowser(uint value)
		{
			return new HHTMLBrowser
			{
				Value = value
			};
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00033496 File Offset: 0x00031696
		public static implicit operator uint(HHTMLBrowser value)
		{
			return value.Value;
		}

		// Token: 0x040007A9 RID: 1961
		public uint Value;
	}
}
