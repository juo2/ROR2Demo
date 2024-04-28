using System;

namespace SteamNative
{
	// Token: 0x02000158 RID: 344
	internal struct SteamItemInstanceID_t
	{
		// Token: 0x060009E8 RID: 2536 RVA: 0x000334A0 File Offset: 0x000316A0
		public static implicit operator SteamItemInstanceID_t(ulong value)
		{
			return new SteamItemInstanceID_t
			{
				Value = value
			};
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000334BE File Offset: 0x000316BE
		public static implicit operator ulong(SteamItemInstanceID_t value)
		{
			return value.Value;
		}

		// Token: 0x040007AA RID: 1962
		public ulong Value;
	}
}
