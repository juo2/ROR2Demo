using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000012 RID: 18
	public static class Common
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000362C File Offset: 0x0000182C
		public static bool IsOperationComplete(Result result)
		{
			bool result2;
			Helper.TryMarshalGet(Bindings.EOS_EResult_IsOperationComplete(result), out result2);
			return result2;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00003648 File Offset: 0x00001848
		public static string ToString(Result result)
		{
			string result2;
			Helper.TryMarshalGet(Bindings.EOS_EResult_ToString(result), out result2);
			return result2;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00003664 File Offset: 0x00001864
		public static Result ToString(byte[] byteArray, out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			uint length;
			Helper.TryMarshalSet<byte>(ref zero, byteArray, out length);
			IntPtr zero2 = IntPtr.Zero;
			uint size = 1024U;
			Helper.TryMarshalAllocate(ref zero2, size);
			Result result = Bindings.EOS_ByteArray_ToString(zero, length, zero2, ref size);
			Helper.TryMarshalDispose(ref zero);
			Helper.TryMarshalGet(zero2, out outBuffer);
			Helper.TryMarshalDispose(ref zero2);
			return result;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000036BC File Offset: 0x000018BC
		public static string ToString(byte[] byteArray)
		{
			string result;
			Common.ToString(byteArray, out result);
			return result;
		}

		// Token: 0x0400000D RID: 13
		public const ulong InvalidNotificationid = 0UL;

		// Token: 0x0400000E RID: 14
		public const int PagequeryApiLatest = 1;

		// Token: 0x0400000F RID: 15
		public const int PagequeryMaxcountDefault = 10;

		// Token: 0x04000010 RID: 16
		public const int PagequeryMaxcountMaximum = 100;

		// Token: 0x04000011 RID: 17
		public const int PaginationApiLatest = 1;
	}
}
