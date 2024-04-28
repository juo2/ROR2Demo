using System;
using RoR2;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor.CorruptMode
{
	// Token: 0x02000110 RID: 272
	public class CorruptMode : CorruptModeBase
	{
		// Token: 0x060004C8 RID: 1224 RVA: 0x000147B0 File Offset: 0x000129B0
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority && base.skillLocator)
			{
				base.skillLocator.primary.SetSkillOverride(this, this.primaryOverrideSkillDef, GenericSkill.SkillOverridePriority.Upgrade);
				base.skillLocator.secondary.SetSkillOverride(this, this.secondaryOverrideSkillDef, GenericSkill.SkillOverridePriority.Upgrade);
				base.skillLocator.utility.SetSkillOverride(this, this.utilityOverrideSkillDef, GenericSkill.SkillOverridePriority.Upgrade);
				base.skillLocator.special.SetSkillOverride(this, this.specialOverrideSkillDef, GenericSkill.SkillOverridePriority.Upgrade);
			}
			if (this.voidSurvivorController && NetworkServer.active)
			{
				base.characterBody.AddBuff(this.voidSurvivorController.corruptedBuffDef);
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x00014864 File Offset: 0x00012A64
		public override void OnExit()
		{
			if (base.isAuthority && base.skillLocator)
			{
				base.skillLocator.primary.UnsetSkillOverride(this, this.primaryOverrideSkillDef, GenericSkill.SkillOverridePriority.Upgrade);
				base.skillLocator.secondary.UnsetSkillOverride(this, this.secondaryOverrideSkillDef, GenericSkill.SkillOverridePriority.Upgrade);
				base.skillLocator.utility.UnsetSkillOverride(this, this.utilityOverrideSkillDef, GenericSkill.SkillOverridePriority.Upgrade);
				base.skillLocator.special.UnsetSkillOverride(this, this.specialOverrideSkillDef, GenericSkill.SkillOverridePriority.Upgrade);
			}
			if (this.voidSurvivorController && NetworkServer.active)
			{
				base.characterBody.RemoveBuff(this.voidSurvivorController.corruptedBuffDef);
			}
			base.OnExit();
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00014918 File Offset: 0x00012B18
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.voidSurvivorController && this.voidSurvivorController.corruption <= this.voidSurvivorController.minimumCorruption && !this.voidSurvivorController.isPermanentlyCorrupted && this.voidSurvivorController.bodyStateMachine)
			{
				this.voidSurvivorController.bodyStateMachine.SetInterruptState(new ExitCorruptionTransition(), InterruptPriority.Skill);
			}
		}

		// Token: 0x04000575 RID: 1397
		[SerializeField]
		public SkillDef primaryOverrideSkillDef;

		// Token: 0x04000576 RID: 1398
		[SerializeField]
		public SkillDef secondaryOverrideSkillDef;

		// Token: 0x04000577 RID: 1399
		[SerializeField]
		public SkillDef utilityOverrideSkillDef;

		// Token: 0x04000578 RID: 1400
		[SerializeField]
		public SkillDef specialOverrideSkillDef;
	}
}
