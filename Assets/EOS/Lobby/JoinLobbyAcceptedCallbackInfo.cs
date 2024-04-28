using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200031A RID: 794
	public class JoinLobbyAcceptedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x00015052 File Offset: 0x00013252
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x0001505A File Offset: 0x0001325A
		public object ClientData { get; private set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00015063 File Offset: 0x00013263
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x0001506B File Offset: 0x0001326B
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00015074 File Offset: 0x00013274
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0001507C File Offset: 0x0001327C
		public ulong UiEventId { get; private set; }

		// Token: 0x060013CC RID: 5068 RVA: 0x00015088 File Offset: 0x00013288
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000150A0 File Offset: 0x000132A0
		internal void Set(JoinLobbyAcceptedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.UiEventId = other.Value.UiEventId;
			}
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x000150F5 File Offset: 0x000132F5
		public void Set(object other)
		{
			this.Set(other as JoinLobbyAcceptedCallbackInfoInternal?);
		}
	}
}
