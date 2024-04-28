using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B19 RID: 2841
	public class LunarDetonatorOrb : Orb
	{
		// Token: 0x060040D0 RID: 16592 RVA: 0x0010C63C File Offset: 0x0010A83C
		public override void Begin()
		{
			base.duration = base.distanceToTarget / this.travelSpeed;
			EffectData effectData = new EffectData
			{
				scale = 1f,
				origin = this.origin,
				genericFloat = base.duration
			};
			effectData.SetHurtBoxReference(this.target);
			if (this.orbEffectPrefab)
			{
				EffectManager.SpawnEffect(this.orbEffectPrefab, effectData, true);
			}
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x0010C6AC File Offset: 0x0010A8AC
		public override void OnArrival()
		{
			base.OnArrival();
			if (!this.target)
			{
				return;
			}
			HealthComponent healthComponent = this.target.healthComponent;
			if (!healthComponent)
			{
				return;
			}
			CharacterBody body = healthComponent.body;
			if (!body)
			{
				return;
			}
			int buffCount = body.GetBuffCount(RoR2Content.Buffs.LunarDetonationCharge);
			if (buffCount <= 0)
			{
				return;
			}
			body.ClearTimedBuffs(RoR2Content.Buffs.LunarDetonationCharge);
			Vector3 position = this.target.transform.position;
			DamageInfo damageInfo = new DamageInfo();
			damageInfo.damage = this.baseDamage + this.damagePerStack * (float)buffCount;
			damageInfo.attacker = this.attacker;
			damageInfo.inflictor = null;
			damageInfo.force = Vector3.zero;
			damageInfo.crit = this.isCrit;
			damageInfo.procChainMask = this.procChainMask;
			damageInfo.procCoefficient = this.procCoefficient;
			damageInfo.position = position;
			damageInfo.damageColorIndex = this.damageColorIndex;
			healthComponent.TakeDamage(damageInfo);
			GlobalEventManager.instance.OnHitEnemy(damageInfo, healthComponent.gameObject);
			GlobalEventManager.instance.OnHitAll(damageInfo, healthComponent.gameObject);
			EffectManager.SpawnEffect(this.detonationEffectPrefab, new EffectData
			{
				origin = position,
				rotation = Quaternion.identity,
				scale = Mathf.Log((float)buffCount, 5f)
			}, true);
		}

		// Token: 0x04003F44 RID: 16196
		public float travelSpeed = 60f;

		// Token: 0x04003F45 RID: 16197
		public float baseDamage;

		// Token: 0x04003F46 RID: 16198
		public float damagePerStack;

		// Token: 0x04003F47 RID: 16199
		public GameObject attacker;

		// Token: 0x04003F48 RID: 16200
		public bool isCrit;

		// Token: 0x04003F49 RID: 16201
		public ProcChainMask procChainMask;

		// Token: 0x04003F4A RID: 16202
		public float procCoefficient;

		// Token: 0x04003F4B RID: 16203
		public DamageColorIndex damageColorIndex;

		// Token: 0x04003F4C RID: 16204
		public GameObject detonationEffectPrefab;

		// Token: 0x04003F4D RID: 16205
		public GameObject orbEffectPrefab;
	}
}
