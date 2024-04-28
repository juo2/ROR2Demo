using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000264 RID: 612
	public class QueryFileListCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x00010BE8 File Offset: 0x0000EDE8
		public Result ResultCode { get; private set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x00010BF1 File Offset: 0x0000EDF1
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x00010BF9 File Offset: 0x0000EDF9
		public object ClientData { get; private set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00010C02 File Offset: 0x0000EE02
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00010C0A File Offset: 0x0000EE0A
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00010C13 File Offset: 0x0000EE13
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00010C1B File Offset: 0x0000EE1B
		public uint FileCount { get; private set; }

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00010C24 File Offset: 0x0000EE24
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x00010C34 File Offset: 0x0000EE34
		internal void Set(QueryFileListCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.FileCount = other.Value.FileCount;
			}
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x00010C9E File Offset: 0x0000EE9E
		public void Set(object other)
		{
			this.Set(other as QueryFileListCallbackInfoInternal?);
		}
	}
}
