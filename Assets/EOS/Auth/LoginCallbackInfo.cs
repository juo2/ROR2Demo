using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000523 RID: 1315
	public class LoginCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x00021A42 File Offset: 0x0001FC42
		// (set) Token: 0x06001FC9 RID: 8137 RVA: 0x00021A4A File Offset: 0x0001FC4A
		public Result ResultCode { get; private set; }

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x00021A53 File Offset: 0x0001FC53
		// (set) Token: 0x06001FCB RID: 8139 RVA: 0x00021A5B File Offset: 0x0001FC5B
		public object ClientData { get; private set; }

		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x00021A64 File Offset: 0x0001FC64
		// (set) Token: 0x06001FCD RID: 8141 RVA: 0x00021A6C File Offset: 0x0001FC6C
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x00021A75 File Offset: 0x0001FC75
		// (set) Token: 0x06001FCF RID: 8143 RVA: 0x00021A7D File Offset: 0x0001FC7D
		public PinGrantInfo PinGrantInfo { get; private set; }

		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x00021A86 File Offset: 0x0001FC86
		// (set) Token: 0x06001FD1 RID: 8145 RVA: 0x00021A8E File Offset: 0x0001FC8E
		public ContinuanceToken ContinuanceToken { get; private set; }

		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001FD2 RID: 8146 RVA: 0x00021A97 File Offset: 0x0001FC97
		// (set) Token: 0x06001FD3 RID: 8147 RVA: 0x00021A9F File Offset: 0x0001FC9F
		public AccountFeatureRestrictedInfo AccountFeatureRestrictedInfo { get; private set; }

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x00021AA8 File Offset: 0x0001FCA8
		// (set) Token: 0x06001FD5 RID: 8149 RVA: 0x00021AB0 File Offset: 0x0001FCB0
		public EpicAccountId SelectedAccountId { get; private set; }

		// Token: 0x06001FD6 RID: 8150 RVA: 0x00021AB9 File Offset: 0x0001FCB9
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x00021AC8 File Offset: 0x0001FCC8
		internal void Set(LoginCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PinGrantInfo = other.Value.PinGrantInfo;
				this.ContinuanceToken = other.Value.ContinuanceToken;
				this.AccountFeatureRestrictedInfo = other.Value.AccountFeatureRestrictedInfo;
				this.SelectedAccountId = other.Value.SelectedAccountId;
			}
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x00021B74 File Offset: 0x0001FD74
		public void Set(object other)
		{
			this.Set(other as LoginCallbackInfoInternal?);
		}
	}
}
