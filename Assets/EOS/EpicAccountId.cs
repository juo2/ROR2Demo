using System;

namespace Epic.OnlineServices
{
	// Token: 0x02000015 RID: 21
	public sealed class EpicAccountId : Handle
	{
		// Token: 0x06000283 RID: 643 RVA: 0x000036D3 File Offset: 0x000018D3
		public EpicAccountId()
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000036DB File Offset: 0x000018DB
		public EpicAccountId(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000373C File Offset: 0x0000193C
		public static EpicAccountId FromString(string accountIdString)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.TryMarshalSet(ref zero, accountIdString);
			IntPtr source = Bindings.EOS_EpicAccountId_FromString(zero);
			Helper.TryMarshalDispose(ref zero);
			EpicAccountId result;
			Helper.TryMarshalGet<EpicAccountId>(source, out result);
			return result;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00003770 File Offset: 0x00001970
		public bool IsValid()
		{
			bool result;
			Helper.TryMarshalGet(Bindings.EOS_EpicAccountId_IsValid(base.InnerHandle), out result);
			return result;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00003794 File Offset: 0x00001994
		public Result ToString(out string outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			int size = 33;
			Helper.TryMarshalAllocate(ref zero, size);
			Result result = Bindings.EOS_EpicAccountId_ToString(base.InnerHandle, zero, ref size);
			Helper.TryMarshalGet(zero, out outBuffer);
			Helper.TryMarshalDispose(ref zero);
			return result;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000037D4 File Offset: 0x000019D4
		public override string ToString()
		{
			string result;
			this.ToString(out result);
			return result;
		}

		// Token: 0x0400001F RID: 31
		public const int EpicaccountidMaxLength = 32;
	}
}
