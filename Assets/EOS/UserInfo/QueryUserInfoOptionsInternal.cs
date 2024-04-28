using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003C RID: 60
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700005C RID: 92
		// (set) Token: 0x0600036D RID: 877 RVA: 0x000044DE File Offset: 0x000026DE
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700005D RID: 93
		// (set) Token: 0x0600036E RID: 878 RVA: 0x000044ED File Offset: 0x000026ED
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000044FC File Offset: 0x000026FC
		public void Set(QueryUserInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00004520 File Offset: 0x00002720
		public void Set(object other)
		{
			this.Set(other as QueryUserInfoOptions);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000452E File Offset: 0x0000272E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000171 RID: 369
		private int m_ApiVersion;

		// Token: 0x04000172 RID: 370
		private IntPtr m_LocalUserId;

		// Token: 0x04000173 RID: 371
		private IntPtr m_TargetUserId;
	}
}
