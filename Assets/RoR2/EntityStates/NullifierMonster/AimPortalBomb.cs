using System;
using System.Linq;
using RoR2;
using UnityEngine;

namespace EntityStates.NullifierMonster
{
	// Token: 0x02000234 RID: 564
	public class AimPortalBomb : BaseState
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00029438 File Offset: 0x00027638
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.viewer = base.characterBody;
				bullseyeSearch.searchOrigin = base.characterBody.corePosition;
				bullseyeSearch.searchDirection = base.characterBody.corePosition;
				bullseyeSearch.maxDistanceFilter = FirePortalBomb.maxDistance;
				bullseyeSearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam());
				bullseyeSearch.sortMode = BullseyeSearch.SortMode.DistanceAndAngle;
				bullseyeSearch.RefreshCandidates();
				this.target = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
				if (this.target)
				{
					this.pointA = this.RaycastToFloor(this.target.transform.position);
				}
			}
			this.duration = AimPortalBomb.baseDuration;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000294F8 File Offset: 0x000276F8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				EntityState entityState = null;
				if (this.target)
				{
					this.pointB = this.RaycastToFloor(this.target.transform.position);
					if (this.pointA != null && this.pointB != null)
					{
						Ray aimRay = base.GetAimRay();
						Vector3 forward = this.pointA.Value - aimRay.origin;
						Vector3 forward2 = this.pointB.Value - aimRay.origin;
						Quaternion a = Quaternion.LookRotation(forward);
						Quaternion quaternion = Quaternion.LookRotation(forward2);
						Quaternion value = quaternion;
						Quaternion value2 = Quaternion.SlerpUnclamped(a, quaternion, 1f + AimPortalBomb.arcMultiplier);
						entityState = new FirePortalBomb
						{
							startRotation = new Quaternion?(value),
							endRotation = new Quaternion?(value2)
						};
					}
				}
				if (entityState != null)
				{
					this.outer.SetNextState(entityState);
					return;
				}
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0002960C File Offset: 0x0002780C
		private Vector3? RaycastToFloor(Vector3 position)
		{
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(position, Vector3.down), out raycastHit, 10f, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
			{
				return new Vector3?(raycastHit.point);
			}
			return null;
		}

		// Token: 0x04000B97 RID: 2967
		private HurtBox target;

		// Token: 0x04000B98 RID: 2968
		public static float baseDuration;

		// Token: 0x04000B99 RID: 2969
		public static float arcMultiplier;

		// Token: 0x04000B9A RID: 2970
		private float duration;

		// Token: 0x04000B9B RID: 2971
		private Vector3? pointA;

		// Token: 0x04000B9C RID: 2972
		private Vector3? pointB;
	}
}
