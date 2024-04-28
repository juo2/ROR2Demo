using System;
using UnityEngine;

namespace RoR2.Orbs
{
	// Token: 0x02000B21 RID: 2849
	public class SimpleLightningStrikeOrb : GenericDamageOrb, IOrbFixedUpdateBehavior
	{
		// Token: 0x060040FE RID: 16638 RVA: 0x0010D0E7 File Offset: 0x0010B2E7
		public override void Begin()
		{
			base.Begin();
			base.duration = 0.25f;
			if (this.target)
			{
				this.lastKnownTargetPosition = this.target.transform.position;
			}
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x0010D11D File Offset: 0x0010B31D
		protected override GameObject GetOrbEffect()
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/SimpleLightningStrikeOrbEffect");
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x0010D12C File Offset: 0x0010B32C
		public override void OnArrival()
		{
			EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/SimpleLightningStrikeImpact"), new EffectData
			{
				origin = this.lastKnownTargetPosition
			}, true);
			if (this.attacker)
			{
				new BlastAttack
				{
					attacker = this.attacker,
					baseDamage = this.damageValue,
					baseForce = 0f,
					bonusForce = Vector3.down * 1500f,
					crit = this.isCrit,
					damageColorIndex = DamageColorIndex.Item,
					falloffModel = BlastAttack.FalloffModel.None,
					inflictor = null,
					position = this.lastKnownTargetPosition,
					procChainMask = this.procChainMask,
					procCoefficient = 1f,
					radius = 3f,
					teamIndex = TeamComponent.GetObjectTeam(this.attacker)
				}.Fire();
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x0010D20D File Offset: 0x0010B40D
		public void FixedUpdate()
		{
			if (this.target)
			{
				this.lastKnownTargetPosition = this.target.transform.position;
			}
		}

		// Token: 0x04003F75 RID: 16245
		private Vector3 lastKnownTargetPosition;
	}
}
