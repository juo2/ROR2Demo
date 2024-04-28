using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048E RID: 1166
	public sealed class Transaction : Handle
	{
		// Token: 0x06001C67 RID: 7271 RVA: 0x000036D3 File Offset: 0x000018D3
		public Transaction()
		{
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000036DB File Offset: 0x000018DB
		public Transaction(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x0001DFF4 File Offset: 0x0001C1F4
		public Result CopyEntitlementByIndex(TransactionCopyEntitlementByIndexOptions options, out Entitlement outEntitlement)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<TransactionCopyEntitlementByIndexOptionsInternal, TransactionCopyEntitlementByIndexOptions>(ref zero, options);
			IntPtr zero2 = IntPtr.Zero;
			Result result = Bindings.EOS_Ecom_Transaction_CopyEntitlementByIndex(base.InnerHandle, zero, ref zero2);
			Helper.TryMarshalDispose(ref zero);
			if (Helper.TryMarshalGet<EntitlementInternal, Entitlement>(zero2, out outEntitlement))
			{
				Bindings.EOS_Ecom_Entitlement_Release(zero2);
			}
			return result;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x0001E03C File Offset: 0x0001C23C
		public uint GetEntitlementsCount(TransactionGetEntitlementsCountOptions options)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet<TransactionGetEntitlementsCountOptionsInternal, TransactionGetEntitlementsCountOptions>(ref zero, options);
			uint result = Bindings.EOS_Ecom_Transaction_GetEntitlementsCount(base.InnerHandle, zero);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x0001E06C File Offset: 0x0001C26C
		public Result GetTransactionId(out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			int size = 65;
			Helper.TryMarshalAllocate(ref zero, size);
			Result result = Bindings.EOS_Ecom_Transaction_GetTransactionId(base.InnerHandle, zero, ref size);
			Helper.TryMarshalGet(zero, out outBuffer);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0001E0A9 File Offset: 0x0001C2A9
		public void Release()
		{
			Bindings.EOS_Ecom_Transaction_Release(base.InnerHandle);
		}

		// Token: 0x04000D46 RID: 3398
		public const int TransactionCopyentitlementbyindexApiLatest = 1;

		// Token: 0x04000D47 RID: 3399
		public const int TransactionGetentitlementscountApiLatest = 1;
	}
}
