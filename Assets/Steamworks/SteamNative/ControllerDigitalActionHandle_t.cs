using System;

namespace SteamNative
{
	// Token: 0x02000153 RID: 339
	internal struct ControllerDigitalActionHandle_t
	{
		// Token: 0x060009DE RID: 2526 RVA: 0x000333D8 File Offset: 0x000315D8
		public static implicit operator ControllerDigitalActionHandle_t(ulong value)
		{
			return new ControllerDigitalActionHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000333F6 File Offset: 0x000315F6
		public static implicit operator ulong(ControllerDigitalActionHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x040007A5 RID: 1957
		public ulong Value;
	}
}
