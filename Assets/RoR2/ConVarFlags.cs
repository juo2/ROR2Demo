using System;

namespace RoR2
{
	// Token: 0x0200066C RID: 1644
	[Flags]
	public enum ConVarFlags
	{
		// Token: 0x04002580 RID: 9600
		None = 0,
		// Token: 0x04002581 RID: 9601
		ExecuteOnServer = 1,
		// Token: 0x04002582 RID: 9602
		SenderMustBeServer = 2,
		// Token: 0x04002583 RID: 9603
		Archive = 4,
		// Token: 0x04002584 RID: 9604
		Cheat = 8,
		// Token: 0x04002585 RID: 9605
		Engine = 16
	}
}
