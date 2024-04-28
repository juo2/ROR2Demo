using System;

namespace SteamNative
{
	// Token: 0x0200013B RID: 315
	internal struct AccountID_t
	{
		// Token: 0x060009AE RID: 2478 RVA: 0x00033018 File Offset: 0x00031218
		public static implicit operator AccountID_t(uint value)
		{
			return new AccountID_t
			{
				Value = value
			};
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00033036 File Offset: 0x00031236
		public static implicit operator uint(AccountID_t value)
		{
			return value.Value;
		}

		// Token: 0x0400078D RID: 1933
		public uint Value;
	}
}
