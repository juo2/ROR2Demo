using System;
using System.Linq;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.ClayGrenadier
{
	// Token: 0x020003FC RID: 1020
	public class FaceSlam : BaseState
	{
		// Token: 0x06001252 RID: 4690 RVA: 0x0005195C File Offset: 0x0004FB5C
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			this.duration = FaceSlam.baseDuration / this.attackSpeedStat;
			this.durationBeforeBlast = FaceSlam.baseDurationBeforeBlast / this.attackSpeedStat;
			Util.PlayAttackSpeedSound(FaceSlam.attackSoundString, base.gameObject, this.attackSpeedStat);
			base.PlayAnimation(FaceSlam.animationLayerName, FaceSlam.animationStateName, FaceSlam.playbackRateParam, this.duration);
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.characterDirection.forward;
			}
			Transform transform = base.FindModelChild(FaceSlam.chargeEffectMuzzleString);
			if (transform && FaceSlam.chargeEffectPrefab)
			{
				this.chargeInstance = UnityEngine.Object.Instantiate<GameObject>(FaceSlam.chargeEffectPrefab, transform.position, transform.rotation);
				this.chargeInstance.transform.parent = transform;
				ScaleParticleSystemDuration component = this.chargeInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.durationBeforeBlast;
				}
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00051A6D File Offset: 0x0004FC6D
		public override void OnExit()
		{
			if (this.chargeInstance)
			{
				EntityState.Destroy(this.chargeInstance);
			}
			base.OnExit();
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00051A90 File Offset: 0x0004FC90
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.durationBeforeBlast && !this.hasFiredBlast)
			{
				this.hasFiredBlast = true;
				if (this.chargeInstance)
				{
					EntityState.Destroy(this.chargeInstance);
				}
				Vector3 footPosition = base.characterBody.footPosition;
				EffectManager.SpawnEffect(FaceSlam.blastImpactEffect, new EffectData
				{
					origin = footPosition,
					scale = FaceSlam.blastRadius
				}, true);
				if (NetworkServer.active && base.healthComponent)
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = base.healthComponent.combinedHealth * FaceSlam.healthCostFraction;
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
						Transform transform = base.FindModelChild(FaceSlam.blastMuzzleString);
						if (transform)
						{
							this.attack = new BlastAttack();
							this.attack.attacker = base.gameObject;
							this.attack.inflictor = base.gameObject;
							this.attack.teamIndex = TeamComponent.GetObjectTeam(base.gameObject);
							this.attack.baseDamage = this.damageStat * FaceSlam.blastDamageCoefficient;
							this.attack.baseForce = FaceSlam.blastForceMagnitude;
							this.attack.position = transform.position;
							this.attack.radius = FaceSlam.blastRadius;
							this.attack.bonusForce = new Vector3(0f, FaceSlam.blastUpwardForce, 0f);
							this.attack.damageType = DamageType.ClayGoo;
							this.attack.Fire();
						}
					}
					Vector3 position = footPosition;
					Vector3 up = Vector3.up;
					RaycastHit raycastHit;
					if (Physics.Raycast(base.GetAimRay(), out raycastHit, 1000f, LayerIndex.world.mask))
					{
						position = raycastHit.point;
					}
					BullseyeSearch bullseyeSearch = new BullseyeSearch();
					bullseyeSearch.viewer = base.characterBody;
					bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
					bullseyeSearch.teamMaskFilter.RemoveTeam(base.characterBody.teamComponent.teamIndex);
					bullseyeSearch.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
					bullseyeSearch.minDistanceFilter = 0f;
					bullseyeSearch.maxDistanceFilter = 1000f;
					bullseyeSearch.searchOrigin = base.inputBank.aimOrigin;
					bullseyeSearch.searchDirection = base.inputBank.aimDirection;
					bullseyeSearch.maxAngleFilter = FaceSlam.projectileSnapOnAngle;
					bullseyeSearch.filterByLoS = false;
					bullseyeSearch.RefreshCandidates();
					HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
					if (hurtBox && hurtBox.healthComponent)
					{
						position = hurtBox.healthComponent.body.footPosition;
					}
					ProjectileManager.instance.FireProjectile(FaceSlam.projectilePrefab, position, Quaternion.identity, base.gameObject, base.characterBody.damage * FaceSlam.projectileDamageCoefficient, FaceSlam.projectileForce, Util.CheckRoll(base.characterBody.crit, base.characterBody.master), DamageColorIndex.Default, null, -1f);
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x04001779 RID: 6009
		public static float baseDuration = 3.5f;

		// Token: 0x0400177A RID: 6010
		public static float baseDurationBeforeBlast = 1.5f;

		// Token: 0x0400177B RID: 6011
		public static string animationLayerName = "Body";

		// Token: 0x0400177C RID: 6012
		public static string animationStateName = "FaceSlam";

		// Token: 0x0400177D RID: 6013
		public static string playbackRateParam = "FaceSlam.playbackRate";

		// Token: 0x0400177E RID: 6014
		public static GameObject chargeEffectPrefab;

		// Token: 0x0400177F RID: 6015
		public static string chargeEffectMuzzleString;

		// Token: 0x04001780 RID: 6016
		public static GameObject blastImpactEffect;

		// Token: 0x04001781 RID: 6017
		public static float blastDamageCoefficient = 4f;

		// Token: 0x04001782 RID: 6018
		public static float blastForceMagnitude = 16f;

		// Token: 0x04001783 RID: 6019
		public static float blastUpwardForce;

		// Token: 0x04001784 RID: 6020
		public static float blastRadius = 3f;

		// Token: 0x04001785 RID: 6021
		public static string attackSoundString;

		// Token: 0x04001786 RID: 6022
		public static string blastMuzzleString;

		// Token: 0x04001787 RID: 6023
		public static GameObject projectilePrefab;

		// Token: 0x04001788 RID: 6024
		public static float projectileDamageCoefficient;

		// Token: 0x04001789 RID: 6025
		public static float projectileForce;

		// Token: 0x0400178A RID: 6026
		public static float projectileSnapOnAngle;

		// Token: 0x0400178B RID: 6027
		public static float healthCostFraction;

		// Token: 0x0400178C RID: 6028
		private BlastAttack attack;

		// Token: 0x0400178D RID: 6029
		private Animator modelAnimator;

		// Token: 0x0400178E RID: 6030
		private Transform modelTransform;

		// Token: 0x0400178F RID: 6031
		private bool hasFiredBlast;

		// Token: 0x04001790 RID: 6032
		private float duration;

		// Token: 0x04001791 RID: 6033
		private float durationBeforeBlast;

		// Token: 0x04001792 RID: 6034
		private GameObject chargeInstance;
	}
}
