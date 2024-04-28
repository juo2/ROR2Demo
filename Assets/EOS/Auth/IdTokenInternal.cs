using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200051D RID: 1309
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct IdTokenInternal : ISettable, IDisposable
	{
		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x00021774 File Offset: 0x0001F974
		// (set) Token: 0x06001FA1 RID: 8097 RVA: 0x00021790 File Offset: 0x0001F990
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

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x000217A0 File Offset: 0x0001F9A0
		// (set) Token: 0x06001FA3 RID: 8099 RVA: 0x000217BC File Offset: 0x0001F9BC
		public string JsonWebToken
		{
			get
			{
				string result;
				Helper.TryMarshalGet(this.m_JsonWebToken, out result);
				return result;
			}
			set
			{
				Helper.TryMarshalSet(ref this.m_JsonWebToken, value);
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000217CB File Offset: 0x0001F9CB
		public void Set(IdToken other)
		{
			if (other != null)
			{
				this.m_ApiVersion = 1;
				this.AccountId = other.AccountId;
				this.JsonWebToken = other.JsonWebToken;
			}
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x000217EF File Offset: 0x0001F9EF
		public void Set(object other)
		{
			this.Set(other as IdToken);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000217FD File Offset: 0x0001F9FD
		public void Dispose()
		{
			Helper.TryMarshalDispose(ref this.m_AccountId);
			Helper.TryMarshalDispose(ref this.m_JsonWebToken);
		}

		// Token: 0x04000EA0 RID: 3744
		private int m_ApiVersion;

		// Token: 0x04000EA1 RID: 3745
		private IntPtr m_AccountId;

		// Token: 0x04000EA2 RID: 3746
		private IntPtr m_JsonWebToken;
	}
}
