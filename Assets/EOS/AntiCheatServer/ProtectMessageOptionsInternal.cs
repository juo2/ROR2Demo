using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000568 RID: 1384
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ProtectMessageOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A46 RID: 2630
		// (set) Token: 0x0600218B RID: 8587 RVA: 0x000236B3 File Offset: 0x000218B3
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (set) Token: 0x0600218C RID: 8588 RVA: 0x000236BC File Offset: 0x000218BC
		public byte[] Data
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x000236D1 File Offset: 0x000218D1
		public uint OutBufferSizeBytes
		{
			set
			{
				this.m_OutBufferSizeBytes = value;
			}
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x000236DA File Offset: 0x000218DA
		public void Set(ProtectMessageOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.ClientHandle;
				this.Data = other.Data;
				this.OutBufferSizeBytes = other.OutBufferSizeBytes;
			}
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0002370A File Offset: 0x0002190A
		public void Set(object other)
		{
			this.Set(other as ProtectMessageOptions);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x00023718 File Offset: 0x00021918
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientHandle);
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04000F67 RID: 3943
		private int m_ApiVersion;

		// Token: 0x04000F68 RID: 3944
		private IntPtr m_ClientHandle;

		// Token: 0x04000F69 RID: 3945
		private uint m_DataLengthBytes;

		// Token: 0x04000F6A RID: 3946
		private IntPtr m_Data;

		// Token: 0x04000F6B RID: 3947
		private uint m_OutBufferSizeBytes;
	}
}
