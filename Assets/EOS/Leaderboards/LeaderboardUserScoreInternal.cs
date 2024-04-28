using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003C7 RID: 967
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardUserScoreInternal : ISettable, IDisposable
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x00018B6C File Offset: 0x00016D6C
		// (set) Token: 0x0600176D RID: 5997 RVA: 0x00018B88 File Offset: 0x00016D88
		public ProductUserId UserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_UserId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x00018B97 File Offset: 0x00016D97
		// (set) Token: 0x0600176F RID: 5999 RVA: 0x00018B9F File Offset: 0x00016D9F
		public int Score
		{
			get
			{
				return this.m_Score;
			}
			set
			{
				this.m_Score = value;
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00018BA8 File Offset: 0x00016DA8
		public void Set(LeaderboardUserScore other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.UserId;
				this.Score = other.Score;
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00018BCC File Offset: 0x00016DCC
		public void Set(object other)
		{
			this.Set(other as LeaderboardUserScore);
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00018BDA File Offset: 0x00016DDA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
		}

		// Token: 0x04000AFA RID: 2810
		private int m_ApiVersion;

		// Token: 0x04000AFB RID: 2811
		private IntPtr m_UserId;

		// Token: 0x04000AFC RID: 2812
		private int m_Score;
	}
}
