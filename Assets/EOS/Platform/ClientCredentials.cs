using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x020005DE RID: 1502
	public class ClientCredentials : ISettable
	{
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002469 RID: 9321 RVA: 0x0002698F File Offset: 0x00024B8F
		// (set) Token: 0x0600246A RID: 9322 RVA: 0x00026997 File Offset: 0x00024B97
		public string ClientId { get; set; }

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600246B RID: 9323 RVA: 0x000269A0 File Offset: 0x00024BA0
		// (set) Token: 0x0600246C RID: 9324 RVA: 0x000269A8 File Offset: 0x00024BA8
		public string ClientSecret { get; set; }

		// Token: 0x0600246D RID: 9325 RVA: 0x000269B4 File Offset: 0x00024BB4
		internal void Set(ClientCredentialsInternal? other)
		{
			if (other != null)
			{
				this.ClientId = other.Value.ClientId;
				this.ClientSecret = other.Value.ClientSecret;
			}
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x000269F4 File Offset: 0x00024BF4
		public void Set(object other)
		{
			this.Set(other as ClientCredentialsInternal?);
		}
	}
}
