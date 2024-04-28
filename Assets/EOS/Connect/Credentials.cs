using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004C3 RID: 1219
	public class Credentials : ISettable
	{
		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x0001F764 File Offset: 0x0001D964
		// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0001F76C File Offset: 0x0001D96C
		public string Token { get; set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0001F775 File Offset: 0x0001D975
		// (set) Token: 0x06001D91 RID: 7569 RVA: 0x0001F77D File Offset: 0x0001D97D
		public ExternalCredentialType Type { get; set; }

		// Token: 0x06001D92 RID: 7570 RVA: 0x0001F788 File Offset: 0x0001D988
		internal void Set(CredentialsInternal? other)
		{
			if (other != null)
			{
				this.Token = other.Value.Token;
				this.Type = other.Value.Type;
			}
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x0001F7C8 File Offset: 0x0001D9C8
		public void Set(object other)
		{
			this.Set(other as CredentialsInternal?);
		}
	}
}
