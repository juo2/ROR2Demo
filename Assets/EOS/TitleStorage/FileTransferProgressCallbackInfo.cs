using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200006D RID: 109
	public class FileTransferProgressCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00005694 File Offset: 0x00003894
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000569C File Offset: 0x0000389C
		public object ClientData { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000056A5 File Offset: 0x000038A5
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x000056AD File Offset: 0x000038AD
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000056B6 File Offset: 0x000038B6
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x000056BE File Offset: 0x000038BE
		public string Filename { get; private set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x000056C7 File Offset: 0x000038C7
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x000056CF File Offset: 0x000038CF
		public uint BytesTransferred { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x000056D8 File Offset: 0x000038D8
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x000056E0 File Offset: 0x000038E0
		public uint TotalFileSizeBytes { get; private set; }

		// Token: 0x06000477 RID: 1143 RVA: 0x000056EC File Offset: 0x000038EC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00005704 File Offset: 0x00003904
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

		// Token: 0x06000479 RID: 1145 RVA: 0x00005783 File Offset: 0x00003983
		public void Set(object other)
		{
			this.Set(other as FileTransferProgressCallbackInfoInternal?);
		}
	}
}
