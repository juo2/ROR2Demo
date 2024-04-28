using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Loader
{
	// Token: 0x020002C7 RID: 711
	public class GroundSlam : BaseCharacterMain
	{
		// Token: 0x06000C98 RID: 3224 RVA: 0x000350B8 File Offset: 0x000332B8
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Body", "GroundSlam", 0.2f);
			if (base.isAuthority)
			{
				base.characterMotor.onMovementHit += this.OnMovementHit;
				base.characterMotor.velocity.y = GroundSlam.initialVerticalVelocity;
			}
			Util.PlaySound(GroundSlam.enterSoundString, base.gameObject);
			this.previousAirControl = base.characterMotor.airControl;
			base.characterMotor.airControl = GroundSlam.airControl;
			this.leftFistEffectInstance = UnityEngine.Object.Instantiate<GameObject>(GroundSlam.fistEffectPrefab, base.FindModelChild("MechHandR"));
			this.rightFistEffectInstance = UnityEngine.Object.Instantiate<GameObject>(GroundSlam.fistEffectPrefab, base.FindModelChild("MechHandL"));
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003517C File Offset: 0x0003337C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.characterMotor)
			{
				base.characterMotor.moveDirection = base.inputBank.moveVector;
				base.characterDirection.moveVector = base.characterMotor.moveDirection;
				CharacterMotor characterMotor = base.characterMotor;
				characterMotor.velocity.y = characterMotor.velocity.y + GroundSlam.verticalAcceleration * Time.fixedDeltaTime;
				if (base.fixedAge >= GroundSlam.minimumDuration && (this.detonateNextFrame || base.characterMotor.Motor.GroundingStatus.IsStableOnGround))
				{
					this.DetonateAuthority();
					this.outer.SetNextStateToMain();
				}
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00035234 File Offset: 0x00033434
		public override void OnExit()
		{
			if (base.isAuthority)
			{
				base.characterMotor.onMovementHit -= this.OnMovementHit;
				base.characterMotor.Motor.ForceUnground();
				base.characterMotor.velocity *= GroundSlam.exitSlowdownCoefficient;
				base.characterMotor.velocity.y = GroundSlam.exitVerticalVelocity;
			}
			base.characterMotor.airControl = this.previousAirControl;
			EntityState.Destroy(this.leftFistEffectInstance);
			EntityState.Destroy(this.rightFistEffectInstance);
			base.OnExit();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000352CD File Offset: 0x000334CD
		private void OnMovementHit(ref CharacterMotor.MovementHitInfo movementHitInfo)
		{
			this.detonateNextFrame = true;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000352D8 File Offset: 0x000334D8
		protected BlastAttack.Result DetonateAuthority()
		{
			Vector3 footPosition = base.characterBody.footPosition;
			EffectManager.SpawnEffect(GroundSlam.blastEffectPrefab, new EffectData
			{
				origin = footPosition,
				scale = GroundSlam.blastRadius
			}, true);
			return new BlastAttack
			{
				attacker = base.gameObject,
				baseDamage = this.damageStat * GroundSlam.blastDamageCoefficient,
				baseForce = GroundSlam.blastForce,
				bonusForce = GroundSlam.blastBonusForce,
				crit = base.RollCrit(),
				damageType = DamageType.Stun1s,
				falloffModel = BlastAttack.FalloffModel.None,
				procCoefficient = GroundSlam.blastProcCoefficient,
				radius = GroundSlam.blastRadius,
				position = footPosition,
				attackerFiltering = AttackerFiltering.NeverHitSelf,
				impactEffect = EffectCatalog.FindEffectIndexFromPrefab(GroundSlam.blastImpactEffectPrefab),
				teamIndex = base.teamComponent.teamIndex
			}.Fire();
		}

		// Token: 0x04000F5D RID: 3933
		public static float airControl;

		// Token: 0x04000F5E RID: 3934
		public static float minimumDuration;

		// Token: 0x04000F5F RID: 3935
		public static float blastRadius;

		// Token: 0x04000F60 RID: 3936
		public static float blastProcCoefficient;

		// Token: 0x04000F61 RID: 3937
		public static float blastDamageCoefficient;

		// Token: 0x04000F62 RID: 3938
		public static float blastForce;

		// Token: 0x04000F63 RID: 3939
		public static string enterSoundString;

		// Token: 0x04000F64 RID: 3940
		public static float initialVerticalVelocity;

		// Token: 0x04000F65 RID: 3941
		public static float exitVerticalVelocity;

		// Token: 0x04000F66 RID: 3942
		public static float verticalAcceleration;

		// Token: 0x04000F67 RID: 3943
		public static float exitSlowdownCoefficient;

		// Token: 0x04000F68 RID: 3944
		public static Vector3 blastBonusForce;

		// Token: 0x04000F69 RID: 3945
		public static GameObject blastImpactEffectPrefab;

		// Token: 0x04000F6A RID: 3946
		public static GameObject blastEffectPrefab;

		// Token: 0x04000F6B RID: 3947
		public static GameObject fistEffectPrefab;

		// Token: 0x04000F6C RID: 3948
		private float previousAirControl;

		// Token: 0x04000F6D RID: 3949
		private GameObject leftFistEffectInstance;

		// Token: 0x04000F6E RID: 3950
		private GameObject rightFistEffectInstance;

		// Token: 0x04000F6F RID: 3951
		private bool detonateNextFrame;
	}
}
