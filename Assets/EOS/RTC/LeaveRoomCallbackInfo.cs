using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001D8 RID: 472
	public class LeaveRoomCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0000DABF File Offset: 0x0000BCBF
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0000DAC7 File Offset: 0x0000BCC7
		public Result ResultCode { get; private set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0000DAD0 File Offset: 0x0000BCD0
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0000DAD8 File Offset: 0x0000BCD8
		public object ClientData { get; private set; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0000DAE1 File Offset: 0x0000BCE1
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0000DAE9 File Offset: 0x0000BCE9
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0000DAF2 File Offset: 0x0000BCF2
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0000DAFA File Offset: 0x0000BCFA
		public string RoomName { get; private set; }

		// Token: 0x06000C9C RID: 3228 RVA: 0x0000DB03 File Offset: 0x0000BD03
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0000DB10 File Offset: 0x0000BD10
		internal void Set(LeaveRoomCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0000DB7A File Offset: 0x0000BD7A
		public void Set(object other)
		{
			this.Set(other as LeaveRoomCallbackInfoInternal?);
		}
	}
}
