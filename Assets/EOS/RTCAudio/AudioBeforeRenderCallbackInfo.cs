using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000170 RID: 368
	public class AudioBeforeRenderCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0000AF21 File Offset: 0x00009121
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x0000AF29 File Offset: 0x00009129
		public object ClientData { get; private set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x0000AF32 File Offset: 0x00009132
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0000AF3A File Offset: 0x0000913A
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0000AF43 File Offset: 0x00009143
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0000AF4B File Offset: 0x0000914B
		public string RoomName { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x0000AF54 File Offset: 0x00009154
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0000AF5C File Offset: 0x0000915C
		public AudioBuffer Buffer { get; private set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x0000AF65 File Offset: 0x00009165
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x0000AF6D File Offset: 0x0000916D
		public ProductUserId ParticipantId { get; private set; }

		// Token: 0x060009FA RID: 2554 RVA: 0x0000AF78 File Offset: 0x00009178
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0000AF90 File Offset: 0x00009190
		internal void Set(AudioBeforeRenderCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Buffer = other.Value.Buffer;
				this.ParticipantId = other.Value.ParticipantId;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000B00F File Offset: 0x0000920F
		public void Set(object other)
		{
			this.Set(other as AudioBeforeRenderCallbackInfoInternal?);
		}
	}
}
