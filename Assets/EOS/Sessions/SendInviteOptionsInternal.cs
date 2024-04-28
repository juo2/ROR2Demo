using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000105 RID: 261
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001A9 RID: 425
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x000083B3 File Offset: 0x000065B3
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x170001AA RID: 426
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x000083C2 File Offset: 0x000065C2
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170001AB RID: 427
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x000083D1 File Offset: 0x000065D1
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000083E0 File Offset: 0x000065E0
		public void Set(SendInviteOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00008410 File Offset: 0x00006610
		public void Set(object other)
		{
			this.Set(other as SendInviteOptions);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000841E File Offset: 0x0000661E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x0400039D RID: 925
		private int m_ApiVersion;

		// Token: 0x0400039E RID: 926
		private IntPtr m_SessionName;

		// Token: 0x0400039F RID: 927
		private IntPtr m_LocalUserId;

		// Token: 0x040003A0 RID: 928
		private IntPtr m_TargetUserId;
	}
}
