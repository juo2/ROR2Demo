using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200000B RID: 11
	public static class HelperExtensions
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00003612 File Offset: 0x00001812
		public static bool IsOperationComplete(this Result result)
		{
			return Common.IsOperationComplete(result);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000361A File Offset: 0x0000181A
		public static string ToHexString(this byte[] byteArray)
		{
			return Common.ToString(byteArray);
		}
	}
}
