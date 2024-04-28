using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D1 RID: 209
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteIdByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000168 RID: 360
		// (set) Token: 0x060006A9 RID: 1705 RVA: 0x00007AF6 File Offset: 0x00005CF6
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000169 RID: 361
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x00007B05 File Offset: 0x00005D05
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00007B0E File Offset: 0x00005D0E
		public void Set(GetInviteIdByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Index = other.Index;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00007B32 File Offset: 0x00005D32
		public void Set(object other)
		{
			this.Set(other as GetInviteIdByIndexOptions);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00007B40 File Offset: 0x00005D40
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400034C RID: 844
		private int m_ApiVersion;

		// Token: 0x0400034D RID: 845
		private IntPtr m_LocalUserId;

		// Token: 0x0400034E RID: 846
		private uint m_Index;
	}
}
