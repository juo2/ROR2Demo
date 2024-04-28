using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200050C RID: 1292
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AccountFeatureRestrictedInfoInternal : ISettable, IDisposable
	{
		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x00020D6C File Offset: 0x0001EF6C
		// (set) Token: 0x06001F3E RID: 7998 RVA: 0x00020D88 File Offset: 0x0001EF88
		public string VerificationURI
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_VerificationURI, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_VerificationURI, value);
			}
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00020D97 File Offset: 0x0001EF97
		public void Set(AccountFeatureRestrictedInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.VerificationURI = other.VerificationURI;
			}
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00020DAF File Offset: 0x0001EFAF
		public void Set(object other)
		{
			this.Set(other as AccountFeatureRestrictedInfo);
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00020DBD File Offset: 0x0001EFBD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_VerificationURI);
		}

		// Token: 0x04000E6B RID: 3691
		private int m_ApiVersion;

		// Token: 0x04000E6C RID: 3692
		private IntPtr m_VerificationURI;
	}
}
