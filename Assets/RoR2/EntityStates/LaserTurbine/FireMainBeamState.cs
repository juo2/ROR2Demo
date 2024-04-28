using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.LaserTurbine
{
	// Token: 0x020002DE RID: 734
	public class FireMainBeamState : LaserTurbineBaseState
	{
		// Token: 0x06000D18 RID: 3352 RVA: 0x00036F1C File Offset: 0x0003511C
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.initialAimRay = base.GetAimRay();
			}
			if (NetworkServer.active)
			{
				this.isCrit = base.ownerBody.RollCrit();
				this.FireBeamServer(this.initialAimRay, FireMainBeamState.forwardBeamTracerEffect, FireMainBeamState.mainBeamMaxDistance, true);
			}
			base.laserTurbineController.showTurbineDisplay = false;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00036F7E File Offset: 0x0003517E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= FireMainBeamState.baseDuration)
			{
				this.outer.SetNextState(new RechargeState());
			}
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00036FAC File Offset: 0x000351AC
		public override void OnExit()
		{
			if (NetworkServer.active && !this.outer.destroying)
			{
				Vector3 direction = this.initialAimRay.origin - this.beamHitPosition;
				Ray aimRay = new Ray(this.beamHitPosition, direction);
				this.FireBeamServer(aimRay, FireMainBeamState.backwardBeamTracerEffect, direction.magnitude, false);
			}
			base.laserTurbineController.showTurbineDisplay = true;
			base.OnExit();
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00037018 File Offset: 0x00035218
		private void FireBeamServer(Ray aimRay, GameObject tracerEffectPrefab, float maxDistance, bool isInitialBeam)
		{
			bool didHit = false;
			BulletAttack bulletAttack = new BulletAttack();
			bulletAttack.origin = aimRay.origin;
			bulletAttack.aimVector = aimRay.direction;
			bulletAttack.bulletCount = 1U;
			bulletAttack.damage = base.GetDamage() * FireMainBeamState.mainBeamDamageCoefficient;
			bulletAttack.damageColorIndex = DamageColorIndex.Item;
			bulletAttack.damageType = DamageType.Generic;
			bulletAttack.falloffModel = BulletAttack.FalloffModel.None;
			bulletAttack.force = FireMainBeamState.mainBeamForce;
			bulletAttack.hitEffectPrefab = FireMainBeamState.mainBeamImpactEffect;
			bulletAttack.HitEffectNormal = false;
			bulletAttack.hitMask = LayerIndex.entityPrecise.mask;
			bulletAttack.isCrit = this.isCrit;
			bulletAttack.maxDistance = maxDistance;
			bulletAttack.minSpread = 0f;
			bulletAttack.maxSpread = 0f;
			bulletAttack.muzzleName = "";
			bulletAttack.owner = base.ownerBody.gameObject;
			bulletAttack.procChainMask = default(ProcChainMask);
			bulletAttack.procCoefficient = FireMainBeamState.mainBeamProcCoefficient;
			bulletAttack.queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
			bulletAttack.radius = FireMainBeamState.mainBeamRadius;
			bulletAttack.smartCollision = true;
			bulletAttack.sniper = false;
			bulletAttack.spreadPitchScale = 1f;
			bulletAttack.spreadYawScale = 1f;
			bulletAttack.stopperMask = LayerIndex.world.mask;
			bulletAttack.tracerEffectPrefab = (isInitialBeam ? tracerEffectPrefab : null);
			bulletAttack.weapon = base.gameObject;
			TeamIndex teamIndex = base.ownerBody.teamComponent.teamIndex;
			bulletAttack.hitCallback = delegate(BulletAttack _bulletAttack, ref BulletAttack.BulletHit info)
			{
				bool flag = BulletAttack.defaultHitCallback(_bulletAttack, ref info);
				if (!isInitialBeam)
				{
					return true;
				}
				if (flag)
				{
					HealthComponent healthComponent = info.hitHurtBox ? info.hitHurtBox.healthComponent : null;
					if (healthComponent && healthComponent.alive && info.hitHurtBox.teamIndex != teamIndex)
					{
						flag = false;
					}
				}
				if (!flag)
				{
					didHit = true;
					this.beamHitPosition = info.point;
				}
				return flag;
			};
			bulletAttack.filterCallback = delegate(BulletAttack _bulletAttack, ref BulletAttack.BulletHit info)
			{
				return (!info.entityObject || info.entityObject != _bulletAttack.owner) && BulletAttack.defaultFilterCallback(_bulletAttack, ref info);
			};
			bulletAttack.Fire();
			if (!didHit)
			{
				RaycastHit raycastHit;
				if (Physics.Raycast(aimRay, out raycastHit, FireMainBeamState.mainBeamMaxDistance, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					didHit = true;
					this.beamHitPosition = raycastHit.point;
				}
				else
				{
					this.beamHitPosition = aimRay.GetPoint(FireMainBeamState.mainBeamMaxDistance);
				}
			}
			if (didHit & isInitialBeam)
			{
				FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
				fireProjectileInfo.projectilePrefab = FireMainBeamState.secondBombPrefab;
				fireProjectileInfo.owner = base.ownerBody.gameObject;
				fireProjectileInfo.position = this.beamHitPosition - aimRay.direction * 0.5f;
				fireProjectileInfo.rotation = Quaternion.identity;
				fireProjectileInfo.damage = base.GetDamage() * FireMainBeamState.secondBombDamageCoefficient;
				fireProjectileInfo.damageColorIndex = DamageColorIndex.Item;
				fireProjectileInfo.crit = this.isCrit;
				ProjectileManager.instance.FireProjectile(fireProjectileInfo);
			}
			if (!isInitialBeam)
			{
				EffectData effectData = new EffectData
				{
					origin = aimRay.origin,
					start = base.transform.position
				};
				effectData.SetNetworkedObjectReference(base.gameObject);
				EffectManager.SpawnEffect(tracerEffectPrefab, effectData, true);
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00037304 File Offset: 0x00035504
		public override void OnSerialize(NetworkWriter writer)
		{
			base.OnSerialize(writer);
			Vector3 origin = this.initialAimRay.origin;
			Vector3 direction = this.initialAimRay.direction;
			writer.Write(origin);
			writer.Write(direction);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00037340 File Offset: 0x00035540
		public override void OnDeserialize(NetworkReader reader)
		{
			base.OnDeserialize(reader);
			Vector3 origin = reader.ReadVector3();
			Vector3 direction = reader.ReadVector3();
			this.initialAimRay = new Ray(origin, direction);
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldFollow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000FEF RID: 4079
		public static float baseDuration;

		// Token: 0x04000FF0 RID: 4080
		public static float mainBeamDamageCoefficient;

		// Token: 0x04000FF1 RID: 4081
		public static float mainBeamProcCoefficient;

		// Token: 0x04000FF2 RID: 4082
		public static float mainBeamForce;

		// Token: 0x04000FF3 RID: 4083
		public static float mainBeamRadius;

		// Token: 0x04000FF4 RID: 4084
		public static float mainBeamMaxDistance;

		// Token: 0x04000FF5 RID: 4085
		public static GameObject forwardBeamTracerEffect;

		// Token: 0x04000FF6 RID: 4086
		public static GameObject backwardBeamTracerEffect;

		// Token: 0x04000FF7 RID: 4087
		public static GameObject mainBeamImpactEffect;

		// Token: 0x04000FF8 RID: 4088
		public static GameObject secondBombPrefab;

		// Token: 0x04000FF9 RID: 4089
		public static float secondBombDamageCoefficient;

		// Token: 0x04000FFA RID: 4090
		private Ray initialAimRay;

		// Token: 0x04000FFB RID: 4091
		private Vector3 beamHitPosition;

		// Token: 0x04000FFC RID: 4092
		private bool isCrit;
	}
}
