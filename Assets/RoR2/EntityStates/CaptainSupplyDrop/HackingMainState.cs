using System;
using System.Collections.Generic;
using HG;
using RoR2;
using UnityEngine;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x02000419 RID: 1049
	public class HackingMainState : BaseMainState
	{
		// Token: 0x060012DF RID: 4831 RVA: 0x00054198 File Offset: 0x00052398
		public override void OnEnter()
		{
			base.OnEnter();
			this.radius = HackingMainState.baseRadius;
			if (base.isAuthority)
			{
				this.sphereSearch = new SphereSearch();
				this.sphereSearch.origin = base.transform.position;
				this.sphereSearch.mask = LayerIndex.CommonMasks.interactable;
				this.sphereSearch.queryTriggerInteraction = QueryTriggerInteraction.Collide;
				this.sphereSearch.radius = this.radius;
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0005418D File Offset: 0x0005238D
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0005420C File Offset: 0x0005240C
		private PurchaseInteraction ScanForTarget()
		{
			List<Collider> list = CollectionPool<Collider, List<Collider>>.RentCollection();
			this.sphereSearch.ClearCandidates();
			this.sphereSearch.RefreshCandidates();
			this.sphereSearch.FilterCandidatesByColliderEntities();
			this.sphereSearch.OrderCandidatesByDistance();
			this.sphereSearch.FilterCandidatesByDistinctColliderEntities();
			this.sphereSearch.GetColliders(list);
			PurchaseInteraction result = null;
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				PurchaseInteraction component = list[i].GetComponent<EntityLocator>().entity.GetComponent<PurchaseInteraction>();
				if (HackingMainState.PurchaseInteractionIsValidTarget(component))
				{
					result = component;
					break;
				}
				i++;
			}
			CollectionPool<Collider, List<Collider>>.ReturnCollection(list);
			return result;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000542AC File Offset: 0x000524AC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.scanTimer -= Time.fixedDeltaTime;
				if (this.scanTimer <= 0f)
				{
					this.scanTimer = HackingMainState.scanInterval;
					PurchaseInteraction purchaseInteraction = this.ScanForTarget();
					if (purchaseInteraction)
					{
						this.outer.SetNextState(new HackingInProgressState
						{
							target = purchaseInteraction
						});
					}
				}
			}
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x00054317 File Offset: 0x00052517
		public static bool PurchaseInteractionIsValidTarget(PurchaseInteraction purchaseInteraction)
		{
			return purchaseInteraction && (purchaseInteraction.costType == CostTypeIndex.Money && purchaseInteraction.cost > 0) && purchaseInteraction.available;
		}

		// Token: 0x04001840 RID: 6208
		public static float baseRadius = 7f;

		// Token: 0x04001841 RID: 6209
		public static float scanInterval = 5f;

		// Token: 0x04001842 RID: 6210
		private float radius;

		// Token: 0x04001843 RID: 6211
		private float scanTimer;

		// Token: 0x04001844 RID: 6212
		private SphereSearch sphereSearch;
	}
}
