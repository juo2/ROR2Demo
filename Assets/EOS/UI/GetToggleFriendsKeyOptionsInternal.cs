using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000047 RID: 71
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetToggleFriendsKeyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x060003AF RID: 943 RVA: 0x00004B30 File Offset: 0x00002D30
		public void Set(GetToggleFriendsKeyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00004B3C File Offset: 0x00002D3C
		public void Set(object other)
		{
			this.Set(other as GetToggleFriendsKeyOptions);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000193 RID: 403
		private int m_ApiVersion;
	}
}
