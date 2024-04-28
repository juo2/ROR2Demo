using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AC RID: 428
	public class UpdateSendingCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x0000C798 File Offset: 0x0000A998
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		public Result ResultCode { get; private set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x0000C7A9 File Offset: 0x0000A9A9
		// (set) Token: 0x06000B65 RID: 2917 RVA: 0x0000C7B1 File Offset: 0x0000A9B1
		public object ClientData { get; private set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x0000C7BA File Offset: 0x0000A9BA
		// (set) Token: 0x06000B67 RID: 2919 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x0000C7CB File Offset: 0x0000A9CB
		// (set) Token: 0x06000B69 RID: 2921 RVA: 0x0000C7D3 File Offset: 0x0000A9D3
		public string RoomName { get; private set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		// (set) Token: 0x06000B6B RID: 2923 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		public RTCAudioStatus AudioStatus { get; private set; }

		// Token: 0x06000B6C RID: 2924 RVA: 0x0000C7ED File Offset: 0x0000A9ED
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0000C7FC File Offset: 0x0000A9FC
		internal void Set(UpdateSendingCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.AudioStatus = other.Value.AudioStatus;
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0000C87B File Offset: 0x0000AA7B
		public void Set(object other)
		{
			this.Set(other as UpdateSendingCallbackInfoInternal?);
		}
	}
}
