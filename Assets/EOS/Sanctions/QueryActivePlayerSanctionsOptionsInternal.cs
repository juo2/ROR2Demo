using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x0200015A RID: 346
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryActivePlayerSanctionsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700023D RID: 573
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0000A876 File Offset: 0x00008A76
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x1700023E RID: 574
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x0000A885 File Offset: 0x00008A85
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0000A894 File Offset: 0x00008A94
		public void Set(QueryActivePlayerSanctionsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.TargetUserId;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public void Set(object other)
		{
			this.Set(other as QueryActivePlayerSanctionsOptions);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0000A8C6 File Offset: 0x00008AC6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400048B RID: 1163
		private int m_ApiVersion;

		// Token: 0x0400048C RID: 1164
		private IntPtr m_TargetUserId;

		// Token: 0x0400048D RID: 1165
		private IntPtr m_LocalUserId;
	}
}
