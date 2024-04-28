using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C1 RID: 193
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateSessionModificationOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700014B RID: 331
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x000076D3 File Offset: 0x000058D3
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x1700014C RID: 332
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x000076E2 File Offset: 0x000058E2
		public string BucketId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_BucketId, value);
			}
		}

		// Token: 0x1700014D RID: 333
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x000076F1 File Offset: 0x000058F1
		public uint MaxPlayers
		{
			set
			{
				this.m_MaxPlayers = value;
			}
		}

		// Token: 0x1700014E RID: 334
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x000076FA File Offset: 0x000058FA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700014F RID: 335
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x00007709 File Offset: 0x00005909
		public bool PresenceEnabled
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceEnabled, value);
			}
		}

		// Token: 0x17000150 RID: 336
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00007718 File Offset: 0x00005918
		public string SessionId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionId, value);
			}
		}

		// Token: 0x17000151 RID: 337
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x00007727 File Offset: 0x00005927
		public bool SanctionsEnabled
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SanctionsEnabled, value);
			}
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00007738 File Offset: 0x00005938
		public void Set(CreateSessionModificationOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 4;
				this.SessionName = other.SessionName;
				this.BucketId = other.BucketId;
				this.MaxPlayers = other.MaxPlayers;
				this.LocalUserId = other.LocalUserId;
				this.PresenceEnabled = other.PresenceEnabled;
				this.SessionId = other.SessionId;
				this.SanctionsEnabled = other.SanctionsEnabled;
			}
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x000077A3 File Offset: 0x000059A3
		public void Set(object other)
		{
			this.Set(other as CreateSessionModificationOptions);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x000077B1 File Offset: 0x000059B1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
			Helper.TryMarshalDispose(ref this.m_BucketId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_SessionId);
		}

		// Token: 0x0400032B RID: 811
		private int m_ApiVersion;

		// Token: 0x0400032C RID: 812
		private IntPtr m_SessionName;

		// Token: 0x0400032D RID: 813
		private IntPtr m_BucketId;

		// Token: 0x0400032E RID: 814
		private uint m_MaxPlayers;

		// Token: 0x0400032F RID: 815
		private IntPtr m_LocalUserId;

		// Token: 0x04000330 RID: 816
		private int m_PresenceEnabled;

		// Token: 0x04000331 RID: 817
		private IntPtr m_SessionId;

		// Token: 0x04000332 RID: 818
		private int m_SanctionsEnabled;
	}
}
