using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000109 RID: 265
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsAttributeInternal : ISettable, IDisposable
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x000085D0 File Offset: 0x000067D0
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x000085EC File Offset: 0x000067EC
		public AttributeData Data
		{
			get
			{
				AttributeData result;
				Helper.TryMarshalGet<AttributeDataInternal, AttributeData>(this.m_Data, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<AttributeDataInternal, AttributeData>(ref this.m_Data, value);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000085FB File Offset: 0x000067FB
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00008603 File Offset: 0x00006803
		public SessionAttributeAdvertisementType AdvertisementType
		{
			get
			{
				return this.m_AdvertisementType;
			}
			set
			{
				this.m_AdvertisementType = value;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000860C File Offset: 0x0000680C
		public void Set(SessionDetailsAttribute other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.Data = other.Data;
				this.AdvertisementType = other.AdvertisementType;
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00008630 File Offset: 0x00006830
		public void Set(object other)
		{
			this.Set(other as SessionDetailsAttribute);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0000863E File Offset: 0x0000683E
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Data);
		}

		// Token: 0x040003AD RID: 941
		private int m_ApiVersion;

		// Token: 0x040003AE RID: 942
		private IntPtr m_Data;

		// Token: 0x040003AF RID: 943
		private SessionAttributeAdvertisementType m_AdvertisementType;
	}
}
