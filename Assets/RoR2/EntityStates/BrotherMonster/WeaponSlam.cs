using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200044F RID: 1103
	public class WeaponSlam : BaseState
	{
		// Token: 0x060013BA RID: 5050 RVA: 0x0005797C File Offset: 0x00055B7C
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			Util.PlayAttackSpeedSound(WeaponSlam.attackSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayCrossfade("FullBody Override", "WeaponSlam", "WeaponSlam.playbackRate", WeaponSlam.duration, 0.1f);
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.GetAimRay().direction;
			}
			if (this.modelTransform)
			{
				AimAnimator component = this.modelTransform.GetComponent<AimAnimator>();
				if (component)
				{
					component.enabled = true;
				}
			}
			if (base.isAuthority)
			{
				OverlapAttack overlapAttack = new OverlapAttack();
				overlapAttack.attacker = base.gameObject;
				overlapAttack.damage = WeaponSlam.damageCoefficient * this.damageStat;
				overlapAttack.damageColorIndex = DamageColorIndex.Default;
				overlapAttack.damageType = DamageType.Generic;
				overlapAttack.hitEffectPrefab = WeaponSlam.weaponHitEffectPrefab;
				overlapAttack.hitBoxGroup = Array.Find<HitBoxGroup>(this.modelTransform.GetComponents<HitBoxGroup>(), (HitBoxGroup element) => element.groupName == "WeaponBig");
				overlapAttack.impactSound = WeaponSlam.weaponImpactSound.index;
				overlapAttack.inflictor = base.gameObject;
				overlapAttack.procChainMask = default(ProcChainMask);
				overlapAttack.pushAwayForce = WeaponSlam.weaponForce;
				overlapAttack.procCoefficient = 1f;
				overlapAttack.teamIndex = base.GetTeam();
				this.weaponAttack = overlapAttack;
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x00057AF8 File Offset: 0x00055CF8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.inputBank && base.skillLocator && base.skillLocator.utility.IsReady() && base.inputBank.skill3.justPressed)
			{
				base.skillLocator.utility.ExecuteIfReady();
				return;
			}
			if (this.modelAnimator)
			{
				if (base.isAuthority && this.modelAnimator.GetFloat("weapon.hitBoxActive") > 0.5f)
				{
					this.weaponAttack.Fire(null);
				}
				if (this.modelAnimator.GetFloat("blast.hitBoxActive") > 0.5f && !this.hasDoneBlastAttack)
				{
					this.hasDoneBlastAttack = true;
					EffectManager.SimpleMuzzleFlash(WeaponSlam.slamImpactEffect, base.gameObject, WeaponSlam.muzzleString, false);
					if (base.isAuthority)
					{
						if (base.characterDirection)
						{
							base.characterDirection.moveVector = base.characterDirection.forward;
						}
						if (this.modelTransform)
						{
							Transform transform = base.FindModelChild(WeaponSlam.muzzleString);
							if (transform)
							{
								this.blastAttack = new BlastAttack();
								this.blastAttack.attacker = base.gameObject;
								this.blastAttack.inflictor = base.gameObject;
								this.blastAttack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
								this.blastAttack.baseDamage = this.damageStat * WeaponSlam.damageCoefficient;
								this.blastAttack.baseForce = WeaponSlam.forceMagnitude;
								this.blastAttack.position = transform.position;
								this.blastAttack.radius = WeaponSlam.radius;
								this.blastAttack.bonusForce = new Vector3(0f, WeaponSlam.upwardForce, 0f);
								this.blastAttack.Fire();
							}
						}
						if (PhaseCounter.instance && PhaseCounter.instance.phase == 3)
						{
							Transform transform2 = base.FindModelChild(WeaponSlam.muzzleString);
							float num = WeaponSlam.waveProjectileArc / (float)WeaponSlam.waveProjectileCount;
							Vector3 point = Vector3.ProjectOnPlane(base.characterDirection.forward, Vector3.up);
							Vector3 position = base.characterBody.footPosition;
							if (transform2)
							{
								position = transform2.position;
							}
							for (int i = 0; i < WeaponSlam.waveProjectileCount; i++)
							{
								Vector3 forward = Quaternion.AngleAxis(num * ((float)i - (float)WeaponSlam.waveProjectileCount / 2f), Vector3.up) * point;
								ProjectileManager.instance.FireProjectile(WeaponSlam.waveProjectilePrefab, position, Util.QuaternionSafeLookRotation(forward), base.gameObject, base.characterBody.damage * WeaponSlam.waveProjectileDamageCoefficient, WeaponSlam.waveProjectileForce, Util.CheckRoll(base.characterBody.crit, base.characterBody.master), DamageColorIndex.Default, null, -1f);
							}
							ProjectileManager.instance.FireProjectile(WeaponSlam.pillarProjectilePrefab, position, Quaternion.identity, base.gameObject, base.characterBody.damage * WeaponSlam.pillarDamageCoefficient, 0f, Util.CheckRoll(base.characterBody.crit, base.characterBody.master), DamageColorIndex.Default, null, -1f);
						}
					}
				}
			}
			if (base.fixedAge >= WeaponSlam.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x00057E60 File Offset: 0x00056060
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			if (base.fixedAge <= WeaponSlam.durationBeforePriorityReduces)
			{
				return InterruptPriority.PrioritySkill;
			}
			return InterruptPriority.Skill;
		}

		// Token: 0x04001921 RID: 6433
		public static float duration = 3.5f;

		// Token: 0x04001922 RID: 6434
		public static float damageCoefficient = 4f;

		// Token: 0x04001923 RID: 6435
		public static float forceMagnitude = 16f;

		// Token: 0x04001924 RID: 6436
		public static float upwardForce;

		// Token: 0x04001925 RID: 6437
		public static float radius = 3f;

		// Token: 0x04001926 RID: 6438
		public static string attackSoundString;

		// Token: 0x04001927 RID: 6439
		public static string muzzleString;

		// Token: 0x04001928 RID: 6440
		public static GameObject slamImpactEffect;

		// Token: 0x04001929 RID: 6441
		public static float durationBeforePriorityReduces;

		// Token: 0x0400192A RID: 6442
		public static GameObject waveProjectilePrefab;

		// Token: 0x0400192B RID: 6443
		public static float waveProjectileArc;

		// Token: 0x0400192C RID: 6444
		public static int waveProjectileCount;

		// Token: 0x0400192D RID: 6445
		public static float waveProjectileDamageCoefficient;

		// Token: 0x0400192E RID: 6446
		public static float waveProjectileForce;

		// Token: 0x0400192F RID: 6447
		public static float weaponDamageCoefficient;

		// Token: 0x04001930 RID: 6448
		public static float weaponForce;

		// Token: 0x04001931 RID: 6449
		public static GameObject pillarProjectilePrefab;

		// Token: 0x04001932 RID: 6450
		public static float pillarDamageCoefficient;

		// Token: 0x04001933 RID: 6451
		public static GameObject weaponHitEffectPrefab;

		// Token: 0x04001934 RID: 6452
		public static NetworkSoundEventDef weaponImpactSound;

		// Token: 0x04001935 RID: 6453
		private BlastAttack blastAttack;

		// Token: 0x04001936 RID: 6454
		private OverlapAttack weaponAttack;

		// Token: 0x04001937 RID: 6455
		private Animator modelAnimator;

		// Token: 0x04001938 RID: 6456
		private Transform modelTransform;

		// Token: 0x04001939 RID: 6457
		private bool hasDoneBlastAttack;
	}
}
