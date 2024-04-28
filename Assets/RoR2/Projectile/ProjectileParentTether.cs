using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BAF RID: 2991
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileDamage))]
	public class ProjectileParentTether : MonoBehaviour
	{
		// Token: 0x06004419 RID: 17433 RVA: 0x0011B42F File Offset: 0x0011962F
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
			this.attackTimer = 0f;
			this.UpdateTetherGraphic();
		}

		// Token: 0x0600441A RID: 17434 RVA: 0x0011B45C File Offset: 0x0011965C
		private void UpdateTetherGraphic()
		{
			if (this.ShouldIFire())
			{
				if (this.tetherEffectPrefab && !this.tetherEffectInstance)
				{
					this.tetherEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.tetherEffectPrefab, base.transform.position, base.transform.rotation);
					this.tetherEffectInstance.transform.parent = base.transform;
					ChildLocator component = this.tetherEffectInstance.GetComponent<ChildLocator>();
					this.tetherEffectInstanceEnd = component.FindChild("LaserEnd").gameObject;
				}
				if (this.tetherEffectInstance)
				{
					Ray aimRay = this.GetAimRay();
					this.tetherEffectInstance.transform.rotation = Util.QuaternionSafeLookRotation(aimRay.direction);
					this.tetherEffectInstanceEnd.transform.position = aimRay.origin + aimRay.direction * this.GetRayDistance();
				}
			}
		}

		// Token: 0x0600441B RID: 17435 RVA: 0x0011B54C File Offset: 0x0011974C
		private float GetRayDistance()
		{
			if (this.projectileController.owner)
			{
				return (this.projectileController.owner.transform.position - base.transform.position).magnitude;
			}
			return 0f;
		}

		// Token: 0x0600441C RID: 17436 RVA: 0x0011B5A0 File Offset: 0x001197A0
		private Ray GetAimRay()
		{
			Ray result = default(Ray);
			result.origin = base.transform.position;
			result.direction = this.projectileController.owner.transform.position - result.origin;
			return result;
		}

		// Token: 0x0600441D RID: 17437 RVA: 0x0011B5F0 File Offset: 0x001197F0
		private bool ShouldIFire()
		{
			return !this.stickOnImpact || this.stickOnImpact.stuck;
		}

		// Token: 0x0600441E RID: 17438 RVA: 0x0011B60C File Offset: 0x0011980C
		private void Update()
		{
			this.UpdateTetherGraphic();
		}

		// Token: 0x0600441F RID: 17439 RVA: 0x0011B614 File Offset: 0x00119814
		private void FixedUpdate()
		{
			if (this.ShouldIFire())
			{
				this.lifetimeStopwatch += Time.fixedDeltaTime;
			}
			if (this.lifetimeStopwatch > this.lifetime)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
			if (this.projectileController.owner.transform && this.ShouldIFire())
			{
				this.myTeamIndex = (this.projectileController.teamFilter ? this.projectileController.teamFilter.teamIndex : TeamIndex.Neutral);
				this.attackTimer -= Time.fixedDeltaTime;
				if (this.attackTimer <= 0f)
				{
					Ray aimRay = this.GetAimRay();
					this.attackTimer = this.attackInterval;
					if (aimRay.direction.magnitude < this.maxTetherRange && NetworkServer.active)
					{
						new BulletAttack
						{
							owner = this.projectileController.owner,
							origin = aimRay.origin,
							aimVector = aimRay.direction,
							minSpread = 0f,
							damage = this.damageCoefficient * this.projectileDamage.damage,
							force = 0f,
							hitEffectPrefab = this.impactEffect,
							isCrit = this.projectileDamage.crit,
							radius = this.raycastRadius,
							falloffModel = BulletAttack.FalloffModel.None,
							stopperMask = 0,
							hitMask = LayerIndex.entityPrecise.mask,
							procCoefficient = this.procCoefficient,
							maxDistance = this.GetRayDistance()
						}.Fire();
					}
				}
			}
		}

		// Token: 0x04004279 RID: 17017
		private ProjectileController projectileController;

		// Token: 0x0400427A RID: 17018
		private ProjectileDamage projectileDamage;

		// Token: 0x0400427B RID: 17019
		private TeamIndex myTeamIndex;

		// Token: 0x0400427C RID: 17020
		public float attackInterval = 1f;

		// Token: 0x0400427D RID: 17021
		public float maxTetherRange = 20f;

		// Token: 0x0400427E RID: 17022
		public float procCoefficient = 0.1f;

		// Token: 0x0400427F RID: 17023
		public float damageCoefficient = 1f;

		// Token: 0x04004280 RID: 17024
		public float raycastRadius;

		// Token: 0x04004281 RID: 17025
		public float lifetime;

		// Token: 0x04004282 RID: 17026
		public GameObject impactEffect;

		// Token: 0x04004283 RID: 17027
		public GameObject tetherEffectPrefab;

		// Token: 0x04004284 RID: 17028
		public ProjectileStickOnImpact stickOnImpact;

		// Token: 0x04004285 RID: 17029
		private GameObject tetherEffectInstance;

		// Token: 0x04004286 RID: 17030
		private GameObject tetherEffectInstanceEnd;

		// Token: 0x04004287 RID: 17031
		private float attackTimer;

		// Token: 0x04004288 RID: 17032
		private float lifetimeStopwatch;
	}
}
