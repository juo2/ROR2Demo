using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000FA RID: 250
	public class RegisterPlayersCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00007FBC File Offset: 0x000061BC
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x00007FC4 File Offset: 0x000061C4
		public Result ResultCode { get; private set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00007FCD File Offset: 0x000061CD
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x00007FD5 File Offset: 0x000061D5
		public object ClientData { get; private set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00007FDE File Offset: 0x000061DE
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x00007FE6 File Offset: 0x000061E6
		public ProductUserId[] RegisteredPlayers { get; private set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00007FEF File Offset: 0x000061EF
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x00007FF7 File Offset: 0x000061F7
		public ProductUserId[] SanctionedPlayers { get; private set; }

		// Token: 0x06000766 RID: 1894 RVA: 0x00008000 File Offset: 0x00006200
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00008010 File Offset: 0x00006210
		internal void Set(RegisterPlayersCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.RegisteredPlayers = other.Value.RegisteredPlayers;
				this.SanctionedPlayers = other.Value.SanctionedPlayers;
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000807A File Offset: 0x0000627A
		public void Set(object other)
		{
			this.Set(other as RegisterPlayersCallbackInfoInternal?);
		}
	}
}
