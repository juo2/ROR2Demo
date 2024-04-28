using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E1 RID: 993
	public class CreateUserCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00019588 File Offset: 0x00017788
		// (set) Token: 0x06001804 RID: 6148 RVA: 0x00019590 File Offset: 0x00017790
		public Result ResultCode { get; private set; }

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00019599 File Offset: 0x00017799
		// (set) Token: 0x06001806 RID: 6150 RVA: 0x000195A1 File Offset: 0x000177A1
		public object ClientData { get; private set; }

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x000195AA File Offset: 0x000177AA
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x000195B2 File Offset: 0x000177B2
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x000195BB File Offset: 0x000177BB
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x000195C3 File Offset: 0x000177C3
		public string KWSUserId { get; private set; }

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x000195CC File Offset: 0x000177CC
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x000195D4 File Offset: 0x000177D4
		public bool IsMinor { get; private set; }

		// Token: 0x0600180D RID: 6157 RVA: 0x000195DD File Offset: 0x000177DD
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000195EC File Offset: 0x000177EC
		internal void Set(CreateUserCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.KWSUserId = other.Value.KWSUserId;
				this.IsMinor = other.Value.IsMinor;
			}
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0001966B File Offset: 0x0001786B
		public void Set(object other)
		{
			this.Set(other as CreateUserCallbackInfoInternal?);
		}
	}
}
