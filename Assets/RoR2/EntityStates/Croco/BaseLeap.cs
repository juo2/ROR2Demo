using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.Croco
{
	// Token: 0x020003DC RID: 988
	public class BaseLeap : BaseCharacterMain
	{
		// Token: 0x060011A6 RID: 4518 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual DamageType GetBlastDamageType()
		{
			return DamageType.Generic;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0004DCA4 File Offset: 0x0004BEA4
		public override void OnEnter()
		{
			base.OnEnter();
			this.crocoDamageTypeController = base.GetComponent<CrocoDamageTypeController>();
			this.previousAirControl = base.characterMotor.airControl;
			base.characterMotor.airControl = BaseLeap.airControl;
			Vector3 direction = base.GetAimRay().direction;
			if (base.isAuthority)
			{
				base.characterBody.isSprinting = true;
				direction.y = Mathf.Max(direction.y, BaseLeap.minimumY);
				Vector3 a = direction.normalized * BaseLeap.aimVelocity * this.moveSpeedStat;
				Vector3 b = Vector3.up * BaseLeap.upwardVelocity;
				Vector3 b2 = new Vector3(direction.x, 0f, direction.z).normalized * BaseLeap.forwardVelocity;
				base.characterMotor.Motor.ForceUnground();
				base.characterMotor.velocity = a + b + b2;
				this.isCritAuthority = base.RollCrit();
			}
			base.characterBody.bodyFlags |= CharacterBody.BodyFlags.IgnoreFallDamage;
			base.GetModelTransform().GetComponent<AimAnimator>().enabled = true;
			base.PlayCrossfade("Gesture, Override", "Leap", 0.1f);
			base.PlayCrossfade("Gesture, AdditiveHigh", "Leap", 0.1f);
			base.PlayCrossfade("Gesture, Override", "Leap", 0.1f);
			Util.PlaySound(BaseLeap.leapSoundString, base.gameObject);
			base.characterDirection.moveVector = direction;
			this.leftFistEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.fistEffectPrefab, base.FindModelChild("MuzzleHandL"));
			this.rightFistEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.fistEffectPrefab, base.FindModelChild("MuzzleHandR"));
			if (base.isAuthority)
			{
				base.characterMotor.onMovementHit += this.OnMovementHit;
			}
			Util.PlaySound(BaseLeap.soundLoopStartEvent, base.gameObject);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0004DE96 File Offset: 0x0004C096
		private void OnMovementHit(ref CharacterMotor.MovementHitInfo movementHitInfo)
		{
			this.detonateNextFrame = true;
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0004DEA0 File Offset: 0x0004C0A0
		protected override void UpdateAnimationParameters()
		{
			base.UpdateAnimationParameters();
			float value = Mathf.Clamp01(Util.Remap(base.estimatedVelocity.y, BaseLeap.minYVelocityForAnim, BaseLeap.maxYVelocityForAnim, 0f, 1f)) * 0.97f;
			base.modelAnimator.SetFloat("LeapCycle", value, 0.1f, Time.deltaTime);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0004DF00 File Offset: 0x0004C100
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.characterMotor)
			{
				base.characterMotor.moveDirection = base.inputBank.moveVector;
				if (base.fixedAge >= BaseLeap.minimumDuration && (this.detonateNextFrame || (base.characterMotor.Motor.GroundingStatus.IsStableOnGround && !base.characterMotor.Motor.LastGroundingStatus.IsStableOnGround)))
				{
					this.DoImpactAuthority();
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0004DF92 File Offset: 0x0004C192
		protected virtual void DoImpactAuthority()
		{
			if (BaseLeap.landingSound)
			{
				EffectManager.SimpleSoundEffect(BaseLeap.landingSound.index, base.characterBody.footPosition, true);
			}
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0004DFBC File Offset: 0x0004C1BC
		protected BlastAttack.Result DetonateAuthority()
		{
			Vector3 footPosition = base.characterBody.footPosition;
			EffectManager.SpawnEffect(this.blastEffectPrefab, new EffectData
			{
				origin = footPosition,
				scale = BaseLeap.blastRadius
			}, true);
			return new BlastAttack
			{
				attacker = base.gameObject,
				baseDamage = this.damageStat * this.blastDamageCoefficient,
				baseForce = this.blastForce,
				bonusForce = this.blastBonusForce,
				crit = this.isCritAuthority,
				damageType = this.GetBlastDamageType(),
				falloffModel = BlastAttack.FalloffModel.None,
				procCoefficient = BaseLeap.blastProcCoefficient,
				radius = BaseLeap.blastRadius,
				position = footPosition,
				attackerFiltering = AttackerFiltering.NeverHitSelf,
				impactEffect = EffectCatalog.FindEffectIndexFromPrefab(this.blastImpactEffectPrefab),
				teamIndex = base.teamComponent.teamIndex
			}.Fire();
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0004E0A0 File Offset: 0x0004C2A0
		protected void DropAcidPoolAuthority()
		{
			Vector3 footPosition = base.characterBody.footPosition;
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				projectilePrefab = BaseLeap.projectilePrefab,
				crit = this.isCritAuthority,
				force = 0f,
				damage = this.damageStat,
				owner = base.gameObject,
				rotation = Quaternion.identity,
				position = footPosition
			};
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0004E124 File Offset: 0x0004C324
		public override void OnExit()
		{
			Util.PlaySound(BaseLeap.soundLoopStopEvent, base.gameObject);
			if (base.isAuthority)
			{
				base.characterMotor.onMovementHit -= this.OnMovementHit;
			}
			base.characterBody.bodyFlags &= ~CharacterBody.BodyFlags.IgnoreFallDamage;
			base.characterMotor.airControl = this.previousAirControl;
			base.characterBody.isSprinting = false;
			int layerIndex = base.modelAnimator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				base.modelAnimator.SetLayerWeight(layerIndex, 2f);
				this.PlayAnimation("Impact", "LightImpact");
			}
			base.PlayCrossfade("Gesture, Override", "BufferEmpty", 0.1f);
			base.PlayCrossfade("Gesture, AdditiveHigh", "BufferEmpty", 0.1f);
			EntityState.Destroy(this.leftFistEffectInstance);
			EntityState.Destroy(this.rightFistEffectInstance);
			base.OnExit();
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001662 RID: 5730
		public static float minimumDuration;

		// Token: 0x04001663 RID: 5731
		public static float blastRadius;

		// Token: 0x04001664 RID: 5732
		public static float blastProcCoefficient;

		// Token: 0x04001665 RID: 5733
		[SerializeField]
		public float blastDamageCoefficient;

		// Token: 0x04001666 RID: 5734
		[SerializeField]
		public float blastForce;

		// Token: 0x04001667 RID: 5735
		public static string leapSoundString;

		// Token: 0x04001668 RID: 5736
		public static GameObject projectilePrefab;

		// Token: 0x04001669 RID: 5737
		[SerializeField]
		public Vector3 blastBonusForce;

		// Token: 0x0400166A RID: 5738
		[SerializeField]
		public GameObject blastImpactEffectPrefab;

		// Token: 0x0400166B RID: 5739
		[SerializeField]
		public GameObject blastEffectPrefab;

		// Token: 0x0400166C RID: 5740
		public static float airControl;

		// Token: 0x0400166D RID: 5741
		public static float aimVelocity;

		// Token: 0x0400166E RID: 5742
		public static float upwardVelocity;

		// Token: 0x0400166F RID: 5743
		public static float forwardVelocity;

		// Token: 0x04001670 RID: 5744
		public static float minimumY;

		// Token: 0x04001671 RID: 5745
		public static float minYVelocityForAnim;

		// Token: 0x04001672 RID: 5746
		public static float maxYVelocityForAnim;

		// Token: 0x04001673 RID: 5747
		public static float knockbackForce;

		// Token: 0x04001674 RID: 5748
		[SerializeField]
		public GameObject fistEffectPrefab;

		// Token: 0x04001675 RID: 5749
		public static string soundLoopStartEvent;

		// Token: 0x04001676 RID: 5750
		public static string soundLoopStopEvent;

		// Token: 0x04001677 RID: 5751
		public static NetworkSoundEventDef landingSound;

		// Token: 0x04001678 RID: 5752
		private float previousAirControl;

		// Token: 0x04001679 RID: 5753
		private GameObject leftFistEffectInstance;

		// Token: 0x0400167A RID: 5754
		private GameObject rightFistEffectInstance;

		// Token: 0x0400167B RID: 5755
		protected bool isCritAuthority;

		// Token: 0x0400167C RID: 5756
		protected CrocoDamageTypeController crocoDamageTypeController;

		// Token: 0x0400167D RID: 5757
		private bool detonateNextFrame;
	}
}
