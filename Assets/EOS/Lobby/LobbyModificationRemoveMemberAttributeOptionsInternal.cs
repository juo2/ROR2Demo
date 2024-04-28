using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000352 RID: 850
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationRemoveMemberAttributeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700064D RID: 1613
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x00017209 File Offset: 0x00015409
		public string Key
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00017218 File Offset: 0x00015418
		public void Set(LobbyModificationRemoveMemberAttributeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
			}
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00017230 File Offset: 0x00015430
		public void Set(object other)
		{
			this.Set(other as LobbyModificationRemoveMemberAttributeOptions);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0001723E File Offset: 0x0001543E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
		}

		// Token: 0x04000A39 RID: 2617
		private int m_ApiVersion;

		// Token: 0x04000A3A RID: 2618
		private IntPtr m_Key;
	}
}
