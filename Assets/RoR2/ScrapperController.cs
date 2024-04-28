using System;
using EntityStates.Scrapper;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000881 RID: 2177
	public class ScrapperController : NetworkBehaviour
	{
		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x000CB1F2 File Offset: 0x000C93F2
		// (set) Token: 0x06002FAD RID: 12205 RVA: 0x000CB1FA File Offset: 0x000C93FA
		public ItemIndex lastScrappedItemIndex { get; private set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x000CB203 File Offset: 0x000C9403
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x000CB20B File Offset: 0x000C940B
		public int itemsEaten { get; set; }

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000026ED File Offset: 0x000008ED
		private void Start()
		{
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x000026ED File Offset: 0x000008ED
		private void Update()
		{
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000CB214 File Offset: 0x000C9414
		[Server]
		public void AssignPotentialInteractor(Interactor potentialInteractor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ScrapperController::AssignPotentialInteractor(RoR2.Interactor)' called on client");
				return;
			}
			this.interactor = potentialInteractor;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x000CB234 File Offset: 0x000C9434
		[Server]
		public void BeginScrapping(int intPickupIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ScrapperController::BeginScrapping(System.Int32)' called on client");
				return;
			}
			this.itemsEaten = 0;
			PickupDef pickupDef = PickupCatalog.GetPickupDef(new PickupIndex(intPickupIndex));
			if (pickupDef != null && this.interactor)
			{
				this.lastScrappedItemIndex = pickupDef.itemIndex;
				CharacterBody component = this.interactor.GetComponent<CharacterBody>();
				if (component && component.inventory)
				{
					int num = Mathf.Min(this.maxItemsToScrapAtATime, component.inventory.GetItemCount(pickupDef.itemIndex));
					if (num > 0)
					{
						component.inventory.RemoveItem(pickupDef.itemIndex, num);
						this.itemsEaten += num;
						for (int i = 0; i < num; i++)
						{
							ScrapperController.CreateItemTakenOrb(component.corePosition, base.gameObject, pickupDef.itemIndex);
						}
					}
				}
			}
			if (this.esm)
			{
				this.esm.SetNextState(new WaitToBeginScrapping());
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000CB330 File Offset: 0x000C9530
		[Server]
		public static void CreateItemTakenOrb(Vector3 effectOrigin, GameObject targetObject, ItemIndex itemIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ScrapperController::CreateItemTakenOrb(UnityEngine.Vector3,UnityEngine.GameObject,RoR2.ItemIndex)' called on client");
				return;
			}
			GameObject effectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/OrbEffects/ItemTakenOrbEffect");
			EffectData effectData = new EffectData
			{
				origin = effectOrigin,
				genericFloat = 1.5f,
				genericUInt = (uint)(itemIndex + 1)
			};
			effectData.SetNetworkedObjectReference(targetObject);
			EffectManager.SpawnEffect(effectPrefab, effectData, true);
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x000CB39C File Offset: 0x000C959C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003179 RID: 12665
		public PickupPickerController pickupPickerController;

		// Token: 0x0400317A RID: 12666
		public EntityStateMachine esm;

		// Token: 0x0400317D RID: 12669
		public int maxItemsToScrapAtATime = 10;

		// Token: 0x0400317E RID: 12670
		private Interactor interactor;
	}
}
