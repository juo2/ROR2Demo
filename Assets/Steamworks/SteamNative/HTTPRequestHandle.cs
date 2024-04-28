using System;

namespace SteamNative
{
	// Token: 0x0200014F RID: 335
	internal struct HTTPRequestHandle
	{
		// Token: 0x060009D6 RID: 2518 RVA: 0x00033338 File Offset: 0x00031538
		public static implicit operator HTTPRequestHandle(uint value)
		{
			return new HTTPRequestHandle
			{
				Value = value
			};
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x00033356 File Offset: 0x00031556
		public static implicit operator uint(HTTPRequestHandle value)
		{
			return value.Value;
		}

		// Token: 0x040007A1 RID: 1953
		public uint Value;
	}
}
