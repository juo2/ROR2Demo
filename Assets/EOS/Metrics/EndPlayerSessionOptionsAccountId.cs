using System;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x020002E2 RID: 738
	public class EndPlayerSessionOptionsAccountId : ISettable
	{
		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00013EC4 File Offset: 0x000120C4
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x00013ECC File Offset: 0x000120CC
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

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x00013ED8 File Offset: 0x000120D8
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x00013EFB File Offset: 0x000120FB
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

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x00013F14 File Offset: 0x00012114
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x00013F37 File Offset: 0x00012137
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

		// Token: 0x060012B9 RID: 4793 RVA: 0x00013F4E File Offset: 0x0001214E
		public static implicit operator EndPlayerSessionOptionsAccountId(EpicAccountId value)
		{
			return new EndPlayerSessionOptionsAccountId
			{
				Epic = value
			};
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00013F5C File Offset: 0x0001215C
		public static implicit operator EndPlayerSessionOptionsAccountId(string value)
		{
			return new EndPlayerSessionOptionsAccountId
			{
				External = value
			};
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x00013F6C File Offset: 0x0001216C
		internal void Set(EndPlayerSessionOptionsAccountIdInternal? other)
		{
			if (other != null)
			{
				this.Epic = other.Value.Epic;
				this.External = other.Value.External;
			}
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00013FAC File Offset: 0x000121AC
		public void Set(object other)
		{
			this.Set(other as EndPlayerSessionOptionsAccountIdInternal?);
		}

		// Token: 0x040008C5 RID: 2245
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x040008C6 RID: 2246
		private EpicAccountId m_Epic;

		// Token: 0x040008C7 RID: 2247
		private string m_External;
	}
}
