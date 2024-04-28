using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RoR2
{
	// Token: 0x02000741 RID: 1857
	public class BullseyeSearch
	{
		// Token: 0x17000359 RID: 857
		// (set) Token: 0x06002698 RID: 9880 RVA: 0x000A8293 File Offset: 0x000A6493
		public float minAngleFilter
		{
			set
			{
				this.maxThetaDot = Mathf.Cos(Mathf.Clamp(value, 0f, 180f) * 0.017453292f);
			}
		}

		// Token: 0x1700035A RID: 858
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x000A82B6 File Offset: 0x000A64B6
		public float maxAngleFilter
		{
			set
			{
				this.minThetaDot = Mathf.Cos(Mathf.Clamp(value, 0f, 180f) * 0.017453292f);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x000A82D9 File Offset: 0x000A64D9
		private bool filterByDistance
		{
			get
			{
				return this.minDistanceFilter > 0f || this.maxDistanceFilter < float.PositiveInfinity || (this.viewer && this.viewer.visionDistance < float.PositiveInfinity);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x000A8318 File Offset: 0x000A6518
		private bool filterByAngle
		{
			get
			{
				return this.minThetaDot > -1f || this.maxThetaDot < 1f;
			}
		}

		// Token: 0x0600269C RID: 9884 RVA: 0x000A8338 File Offset: 0x000A6538
		private Func<HurtBox, BullseyeSearch.CandidateInfo> GetSelector()
		{
			bool getDot = this.filterByAngle;
			bool getDistanceSqr = this.filterByDistance;
			getDistanceSqr |= (this.sortMode == BullseyeSearch.SortMode.Distance || this.sortMode == BullseyeSearch.SortMode.DistanceAndAngle);
			getDot |= (this.sortMode == BullseyeSearch.SortMode.Angle || this.sortMode == BullseyeSearch.SortMode.DistanceAndAngle);
			bool getDifference = getDot | getDistanceSqr;
			bool getPosition = (getDot | getDistanceSqr) || this.filterByLoS;
			return delegate(HurtBox hurtBox)
			{
				BullseyeSearch.CandidateInfo candidateInfo = new BullseyeSearch.CandidateInfo
				{
					hurtBox = hurtBox
				};
				if (getPosition)
				{
					candidateInfo.position = hurtBox.transform.position;
				}
				Vector3 vector = default(Vector3);
				if (getDifference)
				{
					vector = candidateInfo.position - this.searchOrigin;
				}
				if (getDot)
				{
					candidateInfo.dot = Vector3.Dot(this.searchDirection, vector.normalized);
				}
				if (getDistanceSqr)
				{
					candidateInfo.distanceSqr = vector.sqrMagnitude;
				}
				return candidateInfo;
			};
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x000A83EC File Offset: 0x000A65EC
		public void RefreshCandidates()
		{
			Func<HurtBox, BullseyeSearch.CandidateInfo> selector = this.GetSelector();
			this.candidatesEnumerable = (from hurtBox in HurtBox.readOnlyBullseyesList
			where this.teamMaskFilter.HasTeam(hurtBox.teamIndex)
			select hurtBox).Select(selector);
			if (this.filterByAngle)
			{
				this.candidatesEnumerable = this.candidatesEnumerable.Where(new Func<BullseyeSearch.CandidateInfo, bool>(this.<RefreshCandidates>g__DotOkay|25_1));
			}
			if (this.filterByDistance)
			{
				BullseyeSearch.<>c__DisplayClass25_0 CS$<>8__locals1 = new BullseyeSearch.<>c__DisplayClass25_0();
				float num = this.maxDistanceFilter;
				if (this.viewer)
				{
					num = Mathf.Min(num, this.viewer.visionDistance);
				}
				CS$<>8__locals1.minDistanceSqr = this.minDistanceFilter * this.minDistanceFilter;
				CS$<>8__locals1.maxDistanceSqr = num * num;
				this.candidatesEnumerable = this.candidatesEnumerable.Where(new Func<BullseyeSearch.CandidateInfo, bool>(CS$<>8__locals1.<RefreshCandidates>g__DistanceOkay|2));
			}
			if (this.filterByDistinctEntity)
			{
				this.candidatesEnumerable = this.candidatesEnumerable.Distinct(default(BullseyeSearch.CandidateInfo.EntityEqualityComparer));
			}
			Func<BullseyeSearch.CandidateInfo, float> sorter = this.GetSorter();
			if (sorter != null)
			{
				this.candidatesEnumerable = this.candidatesEnumerable.OrderBy(sorter);
			}
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000A84F8 File Offset: 0x000A66F8
		private Func<BullseyeSearch.CandidateInfo, float> GetSorter()
		{
			switch (this.sortMode)
			{
			case BullseyeSearch.SortMode.Distance:
				return (BullseyeSearch.CandidateInfo candidateInfo) => candidateInfo.distanceSqr;
			case BullseyeSearch.SortMode.Angle:
				return (BullseyeSearch.CandidateInfo candidateInfo) => -candidateInfo.dot;
			case BullseyeSearch.SortMode.DistanceAndAngle:
				return (BullseyeSearch.CandidateInfo candidateInfo) => -candidateInfo.dot * candidateInfo.distanceSqr;
			default:
				return null;
			}
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000A8584 File Offset: 0x000A6784
		public void FilterCandidatesByHealthFraction(float minHealthFraction = 0f, float maxHealthFraction = 1f)
		{
			if (minHealthFraction > 0f)
			{
				if (maxHealthFraction < 1f)
				{
					this.candidatesEnumerable = this.candidatesEnumerable.Where(delegate(BullseyeSearch.CandidateInfo v)
					{
						float combinedHealthFraction = v.hurtBox.healthComponent.combinedHealthFraction;
						return combinedHealthFraction >= minHealthFraction && combinedHealthFraction <= maxHealthFraction;
					});
					return;
				}
				this.candidatesEnumerable = from v in this.candidatesEnumerable
				where v.hurtBox.healthComponent.combinedHealthFraction >= minHealthFraction
				select v;
				return;
			}
			else
			{
				if (maxHealthFraction < 1f)
				{
					this.candidatesEnumerable = from v in this.candidatesEnumerable
					where v.hurtBox.healthComponent.combinedHealthFraction <= maxHealthFraction
					select v;
					return;
				}
				return;
			}
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000A8628 File Offset: 0x000A6828
		public void FilterOutGameObject(GameObject gameObject)
		{
			this.candidatesEnumerable = from v in this.candidatesEnumerable
			where v.hurtBox.healthComponent.gameObject != gameObject
			select v;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000A8660 File Offset: 0x000A6860
		public IEnumerable<HurtBox> GetResults()
		{
			IEnumerable<BullseyeSearch.CandidateInfo> source = this.candidatesEnumerable;
			if (this.filterByLoS)
			{
				source = from candidateInfo in source
				where this.CheckLoS(candidateInfo.position)
				select candidateInfo;
			}
			if (this.viewer)
			{
				source = from candidateInfo in source
				where this.CheckVisible(candidateInfo.hurtBox.healthComponent.gameObject)
				select candidateInfo;
			}
			return from candidateInfo in source
			select candidateInfo.hurtBox;
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000A86D4 File Offset: 0x000A68D4
		private bool CheckLoS(Vector3 targetPosition)
		{
			Vector3 direction = targetPosition - this.searchOrigin;
			RaycastHit raycastHit;
			return !Physics.Raycast(this.searchOrigin, direction, out raycastHit, direction.magnitude, LayerIndex.world.mask, this.queryTriggerInteraction);
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000A8720 File Offset: 0x000A6920
		private bool CheckVisible(GameObject gameObject)
		{
			CharacterBody component = gameObject.GetComponent<CharacterBody>();
			return !component || component.GetVisibilityLevel(this.viewer) >= VisibilityLevel.Revealed;
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000A87B0 File Offset: 0x000A69B0
		[CompilerGenerated]
		private bool <RefreshCandidates>g__DotOkay|25_1(BullseyeSearch.CandidateInfo candidateInfo)
		{
			return this.minThetaDot <= candidateInfo.dot && candidateInfo.dot <= this.maxThetaDot;
		}

		// Token: 0x04002A8F RID: 10895
		public CharacterBody viewer;

		// Token: 0x04002A90 RID: 10896
		public Vector3 searchOrigin;

		// Token: 0x04002A91 RID: 10897
		public Vector3 searchDirection;

		// Token: 0x04002A92 RID: 10898
		private float minThetaDot = -1f;

		// Token: 0x04002A93 RID: 10899
		private float maxThetaDot = 1f;

		// Token: 0x04002A94 RID: 10900
		public float minDistanceFilter;

		// Token: 0x04002A95 RID: 10901
		public float maxDistanceFilter = float.PositiveInfinity;

		// Token: 0x04002A96 RID: 10902
		public TeamMask teamMaskFilter = TeamMask.allButNeutral;

		// Token: 0x04002A97 RID: 10903
		public bool filterByLoS = true;

		// Token: 0x04002A98 RID: 10904
		public bool filterByDistinctEntity;

		// Token: 0x04002A99 RID: 10905
		public QueryTriggerInteraction queryTriggerInteraction;

		// Token: 0x04002A9A RID: 10906
		public BullseyeSearch.SortMode sortMode = BullseyeSearch.SortMode.Distance;

		// Token: 0x04002A9B RID: 10907
		private IEnumerable<BullseyeSearch.CandidateInfo> candidatesEnumerable;

		// Token: 0x02000742 RID: 1858
		private struct CandidateInfo
		{
			// Token: 0x04002A9C RID: 10908
			public HurtBox hurtBox;

			// Token: 0x04002A9D RID: 10909
			public Vector3 position;

			// Token: 0x04002A9E RID: 10910
			public float dot;

			// Token: 0x04002A9F RID: 10911
			public float distanceSqr;

			// Token: 0x02000743 RID: 1859
			public struct EntityEqualityComparer : IEqualityComparer<BullseyeSearch.CandidateInfo>
			{
				// Token: 0x060026A9 RID: 9897 RVA: 0x000A87F9 File Offset: 0x000A69F9
				public bool Equals(BullseyeSearch.CandidateInfo a, BullseyeSearch.CandidateInfo b)
				{
					return a.hurtBox.healthComponent == b.hurtBox.healthComponent;
				}

				// Token: 0x060026AA RID: 9898 RVA: 0x000A8813 File Offset: 0x000A6A13
				public int GetHashCode(BullseyeSearch.CandidateInfo obj)
				{
					return obj.hurtBox.healthComponent.GetHashCode();
				}
			}
		}

		// Token: 0x02000744 RID: 1860
		public enum SortMode
		{
			// Token: 0x04002AA1 RID: 10913
			None,
			// Token: 0x04002AA2 RID: 10914
			Distance,
			// Token: 0x04002AA3 RID: 10915
			Angle,
			// Token: 0x04002AA4 RID: 10916
			DistanceAndAngle
		}

		// Token: 0x02000745 RID: 1861
		// (Invoke) Token: 0x060026AC RID: 9900
		private delegate BullseyeSearch.CandidateInfo Selector(HurtBox hurtBox);
	}
}
