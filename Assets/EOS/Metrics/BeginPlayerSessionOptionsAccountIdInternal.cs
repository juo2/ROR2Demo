using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x020002DF RID: 735
	[StructLayout(LayoutKind.Explicit, Pack = 4)]
	internal struct BeginPlayerSessionOptionsAccountIdInternal : ISettable, IDisposable
	{
		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060012A5 RID: 4773 RVA: 0x00013D9C File Offset: 0x00011F9C
		// (set) Token: 0x060012A6 RID: 4774 RVA: 0x00013DBF File Offset: 0x00011FBF
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
				Helper.TryMarshalSet<MetricsAccountIdType>(ref this.m_Epic, value, ref this.m_AccountIdType, MetricsAccountIdType.Epic, this);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00013DE0 File Offset: 0x00011FE0
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00013E03 File Offset: 0x00012003
		public string External
		{
			get
			{
				string result;
				Helper.TryMarshalGet<MetricsAccountIdType>(this.m_External, out result, this.m_AccountIdType, MetricsAccountIdType.External);
				return result;
			}
			set
			{
				Helper.TryMarshalSet<MetricsAccountIdType>(ref this.m_External, value, ref this.m_AccountIdType, MetricsAccountIdType.External, this);
			}
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00013E24 File Offset: 0x00012024
		public void Set(BeginPlayerSessionOptionsAccountId other)
		{
			if (other != null)
			{
				this.Epic = other.Epic;
				this.External = other.External;
			}
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00013E41 File Offset: 0x00012041
		public void Set(object other)
		{
			this.Set(other as BeginPlayerSessionOptionsAccountId);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00013E4F File Offset: 0x0001204F
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Epic);
			Helper.TryMarshalDispose<MetricsAccountIdType>(ref this.m_External, this.m_AccountIdType, MetricsAccountIdType.External);
		}

		// Token: 0x040008BF RID: 2239
		[FieldOffset(0)]
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x040008C0 RID: 2240
		[FieldOffset(4)]
		private IntPtr m_Epic;

		// Token: 0x040008C1 RID: 2241
		[FieldOffset(4)]
		private IntPtr m_External;
	}
}
