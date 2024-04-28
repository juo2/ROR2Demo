using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ImpMonster
{
	// Token: 0x0200030E RID: 782
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003BA68 File Offset: 0x00039C68
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			if (base.characterMotor)
			{
				base.characterMotor.enabled = false;
			}
			if (base.modelLocator && base.modelLocator.modelTransform.GetComponent<ChildLocator>() && DeathState.initialEffect)
			{
				EffectManager.SimpleMuzzleFlash(DeathState.initialEffect, base.gameObject, "Base", false);
			}
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003BAE8 File Offset: 0x00039CE8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.animator)
			{
				this.stopwatch += Time.fixedDeltaTime;
				if (!this.hasPlayedDeathEffect && this.animator.GetFloat("DeathEffect") > 0.5f)
				{
					this.hasPlayedDeathEffect = true;
					EffectManager.SimpleMuzzleFlash(DeathState.deathEffect, base.gameObject, "Center", false);
				}
				if (this.stopwatch >= DeathState.duration)
				{
					EntityState.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x04001155 RID: 4437
		public static GameObject initialEffect;

		// Token: 0x04001156 RID: 4438
		public static GameObject deathEffect;

		// Token: 0x04001157 RID: 4439
		private static float duration = 1.333f;

		// Token: 0x04001158 RID: 4440
		private float stopwatch;

		// Token: 0x04001159 RID: 4441
		private Animator animator;

		// Token: 0x0400115A RID: 4442
		private bool hasPlayedDeathEffect;
	}
}
