using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200050B RID: 1291
	public class AccountFeatureRestrictedInfo : ISettable
	{
		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x00020D18 File Offset: 0x0001EF18
		// (set) Token: 0x06001F39 RID: 7993 RVA: 0x00020D20 File Offset: 0x0001EF20
		public string VerificationURI { get; set; }

		// Token: 0x06001F3A RID: 7994 RVA: 0x00020D2C File Offset: 0x0001EF2C
		internal void Set(AccountFeatureRestrictedInfoInternal? other)
		{
			if (other != null)
			{
				this.VerificationURI = other.Value.VerificationURI;
			}
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00020D57 File Offset: 0x0001EF57
		public void Set(object other)
		{
			this.Set(other as AccountFeatureRestrictedInfoInternal?);
		}
	}
}
