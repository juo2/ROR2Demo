using System;
using System.Linq;
using RoR2;
using UnityEngine;

namespace EntityStates.Engi.SpiderMine
{
	// Token: 0x02000390 RID: 912
	public class Unburrow : BaseSpiderMineState
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x00047D52 File Offset: 0x00045F52
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Unburrow.baseDuration;
			Util.PlaySound("Play_beetle_worker_idle", base.gameObject);
			base.PlayAnimation("Base", "ArmedToChase", "ArmedToChase.playbackRate", this.duration);
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00047D94 File Offset: 0x00045F94
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				EntityState entityState = null;
				if (!base.projectileStickOnImpact.stuck)
				{
					entityState = new WaitForStick();
				}
				else if (base.projectileTargetComponent.target)
				{
					if (this.duration <= base.fixedAge)
					{
						this.FindBetterTarget(base.projectileTargetComponent.target);
						entityState = new ChaseTarget();
					}
				}
				else
				{
					entityState = new Burrow();
				}
				if (entityState != null)
				{
					this.outer.SetNextState(entityState);
				}
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00047E14 File Offset: 0x00046014
		private BullseyeSearch CreateBullseyeSearch(Vector3 origin)
		{
			return new BullseyeSearch
			{
				searchOrigin = origin,
				filterByDistinctEntity = true,
				maxDistanceFilter = Detonate.blastRadius,
				sortMode = BullseyeSearch.SortMode.Distance,
				maxAngleFilter = 360f,
				teamMaskFilter = TeamMask.GetEnemyTeams(base.projectileController.teamFilter.teamIndex)
			};
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00047E6C File Offset: 0x0004606C
		private void FindBetterTarget(Transform initialTarget)
		{
			BullseyeSearch bullseyeSearch = this.CreateBullseyeSearch(initialTarget.position);
			bullseyeSearch.RefreshCandidates();
			HurtBox[] array = bullseyeSearch.GetResults().ToArray<HurtBox>();
			int num = array.Length;
			int num2 = -1;
			int i = 0;
			int num3 = Math.Min(array.Length, Unburrow.betterTargetSearchLimit);
			while (i < num3)
			{
				HurtBox hurtBox = array[i];
				int num4 = this.CountTargets(hurtBox.transform.position);
				if (num < num4)
				{
					num = num4;
					num2 = i;
				}
				i++;
			}
			if (num2 != -1)
			{
				base.projectileTargetComponent.target = array[num2].transform;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00047EF2 File Offset: 0x000460F2
		private int CountTargets(Vector3 origin)
		{
			BullseyeSearch bullseyeSearch = this.CreateBullseyeSearch(origin);
			bullseyeSearch.RefreshCandidates();
			return bullseyeSearch.GetResults().Count<HurtBox>();
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected override bool shouldStick
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040014C9 RID: 5321
		public static float baseDuration;

		// Token: 0x040014CA RID: 5322
		public static int betterTargetSearchLimit;

		// Token: 0x040014CB RID: 5323
		private float duration;
	}
}
