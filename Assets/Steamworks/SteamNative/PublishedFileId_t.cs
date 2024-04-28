using System;

namespace SteamNative
{
	// Token: 0x02000148 RID: 328
	internal struct PublishedFileId_t
	{
		// Token: 0x060009C8 RID: 2504 RVA: 0x00033220 File Offset: 0x00031420
		public static implicit operator PublishedFileId_t(ulong value)
		{
			return new PublishedFileId_t
			{
				Value = value
			};
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0003323E File Offset: 0x0003143E
		public static implicit operator ulong(PublishedFileId_t value)
		{
			return value.Value;
		}

		// Token: 0x0400079A RID: 1946
		public ulong Value;
	}
}
