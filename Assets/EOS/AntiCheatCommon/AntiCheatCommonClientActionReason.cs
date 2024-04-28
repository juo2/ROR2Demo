using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000574 RID: 1396
	public enum AntiCheatCommonClientActionReason
	{
		// Token: 0x04000F91 RID: 3985
		Invalid,
		// Token: 0x04000F92 RID: 3986
		InternalError,
		// Token: 0x04000F93 RID: 3987
		InvalidMessage,
		// Token: 0x04000F94 RID: 3988
		AuthenticationFailed,
		// Token: 0x04000F95 RID: 3989
		NullClient,
		// Token: 0x04000F96 RID: 3990
		HeartbeatTimeout,
		// Token: 0x04000F97 RID: 3991
		ClientViolation,
		// Token: 0x04000F98 RID: 3992
		BackendViolation,
		// Token: 0x04000F99 RID: 3993
		TemporaryCooldown,
		// Token: 0x04000F9A RID: 3994
		TemporaryBanned,
		// Token: 0x04000F9B RID: 3995
		PermanentBanned
	}
}
