using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200047D RID: 1149
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryEntitlementsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x1700086D RID: 2157
		// (set) Token: 0x06001BF5 RID: 7157 RVA: 0x0001D85B File Offset: 0x0001BA5B
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x1700086E RID: 2158
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x0001D86A File Offset: 0x0001BA6A
		public string[] EntitlementNames
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementNames, value, out this.m_EntitlementNameCount);
			}
		}

		// Token: 0x1700086F RID: 2159
		// (set) Token: 0x06001BF7 RID: 7159 RVA: 0x0001D87F File Offset: 0x0001BA7F
		public bool IncludeRedeemed
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_IncludeRedeemed, value);
			}
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0001D88E File Offset: 0x0001BA8E
		public void Set(QueryEntitlementsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.LocalUserId = other.LocalUserId;
				this.EntitlementNames = other.EntitlementNames;
				this.IncludeRedeemed = other.IncludeRedeemed;
			}
		}

		// Token: 0x06001BF9 RID: 7161 RVA: 0x0001D8BE File Offset: 0x0001BABE
		public void Set(object other)
		{
			this.Set(other as QueryEntitlementsOptions);
		}

		// Token: 0x06001BFA RID: 7162 RVA: 0x0001D8CC File Offset: 0x0001BACC
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_EntitlementNames);
		}

		// Token: 0x04000D09 RID: 3337
		private int m_ApiVersion;

		// Token: 0x04000D0A RID: 3338
		private IntPtr m_LocalUserId;

		// Token: 0x04000D0B RID: 3339
		private IntPtr m_EntitlementNames;

		// Token: 0x04000D0C RID: 3340
		private uint m_EntitlementNameCount;

		// Token: 0x04000D0D RID: 3341
		private int m_IncludeRedeemed;
	}
}
