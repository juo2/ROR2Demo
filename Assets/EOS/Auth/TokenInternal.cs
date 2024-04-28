using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000545 RID: 1349
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TokenInternal : ISettable, IDisposable
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x00022460 File Offset: 0x00020660
		// (set) Token: 0x0600209B RID: 8347 RVA: 0x0002247C File Offset: 0x0002067C
		public string App
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_App, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_App, value);
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x0002248C File Offset: 0x0002068C
		// (set) Token: 0x0600209D RID: 8349 RVA: 0x000224A8 File Offset: 0x000206A8
		public string ClientId
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ClientId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ClientId, value);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x000224B8 File Offset: 0x000206B8
		// (set) Token: 0x0600209F RID: 8351 RVA: 0x000224D4 File Offset: 0x000206D4
		public EpicAccountId AccountId
		{
			get
			{
				EpicAccountId result;
				Helper.TryMarshalGet<EpicAccountId>(this.m_AccountId, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AccountId, value);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x000224E4 File Offset: 0x000206E4
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x00022500 File Offset: 0x00020700
		public string AccessToken
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_AccessToken, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_AccessToken, value);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x0002250F File Offset: 0x0002070F
		// (set) Token: 0x060020A3 RID: 8355 RVA: 0x00022517 File Offset: 0x00020717
		public double ExpiresIn
		{
			get
			{
				return this.m_ExpiresIn;
			}
			set
			{
				this.m_ExpiresIn = value;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00022520 File Offset: 0x00020720
		// (set) Token: 0x060020A5 RID: 8357 RVA: 0x0002253C File Offset: 0x0002073C
		public string ExpiresAt
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_ExpiresAt, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_ExpiresAt, value);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x0002254B File Offset: 0x0002074B
		// (set) Token: 0x060020A7 RID: 8359 RVA: 0x00022553 File Offset: 0x00020753
		public AuthTokenType AuthType
		{
			get
			{
				return this.m_AuthType;
			}
			set
			{
				this.m_AuthType = value;
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x0002255C File Offset: 0x0002075C
		// (set) Token: 0x060020A9 RID: 8361 RVA: 0x00022578 File Offset: 0x00020778
		public string RefreshToken
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RefreshToken, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_RefreshToken, value);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x00022587 File Offset: 0x00020787
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x0002258F File Offset: 0x0002078F
		public double RefreshExpiresIn
		{
			get
			{
				return this.m_RefreshExpiresIn;
			}
			set
			{
				this.m_RefreshExpiresIn = value;
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x00022598 File Offset: 0x00020798
		// (set) Token: 0x060020AD RID: 8365 RVA: 0x000225B4 File Offset: 0x000207B4
		public string RefreshExpiresAt
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_RefreshExpiresAt, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_RefreshExpiresAt, value);
			}
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x000225C4 File Offset: 0x000207C4
		public void Set(Token other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.App = other.App;
				this.ClientId = other.ClientId;
				this.AccountId = other.AccountId;
				this.AccessToken = other.AccessToken;
				this.ExpiresIn = other.ExpiresIn;
				this.ExpiresAt = other.ExpiresAt;
				this.AuthType = other.AuthType;
				this.RefreshToken = other.RefreshToken;
				this.RefreshExpiresIn = other.RefreshExpiresIn;
				this.RefreshExpiresAt = other.RefreshExpiresAt;
			}
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00022653 File Offset: 0x00020853
		public void Set(object other)
		{
			this.Set(other as Token);
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00022664 File Offset: 0x00020864
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_App);
			Helper.TryMarshalDispose(ref this.m_ClientId);
			Helper.TryMarshalDispose(ref this.m_AccountId);
			Helper.TryMarshalDispose(ref this.m_AccessToken);
			Helper.TryMarshalDispose(ref this.m_ExpiresAt);
			Helper.TryMarshalDispose(ref this.m_RefreshToken);
			Helper.TryMarshalDispose(ref this.m_RefreshExpiresAt);
		}

		// Token: 0x04000F04 RID: 3844
		private int m_ApiVersion;

		// Token: 0x04000F05 RID: 3845
		private IntPtr m_App;

		// Token: 0x04000F06 RID: 3846
		private IntPtr m_ClientId;

		// Token: 0x04000F07 RID: 3847
		private IntPtr m_AccountId;

		// Token: 0x04000F08 RID: 3848
		private IntPtr m_AccessToken;

		// Token: 0x04000F09 RID: 3849
		private double m_ExpiresIn;

		// Token: 0x04000F0A RID: 3850
		private IntPtr m_ExpiresAt;

		// Token: 0x04000F0B RID: 3851
		private AuthTokenType m_AuthType;

		// Token: 0x04000F0C RID: 3852
		private IntPtr m_RefreshToken;

		// Token: 0x04000F0D RID: 3853
		private double m_RefreshExpiresIn;

		// Token: 0x04000F0E RID: 3854
		private IntPtr m_RefreshExpiresAt;
	}
}
