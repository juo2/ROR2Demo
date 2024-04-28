using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200023A RID: 570
	public class DeleteCacheCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x0000FD5A File Offset: 0x0000DF5A
		// (set) Token: 0x06000EB5 RID: 3765 RVA: 0x0000FD62 File Offset: 0x0000DF62
		public Result ResultCode { get; private set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x0000FD6B File Offset: 0x0000DF6B
		// (set) Token: 0x06000EB7 RID: 3767 RVA: 0x0000FD73 File Offset: 0x0000DF73
		public object ClientData { get; private set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x0000FD84 File Offset: 0x0000DF84
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06000EBA RID: 3770 RVA: 0x0000FD8D File Offset: 0x0000DF8D
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		internal void Set(DeleteCacheCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0000FDF1 File Offset: 0x0000DFF1
		public void Set(object other)
		{
			this.Set(other as DeleteCacheCallbackInfoInternal?);
		}
	}
}
