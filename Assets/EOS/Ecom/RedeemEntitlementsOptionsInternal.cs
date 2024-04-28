using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048D RID: 1165
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RedeemEntitlementsOptionsInternal : ISettable, IDisposable
	{
		// Token: 0x170008A2 RID: 2210
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x0001DF82 File Offset: 0x0001C182
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.TryMarshalSet(ref this.m_LocalUserId, value);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x0001DF91 File Offset: 0x0001C191
		public string[] EntitlementIds
		{
			set
			{
				Helper.TryMarshalSet<string>(ref this.m_EntitlementIds, value, out this.m_EntitlementIdCount);
			}
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x0001DFA6 File Offset: 0x0001C1A6
		public void Set(RedeemEntitlementsOptions other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.LocalUserId;
				this.EntitlementIds = other.EntitlementIds;
			}
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x0001DFCA File Offset: 0x0001C1CA
		public void Set(object other)
		{
			this.Set(other as RedeemEntitlementsOptions);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0001DFD8 File Offset: 0x0001C1D8
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_LocalUserId);
			Helper.TryMarshalDispose(ref this.m_EntitlementIds);
		}

		// Token: 0x04000D42 RID: 3394
		private int m_ApiVersion;

		// Token: 0x04000D43 RID: 3395
		private IntPtr m_LocalUserId;

		// Token: 0x04000D44 RID: 3396
		private uint m_EntitlementIdCount;

		// Token: 0x04000D45 RID: 3397
		private IntPtr m_EntitlementIds;
	}
}
