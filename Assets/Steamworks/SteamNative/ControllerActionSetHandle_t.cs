using System;

namespace SteamNative
{
	// Token: 0x02000152 RID: 338
	internal struct ControllerActionSetHandle_t
	{
		// Token: 0x060009DC RID: 2524 RVA: 0x000333B0 File Offset: 0x000315B0
		public static implicit operator ControllerActionSetHandle_t(ulong value)
		{
			return new ControllerActionSetHandle_t
			{
				Value = value
			};
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000333CE File Offset: 0x000315CE
		public static implicit operator ulong(ControllerActionSetHandle_t value)
		{
			return value.Value;
		}

		// Token: 0x040007A4 RID: 1956
		public ulong Value;
	}
}
