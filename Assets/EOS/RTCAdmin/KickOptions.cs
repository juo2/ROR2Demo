using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001B6 RID: 438
	public class KickOptions
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0000CB40 File Offset: 0x0000AD40
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0000CB48 File Offset: 0x0000AD48
		public string RoomName { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0000CB51 File Offset: 0x0000AD51
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0000CB59 File Offset: 0x0000AD59
		public ProductUserId TargetUserId { get; set; }
	}
}
