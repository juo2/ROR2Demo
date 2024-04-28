using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003C5 RID: 965
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LeaderboardRecordInternal : ISettable, IDisposable
	{
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x00018A14 File Offset: 0x00016C14
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x00018A30 File Offset: 0x00016C30
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

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x00018A3F File Offset: 0x00016C3F
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x00018A47 File Offset: 0x00016C47
		public uint Rank
		{
			get
			{
				return this.m_Rank;
			}
			set
			{
				this.m_Rank = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x0600175E RID: 5982 RVA: 0x00018A50 File Offset: 0x00016C50
		// (set) Token: 0x0600175F RID: 5983 RVA: 0x00018A58 File Offset: 0x00016C58
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

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x00018A64 File Offset: 0x00016C64
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x00018A80 File Offset: 0x00016C80
		public string UserDisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_UserDisplayName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserDisplayName, value);
			}
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x00018A8F File Offset: 0x00016C8F
		public void Set(LeaderboardRecord other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.UserId = other.UserId;
				this.Rank = other.Rank;
				this.Score = other.Score;
				this.UserDisplayName = other.UserDisplayName;
			}
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x00018ACB File Offset: 0x00016CCB
		public void Set(object other)
		{
			this.Set(other as LeaderboardRecord);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x00018AD9 File Offset: 0x00016CD9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
			Helper.TryMarshalDispose(ref this.m_UserDisplayName);
		}

		// Token: 0x04000AF3 RID: 2803
		private int m_ApiVersion;

		// Token: 0x04000AF4 RID: 2804
		private IntPtr m_UserId;

		// Token: 0x04000AF5 RID: 2805
		private uint m_Rank;

		// Token: 0x04000AF6 RID: 2806
		private int m_Score;

		// Token: 0x04000AF7 RID: 2807
		private IntPtr m_UserDisplayName;
	}
}
