using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A3 RID: 675
	public class OnQueryNATTypeCompleteInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001106 RID: 4358 RVA: 0x00012030 File Offset: 0x00010230
		// (set) Token: 0x06001107 RID: 4359 RVA: 0x00012038 File Offset: 0x00010238
		public Result ResultCode { get; private set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x00012041 File Offset: 0x00010241
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x00012049 File Offset: 0x00010249
		public object ClientData { get; private set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600110A RID: 4362 RVA: 0x00012052 File Offset: 0x00010252
		// (set) Token: 0x0600110B RID: 4363 RVA: 0x0001205A File Offset: 0x0001025A
		public NATType NATType { get; private set; }

		// Token: 0x0600110C RID: 4364 RVA: 0x00012063 File Offset: 0x00010263
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00012070 File Offset: 0x00010270
		internal void Set(OnQueryNATTypeCompleteInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.NATType = other.Value.NATType;
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x000120C5 File Offset: 0x000102C5
		public void Set(object other)
		{
			this.Set(other as OnQueryNATTypeCompleteInfoInternal?);
		}
	}
}
