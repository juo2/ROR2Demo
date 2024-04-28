using System;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000447 RID: 1095
	public class CopyItemReleaseByIndexOptions
	{
		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001AB6 RID: 6838 RVA: 0x0001C2C7 File Offset: 0x0001A4C7
		// (set) Token: 0x06001AB7 RID: 6839 RVA: 0x0001C2CF File Offset: 0x0001A4CF
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001AB8 RID: 6840 RVA: 0x0001C2D8 File Offset: 0x0001A4D8
		// (set) Token: 0x06001AB9 RID: 6841 RVA: 0x0001C2E0 File Offset: 0x0001A4E0
		public string ItemId { get; set; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001ABA RID: 6842 RVA: 0x0001C2E9 File Offset: 0x0001A4E9
		// (set) Token: 0x06001ABB RID: 6843 RVA: 0x0001C2F1 File Offset: 0x0001A4F1
		public uint ReleaseIndex { get; set; }
	}
}
