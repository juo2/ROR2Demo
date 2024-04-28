using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Engi.Mine
{
	// Token: 0x020003A2 RID: 930
	public class Detonate : BaseMineState
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldStick
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060010AD RID: 4269 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldRevertToWaitForStickOnSurfaceLost
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00048B14 File Offset: 0x00046D14
		public override void OnEnter()
		{
			base.OnEnter();
			if (NetworkServer.active)
			{
				this.Explode();
			}
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00048B2C File Offset: 0x00046D2C
		private void Explode()
		{
			ProjectileDamage component = base.GetComponent<ProjectileDamage>();
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			EntityStateMachine armingStateMachine = base.armingStateMachine;
			BaseMineArmingState baseMineArmingState;
			if ((baseMineArmingState = (((armingStateMachine != null) ? armingStateMachine.state : null) as BaseMineArmingState)) != null)
			{
				num = baseMineArmingState.damageScale;
				num2 = baseMineArmingState.forceScale;
				num3 = baseMineArmingState.blastRadiusScale;
			}
			float num4 = Detonate.blastRadius * num3;
			new BlastAttack
			{
				procChainMask = base.projectileController.procChainMask,
				procCoefficient = base.projectileController.procCoefficient,
				attacker = base.projectileController.owner,
				inflictor = base.gameObject,
				teamIndex = base.projectileController.teamFilter.teamIndex,
				procCoefficient = base.projectileController.procCoefficient,
				baseDamage = component.damage * num,
				baseForce = component.force * num2,
				falloffModel = BlastAttack.FalloffModel.None,
				crit = component.crit,
				radius = num4,
				position = base.transform.position,
				damageColorIndex = component.damageColorIndex
			}.Fire();
			if (Detonate.explosionEffectPrefab)
			{
				EffectManager.SpawnEffect(Detonate.explosionEffectPrefab, new EffectData
				{
					origin = base.transform.position,
					rotation = base.transform.rotation,
					scale = num4
				}, true);
			}
			EntityState.Destroy(base.gameObject);
		}

		// Token: 0x040014F4 RID: 5364
		public static float blastRadius;

		// Token: 0x040014F5 RID: 5365
		public static GameObject explosionEffectPrefab;
	}
}
