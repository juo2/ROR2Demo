using System;

namespace SteamNative
{
	// Token: 0x02000150 RID: 336
	internal struct HTTPCookieContainerHandle
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x00033360 File Offset: 0x00031560
		public static implicit operator HTTPCookieContainerHandle(uint value)
		{
			return new HTTPCookieContainerHandle
			{
				Value = value
			};
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0003337E File Offset: 0x0003157E
		public static implicit operator uint(HTTPCookieContainerHandle value)
		{
			return value.Value;
		}

		// Token: 0x040007A2 RID: 1954
		public uint Value;
	}
}
