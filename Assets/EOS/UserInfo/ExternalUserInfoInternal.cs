using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000028 RID: 40
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ExternalUserInfoInternal : ISettable, IDisposable
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00003DDC File Offset: 0x00001FDC
		// (set) Token: 0x060002EB RID: 747 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public ExternalAccountType AccountType
		{
			get
			{
				return this.m_AccountType;
			}
			set
			{
				this.m_AccountType = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00003DF0 File Offset: 0x00001FF0
		// (set) Token: 0x060002ED RID: 749 RVA: 0x00003E0C File Offset: 0x0000200C
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

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00003E1C File Offset: 0x0000201C
		// (set) Token: 0x060002EF RID: 751 RVA: 0x00003E38 File Offset: 0x00002038
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

		// Token: 0x060002F0 RID: 752 RVA: 0x00003E47 File Offset: 0x00002047
		public void Set(ExternalUserInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AccountType = other.AccountType;
				this.AccountId = other.AccountId;
				this.DisplayName = other.DisplayName;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00003E77 File Offset: 0x00002077
		public void Set(object other)
		{
			this.Set(other as ExternalUserInfo);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00003E85 File Offset: 0x00002085
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AccountId);
			Helper.TryMarshalDispose(ref this.m_DisplayName);
		}

		// Token: 0x0400013C RID: 316
		private int m_ApiVersion;

		// Token: 0x0400013D RID: 317
		private ExternalAccountType m_AccountType;

		// Token: 0x0400013E RID: 318
		private IntPtr m_AccountId;

		// Token: 0x0400013F RID: 319
		private IntPtr m_DisplayName;
	}
}
