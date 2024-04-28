using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000124 RID: 292
	public class SpinBeamExit : BaseSpinBeamAttackState
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x000160FC File Offset: 0x000142FC
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

		// Token: 0x0600052B RID: 1323 RVA: 0x0001614A File Offset: 0x0001434A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= base.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04000607 RID: 1543
		[SerializeField]
		public SkillDef skillDefToReplaceAtStocksEmpty;

		// Token: 0x04000608 RID: 1544
		[SerializeField]
		public SkillDef nextSkillDef;
	}
}
