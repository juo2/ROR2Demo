using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000501 RID: 1281
	public class UnlinkAccountCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0002079C File Offset: 0x0001E99C
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x000207A4 File Offset: 0x0001E9A4
		public Result ResultCode { get; private set; }

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x000207AD File Offset: 0x0001E9AD
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x000207B5 File Offset: 0x0001E9B5
		public object ClientData { get; private set; }

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x000207BE File Offset: 0x0001E9BE
		// (set) Token: 0x06001EEE RID: 7918 RVA: 0x000207C6 File Offset: 0x0001E9C6
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06001EEF RID: 7919 RVA: 0x000207CF File Offset: 0x0001E9CF
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000207DC File Offset: 0x0001E9DC
		internal void Set(UnlinkAccountCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00020831 File Offset: 0x0001EA31
		public void Set(object other)
		{
			this.Set(other as UnlinkAccountCallbackInfoInternal?);
		}
	}
}
