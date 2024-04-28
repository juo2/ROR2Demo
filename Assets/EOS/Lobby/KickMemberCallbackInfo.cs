using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000320 RID: 800
	public class KickMemberCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00015330 File Offset: 0x00013530
		// (set) Token: 0x060013F3 RID: 5107 RVA: 0x00015338 File Offset: 0x00013538
		public Result ResultCode { get; private set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00015341 File Offset: 0x00013541
		// (set) Token: 0x060013F5 RID: 5109 RVA: 0x00015349 File Offset: 0x00013549
		public object ClientData { get; private set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00015352 File Offset: 0x00013552
		// (set) Token: 0x060013F7 RID: 5111 RVA: 0x0001535A File Offset: 0x0001355A
		public string LobbyId { get; private set; }

		// Token: 0x060013F8 RID: 5112 RVA: 0x00015363 File Offset: 0x00013563
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x00015370 File Offset: 0x00013570
		internal void Set(KickMemberCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000153C5 File Offset: 0x000135C5
		public void Set(object other)
		{
			this.Set(other as KickMemberCallbackInfoInternal?);
		}
	}
}
