using System;

namespace RoR2.Stats
{
	// Token: 0x02000ABA RID: 2746
	public struct StatEvent
	{
		// Token: 0x04003DA4 RID: 15780
		public string stringValue;

		// Token: 0x04003DA5 RID: 15781
		public double doubleValue;

		// Token: 0x04003DA6 RID: 15782
		public ulong ulongValue;

		// Token: 0x02000ABB RID: 2747
		public enum Type
		{
			// Token: 0x04003DA8 RID: 15784
			Damage,
			// Token: 0x04003DA9 RID: 15785
			Kill,
			// Token: 0x04003DAA RID: 15786
			Walk,
			// Token: 0x04003DAB RID: 15787
			Die,
			// Token: 0x04003DAC RID: 15788
			Lose,
			// Token: 0x04003DAD RID: 15789
			Win
		}
	}
}
