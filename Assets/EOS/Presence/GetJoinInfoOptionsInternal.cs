using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200020E RID: 526
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetJoinInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003B4 RID: 948
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0000EB75 File Offset: 0x0000CD75
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0000EB84 File Offset: 0x0000CD84
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0000EB93 File Offset: 0x0000CD93
		public void Set(GetJoinInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0000EBB7 File Offset: 0x0000CDB7
		public void Set(object other)
		{
			this.Set(other as GetJoinInfoOptions);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0000EBC5 File Offset: 0x0000CDC5
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000670 RID: 1648
		private int m_ApiVersion;

		// Token: 0x04000671 RID: 1649
		private IntPtr m_LocalUserId;

		// Token: 0x04000672 RID: 1650
		private IntPtr m_TargetUserId;
	}
}
