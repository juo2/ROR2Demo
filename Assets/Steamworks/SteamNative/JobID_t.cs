using System;

namespace SteamNative
{
	// Token: 0x02000130 RID: 304
	internal struct JobID_t
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x00032E60 File Offset: 0x00031060
		public static implicit operator JobID_t(ulong value)
		{
			return new JobID_t
			{
				Value = value
			};
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00032E7E File Offset: 0x0003107E
		public static implicit operator ulong(JobID_t value)
		{
			return value.Value;
		}

		// Token: 0x04000782 RID: 1922
		public ulong Value;
	}
}
