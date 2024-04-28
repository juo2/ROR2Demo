using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x0200059D RID: 1437
	public class OnClientAuthStatusChangedCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x00024E38 File Offset: 0x00023038
		// (set) Token: 0x060022E2 RID: 8930 RVA: 0x00024E40 File Offset: 0x00023040
		public object ClientData { get; private set; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00024E49 File Offset: 0x00023049
		// (set) Token: 0x060022E4 RID: 8932 RVA: 0x00024E51 File Offset: 0x00023051
		public IntPtr ClientHandle { get; private set; }

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x00024E5A File Offset: 0x0002305A
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x00024E62 File Offset: 0x00023062
		public AntiCheatCommonClientAuthStatus ClientAuthStatus { get; private set; }

		// Token: 0x060022E7 RID: 8935 RVA: 0x00024E6C File Offset: 0x0002306C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x00024E84 File Offset: 0x00023084
		internal void Set(OnClientAuthStatusChangedCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ClientData = other.Value.ClientData;
				this.ClientHandle = other.Value.ClientHandle;
				this.ClientAuthStatus = other.Value.ClientAuthStatus;
			}
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00024ED9 File Offset: 0x000230D9
		public void Set(object other)
		{
			this.Set(other as OnClientAuthStatusChangedCallbackInfoInternal?);
		}
	}
}
