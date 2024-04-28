using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000226 RID: 550
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PresenceModificationSetJoinInfoOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170003E3 RID: 995
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0000F840 File Offset: 0x0000DA40
		public string JoinInfo
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_JoinInfo, value);
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x0000F84F File Offset: 0x0000DA4F
		public void Set(PresenceModificationSetJoinInfoOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.JoinInfo = other.JoinInfo;
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x0000F867 File Offset: 0x0000DA67
		public void Set(object other)
		{
			this.Set(other as PresenceModificationSetJoinInfoOptions);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0000F875 File Offset: 0x0000DA75
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_JoinInfo);
		}

		// Token: 0x040006BF RID: 1727
		private int m_ApiVersion;

		// Token: 0x040006C0 RID: 1728
		private IntPtr m_JoinInfo;
	}
}
