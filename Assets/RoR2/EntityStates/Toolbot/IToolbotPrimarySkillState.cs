using System;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.Toolbot
{
	// Token: 0x02000192 RID: 402
	public interface IToolbotPrimarySkillState : ISkillState
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000704 RID: 1796
		// (set) Token: 0x06000705 RID: 1797
		Transform muzzleTransform { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000706 RID: 1798
		// (set) Token: 0x06000707 RID: 1799
		string muzzleName { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000708 RID: 1800
		string baseMuzzleName { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000709 RID: 1801
		// (set) Token: 0x0600070A RID: 1802
		bool isInDualWield { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600070B RID: 1803
		// (set) Token: 0x0600070C RID: 1804
		int currentHand { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600070D RID: 1805
		// (set) Token: 0x0600070E RID: 1806
		SkillDef skillDef { get; set; }
	}
}
