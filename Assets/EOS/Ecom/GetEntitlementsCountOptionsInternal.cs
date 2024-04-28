using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200045C RID: 1116
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetEntitlementsCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000842 RID: 2114
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0001D184 File Offset: 0x0001B384
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0001D193 File Offset: 0x0001B393
		public void Set(GetEntitlementsCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x0001D1AB File Offset: 0x0001B3AB
		public void Set(object other)
		{
			this.Set(other as GetEntitlementsCountOptions);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0001D1B9 File Offset: 0x0001B3B9
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000CD3 RID: 3283
		private int m_ApiVersion;

		// Token: 0x04000CD4 RID: 3284
		private IntPtr m_LocalUserId;
	}
}
