using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C9 RID: 201
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DumpSessionStateOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700015C RID: 348
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00007939 File Offset: 0x00005B39
		public string SessionName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_SessionName, value);
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00007948 File Offset: 0x00005B48
		public void Set(DumpSessionStateOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionName = other.SessionName;
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00007960 File Offset: 0x00005B60
		public void Set(object other)
		{
			this.Set(other as DumpSessionStateOptions);
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0000796E File Offset: 0x00005B6E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionName);
		}

		// Token: 0x0400033E RID: 830
		private int m_ApiVersion;

		// Token: 0x0400033F RID: 831
		private IntPtr m_SessionName;
	}
}
