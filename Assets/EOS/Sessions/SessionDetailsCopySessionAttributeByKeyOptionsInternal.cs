using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200010F RID: 271
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopySessionAttributeByKeyOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001B3 RID: 435
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x000086B7 File Offset: 0x000068B7
		public string AttrKey
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AttrKey, value);
			}
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000086C6 File Offset: 0x000068C6
		public void Set(SessionDetailsCopySessionAttributeByKeyOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AttrKey = other.AttrKey;
			}
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000086DE File Offset: 0x000068DE
		public void Set(object other)
		{
			this.Set(other as SessionDetailsCopySessionAttributeByKeyOptions);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x000086EC File Offset: 0x000068EC
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AttrKey);
		}

		// Token: 0x040003B5 RID: 949
		private int m_ApiVersion;

		// Token: 0x040003B6 RID: 950
		private IntPtr m_AttrKey;
	}
}
