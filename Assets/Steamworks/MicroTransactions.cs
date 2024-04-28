using System;
using SteamNative;

namespace Facepunch.Steamworks
{
	// Token: 0x0200016A RID: 362
	public class MicroTransactions : IDisposable
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000AEC RID: 2796 RVA: 0x0003682C File Offset: 0x00034A2C
		// (remove) Token: 0x06000AED RID: 2797 RVA: 0x00036864 File Offset: 0x00034A64
		public event MicroTransactions.AuthorizationResponse OnAuthorizationResponse;

		// Token: 0x06000AEE RID: 2798 RVA: 0x00036899 File Offset: 0x00034A99
		internal MicroTransactions(Client c)
		{
			this.client = c;
			this.client.RegisterCallback<MicroTxnAuthorizationResponse_t>(new Action<MicroTxnAuthorizationResponse_t>(this.onMicroTxnAuthorizationResponse));
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000368BF File Offset: 0x00034ABF
		private void onMicroTxnAuthorizationResponse(MicroTxnAuthorizationResponse_t arg1)
		{
			if (this.OnAuthorizationResponse != null)
			{
				this.OnAuthorizationResponse(arg1.Authorized == 1, (int)arg1.AppID, arg1.OrderID);
			}
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000368E9 File Offset: 0x00034AE9
		public void Dispose()
		{
			this.client = null;
		}

		// Token: 0x04000817 RID: 2071
		internal Client client;

		// Token: 0x02000273 RID: 627
		// (Invoke) Token: 0x06001DF7 RID: 7671
		public delegate void AuthorizationResponse(bool authorized, int appId, ulong orderId);
	}
}
