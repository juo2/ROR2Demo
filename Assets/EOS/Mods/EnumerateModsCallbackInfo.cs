using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002BE RID: 702
	public class EnumerateModsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060011C5 RID: 4549 RVA: 0x00012EE1 File Offset: 0x000110E1
		// (set) Token: 0x060011C6 RID: 4550 RVA: 0x00012EE9 File Offset: 0x000110E9
		public Result ResultCode { get; private set; }

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00012EF2 File Offset: 0x000110F2
		// (set) Token: 0x060011C8 RID: 4552 RVA: 0x00012EFA File Offset: 0x000110FA
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00012F03 File Offset: 0x00011103
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x00012F0B File Offset: 0x0001110B
		public object ClientData { get; private set; }

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x00012F14 File Offset: 0x00011114
		// (set) Token: 0x060011CC RID: 4556 RVA: 0x00012F1C File Offset: 0x0001111C
		public ModEnumerationType Type { get; private set; }

		// Token: 0x060011CD RID: 4557 RVA: 0x00012F25 File Offset: 0x00011125
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00012F34 File Offset: 0x00011134
		internal void Set(EnumerateModsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.LocalUserId = other.Value.LocalUserId;
				this.ClientData = other.Value.ClientData;
				this.Type = other.Value.Type;
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00012F9E File Offset: 0x0001119E
		public void Set(object other)
		{
			this.Set(other as EnumerateModsCallbackInfoInternal?);
		}
	}
}
