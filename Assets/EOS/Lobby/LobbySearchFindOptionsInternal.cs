using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000362 RID: 866
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchFindOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700065E RID: 1630
		// (set) Token: 0x0600157E RID: 5502 RVA: 0x0001764D File Offset: 0x0001584D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0001765C File Offset: 0x0001585C
		public void Set(LobbySearchFindOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00017674 File Offset: 0x00015874
		public void Set(object other)
		{
			this.Set(other as LobbySearchFindOptions);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00017682 File Offset: 0x00015882
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000A5B RID: 2651
		private int m_ApiVersion;

		// Token: 0x04000A5C RID: 2652
		private IntPtr m_LocalUserId;
	}
}
