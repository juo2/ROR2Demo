using System;
using RoR2;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x02000186 RID: 390
	public class FirePlantSonicBoom : FireSonicBoom
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0001D924 File Offset: 0x0001BB24
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active && base.healthComponent && FirePlantSonicBoom.healthCostFraction >= Mathf.Epsilon)
			{
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.damage = base.healthComponent.combinedHealth * FirePlantSonicBoom.healthCostFraction;
				damageInfo.position = base.characterBody.corePosition;
				damageInfo.force = Vector3.zero;
				damageInfo.damageColorIndex = DamageColorIndex.Default;
				damageInfo.crit = false;
				damageInfo.attacker = null;
				damageInfo.inflictor = null;
				damageInfo.damageType = DamageType.NonLethal;
				damageInfo.procCoefficient = 0f;
				damageInfo.procChainMask = default(ProcChainMask);
				base.healthComponent.TakeDamage(damageInfo);
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		protected override void AddDebuff(CharacterBody body)
		{
			SetStateOnHurt component = body.healthComponent.GetComponent<SetStateOnHurt>();
			if (component != null)
			{
				component.SetStun(-1f);
			}
			if (FirePlantSonicBoom.hitEffectPrefab)
			{
				EffectManager.SpawnEffect(FirePlantSonicBoom.hitEffectPrefab, new EffectData
				{
					origin = body.corePosition
				}, true);
			}
			if (base.healthComponent)
			{
				HealOrb healOrb = new HealOrb();
				healOrb.origin = body.corePosition;
				healOrb.target = base.healthComponent.body.mainHurtBox;
				healOrb.healValue = FirePlantSonicBoom.healthFractionPerHit * base.healthComponent.fullHealth;
				healOrb.overrideDuration = UnityEngine.Random.Range(0.3f, 0.6f);
				OrbManager.instance.AddOrb(healOrb);
			}
			Util.PlaySound(FirePlantSonicBoom.impactSoundString, base.gameObject);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001DAA9 File Offset: 0x0001BCA9
		protected override float CalculateDamage()
		{
			return FirePlantSonicBoom.damageCoefficient * this.damageStat;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001DAB7 File Offset: 0x0001BCB7
		protected override float CalculateProcCoefficient()
		{
			return FirePlantSonicBoom.procCoefficient;
		}

		// Token: 0x0400086B RID: 2155
		public static float damageCoefficient;

		// Token: 0x0400086C RID: 2156
		public static float procCoefficient;

		// Token: 0x0400086D RID: 2157
		public static GameObject hitEffectPrefab;

		// Token: 0x0400086E RID: 2158
		public static float healthFractionPerHit;

		// Token: 0x0400086F RID: 2159
		public static float healthCostFraction;

		// Token: 0x04000870 RID: 2160
		public static string impactSoundString;
	}
}
