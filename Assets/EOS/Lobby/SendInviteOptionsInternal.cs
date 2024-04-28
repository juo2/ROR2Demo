using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A8 RID: 936
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006B0 RID: 1712
		// (set) Token: 0x060016D1 RID: 5841 RVA: 0x00018213 File Offset: 0x00016413
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (set) Token: 0x060016D2 RID: 5842 RVA: 0x00018222 File Offset: 0x00016422
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (set) Token: 0x060016D3 RID: 5843 RVA: 0x00018231 File Offset: 0x00016431
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00018240 File Offset: 0x00016440
		public void Set(SendInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00018270 File Offset: 0x00016470
		public void Set(object other)
		{
			this.Set(other as SendInviteOptions);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0001827E File Offset: 0x0001647E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000AB2 RID: 2738
		private int m_ApiVersion;

		// Token: 0x04000AB3 RID: 2739
		private IntPtr m_LobbyId;

		// Token: 0x04000AB4 RID: 2740
		private IntPtr m_LocalUserId;

		// Token: 0x04000AB5 RID: 2741
		private IntPtr m_TargetUserId;
	}
}
