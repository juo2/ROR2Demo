using System;
using System.Linq;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.AcidLarva
{
	// Token: 0x020004A4 RID: 1188
	public class LarvaLeap : BaseCharacterMain
	{
		// Token: 0x0600154E RID: 5454 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected virtual DamageType GetBlastDamageType()
		{
			return DamageType.Generic;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0005E848 File Offset: 0x0005CA48
		public override void OnEnter()
		{
			base.OnEnter();
			this.previousAirControl = base.characterMotor.airControl;
			base.characterMotor.airControl = this.airControl;
			Vector3 direction = base.GetAimRay().direction;
			if (base.isAuthority)
			{
				base.characterBody.isSprinting = true;
				direction.y = Mathf.Max(direction.y, this.minimumY);
				Vector3 a = direction.normalized * this.aimVelocity * this.moveSpeedStat;
				Vector3 b = Vector3.up * this.upwardVelocity;
				Vector3 b2 = new Vector3(direction.x, 0f, direction.z).normalized * this.forwardVelocity;
				base.characterMotor.Motor.ForceUnground();
				base.characterMotor.velocity = a + b + b2;
				this.isCritAuthority = base.RollCrit();
			}
			base.PlayCrossfade("Gesture, Override", "LarvaLeap", 0.1f);
			Util.PlaySound(this.leapSoundString, base.gameObject);
			base.characterDirection.moveVector = direction;
			this.spinEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.spinEffectPrefab, base.FindModelChild(this.spinEffectMuzzleString));
			if (base.isAuthority)
			{
				base.characterMotor.onMovementHit += this.OnMovementHit;
			}
			Util.PlaySound(this.soundLoopStartEvent, base.gameObject);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x0005E9CC File Offset: 0x0005CBCC
		private void OnMovementHit(ref CharacterMotor.MovementHitInfo movementHitInfo)
		{
			this.detonateNextFrame = true;
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x000309AC File Offset: 0x0002EBAC
		protected override void UpdateAnimationParameters()
		{
			base.UpdateAnimationParameters();
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x0005E9D8 File Offset: 0x0005CBD8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.characterMotor)
			{
				base.characterMotor.moveDirection = base.inputBank.moveVector;
				base.characterDirection.moveVector = base.characterMotor.velocity;
				base.characterMotor.disableAirControlUntilCollision = (base.characterMotor.velocity.y < 0f);
				if (base.fixedAge >= this.minimumDuration && (this.detonateNextFrame || (base.characterMotor.Motor.GroundingStatus.IsStableOnGround && !base.characterMotor.Motor.LastGroundingStatus.IsStableOnGround)))
				{
					bool flag = true;
					if (this.confirmDetonate)
					{
						BullseyeSearch bullseyeSearch = new BullseyeSearch();
						bullseyeSearch.viewer = base.characterBody;
						bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
						bullseyeSearch.teamMaskFilter.RemoveTeam(base.characterBody.teamComponent.teamIndex);
						bullseyeSearch.sortMode = BullseyeSearch.SortMode.Distance;
						bullseyeSearch.minDistanceFilter = 0f;
						bullseyeSearch.maxDistanceFilter = this.maxRadiusToConfirmDetonate;
						bullseyeSearch.searchOrigin = base.inputBank.aimOrigin;
						bullseyeSearch.searchDirection = base.inputBank.aimDirection;
						bullseyeSearch.maxAngleFilter = 180f;
						bullseyeSearch.filterByLoS = false;
						bullseyeSearch.RefreshCandidates();
						flag = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
					}
					if (flag)
					{
						this.DoImpactAuthority();
					}
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x0005EB64 File Offset: 0x0005CD64
		protected virtual void DoImpactAuthority()
		{
			this.DetonateAuthority();
			if (this.landingSound)
			{
				EffectManager.SimpleSoundEffect(this.landingSound.index, base.characterBody.footPosition, true);
			}
			base.healthComponent.TakeDamage(new DamageInfo
			{
				damage = base.healthComponent.fullCombinedHealth * this.detonateSelfDamageFraction,
				attacker = base.characterBody.gameObject,
				position = base.characterBody.corePosition,
				damageType = DamageType.Generic
			});
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x0005EBF4 File Offset: 0x0005CDF4
		protected BlastAttack.Result DetonateAuthority()
		{
			Vector3 footPosition = base.characterBody.footPosition;
			EffectManager.SpawnEffect(this.blastEffectPrefab, new EffectData
			{
				origin = footPosition,
				scale = this.blastRadius
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
				procCoefficient = this.blastProcCoefficient,
				radius = this.blastRadius,
				position = footPosition,
				attackerFiltering = AttackerFiltering.NeverHitSelf,
				impactEffect = EffectCatalog.FindEffectIndexFromPrefab(this.blastImpactEffectPrefab),
				teamIndex = base.teamComponent.teamIndex
			}.Fire();
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x0005ECDC File Offset: 0x0005CEDC
		protected void FireProjectile()
		{
			Vector3 footPosition = base.characterBody.footPosition;
			FireProjectileInfo fireProjectileInfo = new FireProjectileInfo
			{
				projectilePrefab = this.projectilePrefab,
				crit = this.isCritAuthority,
				force = 0f,
				damage = this.damageStat,
				owner = base.gameObject,
				rotation = Quaternion.identity,
				position = footPosition
			};
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x0005ED60 File Offset: 0x0005CF60
		public override void OnExit()
		{
			Util.PlaySound(this.soundLoopStopEvent, base.gameObject);
			if (base.isAuthority)
			{
				base.characterMotor.onMovementHit -= this.OnMovementHit;
			}
			base.characterMotor.airControl = this.previousAirControl;
			base.characterBody.isSprinting = false;
			if (this.spinEffectInstance)
			{
				EntityState.Destroy(this.spinEffectInstance);
			}
			this.PlayAnimation("Gesture, Override", "Empty");
			base.OnExit();
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001B23 RID: 6947
		[SerializeField]
		public float minimumDuration;

		// Token: 0x04001B24 RID: 6948
		[SerializeField]
		public float blastRadius;

		// Token: 0x04001B25 RID: 6949
		[SerializeField]
		public float blastProcCoefficient;

		// Token: 0x04001B26 RID: 6950
		[SerializeField]
		public float blastDamageCoefficient;

		// Token: 0x04001B27 RID: 6951
		[SerializeField]
		public float blastForce;

		// Token: 0x04001B28 RID: 6952
		[SerializeField]
		public string leapSoundString;

		// Token: 0x04001B29 RID: 6953
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x04001B2A RID: 6954
		[SerializeField]
		public Vector3 blastBonusForce;

		// Token: 0x04001B2B RID: 6955
		[SerializeField]
		public GameObject blastImpactEffectPrefab;

		// Token: 0x04001B2C RID: 6956
		[SerializeField]
		public GameObject blastEffectPrefab;

		// Token: 0x04001B2D RID: 6957
		[SerializeField]
		public float airControl;

		// Token: 0x04001B2E RID: 6958
		[SerializeField]
		public float aimVelocity;

		// Token: 0x04001B2F RID: 6959
		[SerializeField]
		public float upwardVelocity;

		// Token: 0x04001B30 RID: 6960
		[SerializeField]
		public float forwardVelocity;

		// Token: 0x04001B31 RID: 6961
		[SerializeField]
		public float minimumY;

		// Token: 0x04001B32 RID: 6962
		[SerializeField]
		public float minYVelocityForAnim;

		// Token: 0x04001B33 RID: 6963
		[SerializeField]
		public float maxYVelocityForAnim;

		// Token: 0x04001B34 RID: 6964
		[SerializeField]
		public float knockbackForce;

		// Token: 0x04001B35 RID: 6965
		[SerializeField]
		public float maxRadiusToConfirmDetonate;

		// Token: 0x04001B36 RID: 6966
		[SerializeField]
		public bool confirmDetonate;

		// Token: 0x04001B37 RID: 6967
		[SerializeField]
		public GameObject spinEffectPrefab;

		// Token: 0x04001B38 RID: 6968
		[SerializeField]
		public string spinEffectMuzzleString;

		// Token: 0x04001B39 RID: 6969
		[SerializeField]
		public string soundLoopStartEvent;

		// Token: 0x04001B3A RID: 6970
		[SerializeField]
		public string soundLoopStopEvent;

		// Token: 0x04001B3B RID: 6971
		[SerializeField]
		public NetworkSoundEventDef landingSound;

		// Token: 0x04001B3C RID: 6972
		[SerializeField]
		public float detonateSelfDamageFraction;

		// Token: 0x04001B3D RID: 6973
		private float previousAirControl;

		// Token: 0x04001B3E RID: 6974
		private GameObject spinEffectInstance;

		// Token: 0x04001B3F RID: 6975
		protected bool isCritAuthority;

		// Token: 0x04001B40 RID: 6976
		protected CrocoDamageTypeController crocoDamageTypeController;

		// Token: 0x04001B41 RID: 6977
		private bool detonateNextFrame;
	}
}
