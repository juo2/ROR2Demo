using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x02000157 RID: 343
	public class QueryActivePlayerSanctionsCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0000A71F File Offset: 0x0000891F
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x0000A727 File Offset: 0x00008927
		public Result ResultCode { get; private set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0000A730 File Offset: 0x00008930
		// (set) Token: 0x0600096F RID: 2415 RVA: 0x0000A738 File Offset: 0x00008938
		public object ClientData { get; private set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0000A741 File Offset: 0x00008941
		// (set) Token: 0x06000971 RID: 2417 RVA: 0x0000A749 File Offset: 0x00008949
		public ProductUserId TargetUserId { get; private set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0000A752 File Offset: 0x00008952
		// (set) Token: 0x06000973 RID: 2419 RVA: 0x0000A75A File Offset: 0x0000895A
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x06000974 RID: 2420 RVA: 0x0000A763 File Offset: 0x00008963
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0000A770 File Offset: 0x00008970
		internal void Set(QueryActivePlayerSanctionsCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.TargetUserId = other.Value.TargetUserId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0000A7DA File Offset: 0x000089DA
		public void Set(object other)
		{
			this.Set(other as QueryActivePlayerSanctionsCallbackInfoInternal?);
		}
	}
}
