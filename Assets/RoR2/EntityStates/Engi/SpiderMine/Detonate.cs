using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Engi.SpiderMine
{
	// Token: 0x02000394 RID: 916
	public class Detonate : BaseSpiderMineState
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x00048150 File Offset: 0x00046350
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				ProjectileDamage component = base.GetComponent<ProjectileDamage>();
				Vector3 position = base.transform.position;
				new BlastAttack
				{
					position = position,
					attacker = base.projectileController.owner,
					baseDamage = component.damage,
					baseForce = component.force,
					bonusForce = Vector3.zero,
					crit = component.crit,
					damageColorIndex = component.damageColorIndex,
					damageType = component.damageType,
					falloffModel = BlastAttack.FalloffModel.None,
					inflictor = base.gameObject,
					procChainMask = base.projectileController.procChainMask,
					radius = Detonate.blastRadius,
					teamIndex = base.projectileController.teamFilter.teamIndex,
					procCoefficient = base.projectileController.procCoefficient
				}.Fire();
				EffectManager.SpawnEffect(Detonate.blastEffectPrefab, new EffectData
				{
					origin = position,
					scale = Detonate.blastRadius
				}, true);
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldStick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x040014D4 RID: 5332
		public static float blastRadius;

		// Token: 0x040014D5 RID: 5333
		public static GameObject blastEffectPrefab;
	}
}
