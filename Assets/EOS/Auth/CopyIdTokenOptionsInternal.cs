using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000513 RID: 1299
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyIdTokenOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000990 RID: 2448
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x0002139D File Offset: 0x0001F59D
		public EpicAccountId AccountId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_AccountId, value);
			}
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000213AC File Offset: 0x0001F5AC
		public void Set(CopyIdTokenOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AccountId = other.AccountId;
			}
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000213C4 File Offset: 0x0001F5C4
		public void Set(object other)
		{
			this.Set(other as CopyIdTokenOptions);
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x000213D2 File Offset: 0x0001F5D2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AccountId);
		}

		// Token: 0x04000E89 RID: 3721
		private int m_ApiVersion;

		// Token: 0x04000E8A RID: 3722
		private IntPtr m_AccountId;
	}
}
