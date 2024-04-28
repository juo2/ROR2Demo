using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000327 RID: 807
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaveLobbyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005FA RID: 1530
		// (set) Token: 0x06001420 RID: 5152 RVA: 0x000155F6 File Offset: 0x000137F6
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170005FB RID: 1531
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x00015605 File Offset: 0x00013805
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00015614 File Offset: 0x00013814
		public void Set(LeaveLobbyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.LobbyId = other.LobbyId;
			}
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00015638 File Offset: 0x00013838
		public void Set(object other)
		{
			this.Set(other as LeaveLobbyOptions);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00015646 File Offset: 0x00013846
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_LobbyId);
		}

		// Token: 0x04000996 RID: 2454
		private int m_ApiVersion;

		// Token: 0x04000997 RID: 2455
		private IntPtr m_LocalUserId;

		// Token: 0x04000998 RID: 2456
		private IntPtr m_LobbyId;
	}
}
