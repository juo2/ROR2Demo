using System;

namespace SteamNative
{
	// Token: 0x02000132 RID: 306
	internal struct PackageId_t
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x00032EB0 File Offset: 0x000310B0
		public static implicit operator PackageId_t(uint value)
		{
			return new PackageId_t
			{
				Value = value
			};
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00032ECE File Offset: 0x000310CE
		public static implicit operator uint(PackageId_t value)
		{
			return value.Value;
		}

		// Token: 0x04000784 RID: 1924
		public uint Value;
	}
}
