using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005CB RID: 1483
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PollStatusOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B21 RID: 2849
		// (set) Token: 0x060023CA RID: 9162 RVA: 0x00025CBF File Offset: 0x00023EBF
		public uint OutMessageLength
		{
			set
			{
				this.m_OutMessageLength = value;
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x00025CC8 File Offset: 0x00023EC8
		public void Set(PollStatusOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.OutMessageLength = other.OutMessageLength;
			}
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x00025CE0 File Offset: 0x00023EE0
		public void Set(object other)
		{
			this.Set(other as PollStatusOptions);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040010E5 RID: 4325
		private int m_ApiVersion;

		// Token: 0x040010E6 RID: 4326
		private uint m_OutMessageLength;
	}
}
