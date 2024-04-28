using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200053F RID: 1343
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PinGrantInfoInternal : ISettable, IDisposable
	{
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x0600205D RID: 8285 RVA: 0x00021FF0 File Offset: 0x000201F0
		// (set) Token: 0x0600205E RID: 8286 RVA: 0x0002200C File Offset: 0x0002020C
		public string UserCode
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_UserCode, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_UserCode, value);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x0002201C File Offset: 0x0002021C
		// (set) Token: 0x06002060 RID: 8288 RVA: 0x00022038 File Offset: 0x00020238
		public string VerificationURI
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_VerificationURI, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_VerificationURI, value);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x00022047 File Offset: 0x00020247
		// (set) Token: 0x06002062 RID: 8290 RVA: 0x0002204F File Offset: 0x0002024F
		public int ExpiresIn
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

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002063 RID: 8291 RVA: 0x00022058 File Offset: 0x00020258
		// (set) Token: 0x06002064 RID: 8292 RVA: 0x00022074 File Offset: 0x00020274
		public string VerificationURIComplete
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_VerificationURIComplete, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_VerificationURIComplete, value);
			}
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00022083 File Offset: 0x00020283
		public void Set(PinGrantInfo other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 2;
				this.UserCode = other.UserCode;
				this.VerificationURI = other.VerificationURI;
				this.ExpiresIn = other.ExpiresIn;
				this.VerificationURIComplete = other.VerificationURIComplete;
			}
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000220BF File Offset: 0x000202BF
		public void Set(object other)
		{
			this.Set(other as PinGrantInfo);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x000220CD File Offset: 0x000202CD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_UserCode);
			Helper.TryMarshalDispose(ref this.m_VerificationURI);
			Helper.TryMarshalDispose(ref this.m_VerificationURIComplete);
		}

		// Token: 0x04000EE8 RID: 3816
		private int m_ApiVersion;

		// Token: 0x04000EE9 RID: 3817
		private IntPtr m_UserCode;

		// Token: 0x04000EEA RID: 3818
		private IntPtr m_VerificationURI;

		// Token: 0x04000EEB RID: 3819
		private int m_ExpiresIn;

		// Token: 0x04000EEC RID: 3820
		private IntPtr m_VerificationURIComplete;
	}
}
