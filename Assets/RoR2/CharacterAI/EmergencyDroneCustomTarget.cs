using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.CharacterAI
{
	// Token: 0x02000C7B RID: 3195
	[RequireComponent(typeof(BaseAI))]
	public class EmergencyDroneCustomTarget : MonoBehaviour
	{
		// Token: 0x06004935 RID: 18741 RVA: 0x0012DA72 File Offset: 0x0012BC72
		private void Awake()
		{
			this.ai = base.GetComponent<BaseAI>();
			if (NetworkServer.active)
			{
				this.search = new BullseyeSearch();
			}
		}

		// Token: 0x06004936 RID: 18742 RVA: 0x0012DA92 File Offset: 0x0012BC92
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0012DAA1 File Offset: 0x0012BCA1
		private void FixedUpdateServer()
		{
			this.timer -= Time.fixedDeltaTime;
			if (this.timer <= 0f)
			{
				this.timer = this.searchInterval;
				this.DoSearch();
			}
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x0012DAD4 File Offset: 0x0012BCD4
		private void DoSearch()
		{
			if (this.ai.body)
			{
				Ray aimRay = this.ai.bodyInputBank.GetAimRay();
				this.search.viewer = this.ai.body;
				this.search.filterByDistinctEntity = true;
				this.search.filterByLoS = false;
				this.search.maxDistanceFilter = float.PositiveInfinity;
				this.search.minDistanceFilter = 0f;
				this.search.maxAngleFilter = 360f;
				this.search.searchDirection = aimRay.direction;
				this.search.searchOrigin = aimRay.origin;
				this.search.sortMode = BullseyeSearch.SortMode.Distance;
				this.search.queryTriggerInteraction = QueryTriggerInteraction.UseGlobal;
				TeamMask none = TeamMask.none;
				none.AddTeam(this.ai.master.teamIndex);
				this.search.teamMaskFilter = none;
				this.search.RefreshCandidates();
				this.search.FilterOutGameObject(this.ai.body.gameObject);
				BaseAI.Target customTarget = this.ai.customTarget;
				HurtBox hurtBox = this.search.GetResults().Where(new Func<HurtBox, bool>(this.TargetPassesFilters)).FirstOrDefault<HurtBox>();
				customTarget.gameObject = ((hurtBox != null) ? hurtBox.healthComponent.gameObject : null);
			}
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x0012DC32 File Offset: 0x0012BE32
		private bool TargetPassesFilters(HurtBox hurtBox)
		{
			return EmergencyDroneCustomTarget.IsHurt(hurtBox) && !HealBeamController.HealBeamAlreadyExists(this.ai.body.gameObject, hurtBox.healthComponent);
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x0004CBEC File Offset: 0x0004ADEC
		private static bool IsHurt(HurtBox hurtBox)
		{
			return hurtBox.healthComponent.alive && hurtBox.healthComponent.health < hurtBox.healthComponent.fullHealth;
		}

		// Token: 0x04004604 RID: 17924
		private BaseAI ai;

		// Token: 0x04004605 RID: 17925
		private BullseyeSearch search;

		// Token: 0x04004606 RID: 17926
		public float searchInterval;

		// Token: 0x04004607 RID: 17927
		private float timer;
	}
}
