using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D1 RID: 465
	public class DisconnectedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0000D690 File Offset: 0x0000B890
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x0000D698 File Offset: 0x0000B898
		public Result ResultCode { get; private set; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0000D6A1 File Offset: 0x0000B8A1
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x0000D6A9 File Offset: 0x0000B8A9
		public object ClientData { get; private set; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0000D6B2 File Offset: 0x0000B8B2
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x0000D6BA File Offset: 0x0000B8BA
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C5C RID: 3164 RVA: 0x0000D6C3 File Offset: 0x0000B8C3
		// (set) Token: 0x06000C5D RID: 3165 RVA: 0x0000D6CB File Offset: 0x0000B8CB
		public string RoomName { get; private set; }

		// Token: 0x06000C5E RID: 3166 RVA: 0x0000D6D4 File Offset: 0x0000B8D4
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0000D6E4 File Offset: 0x0000B8E4
		internal void Set(DisconnectedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0000D74E File Offset: 0x0000B94E
		public void Set(object other)
		{
			this.Set(other as DisconnectedCallbackInfoInternal?);
		}
	}
}
