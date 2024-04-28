using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D6 RID: 214
	public class JoinSessionCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x00007CD8 File Offset: 0x00005ED8
		// (set) Token: 0x060006C7 RID: 1735 RVA: 0x00007CE0 File Offset: 0x00005EE0
		public Result ResultCode { get; private set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00007CE9 File Offset: 0x00005EE9
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00007CF1 File Offset: 0x00005EF1
		public object ClientData { get; private set; }

		// Token: 0x060006CA RID: 1738 RVA: 0x00007CFA File Offset: 0x00005EFA
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00007D08 File Offset: 0x00005F08
		internal void Set(JoinSessionCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00007D48 File Offset: 0x00005F48
		public void Set(object other)
		{
			this.Set(other as JoinSessionCallbackInfoInternal?);
		}
	}
}
