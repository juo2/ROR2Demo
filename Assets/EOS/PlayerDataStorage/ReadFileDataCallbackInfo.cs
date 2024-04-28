using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026C RID: 620
	public class ReadFileDataCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00010F1C File Offset: 0x0000F11C
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00010F24 File Offset: 0x0000F124
		public object ClientData { get; private set; }

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00010F2D File Offset: 0x0000F12D
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x00010F35 File Offset: 0x0000F135
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00010F3E File Offset: 0x0000F13E
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x00010F46 File Offset: 0x0000F146
		public string Filename { get; private set; }

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00010F4F File Offset: 0x0000F14F
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x00010F57 File Offset: 0x0000F157
		public uint TotalFileSizeBytes { get; private set; }

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x00010F60 File Offset: 0x0000F160
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x00010F68 File Offset: 0x0000F168
		public bool IsLastChunk { get; private set; }

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000FE8 RID: 4072 RVA: 0x00010F71 File Offset: 0x0000F171
		// (set) Token: 0x06000FE9 RID: 4073 RVA: 0x00010F79 File Offset: 0x0000F179
		public byte[] DataChunk { get; private set; }

		// Token: 0x06000FEA RID: 4074 RVA: 0x00010F84 File Offset: 0x0000F184
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00010F9C File Offset: 0x0000F19C
		internal void Set(ReadFileDataCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.TotalFileSizeBytes = other.Value.TotalFileSizeBytes;
				this.IsLastChunk = other.Value.IsLastChunk;
				this.DataChunk = other.Value.DataChunk;
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00011030 File Offset: 0x0000F230
		public void Set(object other)
		{
			this.Set(other as ReadFileDataCallbackInfoInternal?);
		}
	}
}
