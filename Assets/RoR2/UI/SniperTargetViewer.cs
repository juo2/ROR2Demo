using System;
using System.Collections.Generic;
using HG;
using UnityEngine;

namespace RoR2.UI
{
	// Token: 0x02000D8D RID: 3469
	[RequireComponent(typeof(PointViewer))]
	public class SniperTargetViewer : MonoBehaviour
	{
		// Token: 0x06004F70 RID: 20336 RVA: 0x00148B2A File Offset: 0x00146D2A
		private void Awake()
		{
			this.pointViewer = base.GetComponent<PointViewer>();
			this.OnTransformParentChanged();
		}

		// Token: 0x06004F71 RID: 20337 RVA: 0x00148B3E File Offset: 0x00146D3E
		private void OnTransformParentChanged()
		{
			this.hud = base.GetComponentInParent<HUD>();
		}

		// Token: 0x06004F72 RID: 20338 RVA: 0x00148B4C File Offset: 0x00146D4C
		private void OnDisable()
		{
			this.SetDisplayedTargets(Array.Empty<HurtBox>());
			this.hurtBoxToVisualizer.Clear();
		}

		// Token: 0x06004F73 RID: 20339 RVA: 0x00148B64 File Offset: 0x00146D64
		private void Update()
		{
			List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
			if (this.hud && this.hud.targetMaster)
			{
				TeamIndex teamIndex = this.hud.targetMaster.teamIndex;
				IReadOnlyList<HurtBox> readOnlySniperTargetsList = HurtBox.readOnlySniperTargetsList;
				int i = 0;
				int count = readOnlySniperTargetsList.Count;
				while (i < count)
				{
					HurtBox hurtBox = readOnlySniperTargetsList[i];
					if (hurtBox.healthComponent && hurtBox.healthComponent.alive && FriendlyFireManager.ShouldDirectHitProceed(hurtBox.healthComponent, teamIndex) && hurtBox.healthComponent.body != this.hud.targetMaster.GetBody())
					{
						list.Add(hurtBox);
					}
					i++;
				}
			}
			this.SetDisplayedTargets(list);
			list = CollectionPool<HurtBox, List<HurtBox>>.ReturnCollection(list);
		}

		// Token: 0x06004F74 RID: 20340 RVA: 0x00148C30 File Offset: 0x00146E30
		private void OnTargetDiscovered(HurtBox hurtBox)
		{
			if (!this.hurtBoxToVisualizer.ContainsKey(hurtBox))
			{
				GameObject value = this.pointViewer.AddElement(new PointViewer.AddElementRequest
				{
					elementPrefab = this.visualizerPrefab,
					target = hurtBox.transform,
					targetWorldVerticalOffset = 0f,
					targetWorldRadius = HurtBox.sniperTargetRadius,
					scaleWithDistance = true
				});
				this.hurtBoxToVisualizer.Add(hurtBox, value);
				return;
			}
			Debug.LogWarning(string.Format("Already discovered hurtbox: {0}", hurtBox));
		}

		// Token: 0x06004F75 RID: 20341 RVA: 0x00148CC4 File Offset: 0x00146EC4
		private void OnTargetLost(HurtBox hurtBox)
		{
			GameObject elementInstance;
			if (this.hurtBoxToVisualizer.TryGetValue(hurtBox, out elementInstance))
			{
				this.pointViewer.RemoveElement(elementInstance);
			}
		}

		// Token: 0x06004F76 RID: 20342 RVA: 0x00148CF4 File Offset: 0x00146EF4
		private void SetDisplayedTargets(IReadOnlyList<HurtBox> newDisplayedTargets)
		{
			Util.Swap<List<HurtBox>>(ref this.displayedTargets, ref this.previousDisplayedTargets);
			this.displayedTargets.Clear();
			ListUtils.AddRange<HurtBox, IReadOnlyList<HurtBox>>(this.displayedTargets, newDisplayedTargets);
			List<HurtBox> list = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
			List<HurtBox> list2 = CollectionPool<HurtBox, List<HurtBox>>.RentCollection();
			ListUtils.FindExclusiveEntriesByReference<HurtBox>(this.displayedTargets, this.previousDisplayedTargets, list, list2);
			foreach (HurtBox hurtBox in list2)
			{
				this.OnTargetLost(hurtBox);
			}
			foreach (HurtBox hurtBox2 in list)
			{
				this.OnTargetDiscovered(hurtBox2);
			}
			list2 = CollectionPool<HurtBox, List<HurtBox>>.ReturnCollection(list2);
			list = CollectionPool<HurtBox, List<HurtBox>>.ReturnCollection(list);
		}

		// Token: 0x04004C20 RID: 19488
		public GameObject visualizerPrefab;

		// Token: 0x04004C21 RID: 19489
		private PointViewer pointViewer;

		// Token: 0x04004C22 RID: 19490
		private HUD hud;

		// Token: 0x04004C23 RID: 19491
		private Dictionary<UnityObjectWrapperKey<HurtBox>, GameObject> hurtBoxToVisualizer = new Dictionary<UnityObjectWrapperKey<HurtBox>, GameObject>();

		// Token: 0x04004C24 RID: 19492
		private List<HurtBox> displayedTargets = new List<HurtBox>();

		// Token: 0x04004C25 RID: 19493
		private List<HurtBox> previousDisplayedTargets = new List<HurtBox>();
	}
}
