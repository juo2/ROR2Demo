using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200020A RID: 522
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreatePresenceModificationOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003AD RID: 941
		// (set) Token: 0x06000D9A RID: 3482 RVA: 0x0000E9F5 File Offset: 0x0000CBF5
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public void Set(CreatePresenceModificationOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public void Set(object other)
		{
			this.Set(other as CreatePresenceModificationOptions);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0000EA2A File Offset: 0x0000CC2A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000667 RID: 1639
		private int m_ApiVersion;

		// Token: 0x04000668 RID: 1640
		private IntPtr m_LocalUserId;
	}
}
