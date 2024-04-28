using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000504 RID: 1284
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlinkAccountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700096F RID: 2415
		// (set) Token: 0x06001EFA RID: 7930 RVA: 0x0002089D File Offset: 0x0001EA9D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000208AC File Offset: 0x0001EAAC
		public void Set(UnlinkAccountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000208C4 File Offset: 0x0001EAC4
		public void Set(object other)
		{
			this.Set(other as UnlinkAccountOptions);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000208D2 File Offset: 0x0001EAD2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000E4A RID: 3658
		private int m_ApiVersion;

		// Token: 0x04000E4B RID: 3659
		private IntPtr m_LocalUserId;
	}
}
