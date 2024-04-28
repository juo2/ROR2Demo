using System;

namespace SteamNative
{
	// Token: 0x02000146 RID: 326
	internal struct UGCHandle_t
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x000331D0 File Offset: 0x000313D0
		public static implicit operator UGCHandle_t(ulong value)
		{
			return new UGCHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000331EE File Offset: 0x000313EE
		public static implicit operator ulong(UGCHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x04000798 RID: 1944
		public ulong Value;
	}
}
