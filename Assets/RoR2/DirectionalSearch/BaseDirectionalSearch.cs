using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2.DirectionalSearch
{
	// Token: 0x02000C99 RID: 3225
	public class BaseDirectionalSearch<TSource, TSelector, TCandidateFilter> where TSource : class where TSelector : IGenericWorldSearchSelector<TSource> where TCandidateFilter : IGenericDirectionalSearchFilter<TSource>
	{
		// Token: 0x060049C8 RID: 18888 RVA: 0x0012EF28 File Offset: 0x0012D128
		public BaseDirectionalSearch(TSelector selector, TCandidateFilter candidateFilter)
		{
			this.selector = selector;
			this.candidateFilter = candidateFilter;
		}

		// Token: 0x170006B4 RID: 1716
		// (set) Token: 0x060049C9 RID: 18889 RVA: 0x0012EF83 File Offset: 0x0012D183
		public float minAngleFilter
		{
			set
			{
				this.maxThetaDot = Mathf.Cos(Mathf.Clamp(value, 0f, 180f) * 0.017453292f);
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (set) Token: 0x060049CA RID: 18890 RVA: 0x0012EFA6 File Offset: 0x0012D1A6
		public float maxAngleFilter
		{
			set
			{
				this.minThetaDot = Mathf.Cos(Mathf.Clamp(value, 0f, 180f) * 0.017453292f);
			}
		}

		// Token: 0x060049CB RID: 18891 RVA: 0x0012EFCC File Offset: 0x0012D1CC
		public TSource SearchCandidatesForSingleTarget<TSourceEnumerable>(TSourceEnumerable sourceEnumerable) where TSourceEnumerable : IEnumerable<!0>
		{
			ArrayUtils.Clear<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(this.candidateInfoList, ref this.candidateCount);
			float num = this.minDistanceFilter * this.minDistanceFilter;
			float num2 = this.maxDistanceFilter * this.maxDistanceFilter;
			foreach (TSource source in sourceEnumerable)
			{
				BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo candidateInfo = new BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo
				{
					source = source
				};
				Transform transform = this.selector.GetTransform(source);
				if (transform)
				{
					candidateInfo.position = transform.position;
					candidateInfo.diff = candidateInfo.position - this.searchOrigin;
					float sqrMagnitude = candidateInfo.diff.sqrMagnitude;
					if (sqrMagnitude >= num && sqrMagnitude <= num2)
					{
						candidateInfo.distance = Mathf.Sqrt(sqrMagnitude);
						candidateInfo.dot = ((candidateInfo.distance == 0f) ? 0f : Vector3.Dot(this.searchDirection, candidateInfo.diff / candidateInfo.distance));
						if (candidateInfo.dot >= this.minThetaDot && candidateInfo.dot <= this.maxThetaDot)
						{
							candidateInfo.entity = this.selector.GetRootObject(source);
							ArrayUtils.ArrayAppend<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(ref this.candidateInfoList, ref this.candidateCount, candidateInfo);
						}
					}
				}
			}
			for (int i = this.candidateCount - 1; i >= 0; i--)
			{
				if (!this.candidateFilter.PassesFilter(this.candidateInfoList[i].source))
				{
					ArrayUtils.ArrayRemoveAt<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(this.candidateInfoList, ref this.candidateCount, i, 1);
				}
			}
			Array.Sort<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(this.candidateInfoList, this.GetSorter());
			if (this.filterByDistinctEntity)
			{
				for (int j = this.candidateCount - 1; j >= 0; j--)
				{
					ref BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo ptr = ref this.candidateInfoList[j];
					for (int k = 0; k < j; k++)
					{
						ref BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo ptr2 = ref this.candidateInfoList[k];
						if (ptr.entity == ptr2.entity)
						{
							ArrayUtils.ArrayRemoveAt<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(this.candidateInfoList, ref this.candidateCount, j, 1);
							break;
						}
					}
				}
			}
			TSource result = default(TSource);
			if (this.filterByLoS)
			{
				for (int l = 0; l < this.candidateCount; l++)
				{
					RaycastHit raycastHit;
					if (!Physics.Linecast(this.searchOrigin, this.candidateInfoList[l].position, out raycastHit, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
					{
						result = this.candidateInfoList[l].source;
						break;
					}
				}
			}
			else if (this.candidateCount > 0)
			{
				result = this.candidateInfoList[0].source;
			}
			ArrayUtils.Clear<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(this.candidateInfoList, ref this.candidateCount);
			return result;
		}

		// Token: 0x060049CC RID: 18892 RVA: 0x0012F2DC File Offset: 0x0012D4DC
		private static int DistanceToInversePriority(BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo a, BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo b)
		{
			return a.distance.CompareTo(b.distance);
		}

		// Token: 0x060049CD RID: 18893 RVA: 0x0012F2F0 File Offset: 0x0012D4F0
		private static int AngleToInversePriority(BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo a, BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo b)
		{
			return (-a.dot).CompareTo(-b.dot);
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x0012F314 File Offset: 0x0012D514
		private static int DistanceAndAngleToInversePriority(BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo a, BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo b)
		{
			return (-a.dot * a.distance).CompareTo(-b.dot * b.distance);
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x0012F348 File Offset: 0x0012D548
		private Comparison<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo> GetSorter()
		{
			switch (this.sortMode)
			{
			case SortMode.Distance:
				return new Comparison<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.DistanceToInversePriority);
			case SortMode.Angle:
				return new Comparison<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.AngleToInversePriority);
			case SortMode.DistanceAndAngle:
				return new Comparison<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>(BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.DistanceAndAngleToInversePriority);
			default:
				return null;
			}
		}

		// Token: 0x0400465A RID: 18010
		public Vector3 searchOrigin;

		// Token: 0x0400465B RID: 18011
		public Vector3 searchDirection;

		// Token: 0x0400465C RID: 18012
		private float minThetaDot = -1f;

		// Token: 0x0400465D RID: 18013
		private float maxThetaDot = 1f;

		// Token: 0x0400465E RID: 18014
		public float minDistanceFilter;

		// Token: 0x0400465F RID: 18015
		public float maxDistanceFilter = float.PositiveInfinity;

		// Token: 0x04004660 RID: 18016
		public SortMode sortMode = SortMode.Distance;

		// Token: 0x04004661 RID: 18017
		public bool filterByLoS = true;

		// Token: 0x04004662 RID: 18018
		public bool filterByDistinctEntity;

		// Token: 0x04004663 RID: 18019
		private BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo[] candidateInfoList = Array.Empty<BaseDirectionalSearch<TSource, TSelector, TCandidateFilter>.CandidateInfo>();

		// Token: 0x04004664 RID: 18020
		private int candidateCount;

		// Token: 0x04004665 RID: 18021
		protected TSelector selector;

		// Token: 0x04004666 RID: 18022
		protected TCandidateFilter candidateFilter;

		// Token: 0x02000C9A RID: 3226
		private struct CandidateInfo
		{
			// Token: 0x04004667 RID: 18023
			public TSource source;

			// Token: 0x04004668 RID: 18024
			public Vector3 position;

			// Token: 0x04004669 RID: 18025
			public Vector3 diff;

			// Token: 0x0400466A RID: 18026
			public float distance;

			// Token: 0x0400466B RID: 18027
			public float dot;

			// Token: 0x0400466C RID: 18028
			public GameObject entity;
		}
	}
}
