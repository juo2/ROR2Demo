using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000507 RID: 1287
	public class VerifyIdTokenCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x00020993 File Offset: 0x0001EB93
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x0002099B File Offset: 0x0001EB9B
		public Result ResultCode { get; private set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000209A4 File Offset: 0x0001EBA4
		// (set) Token: 0x06001F0B RID: 7947 RVA: 0x000209AC File Offset: 0x0001EBAC
		public object ClientData { get; private set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000209B5 File Offset: 0x0001EBB5
		// (set) Token: 0x06001F0D RID: 7949 RVA: 0x000209BD File Offset: 0x0001EBBD
		public ProductUserId ProductUserId { get; private set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x000209C6 File Offset: 0x0001EBC6
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x000209CE File Offset: 0x0001EBCE
		public bool IsAccountInfoPresent { get; private set; }

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x000209D7 File Offset: 0x0001EBD7
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x000209DF File Offset: 0x0001EBDF
		public ExternalAccountType AccountIdType { get; private set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x000209E8 File Offset: 0x0001EBE8
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x000209F0 File Offset: 0x0001EBF0
		public string AccountId { get; private set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000209F9 File Offset: 0x0001EBF9
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x00020A01 File Offset: 0x0001EC01
		public string Platform { get; private set; }

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x00020A0A File Offset: 0x0001EC0A
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x00020A12 File Offset: 0x0001EC12
		public string DeviceType { get; private set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x00020A1B File Offset: 0x0001EC1B
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x00020A23 File Offset: 0x0001EC23
		public string ClientId { get; private set; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x00020A2C File Offset: 0x0001EC2C
		// (set) Token: 0x06001F1B RID: 7963 RVA: 0x00020A34 File Offset: 0x0001EC34
		public string ProductId { get; private set; }

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001F1C RID: 7964 RVA: 0x00020A3D File Offset: 0x0001EC3D
		// (set) Token: 0x06001F1D RID: 7965 RVA: 0x00020A45 File Offset: 0x0001EC45
		public string SandboxId { get; private set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00020A4E File Offset: 0x0001EC4E
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x00020A56 File Offset: 0x0001EC56
		public string DeploymentId { get; private set; }

		// Token: 0x06001F20 RID: 7968 RVA: 0x00020A5F File Offset: 0x0001EC5F
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00020A6C File Offset: 0x0001EC6C
		internal void Set(VerifyIdTokenCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.ProductUserId = other.Value.ProductUserId;
				this.IsAccountInfoPresent = other.Value.IsAccountInfoPresent;
				this.AccountIdType = other.Value.AccountIdType;
				this.AccountId = other.Value.AccountId;
				this.Platform = other.Value.Platform;
				this.DeviceType = other.Value.DeviceType;
				this.ClientId = other.Value.ClientId;
				this.ProductId = other.Value.ProductId;
				this.SandboxId = other.Value.SandboxId;
				this.DeploymentId = other.Value.DeploymentId;
			}
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00020B81 File Offset: 0x0001ED81
		public void Set(object other)
		{
			this.Set(other as VerifyIdTokenCallbackInfoInternal?);
		}
	}
}
