using System;
using UnityEngine;

namespace EntityStates.Vulture
{
	// Token: 0x020000DE RID: 222
	public class FallingDeath : GenericCharacterDeath
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x000108D0 File Offset: 0x0000EAD0
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterMotor)
			{
				base.characterMotor.velocity.y = 0f;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000108FC File Offset: 0x0000EAFC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.animator)
			{
				this.animator.SetBool("isGrounded", base.characterMotor.isGrounded);
				return;
			}
			this.animator = base.GetModelAnimator();
			if (this.animator)
			{
				int layerIndex = this.animator.GetLayerIndex("FlyOverride");
				this.animator.SetLayerWeight(layerIndex, 0f);
			}
		}

		// Token: 0x04000406 RID: 1030
		private Animator animator;
	}
}
