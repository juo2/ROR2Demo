using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000273 RID: 627
	public class WriteFileDataCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x00011390 File Offset: 0x0000F590
		// (set) Token: 0x0600101A RID: 4122 RVA: 0x00011398 File Offset: 0x0000F598
		public object ClientData { get; private set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x000113A1 File Offset: 0x0000F5A1
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x000113A9 File Offset: 0x0000F5A9
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x000113B2 File Offset: 0x0000F5B2
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x000113BA File Offset: 0x0000F5BA
		public string Filename { get; private set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x000113C3 File Offset: 0x0000F5C3
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x000113CB File Offset: 0x0000F5CB
		public uint DataBufferLengthBytes { get; private set; }

		// Token: 0x06001021 RID: 4129 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x000113EC File Offset: 0x0000F5EC
		internal void Set(WriteFileDataCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.DataBufferLengthBytes = other.Value.DataBufferLengthBytes;
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00011456 File Offset: 0x0000F656
		public void Set(object other)
		{
			this.Set(other as WriteFileDataCallbackInfoInternal?);
		}
	}
}
