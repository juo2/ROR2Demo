using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000458 RID: 1112
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EntitlementInternal : ISettable, IDisposable
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06001B36 RID: 6966 RVA: 0x0001CF84 File Offset: 0x0001B184
		// (set) Token: 0x06001B37 RID: 6967 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
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

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06001B38 RID: 6968 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
		// (set) Token: 0x06001B39 RID: 6969 RVA: 0x0001CFCC File Offset: 0x0001B1CC
		public string EntitlementId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_EntitlementId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_EntitlementId, value);
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06001B3A RID: 6970 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		// (set) Token: 0x06001B3B RID: 6971 RVA: 0x0001CFF8 File Offset: 0x0001B1F8
		public string CatalogItemId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_CatalogItemId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_CatalogItemId, value);
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06001B3C RID: 6972 RVA: 0x0001D007 File Offset: 0x0001B207
		// (set) Token: 0x06001B3D RID: 6973 RVA: 0x0001D00F File Offset: 0x0001B20F
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

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001B3E RID: 6974 RVA: 0x0001D018 File Offset: 0x0001B218
		// (set) Token: 0x06001B3F RID: 6975 RVA: 0x0001D034 File Offset: 0x0001B234
		public bool Redeemed
		{
			get
			{
				bool result;
				Helper.TryMarshalGet(this.m_Redeemed, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Redeemed, value);
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x0001D043 File Offset: 0x0001B243
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x0001D04B File Offset: 0x0001B24B
		public long EndTimestamp
		{
			get
			{
				return this.m_EndTimestamp;
			}
			set
			{
				this.m_EndTimestamp = value;
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x0001D054 File Offset: 0x0001B254
		public void Set(Entitlement other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.EntitlementName = other.EntitlementName;
				this.EntitlementId = other.EntitlementId;
				this.CatalogItemId = other.CatalogItemId;
				this.ServerIndex = other.ServerIndex;
				this.Redeemed = other.Redeemed;
				this.EndTimestamp = other.EndTimestamp;
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x0001D0B3 File Offset: 0x0001B2B3
		public void Set(object other)
		{
			this.Set(other as Entitlement);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x0001D0C1 File Offset: 0x0001B2C1
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_EntitlementName);
			Helper.TryMarshalDispose(ref this.m_EntitlementId);
			Helper.TryMarshalDispose(ref this.m_CatalogItemId);
		}

		// Token: 0x04000CC6 RID: 3270
		private int m_ApiVersion;

		// Token: 0x04000CC7 RID: 3271
		private IntPtr m_EntitlementName;

		// Token: 0x04000CC8 RID: 3272
		private IntPtr m_EntitlementId;

		// Token: 0x04000CC9 RID: 3273
		private IntPtr m_CatalogItemId;

		// Token: 0x04000CCA RID: 3274
		private int m_ServerIndex;

		// Token: 0x04000CCB RID: 3275
		private int m_Redeemed;

		// Token: 0x04000CCC RID: 3276
		private long m_EndTimestamp;
	}
}
