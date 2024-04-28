using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D9 RID: 217
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct JoinSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700017E RID: 382
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x00007DCC File Offset: 0x00005FCC
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x1700017F RID: 383
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00007DDB File Offset: 0x00005FDB
		public SessionDetails SessionHandle
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionHandle, value);
			}
		}

		// Token: 0x17000180 RID: 384
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x00007DEA File Offset: 0x00005FEA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000181 RID: 385
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00007DF9 File Offset: 0x00005FF9
		public bool PresenceEnabled
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceEnabled, value);
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00007E08 File Offset: 0x00006008
		public void Set(JoinSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.SessionName = other.SessionName;
				this.SessionHandle = other.SessionHandle;
				this.LocalUserId = other.LocalUserId;
				this.PresenceEnabled = other.PresenceEnabled;
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00007E44 File Offset: 0x00006044
		public void Set(object other)
		{
			this.Set(other as JoinSessionOptions);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00007E52 File Offset: 0x00006052
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
			Helper.TryMarshalDispose(ref this.m_SessionHandle);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000362 RID: 866
		private int m_ApiVersion;

		// Token: 0x04000363 RID: 867
		private IntPtr m_SessionName;

		// Token: 0x04000364 RID: 868
		private IntPtr m_SessionHandle;

		// Token: 0x04000365 RID: 869
		private IntPtr m_LocalUserId;

		// Token: 0x04000366 RID: 870
		private int m_PresenceEnabled;
	}
}
