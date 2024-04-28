using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000099 RID: 153
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestStatOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000107 RID: 263
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x00006733 File Offset: 0x00004933
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000108 RID: 264
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00006742 File Offset: 0x00004942
		public IngestData[] Stats
		{
			set
			{
				Helper.TryMarshalSet<IngestDataInternal, IngestData>(ref this.m_Stats, value, out this.m_StatsCount);
			}
		}

		// Token: 0x17000109 RID: 265
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x00006757 File Offset: 0x00004957
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00006766 File Offset: 0x00004966
		public void Set(IngestStatOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.LocalUserId;
				this.Stats = other.Stats;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00006796 File Offset: 0x00004996
		public void Set(object other)
		{
			this.Set(other as IngestStatOptions);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000067A4 File Offset: 0x000049A4
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Stats);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x040002C7 RID: 711
		private int m_ApiVersion;

		// Token: 0x040002C8 RID: 712
		private IntPtr m_LocalUserId;

		// Token: 0x040002C9 RID: 713
		private IntPtr m_Stats;

		// Token: 0x040002CA RID: 714
		private uint m_StatsCount;

		// Token: 0x040002CB RID: 715
		private IntPtr m_TargetUserId;
	}
}
