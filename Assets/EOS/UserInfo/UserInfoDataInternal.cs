using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200003E RID: 62
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserInfoDataInternal : ISettable, IDisposable
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00004634 File Offset: 0x00002834
		// (set) Token: 0x06000380 RID: 896 RVA: 0x00004650 File Offset: 0x00002850
		public EpicAccountId UserId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_UserId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserId, value);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00004660 File Offset: 0x00002860
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000467C File Offset: 0x0000287C
		public string Country
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Country, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Country, value);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000468C File Offset: 0x0000288C
		// (set) Token: 0x06000384 RID: 900 RVA: 0x000046A8 File Offset: 0x000028A8
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

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000385 RID: 901 RVA: 0x000046B8 File Offset: 0x000028B8
		// (set) Token: 0x06000386 RID: 902 RVA: 0x000046D4 File Offset: 0x000028D4
		public string PreferredLanguage
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_PreferredLanguage, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_PreferredLanguage, value);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000387 RID: 903 RVA: 0x000046E4 File Offset: 0x000028E4
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00004700 File Offset: 0x00002900
		public string Nickname
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_Nickname, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_Nickname, value);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00004710 File Offset: 0x00002910
		public void Set(UserInfoData other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.UserId = other.UserId;
				this.Country = other.Country;
				this.DisplayName = other.DisplayName;
				this.PreferredLanguage = other.PreferredLanguage;
				this.Nickname = other.Nickname;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00004763 File Offset: 0x00002963
		public void Set(object other)
		{
			this.Set(other as UserInfoData);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00004771 File Offset: 0x00002971
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserId);
			Helper.TryMarshalDispose(ref this.m_Country);
			Helper.TryMarshalDispose(ref this.m_DisplayName);
			Helper.TryMarshalDispose(ref this.m_PreferredLanguage);
			Helper.TryMarshalDispose(ref this.m_Nickname);
		}

		// Token: 0x04000179 RID: 377
		private int m_ApiVersion;

		// Token: 0x0400017A RID: 378
		private IntPtr m_UserId;

		// Token: 0x0400017B RID: 379
		private IntPtr m_Country;

		// Token: 0x0400017C RID: 380
		private IntPtr m_DisplayName;

		// Token: 0x0400017D RID: 381
		private IntPtr m_PreferredLanguage;

		// Token: 0x0400017E RID: 382
		private IntPtr m_Nickname;
	}
}
