using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000430 RID: 1072
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170007A4 RID: 1956
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x0001B04E File Offset: 0x0001924E
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (set) Token: 0x060019C0 RID: 6592 RVA: 0x0001B05D File Offset: 0x0001925D
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0001B06C File Offset: 0x0001926C
		public void Set(SendInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x0001B090 File Offset: 0x00019290
		public void Set(object other)
		{
			this.Set(other as SendInviteOptions);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x0001B09E File Offset: 0x0001929E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000BF1 RID: 3057
		private int m_ApiVersion;

		// Token: 0x04000BF2 RID: 3058
		private IntPtr m_LocalUserId;

		// Token: 0x04000BF3 RID: 3059
		private IntPtr m_TargetUserId;
	}
}
