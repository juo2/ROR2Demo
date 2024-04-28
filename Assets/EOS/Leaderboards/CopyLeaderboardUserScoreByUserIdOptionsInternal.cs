using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003BA RID: 954
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLeaderboardUserScoreByUserIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006CE RID: 1742
		// (set) Token: 0x06001721 RID: 5921 RVA: 0x00018638 File Offset: 0x00016838
		public ProductUserId UserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x170006CF RID: 1743
		// (set) Token: 0x06001722 RID: 5922 RVA: 0x00018647 File Offset: 0x00016847
		public string StatName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_StatName, value);
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x00018656 File Offset: 0x00016856
		public void Set(CopyLeaderboardUserScoreByUserIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
				this.StatName = other.StatName;
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x0001867A File Offset: 0x0001687A
		public void Set(object other)
		{
			this.Set(other as CopyLeaderboardUserScoreByUserIdOptions);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00018688 File Offset: 0x00016888
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
			Helper.TryMarshalDispose(ref this.m_StatName);
		}

		// Token: 0x04000AD7 RID: 2775
		private int m_ApiVersion;

		// Token: 0x04000AD8 RID: 2776
		private IntPtr m_UserId;

		// Token: 0x04000AD9 RID: 2777
		private IntPtr m_StatName;
	}
}
