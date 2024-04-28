using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005CD RID: 1485
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ProtectMessageOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B24 RID: 2852
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x00025D10 File Offset: 0x00023F10
		public byte[] Data
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (set) Token: 0x060023D4 RID: 9172 RVA: 0x00025D25 File Offset: 0x00023F25
		public uint OutBufferSizeBytes
		{
			set
			{
				this.m_OutBufferSizeBytes = value;
			}
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00025D2E File Offset: 0x00023F2E
		public void Set(ProtectMessageOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Data;
				this.OutBufferSizeBytes = other.OutBufferSizeBytes;
			}
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00025D52 File Offset: 0x00023F52
		public void Set(object other)
		{
			this.Set(other as ProtectMessageOptions);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00025D60 File Offset: 0x00023F60
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x040010E9 RID: 4329
		private int m_ApiVersion;

		// Token: 0x040010EA RID: 4330
		private uint m_DataLengthBytes;

		// Token: 0x040010EB RID: 4331
		private IntPtr m_Data;

		// Token: 0x040010EC RID: 4332
		private uint m_OutBufferSizeBytes;
	}
}
