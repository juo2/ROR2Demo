using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Assassin2
{
	// Token: 0x02000488 RID: 1160
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x0005C1EA File Offset: 0x0005A3EA
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			if (base.characterMotor)
			{
				base.characterMotor.enabled = false;
			}
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0005C218 File Offset: 0x0005A418
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.animator)
			{
				this.stopwatch += Time.fixedDeltaTime;
				if (!this.hasPlayedDeathEffect && this.animator.GetFloat("DeathEffect") > 0.5f)
				{
					this.hasPlayedDeathEffect = true;
					EffectData effectData = new EffectData();
					effectData.origin = base.transform.position;
					EffectManager.SpawnEffect(DeathState.deathEffectPrefab, effectData, false);
				}
				if (this.stopwatch >= DeathState.duration)
				{
					base.DestroyModel();
					EntityState.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0001886B File Offset: 0x00016A6B
		public override void OnExit()
		{
			base.DestroyModel();
			base.OnExit();
		}

		// Token: 0x04001A86 RID: 6790
		public static GameObject deathEffectPrefab;

		// Token: 0x04001A87 RID: 6791
		public static float duration = 1.333f;

		// Token: 0x04001A88 RID: 6792
		private float stopwatch;

		// Token: 0x04001A89 RID: 6793
		private Animator animator;

		// Token: 0x04001A8A RID: 6794
		private bool hasPlayedDeathEffect;
	}
}
