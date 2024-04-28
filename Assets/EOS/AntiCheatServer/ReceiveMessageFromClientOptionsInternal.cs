using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x0200056A RID: 1386
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceiveMessageFromClientOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A4B RID: 2635
		// (set) Token: 0x06002196 RID: 8598 RVA: 0x00023754 File Offset: 0x00021954
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (set) Token: 0x06002197 RID: 8599 RVA: 0x0002375D File Offset: 0x0002195D
		public byte[] Data
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00023772 File Offset: 0x00021972
		public void Set(ReceiveMessageFromClientOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.ClientHandle;
				this.Data = other.Data;
			}
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00023796 File Offset: 0x00021996
		public void Set(object other)
		{
			this.Set(other as ReceiveMessageFromClientOptions);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x000237A4 File Offset: 0x000219A4
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientHandle);
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04000F6E RID: 3950
		private int m_ApiVersion;

		// Token: 0x04000F6F RID: 3951
		private IntPtr m_ClientHandle;

		// Token: 0x04000F70 RID: 3952
		private uint m_DataLengthBytes;

		// Token: 0x04000F71 RID: 3953
		private IntPtr m_Data;
	}
}
