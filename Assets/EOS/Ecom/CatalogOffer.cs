using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000433 RID: 1075
	public class CatalogOffer : ISettable
	{
		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x060019F2 RID: 6642 RVA: 0x0001B4F5 File Offset: 0x000196F5
		// (set) Token: 0x060019F3 RID: 6643 RVA: 0x0001B4FD File Offset: 0x000196FD
		public int ServerIndex { get; set; }

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x0001B506 File Offset: 0x00019706
		// (set) Token: 0x060019F5 RID: 6645 RVA: 0x0001B50E File Offset: 0x0001970E
		public string CatalogNamespace { get; set; }

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x0001B517 File Offset: 0x00019717
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x0001B51F File Offset: 0x0001971F
		public string Id { get; set; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0001B528 File Offset: 0x00019728
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x0001B530 File Offset: 0x00019730
		public string TitleText { get; set; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x0001B539 File Offset: 0x00019739
		// (set) Token: 0x060019FB RID: 6651 RVA: 0x0001B541 File Offset: 0x00019741
		public string DescriptionText { get; set; }

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x0001B54A File Offset: 0x0001974A
		// (set) Token: 0x060019FD RID: 6653 RVA: 0x0001B552 File Offset: 0x00019752
		public string LongDescriptionText { get; set; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0001B55B File Offset: 0x0001975B
		// (set) Token: 0x060019FF RID: 6655 RVA: 0x0001B563 File Offset: 0x00019763
		public string TechnicalDetailsText_DEPRECATED { get; set; }

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0001B56C File Offset: 0x0001976C
		// (set) Token: 0x06001A01 RID: 6657 RVA: 0x0001B574 File Offset: 0x00019774
		public string CurrencyCode { get; set; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x0001B57D File Offset: 0x0001977D
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x0001B585 File Offset: 0x00019785
		public Result PriceResult { get; set; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x0001B58E File Offset: 0x0001978E
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x0001B596 File Offset: 0x00019796
		public uint OriginalPrice_DEPRECATED { get; set; }

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x0001B59F File Offset: 0x0001979F
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x0001B5A7 File Offset: 0x000197A7
		public uint CurrentPrice_DEPRECATED { get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x0001B5B0 File Offset: 0x000197B0
		// (set) Token: 0x06001A09 RID: 6665 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public byte DiscountPercentage { get; set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001A0A RID: 6666 RVA: 0x0001B5C1 File Offset: 0x000197C1
		// (set) Token: 0x06001A0B RID: 6667 RVA: 0x0001B5C9 File Offset: 0x000197C9
		public long ExpirationTimestamp { get; set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x0001B5D2 File Offset: 0x000197D2
		// (set) Token: 0x06001A0D RID: 6669 RVA: 0x0001B5DA File Offset: 0x000197DA
		public uint PurchasedCount { get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x0001B5E3 File Offset: 0x000197E3
		// (set) Token: 0x06001A0F RID: 6671 RVA: 0x0001B5EB File Offset: 0x000197EB
		public int PurchaseLimit { get; set; }

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x0001B5F4 File Offset: 0x000197F4
		// (set) Token: 0x06001A11 RID: 6673 RVA: 0x0001B5FC File Offset: 0x000197FC
		public bool AvailableForPurchase { get; set; }

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0001B605 File Offset: 0x00019805
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x0001B60D File Offset: 0x0001980D
		public ulong OriginalPrice64 { get; set; }

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0001B616 File Offset: 0x00019816
		// (set) Token: 0x06001A15 RID: 6677 RVA: 0x0001B61E File Offset: 0x0001981E
		public ulong CurrentPrice64 { get; set; }

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001A16 RID: 6678 RVA: 0x0001B627 File Offset: 0x00019827
		// (set) Token: 0x06001A17 RID: 6679 RVA: 0x0001B62F File Offset: 0x0001982F
		public uint DecimalPoint { get; set; }

		// Token: 0x06001A18 RID: 6680 RVA: 0x0001B638 File Offset: 0x00019838
		internal void Set(CatalogOfferInternal? other)
		{
			if (other != null)
			{
				this.ServerIndex = other.Value.ServerIndex;
				this.CatalogNamespace = other.Value.CatalogNamespace;
				this.Id = other.Value.Id;
				this.TitleText = other.Value.TitleText;
				this.DescriptionText = other.Value.DescriptionText;
				this.LongDescriptionText = other.Value.LongDescriptionText;
				this.TechnicalDetailsText_DEPRECATED = other.Value.TechnicalDetailsText_DEPRECATED;
				this.CurrencyCode = other.Value.CurrencyCode;
				this.PriceResult = other.Value.PriceResult;
				this.OriginalPrice_DEPRECATED = other.Value.OriginalPrice_DEPRECATED;
				this.CurrentPrice_DEPRECATED = other.Value.CurrentPrice_DEPRECATED;
				this.DiscountPercentage = other.Value.DiscountPercentage;
				this.ExpirationTimestamp = other.Value.ExpirationTimestamp;
				this.PurchasedCount = other.Value.PurchasedCount;
				this.PurchaseLimit = other.Value.PurchaseLimit;
				this.AvailableForPurchase = other.Value.AvailableForPurchase;
				this.OriginalPrice64 = other.Value.OriginalPrice64;
				this.CurrentPrice64 = other.Value.CurrentPrice64;
				this.DecimalPoint = other.Value.DecimalPoint;
			}
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0001B7E0 File Offset: 0x000199E0
		public void Set(object other)
		{
			this.Set(other as CatalogOfferInternal?);
		}
	}
}
