using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000164 RID: 356
	public class AddNotifyAudioBeforeRenderOptions
	{
		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0000AC1F File Offset: 0x00008E1F
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x0000AC27 File Offset: 0x00008E27
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0000AC30 File Offset: 0x00008E30
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x0000AC38 File Offset: 0x00008E38
		public string RoomName { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x0000AC41 File Offset: 0x00008E41
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x0000AC49 File Offset: 0x00008E49
		public bool UnmixedAudio { get; set; }
	}
}
