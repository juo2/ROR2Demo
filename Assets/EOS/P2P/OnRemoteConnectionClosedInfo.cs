using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002A7 RID: 679
	public class OnRemoteConnectionClosedInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x0001210C File Offset: 0x0001030C
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x00012114 File Offset: 0x00010314
		public object ClientData { get; private set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0001211D File Offset: 0x0001031D
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x00012125 File Offset: 0x00010325
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0001212E File Offset: 0x0001032E
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x00012136 File Offset: 0x00010336
		public ProductUserId RemoteUserId { get; private set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0001213F File Offset: 0x0001033F
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x00012147 File Offset: 0x00010347
		public SocketId SocketId { get; private set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x00012150 File Offset: 0x00010350
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x00012158 File Offset: 0x00010358
		public ConnectionClosedReason Reason { get; private set; }

		// Token: 0x06001126 RID: 4390 RVA: 0x00012164 File Offset: 0x00010364
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0001217C File Offset: 0x0001037C
		internal void Set(OnRemoteConnectionClosedInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RemoteUserId = other.Value.RemoteUserId;
				this.SocketId = other.Value.SocketId;
				this.Reason = other.Value.Reason;
			}
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x000121FB File Offset: 0x000103FB
		public void Set(object other)
		{
			this.Set(other as OnRemoteConnectionClosedInfoInternal?);
		}
	}
}
