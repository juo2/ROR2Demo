using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200054E RID: 1358
	public class IOSCredentials : ISettable
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x00022B94 File Offset: 0x00020D94
		// (set) Token: 0x060020F7 RID: 8439 RVA: 0x00022B9C File Offset: 0x00020D9C
		public string Id { get; set; }

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00022BA5 File Offset: 0x00020DA5
		// (set) Token: 0x060020F9 RID: 8441 RVA: 0x00022BAD File Offset: 0x00020DAD
		public string Token { get; set; }

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x00022BB6 File Offset: 0x00020DB6
		// (set) Token: 0x060020FB RID: 8443 RVA: 0x00022BBE File Offset: 0x00020DBE
		public LoginCredentialType Type { get; set; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x00022BC7 File Offset: 0x00020DC7
		// (set) Token: 0x060020FD RID: 8445 RVA: 0x00022BCF File Offset: 0x00020DCF
		public IOSCredentialsSystemAuthCredentialsOptions SystemAuthCredentialsOptions { get; set; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x00022BD8 File Offset: 0x00020DD8
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x00022BE0 File Offset: 0x00020DE0
		public ExternalCredentialType ExternalType { get; set; }

		// Token: 0x06002100 RID: 8448 RVA: 0x00022BEC File Offset: 0x00020DEC
		internal void Set(IOSCredentialsInternal? other)
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

		// Token: 0x06002101 RID: 8449 RVA: 0x00022C6B File Offset: 0x00020E6B
		public void Set(object other)
		{
			this.Set(other as IOSCredentialsInternal?);
		}
	}
}
