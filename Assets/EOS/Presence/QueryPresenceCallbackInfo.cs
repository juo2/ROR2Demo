using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200022B RID: 555
	public class QueryPresenceCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0000F917 File Offset: 0x0000DB17
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x0000F91F File Offset: 0x0000DB1F
		public Result ResultCode { get; private set; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0000F928 File Offset: 0x0000DB28
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0000F930 File Offset: 0x0000DB30
		public object ClientData { get; private set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0000F939 File Offset: 0x0000DB39
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0000F941 File Offset: 0x0000DB41
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0000F94A File Offset: 0x0000DB4A
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0000F952 File Offset: 0x0000DB52
		public EpicAccountId TargetUserId { get; private set; }

		// Token: 0x06000E6D RID: 3693 RVA: 0x0000F95B File Offset: 0x0000DB5B
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0000F968 File Offset: 0x0000DB68
		internal void Set(QueryPresenceCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x0000F9D2 File Offset: 0x0000DBD2
		public void Set(object other)
		{
			this.Set(other as QueryPresenceCallbackInfoInternal?);
		}
	}
}
