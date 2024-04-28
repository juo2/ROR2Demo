using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004BF RID: 1215
	public class CreateUserCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x0001F620 File Offset: 0x0001D820
		// (set) Token: 0x06001D7A RID: 7546 RVA: 0x0001F628 File Offset: 0x0001D828
		public Result ResultCode { get; private set; }

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x0001F631 File Offset: 0x0001D831
		// (set) Token: 0x06001D7C RID: 7548 RVA: 0x0001F639 File Offset: 0x0001D839
		public object ClientData { get; private set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x0001F642 File Offset: 0x0001D842
		// (set) Token: 0x06001D7E RID: 7550 RVA: 0x0001F64A File Offset: 0x0001D84A
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001D7F RID: 7551 RVA: 0x0001F653 File Offset: 0x0001D853
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0001F660 File Offset: 0x0001D860
		internal void Set(CreateUserCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0001F6B5 File Offset: 0x0001D8B5
		public void Set(object other)
		{
			this.Set(other as CreateUserCallbackInfoInternal?);
		}
	}
}
