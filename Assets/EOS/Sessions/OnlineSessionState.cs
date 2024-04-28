using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000F5 RID: 245
	public enum OnlineSessionState
	{
		// Token: 0x0400036C RID: 876
		NoSession,
		// Token: 0x0400036D RID: 877
		Creating,
		// Token: 0x0400036E RID: 878
		Pending,
		// Token: 0x0400036F RID: 879
		Starting,
		// Token: 0x04000370 RID: 880
		InProgress,
		// Token: 0x04000371 RID: 881
		Ending,
		// Token: 0x04000372 RID: 882
		Ended,
		// Token: 0x04000373 RID: 883
		Destroying
	}
}
