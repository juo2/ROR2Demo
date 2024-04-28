using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200016A RID: 362
	public class AddNotifyAudioInputStateOptions
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x0000AD7D File Offset: 0x00008F7D
		// (set) Token: 0x060009D3 RID: 2515 RVA: 0x0000AD85 File Offset: 0x00008F85
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0000AD8E File Offset: 0x00008F8E
		// (set) Token: 0x060009D5 RID: 2517 RVA: 0x0000AD96 File Offset: 0x00008F96
		public string RoomName { get; set; }
	}
}
