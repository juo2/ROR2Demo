using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004BA RID: 1210
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyProductUserInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008E9 RID: 2281
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0001F4D9 File Offset: 0x0001D6D9
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0001F4E8 File Offset: 0x0001D6E8
		public void Set(CopyProductUserInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0001F500 File Offset: 0x0001D700
		public void Set(object other)
		{
			this.Set(other as CopyProductUserInfoOptions);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0001F50E File Offset: 0x0001D70E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000DBD RID: 3517
		private int m_ApiVersion;

		// Token: 0x04000DBE RID: 3518
		private IntPtr m_TargetUserId;
	}
}
