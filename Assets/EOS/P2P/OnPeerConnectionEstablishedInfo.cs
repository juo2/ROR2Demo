using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200029F RID: 671
	public class OnPeerConnectionEstablishedInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x00011EAC File Offset: 0x000100AC
		// (set) Token: 0x060010EB RID: 4331 RVA: 0x00011EB4 File Offset: 0x000100B4
		public object ClientData { get; private set; }

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060010EC RID: 4332 RVA: 0x00011EBD File Offset: 0x000100BD
		// (set) Token: 0x060010ED RID: 4333 RVA: 0x00011EC5 File Offset: 0x000100C5
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x00011ECE File Offset: 0x000100CE
		// (set) Token: 0x060010EF RID: 4335 RVA: 0x00011ED6 File Offset: 0x000100D6
		public ProductUserId RemoteUserId { get; private set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x00011EDF File Offset: 0x000100DF
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x00011EE7 File Offset: 0x000100E7
		public SocketId SocketId { get; private set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x00011EF0 File Offset: 0x000100F0
		// (set) Token: 0x060010F3 RID: 4339 RVA: 0x00011EF8 File Offset: 0x000100F8
		public ConnectionEstablishedType ConnectionType { get; private set; }

		// Token: 0x060010F4 RID: 4340 RVA: 0x00011F04 File Offset: 0x00010104
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00011F1C File Offset: 0x0001011C
		internal void Set(OnPeerConnectionEstablishedInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RemoteUserId = other.Value.RemoteUserId;
				this.SocketId = other.Value.SocketId;
				this.ConnectionType = other.Value.ConnectionType;
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00011F9B File Offset: 0x0001019B
		public void Set(object other)
		{
			this.Set(other as OnPeerConnectionEstablishedInfoInternal?);
		}
	}
}
