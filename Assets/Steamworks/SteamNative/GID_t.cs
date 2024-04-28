using System;

namespace SteamNative
{
	// Token: 0x0200012F RID: 303
	internal struct GID_t
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x00032E38 File Offset: 0x00031038
		public static implicit operator GID_t(ulong value)
		{
			return new GID_t
			{
				Value = value
			};
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00032E56 File Offset: 0x00031056
		public static implicit operator ulong(GID_t value)
		{
			return value.Value;
		}

		// Token: 0x04000781 RID: 1921
		public ulong Value;
	}
}
