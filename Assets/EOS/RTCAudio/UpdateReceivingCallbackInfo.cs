using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A8 RID: 424
	public class UpdateReceivingCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		public Result ResultCode { get; private set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0000C501 File Offset: 0x0000A701
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0000C509 File Offset: 0x0000A709
		public object ClientData { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0000C512 File Offset: 0x0000A712
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0000C51A File Offset: 0x0000A71A
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0000C523 File Offset: 0x0000A723
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0000C52B File Offset: 0x0000A72B
		public string RoomName { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0000C534 File Offset: 0x0000A734
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0000C53C File Offset: 0x0000A73C
		public ProductUserId ParticipantId { get; private set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x0000C545 File Offset: 0x0000A745
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x0000C54D File Offset: 0x0000A74D
		public bool AudioEnabled { get; private set; }

		// Token: 0x06000B47 RID: 2887 RVA: 0x0000C556 File Offset: 0x0000A756
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0000C564 File Offset: 0x0000A764
		internal void Set(UpdateReceivingCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.AudioEnabled = other.Value.AudioEnabled;
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		public void Set(object other)
		{
			this.Set(other as UpdateReceivingCallbackInfoInternal?);
		}
	}
}
