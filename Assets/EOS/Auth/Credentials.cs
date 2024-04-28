using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000516 RID: 1302
	public class Credentials : ISettable
	{
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x000213FA File Offset: 0x0001F5FA
		// (set) Token: 0x06001F6E RID: 8046 RVA: 0x00021402 File Offset: 0x0001F602
		public string Id { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001F6F RID: 8047 RVA: 0x0002140B File Offset: 0x0001F60B
		// (set) Token: 0x06001F70 RID: 8048 RVA: 0x00021413 File Offset: 0x0001F613
		public string Token { get; set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001F71 RID: 8049 RVA: 0x0002141C File Offset: 0x0001F61C
		// (set) Token: 0x06001F72 RID: 8050 RVA: 0x00021424 File Offset: 0x0001F624
		public LoginCredentialType Type { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001F73 RID: 8051 RVA: 0x0002142D File Offset: 0x0001F62D
		// (set) Token: 0x06001F74 RID: 8052 RVA: 0x00021435 File Offset: 0x0001F635
		public IntPtr SystemAuthCredentialsOptions { get; set; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x0002143E File Offset: 0x0001F63E
		// (set) Token: 0x06001F76 RID: 8054 RVA: 0x00021446 File Offset: 0x0001F646
		public ExternalCredentialType ExternalType { get; set; }

		// Token: 0x06001F77 RID: 8055 RVA: 0x00021450 File Offset: 0x0001F650
		internal void Set(CredentialsInternal? other)
		{
			if (other != null)
			{
				this.Id = other.Value.Id;
				this.Token = other.Value.Token;
				this.Type = other.Value.Type;
				this.SystemAuthCredentialsOptions = other.Value.SystemAuthCredentialsOptions;
				this.ExternalType = other.Value.ExternalType;
			}
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x000214CF File Offset: 0x0001F6CF
		public void Set(object other)
		{
			this.Set(other as CredentialsInternal?);
		}
	}
}
