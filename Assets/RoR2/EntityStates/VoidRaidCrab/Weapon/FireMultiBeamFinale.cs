using System;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;

namespace EntityStates.VoidRaidCrab.Weapon
{
	// Token: 0x02000136 RID: 310
	public class FireMultiBeamFinale : BaseFireMultiBeam
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00017CC8 File Offset: 0x00015EC8
		protected override void OnFireBeam(Vector3 beamStart, Vector3 beamEnd)
		{
			if (this.nextSkillDef)
			{
				GenericSkill genericSkill = base.skillLocator.FindSkillByDef(this.skillDefToReplaceAtStocksEmpty);
				if (genericSkill && genericSkill.stock == 0)
				{
					genericSkill.SetBaseSkill(this.nextSkillDef);
				}
			}
			FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
			fireProjectileInfo.projectilePrefab = this.projectilePrefab;
			fireProjectileInfo.position = beamEnd + Vector3.up * this.projectileVerticalSpawnOffset;
			fireProjectileInfo.owner = base.gameObject;
			fireProjectileInfo.damage = this.damageStat * this.projectileDamageCoefficient;
			fireProjectileInfo.crit = Util.CheckRoll(this.critStat, base.characterBody.master);
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000DE8C File Offset: 0x0000C08C
		protected override EntityState InstantiateNextState()
		{
			return EntityStateCatalog.InstantiateState(this.outer.mainStateType);
		}

		// Token: 0x0400069E RID: 1694
		[SerializeField]
		public GameObject projectilePrefab;

		// Token: 0x0400069F RID: 1695
		[SerializeField]
		public float projectileVerticalSpawnOffset;

		// Token: 0x040006A0 RID: 1696
		[SerializeField]
		public float projectileDamageCoefficient;

		// Token: 0x040006A1 RID: 1697
		[SerializeField]
		public SkillDef skillDefToReplaceAtStocksEmpty;

		// Token: 0x040006A2 RID: 1698
		[SerializeField]
		public SkillDef nextSkillDef;
	}
}
