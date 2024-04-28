using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x0200049C RID: 1180
	public class OnCustomInviteAcceptedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x0001E42E File Offset: 0x0001C62E
		// (set) Token: 0x06001CA5 RID: 7333 RVA: 0x0001E436 File Offset: 0x0001C636
		public object ClientData { get; private set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x0001E43F File Offset: 0x0001C63F
		// (set) Token: 0x06001CA7 RID: 7335 RVA: 0x0001E447 File Offset: 0x0001C647
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x0001E450 File Offset: 0x0001C650
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x0001E458 File Offset: 0x0001C658
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0001E461 File Offset: 0x0001C661
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0001E469 File Offset: 0x0001C669
		public string CustomInviteId { get; private set; }

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0001E472 File Offset: 0x0001C672
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x0001E47A File Offset: 0x0001C67A
		public string Payload { get; private set; }

		// Token: 0x06001CAE RID: 7342 RVA: 0x0001E484 File Offset: 0x0001C684
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0001E49C File Offset: 0x0001C69C
		internal void Set(OnCustomInviteAcceptedCallbackInfoInternal? other)
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

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0001E51B File Offset: 0x0001C71B
		public void Set(object other)
		{
			this.Set(other as OnCustomInviteAcceptedCallbackInfoInternal?);
		}
	}
}
