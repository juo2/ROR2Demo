using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000220 RID: 544
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationDataRecordIdInternal : ISettable, IDisposable
	{
		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0000F71C File Offset: 0x0000D91C
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x0000F738 File Offset: 0x0000D938
		public string Key
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Key, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Key, value);
			}
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0000F747 File Offset: 0x0000D947
		public void Set(PresenceModificationDataRecordId other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Key = other.Key;
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0000F75F File Offset: 0x0000D95F
		public void Set(object other)
		{
			this.Set(other as PresenceModificationDataRecordId);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0000F76D File Offset: 0x0000D96D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Key);
		}

		// Token: 0x040006B4 RID: 1716
		private int m_ApiVersion;

		// Token: 0x040006B5 RID: 1717
		private IntPtr m_Key;
	}
}
