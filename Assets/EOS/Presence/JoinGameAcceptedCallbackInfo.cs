using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000213 RID: 531
	public class JoinGameAcceptedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0000F005 File Offset: 0x0000D205
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0000F00D File Offset: 0x0000D20D
		public object ClientData { get; private set; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0000F016 File Offset: 0x0000D216
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0000F01E File Offset: 0x0000D21E
		public string JoinInfo { get; private set; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0000F027 File Offset: 0x0000D227
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x0000F02F File Offset: 0x0000D22F
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0000F038 File Offset: 0x0000D238
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x0000F040 File Offset: 0x0000D240
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0000F049 File Offset: 0x0000D249
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0000F051 File Offset: 0x0000D251
		public ulong UiEventId { get; private set; }

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0000F05C File Offset: 0x0000D25C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0000F074 File Offset: 0x0000D274
		internal void Set(JoinGameAcceptedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.JoinInfo = other.Value.JoinInfo;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.UiEventId = other.Value.UiEventId;
			}
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0000F0F3 File Offset: 0x0000D2F3
		public void Set(object other)
		{
			this.Set(other as JoinGameAcceptedCallbackInfoInternal?);
		}
	}
}
