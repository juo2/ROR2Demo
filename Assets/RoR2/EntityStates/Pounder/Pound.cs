using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Pounder
{
	// Token: 0x02000173 RID: 371
	public class Pound : BaseState
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x0001BB3D File Offset: 0x00019D3D
		public override void OnEnter()
		{
			base.OnEnter();
			this.projectileDamage = base.GetComponent<ProjectileDamage>();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001BB54 File Offset: 0x00019D54
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.poundTimer -= Time.fixedDeltaTime;
			if (this.poundTimer <= 0f && base.projectileController.owner)
			{
				this.poundTimer += 1f / Pound.blastFrequency;
				if (NetworkServer.active)
				{
					new BlastAttack
					{
						attacker = base.projectileController.owner,
						baseDamage = this.projectileDamage.damage,
						baseForce = Pound.blastForce,
						crit = this.projectileDamage.crit,
						damageType = this.projectileDamage.damageType,
						falloffModel = BlastAttack.FalloffModel.None,
						position = base.transform.position,
						radius = Pound.blastRadius,
						teamIndex = base.projectileController.teamFilter.teamIndex
					}.Fire();
					EffectManager.SpawnEffect(Pound.blastEffectPrefab, new EffectData
					{
						origin = base.transform.position,
						scale = Pound.blastRadius
					}, true);
				}
				this.PlayAnimation("Base", "Pound");
			}
			if (NetworkServer.active && base.fixedAge > Pound.duration)
			{
				EntityState.Destroy(base.gameObject);
			}
		}

		// Token: 0x040007DC RID: 2012
		public static float blastRadius;

		// Token: 0x040007DD RID: 2013
		public static float blastProcCoefficient;

		// Token: 0x040007DE RID: 2014
		public static float blastForce;

		// Token: 0x040007DF RID: 2015
		public static float blastFrequency;

		// Token: 0x040007E0 RID: 2016
		public static float duration;

		// Token: 0x040007E1 RID: 2017
		public static GameObject blastEffectPrefab;

		// Token: 0x040007E2 RID: 2018
		private ProjectileDamage projectileDamage;

		// Token: 0x040007E3 RID: 2019
		private float poundTimer;
	}
}
