using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200016C RID: 364
	public class AddNotifyAudioOutputStateOptions
	{
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x0000AE09 File Offset: 0x00009009
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x0000AE11 File Offset: 0x00009011
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x0000AE1A File Offset: 0x0000901A
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x0000AE22 File Offset: 0x00009022
		public string RoomName { get; set; }
	}
}
