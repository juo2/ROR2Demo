using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003E3 RID: 995
	public class CreateUserOptions
	{
		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x00019700 File Offset: 0x00017900
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x00019708 File Offset: 0x00017908
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00019711 File Offset: 0x00017911
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x00019719 File Offset: 0x00017919
		public string DateOfBirth { get; set; }

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00019722 File Offset: 0x00017922
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x0001972A File Offset: 0x0001792A
		public string ParentEmail { get; set; }
	}
}
