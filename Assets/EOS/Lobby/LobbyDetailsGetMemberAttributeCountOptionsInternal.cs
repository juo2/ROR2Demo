using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000339 RID: 825
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberAttributeCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000609 RID: 1545
		// (set) Token: 0x06001463 RID: 5219 RVA: 0x00015AD6 File Offset: 0x00013CD6
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00015AE5 File Offset: 0x00013CE5
		public void Set(LobbyDetailsGetMemberAttributeCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00015AFD File Offset: 0x00013CFD
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsGetMemberAttributeCountOptions);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00015B0B File Offset: 0x00013D0B
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x040009BB RID: 2491
		private int m_ApiVersion;

		// Token: 0x040009BC RID: 2492
		private IntPtr m_TargetUserId;
	}
}
