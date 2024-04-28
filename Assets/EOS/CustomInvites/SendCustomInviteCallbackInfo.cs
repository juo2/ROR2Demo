using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A4 RID: 1188
	public class SendCustomInviteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x0001E75C File Offset: 0x0001C95C
		// (set) Token: 0x06001CDD RID: 7389 RVA: 0x0001E764 File Offset: 0x0001C964
		public Result ResultCode { get; private set; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x0001E76D File Offset: 0x0001C96D
		// (set) Token: 0x06001CDF RID: 7391 RVA: 0x0001E775 File Offset: 0x0001C975
		public object ClientData { get; private set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x0001E77E File Offset: 0x0001C97E
		// (set) Token: 0x06001CE1 RID: 7393 RVA: 0x0001E786 File Offset: 0x0001C986
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x0001E78F File Offset: 0x0001C98F
		// (set) Token: 0x06001CE3 RID: 7395 RVA: 0x0001E797 File Offset: 0x0001C997
		public ProductUserId[] TargetUserIds { get; private set; }

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0001E7A0 File Offset: 0x0001C9A0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		internal void Set(SendCustomInviteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserIds = other.Value.TargetUserIds;
			}
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x0001E81A File Offset: 0x0001CA1A
		public void Set(object other)
		{
			this.Set(other as SendCustomInviteCallbackInfoInternal?);
		}
	}
}
