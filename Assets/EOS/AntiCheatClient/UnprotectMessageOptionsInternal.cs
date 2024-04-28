using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x020005D5 RID: 1493
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnprotectMessageOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000B38 RID: 2872
		// (set) Token: 0x06002401 RID: 9217 RVA: 0x00025F8D File Offset: 0x0002418D
		public byte[] Data
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (set) Token: 0x06002402 RID: 9218 RVA: 0x00025FA2 File Offset: 0x000241A2
		public uint OutBufferSizeBytes
		{
			set
			{
				this.m_OutBufferSizeBytes = value;
			}
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x00025FAB File Offset: 0x000241AB
		public void Set(UnprotectMessageOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Data;
				this.OutBufferSizeBytes = other.OutBufferSizeBytes;
			}
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x00025FCF File Offset: 0x000241CF
		public void Set(object other)
		{
			this.Set(other as UnprotectMessageOptions);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x00025FDD File Offset: 0x000241DD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04001104 RID: 4356
		private int m_ApiVersion;

		// Token: 0x04001105 RID: 4357
		private uint m_DataLengthBytes;

		// Token: 0x04001106 RID: 4358
		private IntPtr m_Data;

		// Token: 0x04001107 RID: 4359
		private uint m_OutBufferSizeBytes;
	}
}
