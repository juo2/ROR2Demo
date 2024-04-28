using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000434 RID: 1076
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogOfferInternal : ISettable, IDisposable
	{
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x0001B7F3 File Offset: 0x000199F3
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x0001B7FB File Offset: 0x000199FB
		public int ServerIndex
		{
			get
			{
				return this.m_ServerIndex;
			}
			set
			{
				this.m_ServerIndex = value;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x0001B804 File Offset: 0x00019A04
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x0001B820 File Offset: 0x00019A20
		public string CatalogNamespace
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_CatalogNamespace, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_CatalogNamespace, value);
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0001B830 File Offset: 0x00019A30
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x0001B84C File Offset: 0x00019A4C
		public string Id
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Id, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Id, value);
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0001B85C File Offset: 0x00019A5C
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x0001B878 File Offset: 0x00019A78
		public string TitleText
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_TitleText, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TitleText, value);
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x0001B888 File Offset: 0x00019A88
		// (set) Token: 0x06001A24 RID: 6692 RVA: 0x0001B8A4 File Offset: 0x00019AA4
		public string DescriptionText
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DescriptionText, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DescriptionText, value);
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0001B8B4 File Offset: 0x00019AB4
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x0001B8D0 File Offset: 0x00019AD0
		public string LongDescriptionText
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_LongDescriptionText, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LongDescriptionText, value);
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		// (set) Token: 0x06001A28 RID: 6696 RVA: 0x0001B8FC File Offset: 0x00019AFC
		public string TechnicalDetailsText_DEPRECATED
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_TechnicalDetailsText_DEPRECATED, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TechnicalDetailsText_DEPRECATED, value);
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0001B90C File Offset: 0x00019B0C
		// (set) Token: 0x06001A2A RID: 6698 RVA: 0x0001B928 File Offset: 0x00019B28
		public string CurrencyCode
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_CurrencyCode, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_CurrencyCode, value);
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0001B937 File Offset: 0x00019B37
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x0001B93F File Offset: 0x00019B3F
		public Result PriceResult
		{
			get
			{
				return this.m_PriceResult;
			}
			set
			{
				this.m_PriceResult = value;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x0001B948 File Offset: 0x00019B48
		// (set) Token: 0x06001A2E RID: 6702 RVA: 0x0001B950 File Offset: 0x00019B50
		public uint OriginalPrice_DEPRECATED
		{
			get
			{
				return this.m_OriginalPrice_DEPRECATED;
			}
			set
			{
				this.m_OriginalPrice_DEPRECATED = value;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x0001B959 File Offset: 0x00019B59
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x0001B961 File Offset: 0x00019B61
		public uint CurrentPrice_DEPRECATED
		{
			get
			{
				return this.m_CurrentPrice_DEPRECATED;
			}
			set
			{
				this.m_CurrentPrice_DEPRECATED = value;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0001B96A File Offset: 0x00019B6A
		// (set) Token: 0x06001A32 RID: 6706 RVA: 0x0001B972 File Offset: 0x00019B72
		public byte DiscountPercentage
		{
			get
			{
				return this.m_DiscountPercentage;
			}
			set
			{
				this.m_DiscountPercentage = value;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x0001B97B File Offset: 0x00019B7B
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x0001B983 File Offset: 0x00019B83
		public long ExpirationTimestamp
		{
			get
			{
				return this.m_ExpirationTimestamp;
			}
			set
			{
				this.m_ExpirationTimestamp = value;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x0001B98C File Offset: 0x00019B8C
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x0001B994 File Offset: 0x00019B94
		public uint PurchasedCount
		{
			get
			{
				return this.m_PurchasedCount;
			}
			set
			{
				this.m_PurchasedCount = value;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0001B99D File Offset: 0x00019B9D
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x0001B9A5 File Offset: 0x00019BA5
		public int PurchaseLimit
		{
			get
			{
				return this.m_PurchaseLimit;
			}
			set
			{
				this.m_PurchaseLimit = value;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0001B9B0 File Offset: 0x00019BB0
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x0001B9CC File Offset: 0x00019BCC
		public bool AvailableForPurchase
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_AvailableForPurchase, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AvailableForPurchase, value);
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0001B9DB File Offset: 0x00019BDB
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x0001B9E3 File Offset: 0x00019BE3
		public ulong OriginalPrice64
		{
			get
			{
				return this.m_OriginalPrice64;
			}
			set
			{
				this.m_OriginalPrice64 = value;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0001B9EC File Offset: 0x00019BEC
		// (set) Token: 0x06001A3E RID: 6718 RVA: 0x0001B9F4 File Offset: 0x00019BF4
		public ulong CurrentPrice64
		{
			get
			{
				return this.m_CurrentPrice64;
			}
			set
			{
				this.m_CurrentPrice64 = value;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0001B9FD File Offset: 0x00019BFD
		// (set) Token: 0x06001A40 RID: 6720 RVA: 0x0001BA05 File Offset: 0x00019C05
		public uint DecimalPoint
		{
			get
			{
				return this.m_DecimalPoint;
			}
			set
			{
				this.m_DecimalPoint = value;
			}
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0001BA10 File Offset: 0x00019C10
		public void Set(CatalogOffer other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 4;
				this.ServerIndex = other.ServerIndex;
				this.CatalogNamespace = other.CatalogNamespace;
				this.Id = other.Id;
				this.TitleText = other.TitleText;
				this.DescriptionText = other.DescriptionText;
				this.LongDescriptionText = other.LongDescriptionText;
				this.TechnicalDetailsText_DEPRECATED = other.TechnicalDetailsText_DEPRECATED;
				this.CurrencyCode = other.CurrencyCode;
				this.PriceResult = other.PriceResult;
				this.OriginalPrice_DEPRECATED = other.OriginalPrice_DEPRECATED;
				this.CurrentPrice_DEPRECATED = other.CurrentPrice_DEPRECATED;
				this.DiscountPercentage = other.DiscountPercentage;
				this.ExpirationTimestamp = other.ExpirationTimestamp;
				this.PurchasedCount = other.PurchasedCount;
				this.PurchaseLimit = other.PurchaseLimit;
				this.AvailableForPurchase = other.AvailableForPurchase;
				this.OriginalPrice64 = other.OriginalPrice64;
				this.CurrentPrice64 = other.CurrentPrice64;
				this.DecimalPoint = other.DecimalPoint;
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0001BB0E File Offset: 0x00019D0E
		public void Set(object other)
		{
			this.Set(other as CatalogOffer);
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0001BB1C File Offset: 0x00019D1C
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_CatalogNamespace);
			Helper.TryMarshalDispose(ref this.m_Id);
			Helper.TryMarshalDispose(ref this.m_TitleText);
			Helper.TryMarshalDispose(ref this.m_DescriptionText);
			Helper.TryMarshalDispose(ref this.m_LongDescriptionText);
			Helper.TryMarshalDispose(ref this.m_TechnicalDetailsText_DEPRECATED);
			Helper.TryMarshalDispose(ref this.m_CurrencyCode);
		}

		// Token: 0x04000C1C RID: 3100
		private int m_ApiVersion;

		// Token: 0x04000C1D RID: 3101
		private int m_ServerIndex;

		// Token: 0x04000C1E RID: 3102
		private IntPtr m_CatalogNamespace;

		// Token: 0x04000C1F RID: 3103
		private IntPtr m_Id;

		// Token: 0x04000C20 RID: 3104
		private IntPtr m_TitleText;

		// Token: 0x04000C21 RID: 3105
		private IntPtr m_DescriptionText;

		// Token: 0x04000C22 RID: 3106
		private IntPtr m_LongDescriptionText;

		// Token: 0x04000C23 RID: 3107
		private IntPtr m_TechnicalDetailsText_DEPRECATED;

		// Token: 0x04000C24 RID: 3108
		private IntPtr m_CurrencyCode;

		// Token: 0x04000C25 RID: 3109
		private Result m_PriceResult;

		// Token: 0x04000C26 RID: 3110
		private uint m_OriginalPrice_DEPRECATED;

		// Token: 0x04000C27 RID: 3111
		private uint m_CurrentPrice_DEPRECATED;

		// Token: 0x04000C28 RID: 3112
		private byte m_DiscountPercentage;

		// Token: 0x04000C29 RID: 3113
		private long m_ExpirationTimestamp;

		// Token: 0x04000C2A RID: 3114
		private uint m_PurchasedCount;

		// Token: 0x04000C2B RID: 3115
		private int m_PurchaseLimit;

		// Token: 0x04000C2C RID: 3116
		private int m_AvailableForPurchase;

		// Token: 0x04000C2D RID: 3117
		private ulong m_OriginalPrice64;

		// Token: 0x04000C2E RID: 3118
		private ulong m_CurrentPrice64;

		// Token: 0x04000C2F RID: 3119
		private uint m_DecimalPoint;
	}
}
