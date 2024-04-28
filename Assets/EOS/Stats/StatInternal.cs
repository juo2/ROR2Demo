using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A3 RID: 163
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct StatInternal : ISettable, IDisposable
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00006AF4 File Offset: 0x00004CF4
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x00006B10 File Offset: 0x00004D10
		public string Name
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Name, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Name, value);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x00006B20 File Offset: 0x00004D20
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x00006B3C File Offset: 0x00004D3C
		public DateTimeOffset? StartTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.TryMarshalGet(this.m_StartTime, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_StartTime, value);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x00006B4C File Offset: 0x00004D4C
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x00006B68 File Offset: 0x00004D68
		public DateTimeOffset? EndTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.TryMarshalGet(this.m_EndTime, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_EndTime, value);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x00006B77 File Offset: 0x00004D77
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x00006B7F File Offset: 0x00004D7F
		public int Value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00006B88 File Offset: 0x00004D88
		public void Set(Stat other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Name = other.Name;
				this.StartTime = other.StartTime;
				this.EndTime = other.EndTime;
				this.Value = other.Value;
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00006BC4 File Offset: 0x00004DC4
		public void Set(object other)
		{
			this.Set(other as Stat);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00006BD2 File Offset: 0x00004DD2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Name);
		}

		// Token: 0x040002E4 RID: 740
		private int m_ApiVersion;

		// Token: 0x040002E5 RID: 741
		private IntPtr m_Name;

		// Token: 0x040002E6 RID: 742
		private long m_StartTime;

		// Token: 0x040002E7 RID: 743
		private long m_EndTime;

		// Token: 0x040002E8 RID: 744
		private int m_Value;
	}
}
