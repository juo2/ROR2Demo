using System;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200021B RID: 539
	public class PresenceChangedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0000F188 File Offset: 0x0000D388
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x0000F190 File Offset: 0x0000D390
		public object ClientData { get; private set; }

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0000F199 File Offset: 0x0000D399
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0000F1A1 File Offset: 0x0000D3A1
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0000F1AA File Offset: 0x0000D3AA
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0000F1B2 File Offset: 0x0000D3B2
		public EpicAccountId PresenceUserId { get; private set; }

		// Token: 0x06000E18 RID: 3608 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		internal void Set(PresenceChangedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PresenceUserId = other.Value.PresenceUserId;
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0000F229 File Offset: 0x0000D429
		public void Set(object other)
		{
			this.Set(other as PresenceChangedCallbackInfoInternal?);
		}
	}
}
