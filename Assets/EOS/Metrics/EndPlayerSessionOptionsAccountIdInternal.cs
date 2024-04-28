using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x020002E3 RID: 739
	[StructLayout(LayoutKind.Explicit, Pack = 4)]
	internal struct EndPlayerSessionOptionsAccountIdInternal : ISettable, IDisposable
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x00013FC0 File Offset: 0x000121C0
		// (set) Token: 0x060012BF RID: 4799 RVA: 0x00013FE3 File Offset: 0x000121E3
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

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00014004 File Offset: 0x00012204
		// (set) Token: 0x060012C1 RID: 4801 RVA: 0x00014027 File Offset: 0x00012227
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

		// Token: 0x060012C2 RID: 4802 RVA: 0x00014048 File Offset: 0x00012248
		public void Set(EndPlayerSessionOptionsAccountId other)
		{
			if (other != null)
			{
				this.Epic = other.Epic;
				this.External = other.External;
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00014065 File Offset: 0x00012265
		public void Set(object other)
		{
			this.Set(other as EndPlayerSessionOptionsAccountId);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00014073 File Offset: 0x00012273
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_Epic);
			Helper.TryMarshalDispose<MetricsAccountIdType>(ref this.m_External, this.m_AccountIdType, MetricsAccountIdType.External);
		}

		// Token: 0x040008C8 RID: 2248
		[FieldOffset(0)]
		private MetricsAccountIdType m_AccountIdType;

		// Token: 0x040008C9 RID: 2249
		[FieldOffset(4)]
		private IntPtr m_Epic;

		// Token: 0x040008CA RID: 2250
		[FieldOffset(4)]
		private IntPtr m_External;
	}
}
