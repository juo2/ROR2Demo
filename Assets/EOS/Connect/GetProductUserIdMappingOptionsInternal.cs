using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D0 RID: 1232
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetProductUserIdMappingOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000918 RID: 2328
		// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x0001FC8C File Offset: 0x0001DE8C
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000919 RID: 2329
		// (set) Token: 0x06001DE1 RID: 7649 RVA: 0x0001FC9B File Offset: 0x0001DE9B
		public ExternalAccountType AccountIdType
		{
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x1700091A RID: 2330
		// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		public ProductUserId TargetProductUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetProductUserId, value);
			}
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0001FCB3 File Offset: 0x0001DEB3
		public void Set(GetProductUserIdMappingOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.AccountIdType = other.AccountIdType;
				this.TargetProductUserId = other.TargetProductUserId;
			}
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0001FCE3 File Offset: 0x0001DEE3
		public void Set(object other)
		{
			this.Set(other as GetProductUserIdMappingOptions);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x0001FCF1 File Offset: 0x0001DEF1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetProductUserId);
		}

		// Token: 0x04000DF1 RID: 3569
		private int m_ApiVersion;

		// Token: 0x04000DF2 RID: 3570
		private IntPtr m_LocalUserId;

		// Token: 0x04000DF3 RID: 3571
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000DF4 RID: 3572
		private IntPtr m_TargetProductUserId;
	}
}
