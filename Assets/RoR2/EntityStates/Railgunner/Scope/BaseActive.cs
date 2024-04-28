using System;
using RoR2;
using RoR2.Audio;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.Railgunner.Scope
{
	// Token: 0x02000204 RID: 516
	public class BaseActive : BaseScopeState
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x00025D64 File Offset: 0x00023F64
		public override void OnEnter()
		{
			base.OnEnter();
			base.SetScopeAlpha(1f);
			base.StartScopeParamsOverride(0f);
			SkillLocator skillLocator = base.skillLocator;
			GenericSkill genericSkill = (skillLocator != null) ? skillLocator.primary : null;
			if (genericSkill)
			{
				this.TryOverrideSkill(genericSkill);
				genericSkill.onSkillChanged += this.TryOverrideSkill;
			}
			if (base.isAuthority)
			{
				this.loopPtr = LoopSoundManager.PlaySoundLoopLocal(base.gameObject, this.loopSound);
			}
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00025DE0 File Offset: 0x00023FE0
		public override void OnExit()
		{
			if (this.loopPtr.isValid)
			{
				LoopSoundManager.StopSoundLoopLocal(this.loopPtr);
			}
			SkillLocator skillLocator = base.skillLocator;
			GenericSkill genericSkill = (skillLocator != null) ? skillLocator.primary : null;
			if (genericSkill)
			{
				genericSkill.onSkillChanged -= this.TryOverrideSkill;
			}
			if (this.overriddenSkill)
			{
				this.overriddenSkill.UnsetSkillOverride(this, this.primaryOverride, GenericSkill.SkillOverridePriority.Contextual);
			}
			base.EndScopeParamsOverride(0f);
			base.OnExit();
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00025E63 File Offset: 0x00024063
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && (!base.IsKeyDownAuthority() || base.characterBody.isSprinting))
			{
				this.outer.SetNextState(this.GetNextState());
			}
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00025E9C File Offset: 0x0002409C
		private void TryOverrideSkill(GenericSkill skill)
		{
			if (skill && !this.overriddenSkill && !skill.HasSkillOverrideOfPriority(GenericSkill.SkillOverridePriority.Contextual))
			{
				this.overriddenSkill = skill;
				this.overriddenSkill.SetSkillOverride(this, this.primaryOverride, GenericSkill.SkillOverridePriority.Contextual);
				this.overriddenSkill.stock = base.skillLocator.secondary.stock;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00025EFC File Offset: 0x000240FC
		protected virtual BaseWindDown GetNextState()
		{
			return new BaseWindDown();
		}

		// Token: 0x04000AA6 RID: 2726
		[SerializeField]
		public SkillDef primaryOverride;

		// Token: 0x04000AA7 RID: 2727
		[SerializeField]
		public LoopSoundDef loopSound;

		// Token: 0x04000AA8 RID: 2728
		private GenericSkill overriddenSkill;

		// Token: 0x04000AA9 RID: 2729
		private LoopSoundManager.SoundLoopPtr loopPtr;
	}
}
