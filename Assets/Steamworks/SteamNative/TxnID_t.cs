using System;

namespace SteamNative
{
	// Token: 0x02000131 RID: 305
	internal struct TxnID_t
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x00032E88 File Offset: 0x00031088
		public static implicit operator TxnID_t(GID_t value)
		{
			return new TxnID_t
			{
				Value = value
			};
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00032EA6 File Offset: 0x000310A6
		public static implicit operator GID_t(TxnID_t value)
		{
			return value.Value;
		}

		// Token: 0x04000783 RID: 1923
		public GID_t Value;
	}
}
