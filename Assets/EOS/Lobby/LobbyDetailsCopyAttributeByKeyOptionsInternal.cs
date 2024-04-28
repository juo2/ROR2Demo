using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200032D RID: 813
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyAttributeByKeyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005FF RID: 1535
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x0001592E File Offset: 0x00013B2E
		public string AttrKey
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AttrKey, value);
			}
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0001593D File Offset: 0x00013B3D
		public void Set(LobbyDetailsCopyAttributeByKeyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AttrKey = other.AttrKey;
			}
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00015955 File Offset: 0x00013B55
		public void Set(object other)
		{
			this.Set(other as LobbyDetailsCopyAttributeByKeyOptions);
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00015963 File Offset: 0x00013B63
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AttrKey);
		}

		// Token: 0x040009AB RID: 2475
		private int m_ApiVersion;

		// Token: 0x040009AC RID: 2476
		private IntPtr m_AttrKey;
	}
}
