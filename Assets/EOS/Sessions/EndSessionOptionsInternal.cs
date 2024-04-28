using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CD RID: 205
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000163 RID: 355
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x00007A3D File Offset: 0x00005C3D
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00007A4C File Offset: 0x00005C4C
		public void Set(EndSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00007A64 File Offset: 0x00005C64
		public void Set(object other)
		{
			this.Set(other as EndSessionOptions);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00007A72 File Offset: 0x00005C72
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
		}

		// Token: 0x04000345 RID: 837
		private int m_ApiVersion;

		// Token: 0x04000346 RID: 838
		private IntPtr m_SessionName;
	}
}
