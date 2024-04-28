using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000224 RID: 548
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetDataOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003E1 RID: 993
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0000F7E6 File Offset: 0x0000D9E6
		public DataRecord[] Records
		{
			set
			{
				Helper.TryMarshalSet<DataRecordInternal, DataRecord>(ref this.m_Records, value, out this.m_RecordsCount);
			}
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0000F7FB File Offset: 0x0000D9FB
		public void Set(PresenceModificationSetDataOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Records = other.Records;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0000F813 File Offset: 0x0000DA13
		public void Set(object other)
		{
			this.Set(other as PresenceModificationSetDataOptions);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x0000F821 File Offset: 0x0000DA21
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Records);
		}

		// Token: 0x040006BB RID: 1723
		private int m_ApiVersion;

		// Token: 0x040006BC RID: 1724
		private int m_RecordsCount;

		// Token: 0x040006BD RID: 1725
		private IntPtr m_Records;
	}
}
