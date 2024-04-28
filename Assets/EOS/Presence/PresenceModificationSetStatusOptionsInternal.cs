using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200022A RID: 554
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetStatusOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003E7 RID: 999
		// (set) Token: 0x06000E61 RID: 3681 RVA: 0x0000F8E8 File Offset: 0x0000DAE8
		public Status Status
		{
			set
			{
				this.m_Status = value;
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0000F8F1 File Offset: 0x0000DAF1
		public void Set(PresenceModificationSetStatusOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Status = other.Status;
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0000F909 File Offset: 0x0000DB09
		public void Set(object other)
		{
			this.Set(other as PresenceModificationSetStatusOptions);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040006C5 RID: 1733
		private int m_ApiVersion;

		// Token: 0x040006C6 RID: 1734
		private Status m_Status;
	}
}
