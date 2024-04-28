using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D3 RID: 467
	public class JoinRoomCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0000D7C8 File Offset: 0x0000B9C8
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
		public Result ResultCode { get; private set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0000D7D9 File Offset: 0x0000B9D9
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x0000D7E1 File Offset: 0x0000B9E1
		public object ClientData { get; private set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0000D7EA File Offset: 0x0000B9EA
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0000D7F2 File Offset: 0x0000B9F2
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0000D7FB File Offset: 0x0000B9FB
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0000D803 File Offset: 0x0000BA03
		public string RoomName { get; private set; }

		// Token: 0x06000C6F RID: 3183 RVA: 0x0000D80C File Offset: 0x0000BA0C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0000D81C File Offset: 0x0000BA1C
		internal void Set(JoinRoomCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0000D886 File Offset: 0x0000BA86
		public void Set(object other)
		{
			this.Set(other as JoinRoomCallbackInfoInternal?);
		}
	}
}
