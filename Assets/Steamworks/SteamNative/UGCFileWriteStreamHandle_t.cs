using System;

namespace SteamNative
{
	// Token: 0x02000149 RID: 329
	internal struct UGCFileWriteStreamHandle_t
	{
		// Token: 0x060009CA RID: 2506 RVA: 0x00033248 File Offset: 0x00031448
		public static implicit operator UGCFileWriteStreamHandle_t(ulong value)
		{
			return new UGCFileWriteStreamHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x00033266 File Offset: 0x00031466
		public static implicit operator ulong(UGCFileWriteStreamHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x0400079B RID: 1947
		public ulong Value;
	}
}
