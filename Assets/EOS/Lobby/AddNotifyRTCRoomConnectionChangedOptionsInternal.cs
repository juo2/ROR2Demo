using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020002FB RID: 763
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyRTCRoomConnectionChangedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700057D RID: 1405
		// (set) Token: 0x060012FD RID: 4861 RVA: 0x000142F1 File Offset: 0x000124F1
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x1700057E RID: 1406
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x00014300 File Offset: 0x00012500
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0001430F File Offset: 0x0001250F
		public void Set(AddNotifyRTCRoomConnectionChangedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00014333 File Offset: 0x00012533
		public void Set(object other)
		{
			this.Set(other as AddNotifyRTCRoomConnectionChangedOptions);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00014341 File Offset: 0x00012541
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400090F RID: 2319
		private int m_ApiVersion;

		// Token: 0x04000910 RID: 2320
		private IntPtr m_LobbyId;

		// Token: 0x04000911 RID: 2321
		private IntPtr m_LocalUserId;
	}
}
