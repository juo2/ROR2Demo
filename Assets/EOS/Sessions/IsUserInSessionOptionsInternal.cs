using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D3 RID: 211
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IsUserInSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700016C RID: 364
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x00007B70 File Offset: 0x00005D70
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x1700016D RID: 365
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x00007B7F File Offset: 0x00005D7F
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00007B8E File Offset: 0x00005D8E
		public void Set(IsUserInSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00007BB2 File Offset: 0x00005DB2
		public void Set(object other)
		{
			this.Set(other as IsUserInSessionOptions);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000351 RID: 849
		private int m_ApiVersion;

		// Token: 0x04000352 RID: 850
		private IntPtr m_SessionName;

		// Token: 0x04000353 RID: 851
		private IntPtr m_TargetUserId;
	}
}
