using System;
using System.Collections.Generic;
using RoR2;
using UnityEngine;

namespace EntityStates.DroneWeaponsChainGun
{
	// Token: 0x020003BE RID: 958
	public class AimChainGun : BaseDroneWeaponChainGunState
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x0004B248 File Offset: 0x00049448
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.enemyFinder = new BullseyeSearch();
				this.enemyFinder.teamMaskFilter = TeamMask.allButNeutral;
				this.enemyFinder.maxDistanceFilter = this.maxEnemyDistanceToStartFiring;
				this.enemyFinder.maxAngleFilter = float.MaxValue;
				this.enemyFinder.filterByLoS = true;
				this.enemyFinder.sortMode = BullseyeSearch.SortMode.Angle;
				if (this.bodyTeamComponent)
				{
					this.enemyFinder.teamMaskFilter.RemoveTeam(this.bodyTeamComponent.teamIndex);
				}
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0004B2E0 File Offset: 0x000494E0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge > this.minDuration)
			{
				this.searchRefreshTimer -= Time.fixedDeltaTime;
				if (this.searchRefreshTimer < 0f)
				{
					this.searchRefreshTimer = this.searchRefreshSeconds;
					Ray aimRay = base.GetAimRay();
					this.enemyFinder.searchOrigin = aimRay.origin;
					this.enemyFinder.searchDirection = aimRay.direction;
					this.enemyFinder.RefreshCandidates();
					using (IEnumerator<HurtBox> enumerator = this.enemyFinder.GetResults().GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							HurtBox targetHurtBox = enumerator.Current;
							this.outer.SetNextState(new FireChainGun(targetHurtBox));
						}
					}
				}
			}
		}

		// Token: 0x04001597 RID: 5527
		[SerializeField]
		public float minDuration;

		// Token: 0x04001598 RID: 5528
		[SerializeField]
		public float maxEnemyDistanceToStartFiring;

		// Token: 0x04001599 RID: 5529
		[SerializeField]
		public float searchRefreshSeconds;

		// Token: 0x0400159A RID: 5530
		private BullseyeSearch enemyFinder;

		// Token: 0x0400159B RID: 5531
		private float searchRefreshTimer;
	}
}
