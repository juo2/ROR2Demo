using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BrotherMonster
{
	// Token: 0x02000435 RID: 1077
	public class FistSlam : BaseState
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x00055EEC File Offset: 0x000540EC
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			this.duration = FistSlam.baseDuration / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(FistSlam.attackSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayCrossfade("FullBody Override", "FistSlam", "FistSlam.playbackRate", this.duration, 0.1f);
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.characterDirection.forward;
			}
			if (this.modelTransform)
			{
				AimAnimator component = this.modelTransform.GetComponent<AimAnimator>();
				if (component)
				{
					component.enabled = true;
				}
			}
			Transform transform = base.FindModelChild("MuzzleRight");
			if (transform && FistSlam.chargeEffectPrefab)
			{
				this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(FistSlam.chargeEffectPrefab, transform.position, transform.rotation);
				this.chargeInstance.transform.parent = transform;
				ScaleParticleSystemDuration component2 = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component2)
				{
					component2.newDuration = this.duration / 2.8f;
				}
			}
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0005601E File Offset: 0x0005421E
		public override void OnExit()
		{
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
			this.PlayAnimation("FullBody Override", "BufferEmpty");
			base.OnExit();
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00056050 File Offset: 0x00054250
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.modelAnimator && this.modelAnimator.GetFloat("fist.hitBoxActive") > 0.5f && !this.hasAttacked)
			{
				if (this.chargeInstance)
				{
					EntityState.Destroy(this.chargeInstance);
				}
				EffectManager.SimpleMuzzleFlash(FistSlam.slamImpactEffect, base.gameObject, FistSlam.muzzleString, false);
				if (NetworkServer.active && base.healthComponent)
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = base.healthComponent.combinedHealth * FistSlam.healthCostFraction;
					damageInfo.position = base.characterBody.corePosition;
					damageInfo.force = Vector3.zero;
					damageInfo.damageColorIndex = DamageColorIndex.Default;
					damageInfo.crit = false;
					damageInfo.attacker = null;
					damageInfo.inflictor = null;
					damageInfo.damageType = (DamageType.NonLethal | DamageType.BypassArmor);
					damageInfo.procCoefficient = 0f;
					damageInfo.procChainMask = default(ProcChainMask);
					base.healthComponent.TakeDamage(damageInfo);
				}
				if (base.isAuthority)
				{
					if (this.modelTransform)
					{
						Transform transform = base.FindModelChild(FistSlam.muzzleString);
						if (transform)
						{
							this.attack = new BlastAttack();
							this.attack.attacker = base.gameObject;
							this.attack.inflictor = base.gameObject;
							this.attack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
							this.attack.baseDamage = this.damageStat * FistSlam.damageCoefficient;
							this.attack.baseForce = FistSlam.forceMagnitude;
							this.attack.position = transform.position;
							this.attack.radius = FistSlam.radius;
							this.attack.bonusForce = new Vector3(0f, FistSlam.upwardForce, 0f);
							this.attack.Fire();
						}
					}
					float num = 360f / (float)FistSlam.waveProjectileCount;
					Vector3 point = Vector3.ProjectOnPlane(base.inputBank.aimDirection, Vector3.up);
					Vector3 footPosition = base.characterBody.footPosition;
					for (int i = 0; i < FistSlam.waveProjectileCount; i++)
					{
						Vector3 forward = Quaternion.AngleAxis(num * (float)i, Vector3.up) * point;
						ProjectileManager.instance.FireProjectile(FistSlam.waveProjectilePrefab, footPosition, Util.QuaternionSafeLookRotation(forward), base.gameObject, base.characterBody.damage * FistSlam.waveProjectileDamageCoefficient, FistSlam.waveProjectileForce, Util.CheckRoll(base.characterBody.crit, base.characterBody.master), DamageColorIndex.Default, null, -1f);
					}
				}
				this.hasAttacked = true;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x040018BF RID: 6335
		public static float baseDuration = 3.5f;

		// Token: 0x040018C0 RID: 6336
		public static float damageCoefficient = 4f;

		// Token: 0x040018C1 RID: 6337
		public static float forceMagnitude = 16f;

		// Token: 0x040018C2 RID: 6338
		public static float upwardForce;

		// Token: 0x040018C3 RID: 6339
		public static float radius = 3f;

		// Token: 0x040018C4 RID: 6340
		public static string attackSoundString;

		// Token: 0x040018C5 RID: 6341
		public static string muzzleString;

		// Token: 0x040018C6 RID: 6342
		public static float healthCostFraction;

		// Token: 0x040018C7 RID: 6343
		public static GameObject chargeEffectPrefab;

		// Token: 0x040018C8 RID: 6344
		public static GameObject slamImpactEffect;

		// Token: 0x040018C9 RID: 6345
		public static GameObject waveProjectilePrefab;

		// Token: 0x040018CA RID: 6346
		public static int waveProjectileCount;

		// Token: 0x040018CB RID: 6347
		public static float waveProjectileDamageCoefficient;

		// Token: 0x040018CC RID: 6348
		public static float waveProjectileForce;

		// Token: 0x040018CD RID: 6349
		private BlastAttack attack;

		// Token: 0x040018CE RID: 6350
		private Animator modelAnimator;

		// Token: 0x040018CF RID: 6351
		private Transform modelTransform;

		// Token: 0x040018D0 RID: 6352
		private bool hasAttacked;

		// Token: 0x040018D1 RID: 6353
		private float duration;

		// Token: 0x040018D2 RID: 6354
		private GameObject chargeInstance;
	}
}
