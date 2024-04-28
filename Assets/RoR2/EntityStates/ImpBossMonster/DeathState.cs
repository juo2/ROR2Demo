using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ImpBossMonster
{
	// Token: 0x02000306 RID: 774
	public class DeathState : GenericCharacterDeath
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x0003A9A8 File Offset: 0x00038BA8
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			if (base.characterMotor)
			{
				base.characterMotor.enabled = false;
			}
			if (base.modelLocator)
			{
				Transform modelTransform = base.modelLocator.modelTransform;
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
				if (component)
				{
					component.FindChild("DustCenter").gameObject.SetActive(false);
					if (DeathState.initialEffect)
					{
						EffectManager.SimpleMuzzleFlash(DeathState.initialEffect, base.gameObject, "DeathCenter", false);
					}
				}
				if (component2)
				{
					for (int i = 0; i < component2.baseRendererInfos.Length; i++)
					{
						component2.baseRendererInfos[i].ignoreOverlays = true;
					}
				}
			}
			this.PlayAnimation("Fullbody Override", "Death");
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003AA88 File Offset: 0x00038C88
		public override void FixedUpdate()
		{
			if (this.animator)
			{
				this.stopwatch += Time.fixedDeltaTime;
				if (!this.hasPlayedDeathEffect && this.animator.GetFloat("DeathEffect") > 0.5f)
				{
					this.hasPlayedDeathEffect = true;
					EffectManager.SimpleMuzzleFlash(DeathState.deathEffect, base.gameObject, "DeathCenter", false);
				}
				if (this.stopwatch >= DeathState.duration)
				{
					this.AttemptDeathBehavior();
				}
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0003AB04 File Offset: 0x00038D04
		private void AttemptDeathBehavior()
		{
			if (this.attemptedDeathBehavior)
			{
				return;
			}
			this.attemptedDeathBehavior = true;
			if (base.modelLocator.modelBaseTransform)
			{
				EntityState.Destroy(base.modelLocator.modelBaseTransform.gameObject);
			}
			if (NetworkServer.active)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0003AB5A File Offset: 0x00038D5A
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				this.AttemptDeathBehavior();
			}
			base.OnExit();
		}

		// Token: 0x04001105 RID: 4357
		public static GameObject initialEffect;

		// Token: 0x04001106 RID: 4358
		public static GameObject deathEffect;

		// Token: 0x04001107 RID: 4359
		private static float duration = 3.3166666f;

		// Token: 0x04001108 RID: 4360
		private float stopwatch;

		// Token: 0x04001109 RID: 4361
		private Animator animator;

		// Token: 0x0400110A RID: 4362
		private bool hasPlayedDeathEffect;

		// Token: 0x0400110B RID: 4363
		private bool attemptedDeathBehavior;
	}
}
