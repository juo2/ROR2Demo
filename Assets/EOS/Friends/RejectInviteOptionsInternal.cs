using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200042C RID: 1068
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000797 RID: 1943
		// (set) Token: 0x060019A4 RID: 6564 RVA: 0x0001AE8A File Offset: 0x0001908A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000798 RID: 1944
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x0001AE99 File Offset: 0x00019099
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x0001AEA8 File Offset: 0x000190A8
		public void Set(RejectInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0001AECC File Offset: 0x000190CC
		public void Set(object other)
		{
			this.Set(other as RejectInviteOptions);
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x0001AEDA File Offset: 0x000190DA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000BE4 RID: 3044
		private int m_ApiVersion;

		// Token: 0x04000BE5 RID: 3045
		private IntPtr m_LocalUserId;

		// Token: 0x04000BE6 RID: 3046
		private IntPtr m_TargetUserId;
	}
}
