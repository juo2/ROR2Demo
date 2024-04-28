using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000210 RID: 528
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HasPresenceOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003B8 RID: 952
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0000EC01 File Offset: 0x0000CE01
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0000EC10 File Offset: 0x0000CE10
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0000EC1F File Offset: 0x0000CE1F
		public void Set(HasPresenceOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0000EC43 File Offset: 0x0000CE43
		public void Set(object other)
		{
			this.Set(other as HasPresenceOptions);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0000EC51 File Offset: 0x0000CE51
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000675 RID: 1653
		private int m_ApiVersion;

		// Token: 0x04000676 RID: 1654
		private IntPtr m_LocalUserId;

		// Token: 0x04000677 RID: 1655
		private IntPtr m_TargetUserId;
	}
}
