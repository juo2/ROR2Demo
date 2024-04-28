using System;
using Epic.OnlineServices;
using Epic.OnlineServices.Connect;

namespace RoR2
{
	// Token: 0x020009AC RID: 2476
	public static class EOSExtensions
	{
		// Token: 0x060038A7 RID: 14503 RVA: 0x000EDA13 File Offset: 0x000EBC13
		public static ProductUserId ToProductUserId(this EpicAccountId epicAccountId)
		{
			return EOSPlatformManager.GetPlatformInterface().GetConnectInterface().GetExternalAccountMapping(new GetExternalAccountMappingsOptions
			{
				AccountIdType = ExternalAccountType.Epic,
				LocalUserId = EOSLoginManager.loggedInProductId,
				TargetExternalUserId = epicAccountId.ToString()
			});
		}
	}
}
