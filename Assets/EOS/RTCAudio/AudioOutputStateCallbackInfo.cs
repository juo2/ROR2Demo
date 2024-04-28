using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x0200017E RID: 382
	public class AudioOutputStateCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0000B80B File Offset: 0x00009A0B
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x0000B813 File Offset: 0x00009A13
		public object ClientData { get; private set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0000B81C File Offset: 0x00009A1C
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0000B824 File Offset: 0x00009A24
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0000B82D File Offset: 0x00009A2D
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x0000B835 File Offset: 0x00009A35
		public string RoomName { get; private set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0000B83E File Offset: 0x00009A3E
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x0000B846 File Offset: 0x00009A46
		public RTCAudioOutputStatus Status { get; private set; }

		// Token: 0x06000A6C RID: 2668 RVA: 0x0000B850 File Offset: 0x00009A50
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x0000B868 File Offset: 0x00009A68
		internal void Set(AudioOutputStateCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.RoomName = other.Value.RoomName;
				this.Status = other.Value.Status;
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x0000B8D2 File Offset: 0x00009AD2
		public void Set(object other)
		{
			this.Set(other as AudioOutputStateCallbackInfoInternal?);
		}
	}
}
