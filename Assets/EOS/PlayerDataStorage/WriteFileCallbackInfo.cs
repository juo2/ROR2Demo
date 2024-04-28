using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000271 RID: 625
	public class WriteFileCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x00011259 File Offset: 0x0000F459
		// (set) Token: 0x06001009 RID: 4105 RVA: 0x00011261 File Offset: 0x0000F461
		public Result ResultCode { get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0001126A File Offset: 0x0000F46A
		// (set) Token: 0x0600100B RID: 4107 RVA: 0x00011272 File Offset: 0x0000F472
		public object ClientData { get; private set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0001127B File Offset: 0x0000F47B
		// (set) Token: 0x0600100D RID: 4109 RVA: 0x00011283 File Offset: 0x0000F483
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0001128C File Offset: 0x0000F48C
		// (set) Token: 0x0600100F RID: 4111 RVA: 0x00011294 File Offset: 0x0000F494
		public string Filename { get; private set; }

		// Token: 0x06001010 RID: 4112 RVA: 0x0001129D File Offset: 0x0000F49D
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x000112AC File Offset: 0x0000F4AC
		internal void Set(WriteFileCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x00011316 File Offset: 0x0000F516
		public void Set(object other)
		{
			this.Set(other as WriteFileCallbackInfoInternal?);
		}
	}
}
