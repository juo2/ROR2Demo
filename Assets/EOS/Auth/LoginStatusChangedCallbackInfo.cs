using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000528 RID: 1320
	public class LoginStatusChangedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001FEC RID: 8172 RVA: 0x00021CBA File Offset: 0x0001FEBA
		// (set) Token: 0x06001FED RID: 8173 RVA: 0x00021CC2 File Offset: 0x0001FEC2
		public object ClientData { get; private set; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06001FEE RID: 8174 RVA: 0x00021CCB File Offset: 0x0001FECB
		// (set) Token: 0x06001FEF RID: 8175 RVA: 0x00021CD3 File Offset: 0x0001FED3
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001FF0 RID: 8176 RVA: 0x00021CDC File Offset: 0x0001FEDC
		// (set) Token: 0x06001FF1 RID: 8177 RVA: 0x00021CE4 File Offset: 0x0001FEE4
		public LoginStatus PrevStatus { get; private set; }

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001FF2 RID: 8178 RVA: 0x00021CED File Offset: 0x0001FEED
		// (set) Token: 0x06001FF3 RID: 8179 RVA: 0x00021CF5 File Offset: 0x0001FEF5
		public LoginStatus CurrentStatus { get; private set; }

		// Token: 0x06001FF4 RID: 8180 RVA: 0x00021D00 File Offset: 0x0001FF00
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x00021D18 File Offset: 0x0001FF18
		internal void Set(LoginStatusChangedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PrevStatus = other.Value.PrevStatus;
				this.CurrentStatus = other.Value.CurrentStatus;
			}
		}

		// Token: 0x06001FF6 RID: 8182 RVA: 0x00021D82 File Offset: 0x0001FF82
		public void Set(object other)
		{
			this.Set(other as LoginStatusChangedCallbackInfoInternal?);
		}
	}
}
