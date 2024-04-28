using System;

namespace SteamNative
{
	// Token: 0x0200014E RID: 334
	internal struct ScreenshotHandle
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00033310 File Offset: 0x00031510
		public static implicit operator ScreenshotHandle(uint value)
		{
			return new ScreenshotHandle
			{
				Value = value
			};
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0003332E File Offset: 0x0003152E
		public static implicit operator uint(ScreenshotHandle value)
		{
			return value.Value;
		}

		// Token: 0x040007A0 RID: 1952
		public uint Value;
	}
}
