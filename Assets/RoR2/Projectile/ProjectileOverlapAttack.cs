using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BAD RID: 2989
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileDamage))]
	[RequireComponent(typeof(HitBoxGroup))]
	public class ProjectileOverlapAttack : MonoBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x060043F9 RID: 17401 RVA: 0x0011AB80 File Offset: 0x00118D80
		private void Start()
		{
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
			this.attack = new OverlapAttack();
			this.attack.procChainMask = this.projectileController.procChainMask;
			this.attack.procCoefficient = this.projectileController.procCoefficient * this.overlapProcCoefficient;
			this.attack.attacker = this.projectileController.owner;
			this.attack.inflictor = base.gameObject;
			this.attack.teamIndex = this.projectileController.teamFilter.teamIndex;
			this.attack.damage = this.damageCoefficient * this.projectileDamage.damage;
			this.attack.forceVector = this.forceVector + this.projectileDamage.force * base.transform.forward;
			this.attack.hitEffectPrefab = this.impactEffect;
			this.attack.isCrit = this.projectileDamage.crit;
			this.attack.damageColorIndex = this.projectileDamage.damageColorIndex;
			this.attack.damageType = this.projectileDamage.damageType;
			this.attack.procChainMask = this.projectileController.procChainMask;
			this.attack.maximumOverlapTargets = this.maximumOverlapTargets;
			this.attack.hitBoxGroup = base.GetComponent<HitBoxGroup>();
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x000026ED File Offset: 0x000008ED
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x0011AD00 File Offset: 0x00118F00
		public void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				if (this.resetInterval >= 0f)
				{
					this.resetTimer -= Time.fixedDeltaTime;
					if (this.resetTimer <= 0f)
					{
						this.resetTimer = this.resetInterval;
						this.ResetOverlapAttack();
					}
				}
				this.fireTimer -= Time.fixedDeltaTime;
				if (this.fireTimer <= 0f)
				{
					this.fireTimer = 1f / this.fireFrequency;
					this.attack.damage = this.damageCoefficient * this.projectileDamage.damage;
					if (this.attack.Fire(null))
					{
						UnityEvent unityEvent = this.onServerHit;
						if (unityEvent == null)
						{
							return;
						}
						unityEvent.Invoke();
					}
				}
			}
		}

		// Token: 0x060043FC RID: 17404 RVA: 0x0011ADC1 File Offset: 0x00118FC1
		public void ResetOverlapAttack()
		{
			this.attack.damageType = this.projectileDamage.damageType;
			this.attack.ResetIgnoredHealthComponents();
		}

		// Token: 0x060043FD RID: 17405 RVA: 0x0011ADE4 File Offset: 0x00118FE4
		public void SetDamageCoefficient(float newDamageCoefficient)
		{
			this.damageCoefficient = newDamageCoefficient;
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x0011ADED File Offset: 0x00118FED
		public void AddToDamageCoefficient(float bonusDamageCoefficient)
		{
			this.damageCoefficient += bonusDamageCoefficient;
		}

		// Token: 0x04004262 RID: 16994
		private ProjectileController projectileController;

		// Token: 0x04004263 RID: 16995
		private ProjectileDamage projectileDamage;

		// Token: 0x04004264 RID: 16996
		public float damageCoefficient;

		// Token: 0x04004265 RID: 16997
		public GameObject impactEffect;

		// Token: 0x04004266 RID: 16998
		public Vector3 forceVector;

		// Token: 0x04004267 RID: 16999
		public float overlapProcCoefficient = 1f;

		// Token: 0x04004268 RID: 17000
		public int maximumOverlapTargets = 100;

		// Token: 0x04004269 RID: 17001
		public UnityEvent onServerHit;

		// Token: 0x0400426A RID: 17002
		private OverlapAttack attack;

		// Token: 0x0400426B RID: 17003
		public float fireFrequency = 60f;

		// Token: 0x0400426C RID: 17004
		[Tooltip("If non-negative, the attack clears its hit memory at the specified interval.")]
		public float resetInterval = -1f;

		// Token: 0x0400426D RID: 17005
		private float resetTimer;

		// Token: 0x0400426E RID: 17006
		private float fireTimer;
	}
}
