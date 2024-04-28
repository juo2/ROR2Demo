using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200011B RID: 283
	public class SessionModificationAddAttributeOptions
	{
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00008FB0 File Offset: 0x000071B0
		// (set) Token: 0x06000836 RID: 2102 RVA: 0x00008FB8 File Offset: 0x000071B8
		public AttributeData SessionAttribute { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00008FC1 File Offset: 0x000071C1
		// (set) Token: 0x06000838 RID: 2104 RVA: 0x00008FC9 File Offset: 0x000071C9
		public SessionAttributeAdvertisementType AdvertisementType { get; set; }
	}
}
