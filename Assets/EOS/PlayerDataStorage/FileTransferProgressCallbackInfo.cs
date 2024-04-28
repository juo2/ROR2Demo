using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000248 RID: 584
	public class FileTransferProgressCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x000103DB File Offset: 0x0000E5DB
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x000103E3 File Offset: 0x0000E5E3
		public object ClientData { get; private set; }

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x000103EC File Offset: 0x0000E5EC
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x000103F4 File Offset: 0x0000E5F4
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x000103FD File Offset: 0x0000E5FD
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00010405 File Offset: 0x0000E605
		public string Filename { get; private set; }

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0001040E File Offset: 0x0000E60E
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x00010416 File Offset: 0x0000E616
		public uint BytesTransferred { get; private set; }

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0001041F File Offset: 0x0000E61F
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00010427 File Offset: 0x0000E627
		public uint TotalFileSizeBytes { get; private set; }

		// Token: 0x06000F20 RID: 3872 RVA: 0x00010430 File Offset: 0x0000E630
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x00010448 File Offset: 0x0000E648
		internal void Set(FileTransferProgressCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
				this.BytesTransferred = other.Value.BytesTransferred;
				this.TotalFileSizeBytes = other.Value.TotalFileSizeBytes;
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000104C7 File Offset: 0x0000E6C7
		public void Set(object other)
		{
			this.Set(other as FileTransferProgressCallbackInfoInternal?);
		}
	}
}
