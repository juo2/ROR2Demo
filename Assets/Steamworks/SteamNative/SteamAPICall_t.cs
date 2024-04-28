using System;

namespace SteamNative
{
	// Token: 0x0200013A RID: 314
	internal struct SteamAPICall_t
	{
		// Token: 0x060009AC RID: 2476 RVA: 0x00032FF0 File Offset: 0x000311F0
		public static implicit operator SteamAPICall_t(ulong value)
		{
			return new SteamAPICall_t
			{
				Value = value
			};
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0003300E File Offset: 0x0003120E
		public static implicit operator ulong(SteamAPICall_t value)
		{
			return value.Value;
		}

		// Token: 0x0400078C RID: 1932
		public ulong Value;
	}
}
