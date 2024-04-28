using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020008C7 RID: 2247
	public class TetherVfxOrigin : MonoBehaviour
	{
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600324C RID: 12876 RVA: 0x000D464B File Offset: 0x000D284B
		// (set) Token: 0x0600324D RID: 12877 RVA: 0x000D4653 File Offset: 0x000D2853
		private protected new Transform transform { protected get; private set; }

		// Token: 0x0600324E RID: 12878 RVA: 0x000D465C File Offset: 0x000D285C
		protected void Awake()
		{
			this.transform = base.transform;
			this.tetheredTransforms = CollectionPool<Transform, List<Transform>>.RentCollection();
			this.tetherVfxs = CollectionPool<TetherVfx, List<TetherVfx>>.RentCollection();
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000D4680 File Offset: 0x000D2880
		protected void OnDestroy()
		{
			for (int i = this.tetherVfxs.Count - 1; i >= 0; i--)
			{
				this.RemoveTetherAt(i);
			}
			this.tetherVfxs = CollectionPool<TetherVfx, List<TetherVfx>>.ReturnCollection(this.tetherVfxs);
			this.tetheredTransforms = CollectionPool<Transform, List<Transform>>.ReturnCollection(this.tetheredTransforms);
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000D46D0 File Offset: 0x000D28D0
		protected void AddTether(Transform target)
		{
			if (!target)
			{
				return;
			}
			TetherVfx tetherVfx = null;
			if (this.tetherPrefab)
			{
				tetherVfx = UnityEngine.Object.Instantiate<GameObject>(this.tetherPrefab, this.transform).GetComponent<TetherVfx>();
				tetherVfx.tetherTargetTransform = target;
			}
			this.tetheredTransforms.Add(target);
			this.tetherVfxs.Add(tetherVfx);
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000D472C File Offset: 0x000D292C
		protected void RemoveTetherAt(int i)
		{
			TetherVfx tetherVfx = this.tetherVfxs[i];
			if (tetherVfx)
			{
				tetherVfx.transform.SetParent(null);
				tetherVfx.Terminate();
			}
			this.tetheredTransforms.RemoveAt(i);
			this.tetherVfxs.RemoveAt(i);
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000D4778 File Offset: 0x000D2978
		public void SetTetheredTransforms(List<Transform> newTetheredTransforms)
		{
			List<Transform> list = CollectionPool<Transform, List<Transform>>.RentCollection();
			List<Transform> list2 = CollectionPool<Transform, List<Transform>>.RentCollection();
			ListUtils.FindExclusiveEntriesByReference<Transform>(this.tetheredTransforms, newTetheredTransforms, list2, list);
			int i = 0;
			int count = list2.Count;
			while (i < count)
			{
				List<Transform> list3 = this.tetheredTransforms;
				Transform transform = list2[i];
				this.RemoveTetherAt(ListUtils.FirstOccurrenceByReference<List<Transform>, Transform>(list3, transform));
				i++;
			}
			int j = 0;
			int count2 = list.Count;
			while (j < count2)
			{
				this.AddTether(list[j]);
				j++;
			}
			CollectionPool<Transform, List<Transform>>.ReturnCollection(list2);
			CollectionPool<Transform, List<Transform>>.ReturnCollection(list);
		}

		// Token: 0x04003372 RID: 13170
		public GameObject tetherPrefab;

		// Token: 0x04003374 RID: 13172
		private List<Transform> tetheredTransforms;

		// Token: 0x04003375 RID: 13173
		private List<TetherVfx> tetherVfxs;
	}
}
