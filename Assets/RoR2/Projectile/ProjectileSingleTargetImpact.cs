using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BB2 RID: 2994
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileSingleTargetImpact : MonoBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x06004433 RID: 17459 RVA: 0x0011BD50 File Offset: 0x00119F50
		private void Awake()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
		}

		// Token: 0x06004434 RID: 17460 RVA: 0x0011BD6C File Offset: 0x00119F6C
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			if (!this.alive)
			{
				return;
			}
			Collider collider = impactInfo.collider;
			if (collider)
			{
				DamageInfo damageInfo = new DamageInfo();
				if (this.projectileDamage)
				{
					damageInfo.damage = this.projectileDamage.damage;
					damageInfo.crit = this.projectileDamage.crit;
					damageInfo.attacker = this.projectileController.owner;
					damageInfo.inflictor = base.gameObject;
					damageInfo.position = impactInfo.estimatedPointOfImpact;
					damageInfo.force = this.projectileDamage.force * base.transform.forward;
					damageInfo.procChainMask = this.projectileController.procChainMask;
					damageInfo.procCoefficient = this.projectileController.procCoefficient;
					damageInfo.damageColorIndex = this.projectileDamage.damageColorIndex;
					damageInfo.damageType = this.projectileDamage.damageType;
				}
				else
				{
					Debug.Log("No projectile damage component!");
				}
				HurtBox component = collider.GetComponent<HurtBox>();
				if (component)
				{
					HealthComponent healthComponent = component.healthComponent;
					if (healthComponent)
					{
						if (healthComponent.gameObject == this.projectileController.owner)
						{
							return;
						}
						if (FriendlyFireManager.ShouldDirectHitProceed(healthComponent, this.projectileController.teamFilter.teamIndex))
						{
							Util.PlaySound(this.enemyHitSoundString, base.gameObject);
							if (NetworkServer.active)
							{
								damageInfo.ModifyDamageInfo(component.damageModifier);
								healthComponent.TakeDamage(damageInfo);
								GlobalEventManager.instance.OnHitEnemy(damageInfo, component.healthComponent.gameObject);
							}
						}
						this.alive = false;
					}
				}
				else if (this.destroyOnWorld)
				{
					this.alive = false;
				}
				damageInfo.position = base.transform.position;
				if (NetworkServer.active)
				{
					GlobalEventManager.instance.OnHitAll(damageInfo, collider.gameObject);
				}
			}
			if (!this.alive)
			{
				if (NetworkServer.active && this.impactEffect)
				{
					EffectManager.SimpleImpactEffect(this.impactEffect, impactInfo.estimatedPointOfImpact, -base.transform.forward, !this.projectileController.isPrediction);
				}
				Util.PlaySound(this.hitSoundString, base.gameObject);
				if (this.destroyWhenNotAlive)
				{
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x040042A9 RID: 17065
		private ProjectileController projectileController;

		// Token: 0x040042AA RID: 17066
		private ProjectileDamage projectileDamage;

		// Token: 0x040042AB RID: 17067
		private bool alive = true;

		// Token: 0x040042AC RID: 17068
		public bool destroyWhenNotAlive = true;

		// Token: 0x040042AD RID: 17069
		public bool destroyOnWorld;

		// Token: 0x040042AE RID: 17070
		public GameObject impactEffect;

		// Token: 0x040042AF RID: 17071
		public string hitSoundString;

		// Token: 0x040042B0 RID: 17072
		public string enemyHitSoundString;
	}
}
