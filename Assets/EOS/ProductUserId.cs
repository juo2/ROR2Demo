using System;

namespace Epic.OnlineServices
{
	// Token: 0x0200001D RID: 29
	public sealed class ProductUserId : Handle
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x000036D3 File Offset: 0x000018D3
		public ProductUserId()
		{
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000036DB File Offset: 0x000018DB
		public ProductUserId(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000039C0 File Offset: 0x00001BC0
		public static ProductUserId FromString(string productUserIdString)
		{
			bool flag;
			return ProductUserId.FromString(productUserIdString, out flag);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000039D8 File Offset: 0x00001BD8
		public static ProductUserId FromString(string productUserIdString, out bool success)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, productUserIdString);
			IntPtr source = Bindings.EOS_ProductUserId_FromString(zero);
			Helper.TryMarshalDispose(ref zero);
			ProductUserId result;
			success = Helper.TryMarshalGet<ProductUserId>(source, out result);
			return result;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00003A10 File Offset: 0x00001C10
		public bool IsValid()
		{
			bool result;
			Helper.TryMarshalGet(Bindings.EOS_ProductUserId_IsValid(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00003A34 File Offset: 0x00001C34
		public Result ToString(out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			int size = 33;
			Helper.TryMarshalAllocate(ref zero, size);
			Result result = Bindings.EOS_ProductUserId_ToString(base.InnerHandle, zero, ref size);
			Helper.TryMarshalGet(zero, out outBuffer);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00003A74 File Offset: 0x00001C74
		public override string ToString()
		{
			string result;
			this.ToString(out result);
			return result;
		}

		// Token: 0x04000052 RID: 82
		public const int ProductuseridMaxLength = 32;
	}
}
