using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000208 RID: 520
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPresenceOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003AA RID: 938
		// (set) Token: 0x06000D92 RID: 3474 RVA: 0x0000E97A File Offset: 0x0000CB7A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170003AB RID: 939
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x0000E989 File Offset: 0x0000CB89
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0000E998 File Offset: 0x0000CB98
		public void Set(CopyPresenceOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		public void Set(object other)
		{
			this.Set(other as CopyPresenceOptions);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x0000E9CA File Offset: 0x0000CBCA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000663 RID: 1635
		private int m_ApiVersion;

		// Token: 0x04000664 RID: 1636
		private IntPtr m_LocalUserId;

		// Token: 0x04000665 RID: 1637
		private IntPtr m_TargetUserId;
	}
}
