using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x020002E1 RID: 737
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndPlayerSessionOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700056F RID: 1391
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00013E81 File Offset: 0x00012081
		public EndPlayerSessionOptionsAccountId AccountId
		{
			set
			{
				Helper.TryMarshalSet<EndPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId, value);
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00013E90 File Offset: 0x00012090
		public void Set(EndPlayerSessionOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AccountId = other.AccountId;
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00013EA8 File Offset: 0x000120A8
		public void Set(object other)
		{
			this.Set(other as EndPlayerSessionOptions);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00013EB6 File Offset: 0x000120B6
		public void Dispose()
		{
			Helper.TryMarshalDispose<EndPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId);
		}

		// Token: 0x040008C3 RID: 2243
		private int m_ApiVersion;

		// Token: 0x040008C4 RID: 2244
		private EndPlayerSessionOptionsAccountIdInternal m_AccountId;
	}
}
