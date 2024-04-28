using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000145 RID: 325
	public class UnregisterPlayersCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0000A148 File Offset: 0x00008348
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x0000A150 File Offset: 0x00008350
		public Result ResultCode { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x0000A159 File Offset: 0x00008359
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x0000A161 File Offset: 0x00008361
		public object ClientData { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0000A16A File Offset: 0x0000836A
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x0000A172 File Offset: 0x00008372
		public ProductUserId[] UnregisteredPlayers { get; private set; }

		// Token: 0x0600090C RID: 2316 RVA: 0x0000A17B File Offset: 0x0000837B
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x0000A188 File Offset: 0x00008388
		internal void Set(UnregisterPlayersCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.UnregisteredPlayers = other.Value.UnregisteredPlayers;
			}
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0000A1DD File Offset: 0x000083DD
		public void Set(object other)
		{
			this.Set(other as UnregisterPlayersCallbackInfoInternal?);
		}
	}
}
