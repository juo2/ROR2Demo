using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000522 RID: 1314
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170009B4 RID: 2484
		// (set) Token: 0x06001FC2 RID: 8130 RVA: 0x000219C3 File Offset: 0x0001FBC3
		public LinkAccountFlags LinkAccountFlags
		{
			set
			{
				this.m_LinkAccountFlags = value;
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (set) Token: 0x06001FC3 RID: 8131 RVA: 0x000219CC File Offset: 0x0001FBCC
		public ContinuanceToken ContinuanceToken
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_ContinuanceToken, value);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (set) Token: 0x06001FC4 RID: 8132 RVA: 0x000219DB File Offset: 0x0001FBDB
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000219EA File Offset: 0x0001FBEA
		public void Set(LinkAccountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LinkAccountFlags = other.LinkAccountFlags;
				this.ContinuanceToken = other.ContinuanceToken;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00021A1A File Offset: 0x0001FC1A
		public void Set(object other)
		{
			this.Set(other as LinkAccountOptions);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00021A28 File Offset: 0x0001FC28
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ContinuanceToken);
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000EB3 RID: 3763
		private int m_ApiVersion;

		// Token: 0x04000EB4 RID: 3764
		private LinkAccountFlags m_LinkAccountFlags;

		// Token: 0x04000EB5 RID: 3765
		private IntPtr m_ContinuanceToken;

		// Token: 0x04000EB6 RID: 3766
		private IntPtr m_LocalUserId;
	}
}
