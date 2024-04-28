using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005BF RID: 1471
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProtectMessageOutputLengthOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B1A RID: 2842
		// (set) Token: 0x06002398 RID: 9112 RVA: 0x00025BA9 File Offset: 0x00023DA9
		public uint DataLengthBytes
		{
			set
			{
				this.m_DataLengthBytes = value;
			}
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00025BB2 File Offset: 0x00023DB2
		public void Set(GetProtectMessageOutputLengthOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DataLengthBytes = other.DataLengthBytes;
			}
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00025BCA File Offset: 0x00023DCA
		public void Set(object other)
		{
			this.Set(other as GetProtectMessageOutputLengthOptions);
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x040010DD RID: 4317
		private int m_ApiVersion;

		// Token: 0x040010DE RID: 4318
		private uint m_DataLengthBytes;
	}
}
