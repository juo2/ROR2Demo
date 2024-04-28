using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ImpBossMonster
{
	// Token: 0x02000308 RID: 776
	public class GroundPound : BaseState
	{
		// Token: 0x06000DD5 RID: 3541 RVA: 0x0003AFA0 File Offset: 0x000391A0
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			Util.PlaySound(GroundPound.initialAttackSoundString, base.gameObject);
			this.attack = new BlastAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
			this.attack.baseDamage = this.damageStat * GroundPound.damageCoefficient;
			this.attack.baseForce = GroundPound.forceMagnitude;
			this.attack.radius = GroundPound.blastAttackRadius;
			this.attack.falloffModel = BlastAttack.FalloffModel.SweetSpot;
			this.attack.attackerFiltering = AttackerFiltering.NeverHitSelf;
			this.duration = GroundPound.baseDuration / this.attackSpeedStat;
			base.PlayCrossfade("Fullbody Override", "GroundPound", "GroundPound.playbackRate", this.duration, 0.2f);
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(this.duration + 3f);
			}
			if (this.modelTransform)
			{
				this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0003B0F4 File Offset: 0x000392F4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.modelAnimator)
			{
				if (this.modelAnimator.GetFloat("GroundPound.hitBoxActive") > 0.5f)
				{
					if (!this.hasAttacked)
					{
						if (NetworkServer.active)
						{
							this.attack.position = this.childLocator.FindChild("GroundPoundCenter").transform.position;
							this.attack.Fire();
						}
						if (base.isAuthority)
						{
							EffectManager.SimpleMuzzleFlash(GroundPound.slamEffectPrefab, base.gameObject, "GroundPoundCenter", true);
						}
						EffectManager.SimpleMuzzleFlash(GroundPound.swipeEffectPrefab, base.gameObject, (this.attackCount % 2 == 0) ? "FireVoidspikesL" : "FireVoidspikesR", true);
						this.attackCount++;
						this.hasAttacked = true;
					}
				}
				else
				{
					this.hasAttacked = false;
				}
			}
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001122 RID: 4386
		private float stopwatch;

		// Token: 0x04001123 RID: 4387
		public static float baseDuration = 3.5f;

		// Token: 0x04001124 RID: 4388
		public static float damageCoefficient = 4f;

		// Token: 0x04001125 RID: 4389
		public static float forceMagnitude = 16f;

		// Token: 0x04001126 RID: 4390
		public static float blastAttackRadius;

		// Token: 0x04001127 RID: 4391
		private BlastAttack attack;

		// Token: 0x04001128 RID: 4392
		public static string initialAttackSoundString;

		// Token: 0x04001129 RID: 4393
		public static GameObject chargeEffectPrefab;

		// Token: 0x0400112A RID: 4394
		public static GameObject slamEffectPrefab;

		// Token: 0x0400112B RID: 4395
		public static GameObject hitEffectPrefab;

		// Token: 0x0400112C RID: 4396
		public static GameObject swipeEffectPrefab;

		// Token: 0x0400112D RID: 4397
		private Animator modelAnimator;

		// Token: 0x0400112E RID: 4398
		private Transform modelTransform;

		// Token: 0x0400112F RID: 4399
		private bool hasAttacked;

		// Token: 0x04001130 RID: 4400
		private float duration;

		// Token: 0x04001131 RID: 4401
		private ChildLocator childLocator;

		// Token: 0x04001132 RID: 4402
		private int attackCount;
	}
}
