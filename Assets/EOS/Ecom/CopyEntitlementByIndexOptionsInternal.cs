using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000440 RID: 1088
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyEntitlementByIndexOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170007FD RID: 2045
		// (set) Token: 0x06001A8D RID: 6797 RVA: 0x0001C07F File Offset: 0x0001A27F
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170007FE RID: 2046
		// (set) Token: 0x06001A8E RID: 6798 RVA: 0x0001C08E File Offset: 0x0001A28E
		public uint EntitlementIndex
		{
			set
			{
				this.m_EntitlementIndex = value;
			}
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x0001C097 File Offset: 0x0001A297
		public void Set(CopyEntitlementByIndexOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.EntitlementIndex = other.EntitlementIndex;
			}
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x0001C0BB File Offset: 0x0001A2BB
		public void Set(object other)
		{
			this.Set(other as CopyEntitlementByIndexOptions);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x0001C0C9 File Offset: 0x0001A2C9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000C53 RID: 3155
		private int m_ApiVersion;

		// Token: 0x04000C54 RID: 3156
		private IntPtr m_LocalUserId;

		// Token: 0x04000C55 RID: 3157
		private uint m_EntitlementIndex;
	}
}
