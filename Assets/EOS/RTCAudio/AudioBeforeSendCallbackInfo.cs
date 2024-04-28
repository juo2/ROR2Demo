using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x02000172 RID: 370
	public class AudioBeforeSendCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0000B0B8 File Offset: 0x000092B8
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0000B0C0 File Offset: 0x000092C0
		public object ClientData { get; private set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x0000B0C9 File Offset: 0x000092C9
		// (set) Token: 0x06000A07 RID: 2567 RVA: 0x0000B0D1 File Offset: 0x000092D1
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0000B0DA File Offset: 0x000092DA
		// (set) Token: 0x06000A09 RID: 2569 RVA: 0x0000B0E2 File Offset: 0x000092E2
		public string RoomName { get; private set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0000B0EB File Offset: 0x000092EB
		// (set) Token: 0x06000A0B RID: 2571 RVA: 0x0000B0F3 File Offset: 0x000092F3
		public AudioBuffer Buffer { get; private set; }

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000B0FC File Offset: 0x000092FC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0000B114 File Offset: 0x00009314
		internal void Set(AudioBeforeSendCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Buffer = other.Value.Buffer;
			}
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0000B17E File Offset: 0x0000937E
		public void Set(object other)
		{
			this.Set(other as AudioBeforeSendCallbackInfoInternal?);
		}
	}
}
