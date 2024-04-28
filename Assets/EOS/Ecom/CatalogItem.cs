using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000431 RID: 1073
	public class CatalogItem : ISettable
	{
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0001B0B8 File Offset: 0x000192B8
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x0001B0C0 File Offset: 0x000192C0
		public string CatalogNamespace { get; set; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060019C6 RID: 6598 RVA: 0x0001B0C9 File Offset: 0x000192C9
		// (set) Token: 0x060019C7 RID: 6599 RVA: 0x0001B0D1 File Offset: 0x000192D1
		public string Id { get; set; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x0001B0DA File Offset: 0x000192DA
		// (set) Token: 0x060019C9 RID: 6601 RVA: 0x0001B0E2 File Offset: 0x000192E2
		public string EntitlementName { get; set; }

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x0001B0EB File Offset: 0x000192EB
		// (set) Token: 0x060019CB RID: 6603 RVA: 0x0001B0F3 File Offset: 0x000192F3
		public string TitleText { get; set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x0001B0FC File Offset: 0x000192FC
		// (set) Token: 0x060019CD RID: 6605 RVA: 0x0001B104 File Offset: 0x00019304
		public string DescriptionText { get; set; }

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x0001B10D File Offset: 0x0001930D
		// (set) Token: 0x060019CF RID: 6607 RVA: 0x0001B115 File Offset: 0x00019315
		public string LongDescriptionText { get; set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x0001B11E File Offset: 0x0001931E
		// (set) Token: 0x060019D1 RID: 6609 RVA: 0x0001B126 File Offset: 0x00019326
		public string TechnicalDetailsText { get; set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0001B12F File Offset: 0x0001932F
		// (set) Token: 0x060019D3 RID: 6611 RVA: 0x0001B137 File Offset: 0x00019337
		public string DeveloperText { get; set; }

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0001B140 File Offset: 0x00019340
		// (set) Token: 0x060019D5 RID: 6613 RVA: 0x0001B148 File Offset: 0x00019348
		public EcomItemType ItemType { get; set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x0001B151 File Offset: 0x00019351
		// (set) Token: 0x060019D7 RID: 6615 RVA: 0x0001B159 File Offset: 0x00019359
		public long EntitlementEndTimestamp { get; set; }

		// Token: 0x060019D8 RID: 6616 RVA: 0x0001B164 File Offset: 0x00019364
		internal void Set(CatalogItemInternal? other)
		{
			if (other != null)
			{
				this.CatalogNamespace = other.Value.CatalogNamespace;
				this.Id = other.Value.Id;
				this.EntitlementName = other.Value.EntitlementName;
				this.TitleText = other.Value.TitleText;
				this.DescriptionText = other.Value.DescriptionText;
				this.LongDescriptionText = other.Value.LongDescriptionText;
				this.TechnicalDetailsText = other.Value.TechnicalDetailsText;
				this.DeveloperText = other.Value.DeveloperText;
				this.ItemType = other.Value.ItemType;
				this.EntitlementEndTimestamp = other.Value.EntitlementEndTimestamp;
			}
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0001B24F File Offset: 0x0001944F
		public void Set(object other)
		{
			this.Set(other as CatalogItemInternal?);
		}
	}
}
