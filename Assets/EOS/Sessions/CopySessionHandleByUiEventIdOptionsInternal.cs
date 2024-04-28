using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000BD RID: 189
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleByUiEventIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000141 RID: 321
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x000075D9 File Offset: 0x000057D9
		public ulong UiEventId
		{
			set
			{
				this.m_UiEventId = value;
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000075E2 File Offset: 0x000057E2
		public void Set(CopySessionHandleByUiEventIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UiEventId = other.UiEventId;
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x000075FA File Offset: 0x000057FA
		public void Set(object other)
		{
			this.Set(other as CopySessionHandleByUiEventIdOptions);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400031F RID: 799
		private int m_ApiVersion;

		// Token: 0x04000320 RID: 800
		private ulong m_UiEventId;
	}
}
