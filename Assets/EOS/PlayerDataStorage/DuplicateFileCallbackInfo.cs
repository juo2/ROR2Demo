using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000242 RID: 578
	public class DuplicateFileCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0001001C File Offset: 0x0000E21C
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00010024 File Offset: 0x0000E224
		public Result ResultCode { get; private set; }

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0001002D File Offset: 0x0000E22D
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x00010035 File Offset: 0x0000E235
		public object ClientData { get; private set; }

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0001003E File Offset: 0x0000E23E
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x00010046 File Offset: 0x0000E246
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0001004F File Offset: 0x0000E24F
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0001005C File Offset: 0x0000E25C
		internal void Set(DuplicateFileCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000100B1 File Offset: 0x0000E2B1
		public void Set(object other)
		{
			this.Set(other as DuplicateFileCallbackInfoInternal?);
		}
	}
}
