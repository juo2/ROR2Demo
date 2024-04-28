using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000269 RID: 617
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000454 RID: 1108
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00010D7A File Offset: 0x0000EF7A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (set) Token: 0x06000FC9 RID: 4041 RVA: 0x00010D89 File Offset: 0x0000EF89
		public string Filename
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Filename, value);
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00010D98 File Offset: 0x0000EF98
		public void Set(QueryFileOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.Filename = other.Filename;
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x00010DBC File Offset: 0x0000EFBC
		public void Set(object other)
		{
			this.Set(other as QueryFileOptions);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x00010DCA File Offset: 0x0000EFCA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_Filename);
		}

		// Token: 0x04000748 RID: 1864
		private int m_ApiVersion;

		// Token: 0x04000749 RID: 1865
		private IntPtr m_LocalUserId;

		// Token: 0x0400074A RID: 1866
		private IntPtr m_Filename;
	}
}
