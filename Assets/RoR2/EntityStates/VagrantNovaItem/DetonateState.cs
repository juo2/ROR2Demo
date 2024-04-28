using System;
using EntityStates.VagrantMonster;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VagrantNovaItem
{
	// Token: 0x0200016B RID: 363
	public class DetonateState : BaseVagrantNovaItemState
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x0001B490 File Offset: 0x00019690
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = DetonateState.baseDuration;
			if (NetworkServer.active && base.attachedBody)
			{
				new BlastAttack
				{
					attacker = base.attachedBody.gameObject,
					baseDamage = base.attachedBody.damage * DetonateState.blastDamageCoefficient,
					baseForce = DetonateState.blastForce,
					bonusForce = Vector3.zero,
					attackerFiltering = AttackerFiltering.NeverHitSelf,
					crit = base.attachedBody.RollCrit(),
					damageColorIndex = DamageColorIndex.Item,
					damageType = DamageType.Generic,
					falloffModel = BlastAttack.FalloffModel.None,
					inflictor = base.gameObject,
					position = base.attachedBody.corePosition,
					procChainMask = default(ProcChainMask),
					procCoefficient = DetonateState.blastProcCoefficient,
					radius = DetonateState.blastRadius,
					losType = BlastAttack.LoSType.NearestHit,
					teamIndex = base.attachedBody.teamComponent.teamIndex
				}.Fire();
				EffectData effectData = new EffectData();
				effectData.origin = base.attachedBody.corePosition;
				effectData.SetHurtBoxReference(base.attachedBody.mainHurtBox);
				EffectManager.SpawnEffect(FireMegaNova.novaEffectPrefab, effectData, true);
			}
			base.SetChargeSparkEmissionRateMultiplier(0f);
			Util.PlaySound(DetonateState.explosionSound, base.gameObject);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001B5EA File Offset: 0x000197EA
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new RechargeState());
			}
		}

		// Token: 0x040007B5 RID: 1973
		public static float blastRadius;

		// Token: 0x040007B6 RID: 1974
		public static float blastDamageCoefficient;

		// Token: 0x040007B7 RID: 1975
		public static float blastProcCoefficient;

		// Token: 0x040007B8 RID: 1976
		public static float blastForce;

		// Token: 0x040007B9 RID: 1977
		public static float baseDuration;

		// Token: 0x040007BA RID: 1978
		public static string explosionSound;

		// Token: 0x040007BB RID: 1979
		private float duration;
	}
}
