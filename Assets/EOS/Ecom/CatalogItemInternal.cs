using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000432 RID: 1074
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CatalogItemInternal : ISettable, IDisposable
	{
		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x0001B264 File Offset: 0x00019464
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x0001B280 File Offset: 0x00019480
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

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0001B290 File Offset: 0x00019490
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x0001B2AC File Offset: 0x000194AC
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

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0001B2BC File Offset: 0x000194BC
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x0001B2D8 File Offset: 0x000194D8
		public string EntitlementName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_EntitlementName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_EntitlementName, value);
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0001B2E8 File Offset: 0x000194E8
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x0001B304 File Offset: 0x00019504
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

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0001B314 File Offset: 0x00019514
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x0001B330 File Offset: 0x00019530
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

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0001B340 File Offset: 0x00019540
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x0001B35C File Offset: 0x0001955C
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

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0001B36C File Offset: 0x0001956C
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x0001B388 File Offset: 0x00019588
		public string TechnicalDetailsText
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_TechnicalDetailsText, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_TechnicalDetailsText, value);
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0001B398 File Offset: 0x00019598
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x0001B3B4 File Offset: 0x000195B4
		public string DeveloperText
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DeveloperText, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DeveloperText, value);
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0001B3C3 File Offset: 0x000195C3
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x0001B3CB File Offset: 0x000195CB
		public EcomItemType ItemType
		{
			get
			{
				return this.m_ItemType;
			}
			set
			{
				this.m_ItemType = value;
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x0001B3D4 File Offset: 0x000195D4
		// (set) Token: 0x060019EE RID: 6638 RVA: 0x0001B3DC File Offset: 0x000195DC
		public long EntitlementEndTimestamp
		{
			get
			{
				return this.m_EntitlementEndTimestamp;
			}
			set
			{
				this.m_EntitlementEndTimestamp = value;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0001B3E8 File Offset: 0x000195E8
		public void Set(CatalogItem other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.CatalogNamespace = other.CatalogNamespace;
				this.Id = other.Id;
				this.EntitlementName = other.EntitlementName;
				this.TitleText = other.TitleText;
				this.DescriptionText = other.DescriptionText;
				this.LongDescriptionText = other.LongDescriptionText;
				this.TechnicalDetailsText = other.TechnicalDetailsText;
				this.DeveloperText = other.DeveloperText;
				this.ItemType = other.ItemType;
				this.EntitlementEndTimestamp = other.EntitlementEndTimestamp;
			}
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0001B477 File Offset: 0x00019677
		public void Set(object other)
		{
			this.Set(other as CatalogItem);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x0001B488 File Offset: 0x00019688
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_CatalogNamespace);
			Helper.TryMarshalDispose(ref this.m_Id);
			Helper.TryMarshalDispose(ref this.m_EntitlementName);
			Helper.TryMarshalDispose(ref this.m_TitleText);
			Helper.TryMarshalDispose(ref this.m_DescriptionText);
			Helper.TryMarshalDispose(ref this.m_LongDescriptionText);
			Helper.TryMarshalDispose(ref this.m_TechnicalDetailsText);
			Helper.TryMarshalDispose(ref this.m_DeveloperText);
		}

		// Token: 0x04000BFE RID: 3070
		private int m_ApiVersion;

		// Token: 0x04000BFF RID: 3071
		private IntPtr m_CatalogNamespace;

		// Token: 0x04000C00 RID: 3072
		private IntPtr m_Id;

		// Token: 0x04000C01 RID: 3073
		private IntPtr m_EntitlementName;

		// Token: 0x04000C02 RID: 3074
		private IntPtr m_TitleText;

		// Token: 0x04000C03 RID: 3075
		private IntPtr m_DescriptionText;

		// Token: 0x04000C04 RID: 3076
		private IntPtr m_LongDescriptionText;

		// Token: 0x04000C05 RID: 3077
		private IntPtr m_TechnicalDetailsText;

		// Token: 0x04000C06 RID: 3078
		private IntPtr m_DeveloperText;

		// Token: 0x04000C07 RID: 3079
		private EcomItemType m_ItemType;

		// Token: 0x04000C08 RID: 3080
		private long m_EntitlementEndTimestamp;
	}
}
