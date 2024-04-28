using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200040E RID: 1038
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700076C RID: 1900
		// (set) Token: 0x06001909 RID: 6409 RVA: 0x0001A59A File Offset: 0x0001879A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700076D RID: 1901
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x0001A5A9 File Offset: 0x000187A9
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x0001A5B8 File Offset: 0x000187B8
		public void Set(AcceptInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x0001A5DC File Offset: 0x000187DC
		public void Set(object other)
		{
			this.Set(other as AcceptInviteOptions);
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0001A5EA File Offset: 0x000187EA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000BA9 RID: 2985
		private int m_ApiVersion;

		// Token: 0x04000BAA RID: 2986
		private IntPtr m_LocalUserId;

		// Token: 0x04000BAB RID: 2987
		private IntPtr m_TargetUserId;
	}
}
