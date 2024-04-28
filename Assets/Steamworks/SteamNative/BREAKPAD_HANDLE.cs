using System;

namespace SteamNative
{
	// Token: 0x02000140 RID: 320
	internal struct BREAKPAD_HANDLE
	{
		// Token: 0x060009B8 RID: 2488 RVA: 0x000330E0 File Offset: 0x000312E0
		public static implicit operator BREAKPAD_HANDLE(IntPtr value)
		{
			return new BREAKPAD_HANDLE
			{
				Value = value
			};
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000330FE File Offset: 0x000312FE
		public static implicit operator IntPtr(BREAKPAD_HANDLE value)
		{
			return value.Value;
		}

		// Token: 0x04000792 RID: 1938
		public IntPtr Value;
	}
}
