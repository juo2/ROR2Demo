using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A1 RID: 161
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryStatsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000118 RID: 280
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x00006955 File Offset: 0x00004B55
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000119 RID: 281
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00006964 File Offset: 0x00004B64
		public DateTimeOffset? StartTime
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_StartTime, value);
			}
		}

		// Token: 0x1700011A RID: 282
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x00006973 File Offset: 0x00004B73
		public DateTimeOffset? EndTime
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EndTime, value);
			}
		}

		// Token: 0x1700011B RID: 283
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00006982 File Offset: 0x00004B82
		public string[] StatNames
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_StatNames, value, out this.m_StatNamesCount, true);
			}
		}

		// Token: 0x1700011C RID: 284
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x00006998 File Offset: 0x00004B98
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000069A8 File Offset: 0x00004BA8
		public void Set(QueryStatsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.LocalUserId;
				this.StartTime = other.StartTime;
				this.EndTime = other.EndTime;
				this.StatNames = other.StatNames;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000069FB File Offset: 0x00004BFB
		public void Set(object other)
		{
			this.Set(other as QueryStatsOptions);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00006A09 File Offset: 0x00004C09
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_StatNames);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x040002D9 RID: 729
		private int m_ApiVersion;

		// Token: 0x040002DA RID: 730
		private IntPtr m_LocalUserId;

		// Token: 0x040002DB RID: 731
		private long m_StartTime;

		// Token: 0x040002DC RID: 732
		private long m_EndTime;

		// Token: 0x040002DD RID: 733
		private IntPtr m_StatNames;

		// Token: 0x040002DE RID: 734
		private uint m_StatNamesCount;

		// Token: 0x040002DF RID: 735
		private IntPtr m_TargetUserId;
	}
}
