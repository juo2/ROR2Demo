using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200023E RID: 574
	public class DeleteFileCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		public Result ResultCode { get; private set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0000FEB1 File Offset: 0x0000E0B1
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0000FEB9 File Offset: 0x0000E0B9
		public object ClientData { get; private set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0000FEC2 File Offset: 0x0000E0C2
		// (set) Token: 0x06000ECE RID: 3790 RVA: 0x0000FECA File Offset: 0x0000E0CA
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06000ECF RID: 3791 RVA: 0x0000FED3 File Offset: 0x0000E0D3
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0000FEE0 File Offset: 0x0000E0E0
		internal void Set(DeleteFileCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0000FF35 File Offset: 0x0000E135
		public void Set(object other)
		{
			this.Set(other as DeleteFileCallbackInfoInternal?);
		}
	}
}
