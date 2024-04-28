using System;

namespace SteamNative
{
	// Token: 0x0200013D RID: 317
	internal struct ManifestId_t
	{
		// Token: 0x060009B2 RID: 2482 RVA: 0x00033068 File Offset: 0x00031268
		public static implicit operator ManifestId_t(ulong value)
		{
			return new ManifestId_t
			{
				Value = value
			};
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00033086 File Offset: 0x00031286
		public static implicit operator ulong(ManifestId_t value)
		{
			return value.Value;
		}

		// Token: 0x0400078F RID: 1935
		public ulong Value;
	}
}
