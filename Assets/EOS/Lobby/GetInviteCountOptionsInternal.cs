using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000313 RID: 787
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170005C1 RID: 1473
		// (set) Token: 0x060013A4 RID: 5028 RVA: 0x00014E7D File Offset: 0x0001307D
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x00014E8C File Offset: 0x0001308C
		public void Set(GetInviteCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x00014EA4 File Offset: 0x000130A4
		public void Set(object other)
		{
			this.Set(other as GetInviteCountOptions);
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00014EB2 File Offset: 0x000130B2
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400095B RID: 2395
		private int m_ApiVersion;

		// Token: 0x0400095C RID: 2396
		private IntPtr m_LocalUserId;
	}
}
