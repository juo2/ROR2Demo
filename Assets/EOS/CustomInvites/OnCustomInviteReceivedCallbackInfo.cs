using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004A0 RID: 1184
	public class OnCustomInviteReceivedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		// (set) Token: 0x06001CC1 RID: 7361 RVA: 0x0001E5CC File Offset: 0x0001C7CC
		public object ClientData { get; private set; }

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x0001E5D5 File Offset: 0x0001C7D5
		// (set) Token: 0x06001CC3 RID: 7363 RVA: 0x0001E5DD File Offset: 0x0001C7DD
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x0001E5E6 File Offset: 0x0001C7E6
		// (set) Token: 0x06001CC5 RID: 7365 RVA: 0x0001E5EE File Offset: 0x0001C7EE
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001CC6 RID: 7366 RVA: 0x0001E5F7 File Offset: 0x0001C7F7
		// (set) Token: 0x06001CC7 RID: 7367 RVA: 0x0001E5FF File Offset: 0x0001C7FF
		public string CustomInviteId { get; private set; }

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x0001E608 File Offset: 0x0001C808
		// (set) Token: 0x06001CC9 RID: 7369 RVA: 0x0001E610 File Offset: 0x0001C810
		public string Payload { get; private set; }

		// Token: 0x06001CCA RID: 7370 RVA: 0x0001E61C File Offset: 0x0001C81C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0001E634 File Offset: 0x0001C834
		internal void Set(OnCustomInviteReceivedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.TargetUserId = other.Value.TargetUserId;
				this.LocalUserId = other.Value.LocalUserId;
				this.CustomInviteId = other.Value.CustomInviteId;
				this.Payload = other.Value.Payload;
			}
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0001E6B3 File Offset: 0x0001C8B3
		public void Set(object other)
		{
			this.Set(other as OnCustomInviteReceivedCallbackInfoInternal?);
		}
	}
}
