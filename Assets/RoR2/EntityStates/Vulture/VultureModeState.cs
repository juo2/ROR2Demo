using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Vulture
{
	// Token: 0x020000DF RID: 223
	public class VultureModeState : BaseSkillState
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x00010974 File Offset: 0x0000EB74
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.characterGravityParameterProvider = base.gameObject.GetComponent<ICharacterGravityParameterProvider>();
			this.characterFlightParameterProvider = base.gameObject.GetComponent<ICharacterFlightParameterProvider>();
			if (this.animator)
			{
				this.flyOverrideLayer = this.animator.GetLayerIndex("FlyOverride");
			}
			if (base.characterMotor)
			{
				base.characterMotor.walkSpeedPenaltyCoefficient = this.movementSpeedMultiplier;
			}
			if (base.modelLocator)
			{
				base.modelLocator.normalizeToFloor = false;
			}
			Util.PlaySound(this.enterSoundString, base.gameObject);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00010A24 File Offset: 0x0000EC24
		public override void Update()
		{
			base.Update();
			if (this.animator)
			{
				this.animator.SetLayerWeight(this.flyOverrideLayer, Util.Remap(Mathf.Clamp01(base.age / this.mecanimTransitionDuration), 0f, 1f, 1f - this.flyOverrideMecanimLayerWeight, this.flyOverrideMecanimLayerWeight));
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00010A88 File Offset: 0x0000EC88
		public override void OnExit()
		{
			if (base.characterMotor)
			{
				base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
			}
			base.OnExit();
		}

		// Token: 0x04000407 RID: 1031
		[SerializeField]
		public float mecanimTransitionDuration;

		// Token: 0x04000408 RID: 1032
		[SerializeField]
		public float flyOverrideMecanimLayerWeight;

		// Token: 0x04000409 RID: 1033
		[SerializeField]
		public float movementSpeedMultiplier;

		// Token: 0x0400040A RID: 1034
		[SerializeField]
		public string enterSoundString;

		// Token: 0x0400040B RID: 1035
		protected Animator animator;

		// Token: 0x0400040C RID: 1036
		protected int flyOverrideLayer;

		// Token: 0x0400040D RID: 1037
		protected ICharacterGravityParameterProvider characterGravityParameterProvider;

		// Token: 0x0400040E RID: 1038
		protected ICharacterFlightParameterProvider characterFlightParameterProvider;
	}
}
