using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000305 RID: 773
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyLobbyDetailsHandleByUiEventIdOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000593 RID: 1427
		// (set) Token: 0x06001344 RID: 4932 RVA: 0x00014901 File Offset: 0x00012B01
		public ulong UiEventId
		{
			set
			{
				this.m_UiEventId = value;
			}
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0001490A File Offset: 0x00012B0A
		public void Set(CopyLobbyDetailsHandleByUiEventIdOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.UiEventId = other.UiEventId;
			}
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00014922 File Offset: 0x00012B22
		public void Set(object other)
		{
			this.Set(other as CopyLobbyDetailsHandleByUiEventIdOptions);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x000038B7 File Offset: 0x00001AB7
		public void Dispose()
		{
		}

		// Token: 0x0400092A RID: 2346
		private int m_ApiVersion;

		// Token: 0x0400092B RID: 2347
		private ulong m_UiEventId;
	}
}
