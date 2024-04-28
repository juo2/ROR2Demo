using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003FF RID: 1023
	public class QueryPermissionsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00019F32 File Offset: 0x00018132
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x00019F3A File Offset: 0x0001813A
		public Result ResultCode { get; private set; }

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x00019F43 File Offset: 0x00018143
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x00019F4B File Offset: 0x0001814B
		public object ClientData { get; private set; }

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x00019F54 File Offset: 0x00018154
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x00019F5C File Offset: 0x0001815C
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00019F65 File Offset: 0x00018165
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x00019F6D File Offset: 0x0001816D
		public string KWSUserId { get; private set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x00019F76 File Offset: 0x00018176
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x00019F7E File Offset: 0x0001817E
		public string DateOfBirth { get; private set; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x00019F87 File Offset: 0x00018187
		// (set) Token: 0x060018B0 RID: 6320 RVA: 0x00019F8F File Offset: 0x0001818F
		public bool IsMinor { get; private set; }

		// Token: 0x060018B1 RID: 6321 RVA: 0x00019F98 File Offset: 0x00018198
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x00019FA8 File Offset: 0x000181A8
		internal void Set(QueryPermissionsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.KWSUserId = other.Value.KWSUserId;
				this.DateOfBirth = other.Value.DateOfBirth;
				this.IsMinor = other.Value.IsMinor;
			}
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0001A03C File Offset: 0x0001823C
		public void Set(object other)
		{
			this.Set(other as QueryPermissionsCallbackInfoInternal?);
		}
	}
}
