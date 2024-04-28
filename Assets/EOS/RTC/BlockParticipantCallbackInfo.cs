using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001CD RID: 461
	public class BlockParticipantCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0000D3EF File Offset: 0x0000B5EF
		public Result ResultCode { get; private set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0000D400 File Offset: 0x0000B600
		public object ClientData { get; private set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0000D409 File Offset: 0x0000B609
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0000D411 File Offset: 0x0000B611
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0000D41A File Offset: 0x0000B61A
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0000D422 File Offset: 0x0000B622
		public string RoomName { get; private set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0000D42B File Offset: 0x0000B62B
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0000D433 File Offset: 0x0000B633
		public ProductUserId ParticipantId { get; private set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0000D43C File Offset: 0x0000B63C
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0000D444 File Offset: 0x0000B644
		public bool Blocked { get; private set; }

		// Token: 0x06000C3B RID: 3131 RVA: 0x0000D44D File Offset: 0x0000B64D
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x0000D45C File Offset: 0x0000B65C
		internal void Set(BlockParticipantCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.Blocked = other.Value.Blocked;
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		public void Set(object other)
		{
			this.Set(other as BlockParticipantCallbackInfoInternal?);
		}
	}
}
