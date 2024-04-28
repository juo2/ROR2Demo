using System;
using System.Collections.Generic;
using EntityStates.Engi.MineDeployer;

namespace RoR2.Skills
{
	// Token: 0x02000C01 RID: 3073
	public class EngiMineDeployerSkill : SkillDef
	{
		// Token: 0x060045AA RID: 17834 RVA: 0x001219E8 File Offset: 0x0011FBE8
		public override bool CanExecute(GenericSkill skillSlot)
		{
			List<BaseMineDeployerState> instancesList = BaseMineDeployerState.instancesList;
			for (int i = 0; i < instancesList.Count; i++)
			{
				if (instancesList[i].owner == skillSlot.gameObject)
				{
					return false;
				}
			}
			return base.CanExecute(skillSlot);
		}
	}
}
