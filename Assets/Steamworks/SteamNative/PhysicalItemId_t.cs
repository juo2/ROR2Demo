using System;

namespace SteamNative
{
	// Token: 0x02000136 RID: 310
	internal struct PhysicalItemId_t
	{
		// Token: 0x060009A4 RID: 2468 RVA: 0x00032F50 File Offset: 0x00031150
		public static implicit operator PhysicalItemId_t(uint value)
		{
			return new PhysicalItemId_t
			{
				Value = value
			};
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00032F6E File Offset: 0x0003116E
		public static implicit operator uint(PhysicalItemId_t value)
		{
			return value.Value;
		}

		// Token: 0x04000788 RID: 1928
		public uint Value;
	}
}
