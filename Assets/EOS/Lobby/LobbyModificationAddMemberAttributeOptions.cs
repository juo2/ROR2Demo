using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034D RID: 845
	public class LobbyModificationAddMemberAttributeOptions
	{
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0001712A File Offset: 0x0001532A
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x00017132 File Offset: 0x00015332
		public AttributeData Attribute { get; set; }

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0001713B File Offset: 0x0001533B
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x00017143 File Offset: 0x00015343
		public LobbyAttributeVisibility Visibility { get; set; }
	}
}
