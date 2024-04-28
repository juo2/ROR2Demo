using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000418 RID: 1048
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetStatusOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000776 RID: 1910
		// (set) Token: 0x06001938 RID: 6456 RVA: 0x0001AA14 File Offset: 0x00018C14
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000777 RID: 1911
		// (set) Token: 0x06001939 RID: 6457 RVA: 0x0001AA23 File Offset: 0x00018C23
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0001AA32 File Offset: 0x00018C32
		public void Set(GetStatusOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x0001AA56 File Offset: 0x00018C56
		public void Set(object other)
		{
			this.Set(other as GetStatusOptions);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x0001AA64 File Offset: 0x00018C64
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000BC4 RID: 3012
		private int m_ApiVersion;

		// Token: 0x04000BC5 RID: 3013
		private IntPtr m_LocalUserId;

		// Token: 0x04000BC6 RID: 3014
		private IntPtr m_TargetUserId;
	}
}
