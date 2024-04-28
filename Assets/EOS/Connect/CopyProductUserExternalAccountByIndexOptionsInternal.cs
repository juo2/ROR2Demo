using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004B8 RID: 1208
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserExternalAccountByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008E6 RID: 2278
		// (set) Token: 0x06001D5B RID: 7515 RVA: 0x0001F470 File Offset: 0x0001D670
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (set) Token: 0x06001D5C RID: 7516 RVA: 0x0001F47F File Offset: 0x0001D67F
		public uint ExternalAccountInfoIndex
		{
			set
			{
				this.m_ExternalAccountInfoIndex = value;
			}
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0001F488 File Offset: 0x0001D688
		public void Set(CopyProductUserExternalAccountByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
				this.ExternalAccountInfoIndex = other.ExternalAccountInfoIndex;
			}
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		public void Set(object other)
		{
			this.Set(other as CopyProductUserExternalAccountByIndexOptions);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0001F4BA File Offset: 0x0001D6BA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000DB9 RID: 3513
		private int m_ApiVersion;

		// Token: 0x04000DBA RID: 3514
		private IntPtr m_TargetUserId;

		// Token: 0x04000DBB RID: 3515
		private uint m_ExternalAccountInfoIndex;
	}
}
