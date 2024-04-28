using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B76 RID: 2934
	[RequireComponent(typeof(HealthComponent))]
	[RequireComponent(typeof(ProjectileDamage))]
	[RequireComponent(typeof(ProjectileController))]
	[RequireComponent(typeof(ProjectileStickOnImpact))]
	public class DeathProjectile : MonoBehaviour
	{
		// Token: 0x060042D7 RID: 17111 RVA: 0x00114F90 File Offset: 0x00113190
		private void Awake()
		{
			this.projectileStickOnImpactController = base.GetComponent<ProjectileStickOnImpact>();
			this.projectileController = base.GetComponent<ProjectileController>();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
			this.healthComponent = base.GetComponent<HealthComponent>();
			this.duration = this.baseDuration;
			this.fixedAge = 0f;
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x00114FE4 File Offset: 0x001131E4
		private void FixedUpdate()
		{
			this.fixedAge += Time.deltaTime;
			if (this.duration > 0f)
			{
				if (this.fixedAge >= 1f)
				{
					if (this.projectileStickOnImpactController.stuck)
					{
						if (this.projectileController.owner)
						{
							this.RotateDoll(UnityEngine.Random.Range(90f, 180f));
							this.SpawnTickEffect();
							if (NetworkServer.active)
							{
								DamageInfo damageInfo = new DamageInfo
								{
									attacker = this.projectileController.owner,
									crit = this.projectileDamage.crit,
									damage = this.projectileDamage.damage,
									position = base.transform.position,
									procCoefficient = this.projectileController.procCoefficient,
									damageType = this.projectileDamage.damageType,
									damageColorIndex = this.projectileDamage.damageColorIndex
								};
								HealthComponent victim = this.healthComponent;
								DamageReport damageReport = new DamageReport(damageInfo, victim, damageInfo.damage, this.healthComponent.combinedHealth);
								GlobalEventManager.instance.OnCharacterDeath(damageReport);
							}
						}
						this.duration -= 1f;
					}
					this.fixedAge = 0f;
					return;
				}
			}
			else
			{
				if (!this.doneWithRemovalEvents)
				{
					this.doneWithRemovalEvents = true;
					this.rotateObject.GetComponent<ObjectScaleCurve>().enabled = true;
				}
				if (this.fixedAge >= this.removalTime)
				{
					Util.PlaySound(this.exitSoundString, base.gameObject);
					this.shouldStopSound = false;
					UnityEngine.Object.Destroy(base.gameObject);
				}
			}
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x00115185 File Offset: 0x00113385
		private void OnDisable()
		{
			if (this.shouldStopSound)
			{
				Util.PlaySound(this.exitSoundString, base.gameObject);
				this.shouldStopSound = false;
			}
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x001151A8 File Offset: 0x001133A8
		public void SpawnTickEffect()
		{
			EffectData effectData = new EffectData
			{
				origin = base.transform.position,
				rotation = Quaternion.identity
			};
			EffectManager.SpawnEffect(this.OnKillTickEffect, effectData, false);
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x001151E4 File Offset: 0x001133E4
		public void PlayStickSoundLoop()
		{
			Util.PlaySound(this.activeSoundLoopString, base.gameObject);
			this.shouldStopSound = true;
		}

		// Token: 0x060042DC RID: 17116 RVA: 0x001151FF File Offset: 0x001133FF
		public void RotateDoll(float rotationAmount)
		{
			this.rotateObject.transform.Rotate(new Vector3(0f, 0f, rotationAmount));
		}

		// Token: 0x040040D0 RID: 16592
		private ProjectileStickOnImpact projectileStickOnImpactController;

		// Token: 0x040040D1 RID: 16593
		private ProjectileController projectileController;

		// Token: 0x040040D2 RID: 16594
		private ProjectileDamage projectileDamage;

		// Token: 0x040040D3 RID: 16595
		private HealthComponent healthComponent;

		// Token: 0x040040D4 RID: 16596
		public GameObject OnKillTickEffect;

		// Token: 0x040040D5 RID: 16597
		public TeamIndex teamIndex;

		// Token: 0x040040D6 RID: 16598
		public string activeSoundLoopString;

		// Token: 0x040040D7 RID: 16599
		public string exitSoundString;

		// Token: 0x040040D8 RID: 16600
		private float duration;

		// Token: 0x040040D9 RID: 16601
		private float fixedAge;

		// Token: 0x040040DA RID: 16602
		public float baseDuration = 8f;

		// Token: 0x040040DB RID: 16603
		public float radius = 500f;

		// Token: 0x040040DC RID: 16604
		public GameObject rotateObject;

		// Token: 0x040040DD RID: 16605
		private bool doneWithRemovalEvents;

		// Token: 0x040040DE RID: 16606
		public float removalTime = 1f;

		// Token: 0x040040DF RID: 16607
		private bool shouldStopSound;
	}
}
