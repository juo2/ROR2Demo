using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000241 RID: 577
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteFileOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700041A RID: 1050
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x0000FFB2 File Offset: 0x0000E1B2
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x0000FFC1 File Offset: 0x0000E1C1
		public string Filename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		public void Set(DeleteFileOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Filename = other.Filename;
			}
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0000FFF4 File Offset: 0x0000E1F4
		public void Set(object other)
		{
			this.Set(other as DeleteFileOptions);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00010002 File Offset: 0x0000E202
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Filename);
		}

		// Token: 0x04000700 RID: 1792
		private int m_ApiVersion;

		// Token: 0x04000701 RID: 1793
		private IntPtr m_LocalUserId;

		// Token: 0x04000702 RID: 1794
		private IntPtr m_Filename;
	}
}
