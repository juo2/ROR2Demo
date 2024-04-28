using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x02000297 RID: 663
	public class OnIncomingConnectionRequestInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060010B2 RID: 4274 RVA: 0x00011BCF File Offset: 0x0000FDCF
		// (set) Token: 0x060010B3 RID: 4275 RVA: 0x00011BD7 File Offset: 0x0000FDD7
		public object ClientData { get; private set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060010B4 RID: 4276 RVA: 0x00011BE0 File Offset: 0x0000FDE0
		// (set) Token: 0x060010B5 RID: 4277 RVA: 0x00011BE8 File Offset: 0x0000FDE8
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00011BF1 File Offset: 0x0000FDF1
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x00011BF9 File Offset: 0x0000FDF9
		public ProductUserId RemoteUserId { get; private set; }

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00011C02 File Offset: 0x0000FE02
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x00011C0A File Offset: 0x0000FE0A
		public SocketId SocketId { get; private set; }

		// Token: 0x060010BA RID: 4282 RVA: 0x00011C14 File Offset: 0x0000FE14
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00011C2C File Offset: 0x0000FE2C
		internal void Set(OnIncomingConnectionRequestInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RemoteUserId = other.Value.RemoteUserId;
				this.SocketId = other.Value.SocketId;
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00011C96 File Offset: 0x0000FE96
		public void Set(object other)
		{
			this.Set(other as OnIncomingConnectionRequestInfoInternal?);
		}
	}
}
