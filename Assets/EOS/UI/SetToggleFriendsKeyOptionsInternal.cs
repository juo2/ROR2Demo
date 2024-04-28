using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200005D RID: 93
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetToggleFriendsKeyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000085 RID: 133
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00004E8D File Offset: 0x0000308D
		public KeyCombination KeyCombination
		{
			set
			{
				this.m_KeyCombination = value;
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00004E96 File Offset: 0x00003096
		public void Set(SetToggleFriendsKeyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.KeyCombination = other.KeyCombination;
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00004EAE File Offset: 0x000030AE
		public void Set(object other)
		{
			this.Set(other as SetToggleFriendsKeyOptions);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000222 RID: 546
		private int m_ApiVersion;

		// Token: 0x04000223 RID: 547
		private KeyCombination m_KeyCombination;
	}
}
