using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005D1 RID: 1489
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReceiveMessageFromServerOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B2B RID: 2859
		// (set) Token: 0x060023E5 RID: 9189 RVA: 0x00025E0B File Offset: 0x0002400B
		public byte[] Data
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x060023E6 RID: 9190 RVA: 0x00025E20 File Offset: 0x00024020
		public void Set(ReceiveMessageFromServerOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Data;
			}
		}

		// Token: 0x060023E7 RID: 9191 RVA: 0x00025E38 File Offset: 0x00024038
		public void Set(object other)
		{
			this.Set(other as ReceiveMessageFromServerOptions);
		}

		// Token: 0x060023E8 RID: 9192 RVA: 0x00025E46 File Offset: 0x00024046
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x040010F4 RID: 4340
		private int m_ApiVersion;

		// Token: 0x040010F5 RID: 4341
		private uint m_DataLengthBytes;

		// Token: 0x040010F6 RID: 4342
		private IntPtr m_Data;
	}
}
