using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000543 RID: 1347
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryIdTokenOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170009EF RID: 2543
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x0002224A File Offset: 0x0002044A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (set) Token: 0x0600207F RID: 8319 RVA: 0x00022259 File Offset: 0x00020459
		public EpicAccountId TargetAccountId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetAccountId, value);
			}
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x00022268 File Offset: 0x00020468
		public void Set(QueryIdTokenOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetAccountId = other.TargetAccountId;
			}
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0002228C File Offset: 0x0002048C
		public void Set(object other)
		{
			this.Set(other as QueryIdTokenOptions);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0002229A File Offset: 0x0002049A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetAccountId);
		}

		// Token: 0x04000EF7 RID: 3831
		private int m_ApiVersion;

		// Token: 0x04000EF8 RID: 3832
		private IntPtr m_LocalUserId;

		// Token: 0x04000EF9 RID: 3833
		private IntPtr m_TargetAccountId;
	}
}
