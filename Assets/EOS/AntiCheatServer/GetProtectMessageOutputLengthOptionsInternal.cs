using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000560 RID: 1376
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProtectMessageOutputLengthOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A42 RID: 2626
		// (set) Token: 0x06002168 RID: 8552 RVA: 0x00023651 File Offset: 0x00021851
		public uint DataLengthBytes
		{
			set
			{
				this.m_DataLengthBytes = value;
			}
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0002365A File Offset: 0x0002185A
		public void Set(GetProtectMessageOutputLengthOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.DataLengthBytes = other.DataLengthBytes;
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00023672 File Offset: 0x00021872
		public void Set(object other)
		{
			this.Set(other as GetProtectMessageOutputLengthOptions);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x04000F62 RID: 3938
		private int m_ApiVersion;

		// Token: 0x04000F63 RID: 3939
		private uint m_DataLengthBytes;
	}
}
