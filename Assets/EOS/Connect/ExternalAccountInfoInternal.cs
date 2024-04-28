using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004CA RID: 1226
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ExternalAccountInfoInternal : ISettable, IDisposable
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x0001FA0C File Offset: 0x0001DC0C
		// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x0001FA28 File Offset: 0x0001DC28
		public ProductUserId ProductUserId
		{
			get
			{
				ProductUserId result;
				Helper.TryMarshalGet<ProductUserId>(this.m_ProductUserId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ProductUserId, value);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001DBA RID: 7610 RVA: 0x0001FA38 File Offset: 0x0001DC38
		// (set) Token: 0x06001DBB RID: 7611 RVA: 0x0001FA54 File Offset: 0x0001DC54
		public string DisplayName
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_DisplayName, value);
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x0001FA64 File Offset: 0x0001DC64
		// (set) Token: 0x06001DBD RID: 7613 RVA: 0x0001FA80 File Offset: 0x0001DC80
		public string AccountId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_AccountId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AccountId, value);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x0001FA8F File Offset: 0x0001DC8F
		// (set) Token: 0x06001DBF RID: 7615 RVA: 0x0001FA97 File Offset: 0x0001DC97
		public ExternalAccountType AccountIdType
		{
			get
			{
				return this.m_AccountIdType;
			}
			set
			{
				this.m_AccountIdType = value;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001DC0 RID: 7616 RVA: 0x0001FAA0 File Offset: 0x0001DCA0
		// (set) Token: 0x06001DC1 RID: 7617 RVA: 0x0001FABC File Offset: 0x0001DCBC
		public DateTimeOffset? LastLoginTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.TryMarshalGet(this.m_LastLoginTime, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_LastLoginTime, value);
			}
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0001FACC File Offset: 0x0001DCCC
		public void Set(ExternalAccountInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.ProductUserId = other.ProductUserId;
				this.DisplayName = other.DisplayName;
				this.AccountId = other.AccountId;
				this.AccountIdType = other.AccountIdType;
				this.LastLoginTime = other.LastLoginTime;
			}
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0001FB1F File Offset: 0x0001DD1F
		public void Set(object other)
		{
			this.Set(other as ExternalAccountInfo);
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0001FB2D File Offset: 0x0001DD2D
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_ProductUserId);
			Helper.TryMarshalDispose(ref this.m_DisplayName);
			Helper.TryMarshalDispose(ref this.m_AccountId);
		}

		// Token: 0x04000DDE RID: 3550
		private int m_ApiVersion;

		// Token: 0x04000DDF RID: 3551
		private IntPtr m_ProductUserId;

		// Token: 0x04000DE0 RID: 3552
		private IntPtr m_DisplayName;

		// Token: 0x04000DE1 RID: 3553
		private IntPtr m_AccountId;

		// Token: 0x04000DE2 RID: 3554
		private ExternalAccountType m_AccountIdType;

		// Token: 0x04000DE3 RID: 3555
		private long m_LastLoginTime;
	}
}
