using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000546 RID: 1350
	public class VerifyIdTokenCallbackInfo : ICallbackInfo, ISettable
	{
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x000226C5 File Offset: 0x000208C5
		// (set) Token: 0x060020B2 RID: 8370 RVA: 0x000226CD File Offset: 0x000208CD
		public Result ResultCode { get; private set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x000226D6 File Offset: 0x000208D6
		// (set) Token: 0x060020B4 RID: 8372 RVA: 0x000226DE File Offset: 0x000208DE
		public object ClientData { get; private set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x000226E7 File Offset: 0x000208E7
		// (set) Token: 0x060020B6 RID: 8374 RVA: 0x000226EF File Offset: 0x000208EF
		public string ApplicationId { get; private set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x000226F8 File Offset: 0x000208F8
		// (set) Token: 0x060020B8 RID: 8376 RVA: 0x00022700 File Offset: 0x00020900
		public string ClientId { get; private set; }

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x00022709 File Offset: 0x00020909
		// (set) Token: 0x060020BA RID: 8378 RVA: 0x00022711 File Offset: 0x00020911
		public string ProductId { get; private set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x0002271A File Offset: 0x0002091A
		// (set) Token: 0x060020BC RID: 8380 RVA: 0x00022722 File Offset: 0x00020922
		public string SandboxId { get; private set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x0002272B File Offset: 0x0002092B
		// (set) Token: 0x060020BE RID: 8382 RVA: 0x00022733 File Offset: 0x00020933
		public string DeploymentId { get; private set; }

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0002273C File Offset: 0x0002093C
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x00022744 File Offset: 0x00020944
		public string DisplayName { get; private set; }

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0002274D File Offset: 0x0002094D
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x00022755 File Offset: 0x00020955
		public bool IsExternalAccountInfoPresent { get; private set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0002275E File Offset: 0x0002095E
		// (set) Token: 0x060020C4 RID: 8388 RVA: 0x00022766 File Offset: 0x00020966
		public ExternalAccountType ExternalAccountIdType { get; private set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0002276F File Offset: 0x0002096F
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x00022777 File Offset: 0x00020977
		public string ExternalAccountId { get; private set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x00022780 File Offset: 0x00020980
		// (set) Token: 0x060020C8 RID: 8392 RVA: 0x00022788 File Offset: 0x00020988
		public string ExternalAccountDisplayName { get; private set; }

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x00022791 File Offset: 0x00020991
		// (set) Token: 0x060020CA RID: 8394 RVA: 0x00022799 File Offset: 0x00020999
		public string Platform { get; private set; }

		// Token: 0x060020CB RID: 8395 RVA: 0x000227A2 File Offset: 0x000209A2
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000227B0 File Offset: 0x000209B0
		internal void Set(VerifyIdTokenCallbackInfoInternal? other)
		{
			if (other != null)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.ApplicationId = other.Value.ApplicationId;
				this.ClientId = other.Value.ClientId;
				this.ProductId = other.Value.ProductId;
				this.SandboxId = other.Value.SandboxId;
				this.DeploymentId = other.Value.DeploymentId;
				this.DisplayName = other.Value.DisplayName;
				this.IsExternalAccountInfoPresent = other.Value.IsExternalAccountInfoPresent;
				this.ExternalAccountIdType = other.Value.ExternalAccountIdType;
				this.ExternalAccountId = other.Value.ExternalAccountId;
				this.ExternalAccountDisplayName = other.Value.ExternalAccountDisplayName;
				this.Platform = other.Value.Platform;
			}
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x000228DA File Offset: 0x00020ADA
		public void Set(object other)
		{
			this.Set(other as VerifyIdTokenCallbackInfoInternal?);
		}
	}
}
