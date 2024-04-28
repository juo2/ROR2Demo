using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.HAND.Weapon
{
	// Token: 0x02000337 RID: 823
	public class Slam : BaseState
	{
		// Token: 0x06000ECD RID: 3789 RVA: 0x0003FF2C File Offset: 0x0003E12C
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Slam.baseDuration / this.attackSpeedStat;
			this.modelAnimator = base.GetModelAnimator();
			Transform modelTransform = base.GetModelTransform();
			this.attack = new OverlapAttack();
			this.attack.attacker = base.gameObject;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = TeamComponent.GetObjectTeam(this.attack.attacker);
			this.attack.damage = Slam.impactDamageCoefficient * this.damageStat;
			this.attack.hitEffectPrefab = Slam.hitEffectPrefab;
			this.attack.isCrit = Util.CheckRoll(this.critStat, base.characterBody.master);
			if (modelTransform)
			{
				this.attack.hitBoxGroup = Array.Find<HitBoxGroup>(modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "Hammer");
				ChildLocator component = modelTransform.GetComponent<ChildLocator>();
				if (component)
				{
					this.hammerChildTransform = component.FindChild("SwingCenter");
				}
			}
			if (this.modelAnimator)
			{
				base.PlayAnimation("Gesture", "Slam", "Slam.playbackRate", this.duration);
			}
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(2f);
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00040098 File Offset: 0x0003E298
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active && this.modelAnimator && this.modelAnimator.GetFloat("Hammer.hitBoxActive") > 0.5f)
			{
				if (!this.hasSwung)
				{
					Ray aimRay = base.GetAimRay();
					EffectManager.SimpleMuzzleFlash(Slam.swingEffectPrefab, base.gameObject, "SwingCenter", true);
					ProjectileManager.instance.FireProjectile(Slam.projectilePrefab, aimRay.origin, Util.QuaternionSafeLookRotation(aimRay.direction), base.gameObject, this.damageStat * Slam.earthquakeDamageCoefficient, Slam.forceMagnitude, Util.CheckRoll(this.critStat, base.characterBody.master), DamageColorIndex.Default, null, -1f);
					this.hasSwung = true;
				}
				this.attack.forceVector = this.hammerChildTransform.right;
				this.attack.Fire(null);
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001287 RID: 4743
		public static float baseDuration = 3.5f;

		// Token: 0x04001288 RID: 4744
		public static float returnToIdlePercentage;

		// Token: 0x04001289 RID: 4745
		public static float impactDamageCoefficient = 2f;

		// Token: 0x0400128A RID: 4746
		public static float earthquakeDamageCoefficient = 2f;

		// Token: 0x0400128B RID: 4747
		public static float forceMagnitude = 16f;

		// Token: 0x0400128C RID: 4748
		public static float radius = 3f;

		// Token: 0x0400128D RID: 4749
		public static GameObject hitEffectPrefab;

		// Token: 0x0400128E RID: 4750
		public static GameObject swingEffectPrefab;

		// Token: 0x0400128F RID: 4751
		public static GameObject projectilePrefab;

		// Token: 0x04001290 RID: 4752
		private Transform hammerChildTransform;

		// Token: 0x04001291 RID: 4753
		private OverlapAttack attack;

		// Token: 0x04001292 RID: 4754
		private Animator modelAnimator;

		// Token: 0x04001293 RID: 4755
		private float duration;

		// Token: 0x04001294 RID: 4756
		private bool hasSwung;
	}
}
