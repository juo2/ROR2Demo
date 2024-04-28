using System;

namespace SteamNative
{
	// Token: 0x02000154 RID: 340
	internal struct ControllerAnalogActionHandle_t
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x00033400 File Offset: 0x00031600
		public static implicit operator ControllerAnalogActionHandle_t(ulong value)
		{
			return new ControllerAnalogActionHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0003341E File Offset: 0x0003161E
		public static implicit operator ulong(ControllerAnalogActionHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x040007A6 RID: 1958
		public ulong Value;
	}
}
