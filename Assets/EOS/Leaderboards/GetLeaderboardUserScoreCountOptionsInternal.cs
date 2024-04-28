using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x020003C2 RID: 962
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetLeaderboardUserScoreCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170006DB RID: 1755
		// (set) Token: 0x0600174B RID: 5963 RVA: 0x0001890C File Offset: 0x00016B0C
		public string StatName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_StatName, value);
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0001891B File Offset: 0x00016B1B
		public void Set(GetLeaderboardUserScoreCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.StatName = other.StatName;
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00018933 File Offset: 0x00016B33
		public void Set(object other)
		{
			this.Set(other as GetLeaderboardUserScoreCountOptions);
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00018941 File Offset: 0x00016B41
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_StatName);
		}

		// Token: 0x04000AE8 RID: 2792
		private int m_ApiVersion;

		// Token: 0x04000AE9 RID: 2793
		private IntPtr m_StatName;
	}
}
