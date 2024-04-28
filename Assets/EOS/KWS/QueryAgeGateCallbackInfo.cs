using System;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x020003FB RID: 1019
	public class QueryAgeGateCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00019DF4 File Offset: 0x00017FF4
		// (set) Token: 0x06001891 RID: 6289 RVA: 0x00019DFC File Offset: 0x00017FFC
		public Result ResultCode { get; private set; }

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x00019E05 File Offset: 0x00018005
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x00019E0D File Offset: 0x0001800D
		public object ClientData { get; private set; }

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00019E16 File Offset: 0x00018016
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x00019E1E File Offset: 0x0001801E
		public string CountryCode { get; private set; }

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x00019E27 File Offset: 0x00018027
		// (set) Token: 0x06001897 RID: 6295 RVA: 0x00019E2F File Offset: 0x0001802F
		public uint AgeOfConsent { get; private set; }

		// Token: 0x06001898 RID: 6296 RVA: 0x00019E38 File Offset: 0x00018038
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001899 RID: 6297 RVA: 0x00019E48 File Offset: 0x00018048
		internal void Set(QueryAgeGateCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.CountryCode = other.Value.CountryCode;
				this.AgeOfConsent = other.Value.AgeOfConsent;
			}
		}

		// Token: 0x0600189A RID: 6298 RVA: 0x00019EB2 File Offset: 0x000180B2
		public void Set(object other)
		{
			this.Set(other as QueryAgeGateCallbackInfoInternal?);
		}
	}
}
