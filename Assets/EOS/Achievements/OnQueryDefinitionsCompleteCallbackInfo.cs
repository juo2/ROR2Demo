using System;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200061A RID: 1562
	public class OnQueryDefinitionsCompleteCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00028B94 File Offset: 0x00026D94
		// (set) Token: 0x0600263A RID: 9786 RVA: 0x00028B9C File Offset: 0x00026D9C
		public Result ResultCode { get; private set; }

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600263B RID: 9787 RVA: 0x00028BA5 File Offset: 0x00026DA5
		// (set) Token: 0x0600263C RID: 9788 RVA: 0x00028BAD File Offset: 0x00026DAD
		public object ClientData { get; private set; }

		// Token: 0x0600263D RID: 9789 RVA: 0x00028BB6 File Offset: 0x00026DB6
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600263E RID: 9790 RVA: 0x00028BC4 File Offset: 0x00026DC4
		internal void Set(OnQueryDefinitionsCompleteCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x0600263F RID: 9791 RVA: 0x00028C04 File Offset: 0x00026E04
		public void Set(object other)
		{
			this.Set(other as OnQueryDefinitionsCompleteCallbackInfoInternal?);
		}
	}
}
