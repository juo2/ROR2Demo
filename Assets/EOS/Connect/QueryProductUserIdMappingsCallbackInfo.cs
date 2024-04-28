using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004F9 RID: 1273
	public class QueryProductUserIdMappingsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x0002043D File Offset: 0x0001E63D
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x00020445 File Offset: 0x0001E645
		public Result ResultCode { get; private set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x0002044E File Offset: 0x0001E64E
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x00020456 File Offset: 0x0001E656
		public object ClientData { get; private set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x0002045F File Offset: 0x0001E65F
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x00020467 File Offset: 0x0001E667
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001EB9 RID: 7865 RVA: 0x00020470 File Offset: 0x0001E670
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x00020480 File Offset: 0x0001E680
		internal void Set(QueryProductUserIdMappingsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000204D5 File Offset: 0x0001E6D5
		public void Set(object other)
		{
			this.Set(other as QueryProductUserIdMappingsCallbackInfoInternal?);
		}
	}
}
