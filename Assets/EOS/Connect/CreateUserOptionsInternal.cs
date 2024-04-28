using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004C2 RID: 1218
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateUserOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008F9 RID: 2297
		// (set) Token: 0x06001D8A RID: 7562 RVA: 0x0001F721 File Offset: 0x0001D921
		public ContinuanceToken ContinuanceToken
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ContinuanceToken, value);
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0001F730 File Offset: 0x0001D930
		public void Set(CreateUserOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ContinuanceToken = other.ContinuanceToken;
			}
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0001F748 File Offset: 0x0001D948
		public void Set(object other)
		{
			this.Set(other as CreateUserOptions);
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0001F756 File Offset: 0x0001D956
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ContinuanceToken);
		}

		// Token: 0x04000DCD RID: 3533
		private int m_ApiVersion;

		// Token: 0x04000DCE RID: 3534
		private IntPtr m_ContinuanceToken;
	}
}
