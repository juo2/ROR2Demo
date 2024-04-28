using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000350 RID: 848
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationRemoveAttributeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700064B RID: 1611
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x000171B5 File Offset: 0x000153B5
		public string Key
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x000171C4 File Offset: 0x000153C4
		public void Set(LobbyModificationRemoveAttributeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000171DC File Offset: 0x000153DC
		public void Set(object other)
		{
			this.Set(other as LobbyModificationRemoveAttributeOptions);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000171EA File Offset: 0x000153EA
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
		}

		// Token: 0x04000A36 RID: 2614
		private int m_ApiVersion;

		// Token: 0x04000A37 RID: 2615
		private IntPtr m_Key;
	}
}
