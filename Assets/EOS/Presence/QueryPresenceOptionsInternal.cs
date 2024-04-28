using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200022E RID: 558
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPresenceOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003F3 RID: 1011
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x0000FA6E File Offset: 0x0000DC6E
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0000FA7D File Offset: 0x0000DC7D
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public void Set(QueryPresenceOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0000FAB0 File Offset: 0x0000DCB0
		public void Set(object other)
		{
			this.Set(other as QueryPresenceOptions);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0000FABE File Offset: 0x0000DCBE
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x040006D1 RID: 1745
		private int m_ApiVersion;

		// Token: 0x040006D2 RID: 1746
		private IntPtr m_LocalUserId;

		// Token: 0x040006D3 RID: 1747
		private IntPtr m_TargetUserId;
	}
}
