using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;

namespace EntityStates.LaserTurbine
{
	// Token: 0x020002DC RID: 732
	public class AimState : LaserTurbineBaseState
	{
		// Token: 0x06000D0E RID: 3342 RVA: 0x00036C38 File Offset: 0x00034E38
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				TeamMask enemyTeams = TeamMask.GetEnemyTeams(base.ownerBody.teamComponent.teamIndex);
				HurtBox[] hurtBoxes = new SphereSearch
				{
					radius = AimState.targetAcquisitionRadius,
					mask = LayerIndex.entityPrecise.mask,
					origin = base.transform.position,
					queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
				}.RefreshCandidates().FilterCandidatesByHurtBoxTeam(enemyTeams).OrderCandidatesByDistance().FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();
				float blastRadius = FireMainBeamState.secondBombPrefab.GetComponent<ProjectileImpactExplosion>().blastRadius;
				int num = -1;
				int num2 = 0;
				for (int i = 0; i < hurtBoxes.Length; i++)
				{
					HurtBox[] hurtBoxes2 = new SphereSearch
					{
						radius = blastRadius,
						mask = LayerIndex.entityPrecise.mask,
						origin = hurtBoxes[i].transform.position,
						queryTriggerInteraction = QueryTriggerInteraction.UseGlobal
					}.RefreshCandidates().FilterCandidatesByHurtBoxTeam(enemyTeams).FilterCandidatesByDistinctHurtBoxEntities().GetHurtBoxes();
					if (hurtBoxes2.Length > num2)
					{
						num = i;
						num2 = hurtBoxes2.Length;
					}
				}
				if (num != -1)
				{
					base.simpleRotateToDirection.targetRotation = Quaternion.LookRotation(hurtBoxes[num].transform.position - base.transform.position);
					this.foundTarget = true;
				}
			}
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x00036D85 File Offset: 0x00034F85
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				if (this.foundTarget)
				{
					this.outer.SetNextState(new ChargeMainBeamState());
					return;
				}
				this.outer.SetNextState(new ReadyState());
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldFollow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04000FE8 RID: 4072
		public static float targetAcquisitionRadius;

		// Token: 0x04000FE9 RID: 4073
		private bool foundTarget;
	}
}
