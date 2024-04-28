using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200045A RID: 1114
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetEntitlementsByNameCountOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700083F RID: 2111
		// (set) Token: 0x06001B4A RID: 6986 RVA: 0x0001D109 File Offset: 0x0001B309
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x17000840 RID: 2112
		// (set) Token: 0x06001B4B RID: 6987 RVA: 0x0001D118 File Offset: 0x0001B318
		public string EntitlementName
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_EntitlementName, value);
			}
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0001D127 File Offset: 0x0001B327
		public void Set(GetEntitlementsByNameCountOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.EntitlementName = other.EntitlementName;
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x0001D14B File Offset: 0x0001B34B
		public void Set(object other)
		{
			this.Set(other as GetEntitlementsByNameCountOptions);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x0001D159 File Offset: 0x0001B359
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_EntitlementName);
		}

		// Token: 0x04000CCF RID: 3279
		private int m_ApiVersion;

		// Token: 0x04000CD0 RID: 3280
		private IntPtr m_LocalUserId;

		// Token: 0x04000CD1 RID: 3281
		private IntPtr m_EntitlementName;
	}
}
