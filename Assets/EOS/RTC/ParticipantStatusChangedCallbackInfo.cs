using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x020001E8 RID: 488
	public class ParticipantStatusChangedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0000DD9B File Offset: 0x0000BF9B
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x0000DDA3 File Offset: 0x0000BFA3
		public object ClientData { get; private set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0000DDAC File Offset: 0x0000BFAC
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x0000DDB4 File Offset: 0x0000BFB4
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0000DDBD File Offset: 0x0000BFBD
		// (set) Token: 0x06000CEA RID: 3306 RVA: 0x0000DDC5 File Offset: 0x0000BFC5
		public string RoomName { get; private set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0000DDCE File Offset: 0x0000BFCE
		// (set) Token: 0x06000CEC RID: 3308 RVA: 0x0000DDD6 File Offset: 0x0000BFD6
		public ProductUserId ParticipantId { get; private set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0000DDDF File Offset: 0x0000BFDF
		// (set) Token: 0x06000CEE RID: 3310 RVA: 0x0000DDE7 File Offset: 0x0000BFE7
		public RTCParticipantStatus ParticipantStatus { get; private set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0000DDF0 File Offset: 0x0000BFF0
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		public ParticipantMetadata[] ParticipantMetadata { get; private set; }

		// Token: 0x06000CF1 RID: 3313 RVA: 0x0000DE04 File Offset: 0x0000C004
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x0000DE1C File Offset: 0x0000C01C
		internal void Set(ParticipantStatusChangedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.ParticipantStatus = other.Value.ParticipantStatus;
				this.ParticipantMetadata = other.Value.ParticipantMetadata;
			}
		}

		// Token: 0x06000CF3 RID: 3315 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public void Set(object other)
		{
			this.Set(other as ParticipantStatusChangedCallbackInfoInternal?);
		}
	}
}
