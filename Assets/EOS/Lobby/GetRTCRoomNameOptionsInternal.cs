using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000317 RID: 791
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetRTCRoomNameOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005C8 RID: 1480
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x00014F5C File Offset: 0x0001315C
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x00014F6B File Offset: 0x0001316B
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00014F7A File Offset: 0x0001317A
		public void Set(GetRTCRoomNameOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00014F9E File Offset: 0x0001319E
		public void Set(object other)
		{
			this.Set(other as GetRTCRoomNameOptions);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00014FAC File Offset: 0x000131AC
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000964 RID: 2404
		private int m_ApiVersion;

		// Token: 0x04000965 RID: 2405
		private IntPtr m_LobbyId;

		// Token: 0x04000966 RID: 2406
		private IntPtr m_LocalUserId;
	}
}
