using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039A RID: 922
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PromoteMemberOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000684 RID: 1668
		// (set) Token: 0x06001675 RID: 5749 RVA: 0x00017C1B File Offset: 0x00015E1B
		public string LobbyId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LobbyId, value);
			}
		}

		// Token: 0x17000685 RID: 1669
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x00017C2A File Offset: 0x00015E2A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (set) Token: 0x06001677 RID: 5751 RVA: 0x00017C39 File Offset: 0x00015E39
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00017C48 File Offset: 0x00015E48
		public void Set(PromoteMemberOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LobbyId = other.LobbyId;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00017C78 File Offset: 0x00015E78
		public void Set(object other)
		{
			this.Set(other as PromoteMemberOptions);
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00017C86 File Offset: 0x00015E86
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LobbyId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000A87 RID: 2695
		private int m_ApiVersion;

		// Token: 0x04000A88 RID: 2696
		private IntPtr m_LobbyId;

		// Token: 0x04000A89 RID: 2697
		private IntPtr m_LocalUserId;

		// Token: 0x04000A8A RID: 2698
		private IntPtr m_TargetUserId;
	}
}
