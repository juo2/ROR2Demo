using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000348 RID: 840
	public class LobbyMemberUpdateReceivedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00016E10 File Offset: 0x00015010
		// (set) Token: 0x06001507 RID: 5383 RVA: 0x00016E18 File Offset: 0x00015018
		public object ClientData { get; private set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x00016E21 File Offset: 0x00015021
		// (set) Token: 0x06001509 RID: 5385 RVA: 0x00016E29 File Offset: 0x00015029
		public string LobbyId { get; private set; }

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00016E32 File Offset: 0x00015032
		// (set) Token: 0x0600150B RID: 5387 RVA: 0x00016E3A File Offset: 0x0001503A
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x0600150C RID: 5388 RVA: 0x00016E44 File Offset: 0x00015044
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00016E5C File Offset: 0x0001505C
		internal void Set(LobbyMemberUpdateReceivedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LobbyId = other.Value.LobbyId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00016EB1 File Offset: 0x000150B1
		public void Set(object other)
		{
			this.Set(other as LobbyMemberUpdateReceivedCallbackInfoInternal?);
		}
	}
}
