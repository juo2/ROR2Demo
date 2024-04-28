using System;

namespace SteamNative
{
	// Token: 0x02000151 RID: 337
	internal struct ControllerHandle_t
	{
		// Token: 0x060009DA RID: 2522 RVA: 0x00033388 File Offset: 0x00031588
		public static implicit operator ControllerHandle_t(ulong value)
		{
			return new ControllerHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000333A6 File Offset: 0x000315A6
		public static implicit operator ulong(ControllerHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x040007A3 RID: 1955
		public ulong Value;
	}
}
