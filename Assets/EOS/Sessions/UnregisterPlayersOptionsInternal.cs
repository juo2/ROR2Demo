using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000148 RID: 328
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPlayersOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000215 RID: 533
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0000A260 File Offset: 0x00008460
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x17000216 RID: 534
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0000A26F File Offset: 0x0000846F
		public ProductUserId[] PlayersToUnregister
		{
			set
			{
				Helper.TryMarshalSet<ProductUserId>(ref this.m_PlayersToUnregister, value, out this.m_PlayersToUnregisterCount);
			}
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0000A284 File Offset: 0x00008484
		public void Set(UnregisterPlayersOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.SessionName = other.SessionName;
				this.PlayersToUnregister = other.PlayersToUnregister;
			}
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0000A2A8 File Offset: 0x000084A8
		public void Set(object other)
		{
			this.Set(other as UnregisterPlayersOptions);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0000A2B6 File Offset: 0x000084B6
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
			Helper.TryMarshalDispose(ref this.m_PlayersToUnregister);
		}

		// Token: 0x0400045E RID: 1118
		private int m_ApiVersion;

		// Token: 0x0400045F RID: 1119
		private IntPtr m_SessionName;

		// Token: 0x04000460 RID: 1120
		private IntPtr m_PlayersToUnregister;

		// Token: 0x04000461 RID: 1121
		private uint m_PlayersToUnregisterCount;
	}
}
