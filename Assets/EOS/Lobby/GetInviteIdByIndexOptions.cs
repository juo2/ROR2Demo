using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000314 RID: 788
	public class GetInviteIdByIndexOptions
	{
		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x00014EC0 File Offset: 0x000130C0
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x00014EC8 File Offset: 0x000130C8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00014ED1 File Offset: 0x000130D1
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00014ED9 File Offset: 0x000130D9
		public uint Index { get; set; }
	}
}
