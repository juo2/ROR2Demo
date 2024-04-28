using System;
using RoR2;
using RoR2.Skills;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Huntress
{
	// Token: 0x02000315 RID: 789
	public class AimArrowSnipe : BaseArrowBarrage
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x0003C174 File Offset: 0x0003A374
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAimAnimator = base.GetModelTransform().GetComponent<AimAnimator>();
			if (this.modelAimAnimator)
			{
				this.modelAimAnimator.enabled = true;
			}
			this.primarySkillSlot = (base.skillLocator ? base.skillLocator.primary : null);
			if (this.primarySkillSlot)
			{
				this.primarySkillSlot.SetSkillOverride(this, AimArrowSnipe.primarySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			base.PlayCrossfade("Body", "ArrowBarrageLoop", 0.1f);
			if (AimArrowSnipe.crosshairOverridePrefab)
			{
				this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, AimArrowSnipe.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003C229 File Offset: 0x0003A429
		protected override void HandlePrimaryAttack()
		{
			if (this.primarySkillSlot)
			{
				this.primarySkillSlot.ExecuteIfReady();
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003C244 File Offset: 0x0003A444
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
			if (!this.primarySkillSlot || this.primarySkillSlot.stock == 0)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0003C2A3 File Offset: 0x0003A4A3
		public override void OnExit()
		{
			if (this.primarySkillSlot)
			{
				this.primarySkillSlot.UnsetSkillOverride(this, AimArrowSnipe.primarySkillDef, GenericSkill.SkillOverridePriority.Contextual);
			}
			CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
			if (overrideRequest != null)
			{
				overrideRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x04001179 RID: 4473
		public static SkillDef primarySkillDef;

		// Token: 0x0400117A RID: 4474
		public static GameObject crosshairOverridePrefab;

		// Token: 0x0400117B RID: 4475
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x0400117C RID: 4476
		private GenericSkill primarySkillSlot;

		// Token: 0x0400117D RID: 4477
		private AimAnimator modelAimAnimator;
	}
}
