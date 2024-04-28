using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x020002DE RID: 734
	public class BeginPlayerSessionOptionsAccountId : ISettable
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00013CA3 File Offset: 0x00011EA3
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x00013CAB File Offset: 0x00011EAB
		public MetricsAccountIdType AccountIdType
		{
			get
			{
				return this.m_AccountIdType;
			}
			private set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x00013CB4 File Offset: 0x00011EB4
		// (set) Token: 0x0600129D RID: 4765 RVA: 0x00013CD7 File Offset: 0x00011ED7
		public EpicAccountId Epic
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId, MetricsAccountIdType>(this.m_Epic, out result, this.m_AccountIdType, MetricsAccountIdType.Epic);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<EpicAccountId, MetricsAccountIdType>(ref this.m_Epic, value, ref this.m_AccountIdType, MetricsAccountIdType.Epic, null);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x00013CF0 File Offset: 0x00011EF0
		// (set) Token: 0x0600129F RID: 4767 RVA: 0x00013D13 File Offset: 0x00011F13
		public string External
		{
			get
			{
				string result;
				Helper.TryMarshalGet<string, MetricsAccountIdType>(this.m_External, out result, this.m_AccountIdType, MetricsAccountIdType.External);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<string, MetricsAccountIdType>(ref this.m_External, value, ref this.m_AccountIdType, MetricsAccountIdType.External, null);
			}
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00013D2A File Offset: 0x00011F2A
		public static implicit operator BeginPlayerSessionOptionsAccountId(EpicAccountId value)
		{
			return new BeginPlayerSessionOptionsAccountId
			{
				Epic = value
			};
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00013D38 File Offset: 0x00011F38
		public static implicit operator BeginPlayerSessionOptionsAccountId(string value)
		{
			return new BeginPlayerSessionOptionsAccountId
			{
				External = value
			};
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00013D48 File Offset: 0x00011F48
		internal void Set(BeginPlayerSessionOptionsAccountIdInternal? other)
		{
			if (other != null)
			{
				this.Epic = other.Value.Epic;
				this.External = other.Value.External;
			}
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00013D88 File Offset: 0x00011F88
		public void Set(object other)
		{
			this.Set(other as BeginPlayerSessionOptionsAccountIdInternal?);
		}

		// Token: 0x040008BC RID: 2236
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x040008BD RID: 2237
		private EpicAccountId m_Epic;

		// Token: 0x040008BE RID: 2238
		private string m_External;
	}
}
