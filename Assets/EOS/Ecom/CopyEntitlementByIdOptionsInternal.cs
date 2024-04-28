using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200043E RID: 1086
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170007F9 RID: 2041
		// (set) Token: 0x06001A83 RID: 6787 RVA: 0x0001BFF3 File Offset: 0x0001A1F3
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170007FA RID: 2042
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x0001C002 File Offset: 0x0001A202
		public string EntitlementId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EntitlementId, value);
			}
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x0001C011 File Offset: 0x0001A211
		public void Set(CopyEntitlementByIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.EntitlementId = other.EntitlementId;
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x0001C035 File Offset: 0x0001A235
		public void Set(object other)
		{
			this.Set(other as CopyEntitlementByIdOptions);
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x0001C043 File Offset: 0x0001A243
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_EntitlementId);
		}

		// Token: 0x04000C4E RID: 3150
		private int m_ApiVersion;

		// Token: 0x04000C4F RID: 3151
		private IntPtr m_LocalUserId;

		// Token: 0x04000C50 RID: 3152
		private IntPtr m_EntitlementId;
	}
}
