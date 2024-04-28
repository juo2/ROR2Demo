using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D6 RID: 1238
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000928 RID: 2344
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x0001FF3A File Offset: 0x0001E13A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000929 RID: 2345
		// (set) Token: 0x06001E08 RID: 7688 RVA: 0x0001FF49 File Offset: 0x0001E149
		public ContinuanceToken ContinuanceToken
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ContinuanceToken, value);
			}
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0001FF58 File Offset: 0x0001E158
		public void Set(LinkAccountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.ContinuanceToken = other.ContinuanceToken;
			}
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0001FF7C File Offset: 0x0001E17C
		public void Set(object other)
		{
			this.Set(other as LinkAccountOptions);
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0001FF8A File Offset: 0x0001E18A
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_ContinuanceToken);
		}

		// Token: 0x04000E02 RID: 3586
		private int m_ApiVersion;

		// Token: 0x04000E03 RID: 3587
		private IntPtr m_LocalUserId;

		// Token: 0x04000E04 RID: 3588
		private IntPtr m_ContinuanceToken;
	}
}
