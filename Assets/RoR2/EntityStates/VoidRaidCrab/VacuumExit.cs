using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x0200012C RID: 300
	public class VacuumExit : BaseVacuumAttackState
	{
		// Token: 0x06000551 RID: 1361 RVA: 0x00016BCC File Offset: 0x00014DCC
		public override void OnEnter()
		{
			base.OnEnter();
			if (this.nextSkillDef)
			{
				GenericSkill genericSkill = base.skillLocator.FindSkillByDef(this.skillDefToReplaceAtStocksEmpty);
				if (genericSkill && genericSkill.stock == 0)
				{
					genericSkill.SetBaseSkill(this.nextSkillDef);
				}
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		protected override void OnLifetimeExpiredAuthority()
		{
			this.outer.SetNextStateToMain();
		}

		// Token: 0x04000634 RID: 1588
		[SerializeField]
		public SkillDef skillDefToReplaceAtStocksEmpty;

		// Token: 0x04000635 RID: 1589
		[SerializeField]
		public SkillDef nextSkillDef;
	}
}
