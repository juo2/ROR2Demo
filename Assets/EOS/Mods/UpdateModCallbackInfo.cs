using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002D8 RID: 728
	public class UpdateModCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600126C RID: 4716 RVA: 0x000139B0 File Offset: 0x00011BB0
		// (set) Token: 0x0600126D RID: 4717 RVA: 0x000139B8 File Offset: 0x00011BB8
		public Result ResultCode { get; private set; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x000139C1 File Offset: 0x00011BC1
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x000139C9 File Offset: 0x00011BC9
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x000139D2 File Offset: 0x00011BD2
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x000139DA File Offset: 0x00011BDA
		public object ClientData { get; private set; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x000139E3 File Offset: 0x00011BE3
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x000139EB File Offset: 0x00011BEB
		public ModIdentifier Mod { get; private set; }

		// Token: 0x06001274 RID: 4724 RVA: 0x000139F4 File Offset: 0x00011BF4
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00013A04 File Offset: 0x00011C04
		internal void Set(UpdateModCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.LocalUserId = other.Value.LocalUserId;
				this.ClientData = other.Value.ClientData;
				this.Mod = other.Value.Mod;
			}
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00013A6E File Offset: 0x00011C6E
		public void Set(object other)
		{
			this.Set(other as UpdateModCallbackInfoInternal?);
		}
	}
}
