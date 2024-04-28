using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000B7 RID: 183
	public class AnimatedJump : BaseState
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		public override void OnEnter()
		{
			base.OnEnter();
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				int layerIndex = modelAnimator.GetLayerIndex("Body");
				modelAnimator.CrossFadeInFixedTime("AnimatedJump", 0.25f);
				modelAnimator.Update(0f);
				this.duration = modelAnimator.GetNextAnimatorStateInfo(layerIndex).length;
				AimAnimator component = modelAnimator.GetComponent<AimAnimator>();
				if (component)
				{
					component.enabled = true;
				}
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000CD48 File Offset: 0x0000AF48
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration / 2f && base.isAuthority && !this.hasInputJump)
			{
				this.hasInputJump = true;
				base.characterMotor.moveDirection = base.inputBank.moveVector;
				GenericCharacterMain.ApplyJumpVelocity(base.characterMotor, base.characterBody, 1f, 1f, false);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0400034A RID: 842
		private float duration;

		// Token: 0x0400034B RID: 843
		private bool hasInputJump;
	}
}
