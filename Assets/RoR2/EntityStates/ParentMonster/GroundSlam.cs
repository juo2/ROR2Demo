using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ParentMonster
{
	// Token: 0x02000229 RID: 553
	public class GroundSlam : BaseState
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x00027FB0 File Offset: 0x000261B0
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(GroundSlam.attackSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayCrossfade("Body", "Slam", "Slam.playbackRate", GroundSlam.duration, 0.1f);
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00028038 File Offset: 0x00026238
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator && this.modelAnimator.GetFloat("Slam.hitBoxActive") > 0.5f && !this.hasAttacked)
			{
				if (base.isAuthority)
				{
					if (base.characterDirection)
					{
						base.characterDirection.moveVector = base.characterDirection.forward;
					}
					if (this.modelTransform)
					{
						Transform transform = base.FindModelChild("SlamZone");
						if (transform)
						{
							this.attack = new BlastAttack();
							this.attack.attacker = base.gameObject;
							this.attack.inflictor = base.gameObject;
							this.attack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
							this.attack.baseDamage = this.damageStat * GroundSlam.damageCoefficient;
							this.attack.baseForce = GroundSlam.forceMagnitude;
							this.attack.position = transform.position;
							this.attack.radius = GroundSlam.radius;
							this.attack.Fire();
						}
					}
				}
				this.hasAttacked = true;
			}
			if (base.fixedAge >= GroundSlam.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000B39 RID: 2873
		public static float duration = 3.5f;

		// Token: 0x04000B3A RID: 2874
		public static float damageCoefficient = 4f;

		// Token: 0x04000B3B RID: 2875
		public static float forceMagnitude = 16f;

		// Token: 0x04000B3C RID: 2876
		public static float radius = 3f;

		// Token: 0x04000B3D RID: 2877
		private BlastAttack attack;

		// Token: 0x04000B3E RID: 2878
		public static string attackSoundString;

		// Token: 0x04000B3F RID: 2879
		public static GameObject slamImpactEffect;

		// Token: 0x04000B40 RID: 2880
		public static GameObject meleeTrailEffectL;

		// Token: 0x04000B41 RID: 2881
		public static GameObject meleeTrailEffectR;

		// Token: 0x04000B42 RID: 2882
		private Animator modelAnimator;

		// Token: 0x04000B43 RID: 2883
		private Transform modelTransform;

		// Token: 0x04000B44 RID: 2884
		private bool hasAttacked;
	}
}
