using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x020004D7 RID: 1239
	public class LoginCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0001FFA4 File Offset: 0x0001E1A4
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x0001FFAC File Offset: 0x0001E1AC
		public Result ResultCode { get; private set; }

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x0001FFB5 File Offset: 0x0001E1B5
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x0001FFBD File Offset: 0x0001E1BD
		public object ClientData { get; private set; }

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x0001FFC6 File Offset: 0x0001E1C6
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x0001FFCE File Offset: 0x0001E1CE
		public ProductUserId LocalUserId { get; private set; }

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x0001FFD7 File Offset: 0x0001E1D7
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x0001FFDF File Offset: 0x0001E1DF
		public ContinuanceToken ContinuanceToken { get; private set; }

		// Token: 0x06001E14 RID: 7700 RVA: 0x0001FFE8 File Offset: 0x0001E1E8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x0001FFF8 File Offset: 0x0001E1F8
		internal void Set(LoginCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.ContinuanceToken = other.Value.ContinuanceToken;
			}
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00020062 File Offset: 0x0001E262
		public void Set(object other)
		{
			this.Set(other as LoginCallbackInfoInternal?);
		}
	}
}
