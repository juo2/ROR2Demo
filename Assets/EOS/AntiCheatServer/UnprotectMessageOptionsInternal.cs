using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x02000570 RID: 1392
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnprotectMessageOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000A5E RID: 2654
		// (set) Token: 0x060021BF RID: 8639 RVA: 0x00023980 File Offset: 0x00021B80
		public IntPtr ClientHandle
		{
			set
			{
				this.m_ClientHandle = value;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (set) Token: 0x060021C0 RID: 8640 RVA: 0x00023989 File Offset: 0x00021B89
		public byte[] Data
		{
			set
			{
				Helper.TryMarshalSet<byte>(ref this.m_Data, value, out this.m_DataLengthBytes);
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (set) Token: 0x060021C1 RID: 8641 RVA: 0x0002399E File Offset: 0x00021B9E
		public uint OutBufferSizeBytes
		{
			set
			{
				this.m_OutBufferSizeBytes = value;
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000239A7 File Offset: 0x00021BA7
		public void Set(UnprotectMessageOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ClientHandle = other.ClientHandle;
				this.Data = other.Data;
				this.OutBufferSizeBytes = other.OutBufferSizeBytes;
			}
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000239D7 File Offset: 0x00021BD7
		public void Set(object other)
		{
			this.Set(other as UnprotectMessageOptions);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000239E5 File Offset: 0x00021BE5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ClientHandle);
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x04000F85 RID: 3973
		private int m_ApiVersion;

		// Token: 0x04000F86 RID: 3974
		private IntPtr m_ClientHandle;

		// Token: 0x04000F87 RID: 3975
		private uint m_DataLengthBytes;

		// Token: 0x04000F88 RID: 3976
		private IntPtr m_Data;

		// Token: 0x04000F89 RID: 3977
		private uint m_OutBufferSizeBytes;
	}
}
