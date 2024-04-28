using System;

namespace SteamNative
{
	// Token: 0x02000139 RID: 313
	internal struct CellID_t
	{
		// Token: 0x060009AA RID: 2474 RVA: 0x00032FC8 File Offset: 0x000311C8
		public static implicit operator CellID_t(uint value)
		{
			return new CellID_t
			{
				Value = value
			};
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00032FE6 File Offset: 0x000311E6
		public static implicit operator uint(CellID_t value)
		{
			return value.Value;
		}

		// Token: 0x0400078B RID: 1931
		public uint Value;
	}
}
