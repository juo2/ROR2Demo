using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C7 RID: 199
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DestroySessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700015A RID: 346
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x000078E5 File Offset: 0x00005AE5
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000078F4 File Offset: 0x00005AF4
		public void Set(DestroySessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0000790C File Offset: 0x00005B0C
		public void Set(object other)
		{
			this.Set(other as DestroySessionOptions);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0000791A File Offset: 0x00005B1A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
		}

		// Token: 0x0400033B RID: 827
		private int m_ApiVersion;

		// Token: 0x0400033C RID: 828
		private IntPtr m_SessionName;
	}
}
