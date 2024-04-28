using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200004B RID: 75
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HideFriendsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000076 RID: 118
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00004C4D File Offset: 0x00002E4D
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00004C5C File Offset: 0x00002E5C
		public void Set(HideFriendsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00004C74 File Offset: 0x00002E74
		public void Set(object other)
		{
			this.Set(other as HideFriendsOptions);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00004C82 File Offset: 0x00002E82
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400019B RID: 411
		private int m_ApiVersion;

		// Token: 0x0400019C RID: 412
		private IntPtr m_LocalUserId;
	}
}
