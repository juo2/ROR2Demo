using System;

namespace SteamNative
{
	// Token: 0x02000137 RID: 311
	internal struct DepotId_t
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x00032F78 File Offset: 0x00031178
		public static implicit operator DepotId_t(uint value)
		{
			return new DepotId_t
			{
				Value = value
			};
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x00032F96 File Offset: 0x00031196
		public static implicit operator uint(DepotId_t value)
		{
			return value.Value;
		}

		// Token: 0x04000789 RID: 1929
		public uint Value;
	}
}
