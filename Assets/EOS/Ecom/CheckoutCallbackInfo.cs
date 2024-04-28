using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000437 RID: 1079
	public class CheckoutCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x0001BD1F File Offset: 0x00019F1F
		// (set) Token: 0x06001A57 RID: 6743 RVA: 0x0001BD27 File Offset: 0x00019F27
		public Result ResultCode { get; private set; }

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0001BD30 File Offset: 0x00019F30
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x0001BD38 File Offset: 0x00019F38
		public object ClientData { get; private set; }

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0001BD41 File Offset: 0x00019F41
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x0001BD49 File Offset: 0x00019F49
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0001BD52 File Offset: 0x00019F52
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x0001BD5A File Offset: 0x00019F5A
		public string TransactionId { get; private set; }

		// Token: 0x06001A5E RID: 6750 RVA: 0x0001BD63 File Offset: 0x00019F63
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x0001BD70 File Offset: 0x00019F70
		internal void Set(CheckoutCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TransactionId = other.Value.TransactionId;
			}
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x0001BDDA File Offset: 0x00019FDA
		public void Set(object other)
		{
			this.Set(other as CheckoutCallbackInfoInternal?);
		}
	}
}
