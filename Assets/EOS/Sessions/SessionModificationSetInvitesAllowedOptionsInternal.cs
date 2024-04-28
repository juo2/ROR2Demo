using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000124 RID: 292
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetInvitesAllowedOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001E7 RID: 487
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x00009137 File Offset: 0x00007337
		public bool InvitesAllowed
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_InvitesAllowed, value);
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00009146 File Offset: 0x00007346
		public void Set(SessionModificationSetInvitesAllowedOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.InvitesAllowed = other.InvitesAllowed;
			}
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x0000915E File Offset: 0x0000735E
		public void Set(object other)
		{
			this.Set(other as SessionModificationSetInvitesAllowedOptions);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040003FB RID: 1019
		private int m_ApiVersion;

		// Token: 0x040003FC RID: 1020
		private int m_InvitesAllowed;
	}
}
