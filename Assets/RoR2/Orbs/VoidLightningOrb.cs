using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B25 RID: 2853
	public class VoidLightningOrb : Orb, IOrbFixedUpdateBehavior
	{
		// Token: 0x0600410D RID: 16653 RVA: 0x0010D4BA File Offset: 0x0010B6BA
		public override void Begin()
		{
			this.accumulatedTime = 0f;
			base.duration = (float)(this.totalStrikes - 1) * this.secondsPerStrike;
			this.effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/VoidLightningOrbEffect");
			this.Strike();
		}

		// Token: 0x0600410E RID: 16654 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnArrival()
		{
		}

		// Token: 0x0600410F RID: 16655 RVA: 0x0010D4F4 File Offset: 0x0010B6F4
		public void FixedUpdate()
		{
			this.accumulatedTime += Time.fixedDeltaTime;
			while (this.accumulatedTime > this.secondsPerStrike)
			{
				this.accumulatedTime -= this.secondsPerStrike;
				if (this.target)
				{
					this.origin = this.target.transform.position;
					this.Strike();
				}
			}
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x0010D560 File Offset: 0x0010B760
		private void Strike()
		{
			if (this.target)
			{
				HealthComponent healthComponent = this.target.healthComponent;
				if (healthComponent)
				{
					DamageInfo damageInfo = new DamageInfo();
					damageInfo.damage = this.damageValue;
					damageInfo.attacker = this.attacker;
					damageInfo.inflictor = this.inflictor;
					damageInfo.force = Vector3.zero;
					damageInfo.crit = this.isCrit;
					damageInfo.procChainMask = this.procChainMask;
					damageInfo.procCoefficient = this.procCoefficient;
					damageInfo.position = this.target.transform.position;
					damageInfo.damageColorIndex = this.damageColorIndex;
					damageInfo.damageType = this.damageType;
					healthComponent.TakeDamage(damageInfo);
					GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
					GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
					if (this.target.hurtBoxGroup)
					{
						this.target = this.target.hurtBoxGroup.hurtBoxes[UnityEngine.Random.Range(0, this.target.hurtBoxGroup.hurtBoxes.Length)];
					}
					EffectData effectData = new EffectData
					{
						origin = this.origin,
						genericFloat = 0.1f
					};
					effectData.SetHurtBoxReference(this.target);
					EffectManager.SpawnEffect(this.effectPrefab, effectData, true);
				}
			}
		}

		// Token: 0x04003F7C RID: 16252
		public float damageValue;

		// Token: 0x04003F7D RID: 16253
		public GameObject attacker;

		// Token: 0x04003F7E RID: 16254
		public GameObject inflictor;

		// Token: 0x04003F7F RID: 16255
		public int totalStrikes;

		// Token: 0x04003F80 RID: 16256
		public float secondsPerStrike = 0.5f;

		// Token: 0x04003F81 RID: 16257
		public TeamIndex teamIndex;

		// Token: 0x04003F82 RID: 16258
		public bool isCrit;

		// Token: 0x04003F83 RID: 16259
		public ProcChainMask procChainMask;

		// Token: 0x04003F84 RID: 16260
		public float procCoefficient = 1f;

		// Token: 0x04003F85 RID: 16261
		public DamageColorIndex damageColorIndex;

		// Token: 0x04003F86 RID: 16262
		public DamageType damageType;

		// Token: 0x04003F87 RID: 16263
		private GameObject effectPrefab;

		// Token: 0x04003F88 RID: 16264
		private float accumulatedTime;
	}
}
