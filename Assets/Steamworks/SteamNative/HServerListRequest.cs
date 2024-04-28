using System;

namespace SteamNative
{
	// Token: 0x02000144 RID: 324
	internal struct HServerListRequest
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x00033180 File Offset: 0x00031380
		public static implicit operator HServerListRequest(IntPtr value)
		{
			return new HServerListRequest
			{
				Value = value
			};
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0003319E File Offset: 0x0003139E
		public static implicit operator IntPtr(HServerListRequest value)
		{
			return value.Value;
		}

		// Token: 0x04000796 RID: 1942
		public IntPtr Value;
	}
}
