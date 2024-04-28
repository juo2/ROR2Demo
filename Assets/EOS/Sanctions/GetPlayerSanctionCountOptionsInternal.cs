using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000152 RID: 338
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetPlayerSanctionCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x17000229 RID: 553
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x0000A53B File Offset: 0x0000873B
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_TargetUserId, value);
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0000A54A File Offset: 0x0000874A
		public void Set(GetPlayerSanctionCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.TargetUserId;
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0000A562 File Offset: 0x00008762
		public void Set(object other)
		{
			this.Set(other as GetPlayerSanctionCountOptions);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0000A570 File Offset: 0x00008770
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000476 RID: 1142
		private int m_ApiVersion;

		// Token: 0x04000477 RID: 1143
		private IntPtr m_TargetUserId;
	}
}
