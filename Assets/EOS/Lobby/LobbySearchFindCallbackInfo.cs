using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200035F RID: 863
	public class LobbySearchFindCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x0001758C File Offset: 0x0001578C
		// (set) Token: 0x06001571 RID: 5489 RVA: 0x00017594 File Offset: 0x00015794
		public Result ResultCode { get; private set; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0001759D File Offset: 0x0001579D
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x000175A5 File Offset: 0x000157A5
		public object ClientData { get; private set; }

		// Token: 0x06001574 RID: 5492 RVA: 0x000175AE File Offset: 0x000157AE
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000175BC File Offset: 0x000157BC
		internal void Set(LobbySearchFindCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000175FC File Offset: 0x000157FC
		public void Set(object other)
		{
			this.Set(other as LobbySearchFindCallbackInfoInternal?);
		}
	}
}
