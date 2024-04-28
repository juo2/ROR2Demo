using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000462 RID: 1122
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetOfferCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700084C RID: 2124
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x0001D2FF File Offset: 0x0001B4FF
		public void Set(GetOfferCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x0001D317 File Offset: 0x0001B517
		public void Set(object other)
		{
			this.Set(other as GetOfferCountOptions);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x0001D325 File Offset: 0x0001B525
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000CE0 RID: 3296
		private int m_ApiVersion;

		// Token: 0x04000CE1 RID: 3297
		private IntPtr m_LocalUserId;
	}
}
