using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003B6 RID: 950
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardRecordByUserIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006C7 RID: 1735
		// (set) Token: 0x0600170E RID: 5902 RVA: 0x00018559 File Offset: 0x00016759
		public ProductUserId UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00018568 File Offset: 0x00016768
		public void Set(CopyLeaderboardRecordByUserIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.UserId = other.UserId;
			}
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00018580 File Offset: 0x00016780
		public void Set(object other)
		{
			this.Set(other as CopyLeaderboardRecordByUserIdOptions);
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0001858E File Offset: 0x0001678E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
		}

		// Token: 0x04000ACE RID: 2766
		private int m_ApiVersion;

		// Token: 0x04000ACF RID: 2767
		private IntPtr m_UserId;
	}
}
