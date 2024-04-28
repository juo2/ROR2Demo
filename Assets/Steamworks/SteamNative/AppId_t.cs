using System;

namespace SteamNative
{
	// Token: 0x02000134 RID: 308
	internal struct AppId_t
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x00032F00 File Offset: 0x00031100
		public static implicit operator AppId_t(uint value)
		{
			return new AppId_t
			{
				Value = value
			};
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00032F1E File Offset: 0x0003111E
		public static implicit operator uint(AppId_t value)
		{
			return value.Value;
		}

		// Token: 0x04000786 RID: 1926
		public uint Value;
	}
}
