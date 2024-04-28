using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000222 RID: 546
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationDeleteDataOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003DF RID: 991
		// (set) Token: 0x06000E45 RID: 3653 RVA: 0x0000F78C File Offset: 0x0000D98C
		public PresenceModificationDataRecordId[] Records
		{
			set
			{
				Helper.TryMarshalSet<PresenceModificationDataRecordIdInternal, PresenceModificationDataRecordId>(ref this.m_Records, value, out this.m_RecordsCount);
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0000F7A1 File Offset: 0x0000D9A1
		public void Set(PresenceModificationDeleteDataOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Records = other.Records;
			}
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x0000F7B9 File Offset: 0x0000D9B9
		public void Set(object other)
		{
			this.Set(other as PresenceModificationDeleteDataOptions);
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0000F7C7 File Offset: 0x0000D9C7
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Records);
		}

		// Token: 0x040006B7 RID: 1719
		private int m_ApiVersion;

		// Token: 0x040006B8 RID: 1720
		private int m_RecordsCount;

		// Token: 0x040006B9 RID: 1721
		private IntPtr m_Records;
	}
}
