using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000234 RID: 564
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPresenceOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003FE RID: 1022
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x0000FBEA File Offset: 0x0000DDEA
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x0000FBF9 File Offset: 0x0000DDF9
		public PresenceModification PresenceModificationHandle
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_PresenceModificationHandle, value);
			}
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0000FC08 File Offset: 0x0000DE08
		public void Set(SetPresenceOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.PresenceModificationHandle = other.PresenceModificationHandle;
			}
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public void Set(object other)
		{
			this.Set(other as SetPresenceOptions);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0000FC3A File Offset: 0x0000DE3A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_PresenceModificationHandle);
		}

		// Token: 0x040006DC RID: 1756
		private int m_ApiVersion;

		// Token: 0x040006DD RID: 1757
		private IntPtr m_LocalUserId;

		// Token: 0x040006DE RID: 1758
		private IntPtr m_PresenceModificationHandle;
	}
}
