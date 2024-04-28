using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000319 RID: 793
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IsRTCRoomConnectedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005CC RID: 1484
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x00014FE8 File Offset: 0x000131E8
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x170005CD RID: 1485
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x00014FF7 File Offset: 0x000131F7
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x00015006 File Offset: 0x00013206
		public void Set(IsRTCRoomConnectedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0001502A File Offset: 0x0001322A
		public void Set(object other)
		{
			this.Set(other as IsRTCRoomConnectedOptions);
		}

		// Token: 0x060013C5 RID: 5061 RVA: 0x00015038 File Offset: 0x00013238
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000969 RID: 2409
		private int m_ApiVersion;

		// Token: 0x0400096A RID: 2410
		private IntPtr m_LobbyId;

		// Token: 0x0400096B RID: 2411
		private IntPtr m_LocalUserId;
	}
}
