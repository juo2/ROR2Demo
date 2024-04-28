using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002D4 RID: 724
	public class UninstallModCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x000137EC File Offset: 0x000119EC
		// (set) Token: 0x06001252 RID: 4690 RVA: 0x000137F4 File Offset: 0x000119F4
		public Result ResultCode { get; private set; }

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x000137FD File Offset: 0x000119FD
		// (set) Token: 0x06001254 RID: 4692 RVA: 0x00013805 File Offset: 0x00011A05
		public EpicAccountId LocalUserId { get; private set; }

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x0001380E File Offset: 0x00011A0E
		// (set) Token: 0x06001256 RID: 4694 RVA: 0x00013816 File Offset: 0x00011A16
		public object ClientData { get; private set; }

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0001381F File Offset: 0x00011A1F
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00013827 File Offset: 0x00011A27
		public ModIdentifier Mod { get; private set; }

		// Token: 0x06001259 RID: 4697 RVA: 0x00013830 File Offset: 0x00011A30
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00013840 File Offset: 0x00011A40
		internal void Set(UninstallModCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.LocalUserId = other.Value.LocalUserId;
				this.ClientData = other.Value.ClientData;
				this.Mod = other.Value.Mod;
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x000138AA File Offset: 0x00011AAA
		public void Set(object other)
		{
			this.Set(other as UninstallModCallbackInfoInternal?);
		}
	}
}
