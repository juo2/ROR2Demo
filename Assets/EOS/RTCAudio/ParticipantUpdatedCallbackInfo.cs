using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000198 RID: 408
	public class ParticipantUpdatedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0000BA00 File Offset: 0x00009C00
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0000BA08 File Offset: 0x00009C08
		public object ClientData { get; private set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0000BA11 File Offset: 0x00009C11
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0000BA19 File Offset: 0x00009C19
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0000BA22 File Offset: 0x00009C22
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0000BA2A File Offset: 0x00009C2A
		public string RoomName { get; private set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0000BA33 File Offset: 0x00009C33
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0000BA3B File Offset: 0x00009C3B
		public ProductUserId ParticipantId { get; private set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0000BA44 File Offset: 0x00009C44
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0000BA4C File Offset: 0x00009C4C
		public bool Speaking { get; private set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0000BA55 File Offset: 0x00009C55
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0000BA5D File Offset: 0x00009C5D
		public RTCAudioStatus AudioStatus { get; private set; }

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0000BA68 File Offset: 0x00009C68
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0000BA80 File Offset: 0x00009C80
		internal void Set(ParticipantUpdatedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.ParticipantId = other.Value.ParticipantId;
				this.Speaking = other.Value.Speaking;
				this.AudioStatus = other.Value.AudioStatus;
			}
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x0000BB14 File Offset: 0x00009D14
		public void Set(object other)
		{
			this.Set(other as ParticipantUpdatedCallbackInfoInternal?);
		}
	}
}
