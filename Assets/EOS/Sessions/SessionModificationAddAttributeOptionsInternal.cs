using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011C RID: 284
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationAddAttributeOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170001DE RID: 478
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x00008FD2 File Offset: 0x000071D2
		public AttributeData SessionAttribute
		{
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal, AttributeData>(ref this.m_SessionAttribute, value);
			}
		}

		// Token: 0x170001DF RID: 479
		// (set) Token: 0x0600083B RID: 2107 RVA: 0x00008FE1 File Offset: 0x000071E1
		public SessionAttributeAdvertisementType AdvertisementType
		{
			set
			{
				this.m_AdvertisementType = value;
			}
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00008FEA File Offset: 0x000071EA
		public void Set(SessionModificationAddAttributeOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.SessionAttribute = other.SessionAttribute;
				this.AdvertisementType = other.AdvertisementType;
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0000900E File Offset: 0x0000720E
		public void Set(object other)
		{
			this.Set(other as SessionModificationAddAttributeOptions);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0000901C File Offset: 0x0000721C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_SessionAttribute);
		}

		// Token: 0x040003EE RID: 1006
		private int m_ApiVersion;

		// Token: 0x040003EF RID: 1007
		private IntPtr m_SessionAttribute;

		// Token: 0x040003F0 RID: 1008
		private SessionAttributeAdvertisementType m_AdvertisementType;
	}
}
