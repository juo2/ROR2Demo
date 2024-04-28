using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200022F RID: 559
	public class SetPresenceCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		// (set) Token: 0x06000E81 RID: 3713 RVA: 0x0000FAE0 File Offset: 0x0000DCE0
		public Result ResultCode { get; private set; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0000FAE9 File Offset: 0x0000DCE9
		// (set) Token: 0x06000E83 RID: 3715 RVA: 0x0000FAF1 File Offset: 0x0000DCF1
		public object ClientData { get; private set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0000FAFA File Offset: 0x0000DCFA
		// (set) Token: 0x06000E85 RID: 3717 RVA: 0x0000FB02 File Offset: 0x0000DD02
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x06000E86 RID: 3718 RVA: 0x0000FB0B File Offset: 0x0000DD0B
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x0000FB18 File Offset: 0x0000DD18
		internal void Set(SetPresenceCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0000FB6D File Offset: 0x0000DD6D
		public void Set(object other)
		{
			this.Set(other as SetPresenceCallbackInfoInternal?);
		}
	}
}
