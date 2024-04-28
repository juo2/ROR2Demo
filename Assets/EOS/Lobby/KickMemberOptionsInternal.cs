using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000323 RID: 803
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct KickMemberOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005EE RID: 1518
		// (set) Token: 0x06001407 RID: 5127 RVA: 0x00015453 File Offset: 0x00013653
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x170005EF RID: 1519
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x00015462 File Offset: 0x00013662
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (set) Token: 0x06001409 RID: 5129 RVA: 0x00015471 File Offset: 0x00013671
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00015480 File Offset: 0x00013680
		public void Set(KickMemberOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x0600140B RID: 5131 RVA: 0x000154B0 File Offset: 0x000136B0
		public void Set(object other)
		{
			this.Set(other as KickMemberOptions);
		}

		// Token: 0x0600140C RID: 5132 RVA: 0x000154BE File Offset: 0x000136BE
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400098A RID: 2442
		private int m_ApiVersion;

		// Token: 0x0400098B RID: 2443
		private IntPtr m_LobbyId;

		// Token: 0x0400098C RID: 2444
		private IntPtr m_LocalUserId;

		// Token: 0x0400098D RID: 2445
		private IntPtr m_TargetUserId;
	}
}
