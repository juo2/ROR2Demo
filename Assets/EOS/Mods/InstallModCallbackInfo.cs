using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002C2 RID: 706
	public class InstallModCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0001307E File Offset: 0x0001127E
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x00013086 File Offset: 0x00011286
		public Result ResultCode { get; private set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x0001308F File Offset: 0x0001128F
		// (set) Token: 0x060011E3 RID: 4579 RVA: 0x00013097 File Offset: 0x00011297
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x000130A0 File Offset: 0x000112A0
		// (set) Token: 0x060011E5 RID: 4581 RVA: 0x000130A8 File Offset: 0x000112A8
		public object ClientData { get; private set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060011E6 RID: 4582 RVA: 0x000130B1 File Offset: 0x000112B1
		// (set) Token: 0x060011E7 RID: 4583 RVA: 0x000130B9 File Offset: 0x000112B9
		public ModIdentifier Mod { get; private set; }

		// Token: 0x060011E8 RID: 4584 RVA: 0x000130C2 File Offset: 0x000112C2
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000130D0 File Offset: 0x000112D0
		internal void Set(InstallModCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.LocalUserId = other.Value.LocalUserId;
				this.ClientData = other.Value.ClientData;
				this.Mod = other.Value.Mod;
			}
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0001313A File Offset: 0x0001133A
		public void Set(object other)
		{
			this.Set(other as InstallModCallbackInfoInternal?);
		}
	}
}
