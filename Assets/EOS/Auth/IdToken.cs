using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200051C RID: 1308
	public class IdToken : ISettable
	{
		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001F99 RID: 8089 RVA: 0x000216FC File Offset: 0x0001F8FC
		// (set) Token: 0x06001F9A RID: 8090 RVA: 0x00021704 File Offset: 0x0001F904
		public EpicAccountId AccountId { get; set; }

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0002170D File Offset: 0x0001F90D
		// (set) Token: 0x06001F9C RID: 8092 RVA: 0x00021715 File Offset: 0x0001F915
		public string JsonWebToken { get; set; }

		// Token: 0x06001F9D RID: 8093 RVA: 0x00021720 File Offset: 0x0001F920
		internal void Set(IdTokenInternal? other)
		{
			if (other != null)
			{
				this.AccountId = other.Value.AccountId;
				this.JsonWebToken = other.Value.JsonWebToken;
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00021760 File Offset: 0x0001F960
		public void Set(object other)
		{
			this.Set(other as IdTokenInternal?);
		}
	}
}
