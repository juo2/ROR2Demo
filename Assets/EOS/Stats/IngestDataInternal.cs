using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x02000095 RID: 149
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IngestDataInternal : ISettable, IDisposable
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000654C File Offset: 0x0000474C
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x00006568 File Offset: 0x00004768
		public string StatName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_StatName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_StatName, value);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00006577 File Offset: 0x00004777
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000657F File Offset: 0x0000477F
		public int IngestAmount
		{
			get
			{
				return this.m_IngestAmount;
			}
			set
			{
				this.m_IngestAmount = value;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00006588 File Offset: 0x00004788
		public void Set(IngestData other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.StatName = other.StatName;
				this.IngestAmount = other.IngestAmount;
			}
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000065AC File Offset: 0x000047AC
		public void Set(object other)
		{
			this.Set(other as IngestData);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000065BA File Offset: 0x000047BA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_StatName);
		}

		// Token: 0x040002B9 RID: 697
		private int m_ApiVersion;

		// Token: 0x040002BA RID: 698
		private IntPtr m_StatName;

		// Token: 0x040002BB RID: 699
		private int m_IngestAmount;
	}
}
