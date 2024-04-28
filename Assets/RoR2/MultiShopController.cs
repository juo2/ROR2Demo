using System;
using System.Runtime.InteropServices;
using HG;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020007C4 RID: 1988
	public class MultiShopController : NetworkBehaviour, IHologramContentProvider
	{
		// Token: 0x06002A1F RID: 10783 RVA: 0x000B5C74 File Offset: 0x000B3E74
		private void Start()
		{
			if (Run.instance && NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
				this.CreateTerminals();
				this.Networkcost = Run.instance.GetDifficultyScaledCost(this.baseCost);
				if (this._terminalGameObjects != null)
				{
					GameObject[] array = this._terminalGameObjects;
					for (int i = 0; i < array.Length; i++)
					{
						PurchaseInteraction component = array[i].GetComponent<PurchaseInteraction>();
						component.Networkcost = this.cost;
						component.costType = this.costType;
					}
				}
			}
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x000B5D08 File Offset: 0x000B3F08
		private void OnDestroy()
		{
			if (this._terminalGameObjects != null)
			{
				for (int i = this._terminalGameObjects.Length - 1; i >= 0; i--)
				{
					UnityEngine.Object.Destroy(this._terminalGameObjects[i]);
				}
				this._terminalGameObjects = null;
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000B5D48 File Offset: 0x000B3F48
		private void CreateTerminals()
		{
			this.doCloseOnTerminalPurchase = new bool[this.terminalPositions.Length];
			this._terminalGameObjects = new GameObject[this.terminalPositions.Length];
			this.terminalGameObjects = new ReadOnlyArray<GameObject>(this._terminalGameObjects);
			for (int i = 0; i < this.terminalPositions.Length; i++)
			{
				this.doCloseOnTerminalPurchase[i] = true;
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.terminalPrefab, this.terminalPositions[i].position, this.terminalPositions[i].rotation);
				this._terminalGameObjects[i] = gameObject;
				ShopTerminalBehavior component = gameObject.GetComponent<ShopTerminalBehavior>();
				component.serverMultiShopController = this;
				if (!component.selfGeneratePickup)
				{
					PickupIndex newPickupIndex = PickupIndex.none;
					if (this.doEquipmentInstead)
					{
						newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableEquipmentDropList);
					}
					else
					{
						switch (this.itemTier)
						{
						case ItemTier.Tier1:
							newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableTier1DropList);
							break;
						case ItemTier.Tier2:
							newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableTier2DropList);
							break;
						case ItemTier.Tier3:
							newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableTier3DropList);
							break;
						case ItemTier.Lunar:
							newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableLunarCombinedDropList);
							break;
						case ItemTier.VoidTier1:
							newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableVoidTier1DropList);
							break;
						case ItemTier.VoidTier2:
							newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableVoidTier2DropList);
							break;
						case ItemTier.VoidTier3:
							newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableVoidTier3DropList);
							break;
						case ItemTier.VoidBoss:
							newPickupIndex = this.rng.NextElementUniform<PickupIndex>(Run.instance.availableVoidBossDropList);
							break;
						}
					}
					bool newHidden = this.hideDisplayContent && i >= this.revealCount && this.rng.nextNormalizedFloat < this.hiddenChance;
					component.SetPickupIndex(newPickupIndex, newHidden);
				}
				else
				{
					component.SetHidden(i >= this.revealCount && this.rng.nextNormalizedFloat < this.hiddenChance);
				}
				NetworkServer.Spawn(gameObject);
			}
			GameObject[] array = this._terminalGameObjects;
			for (int j = 0; j < array.Length; j++)
			{
				GameObject gameObject2 = array[j];
				PurchaseInteraction purchaseInteraction = gameObject2.GetComponent<PurchaseInteraction>();
				purchaseInteraction.onPurchase.AddListener(delegate(Interactor interactor)
				{
					this.OnPurchase(interactor, purchaseInteraction);
				});
			}
		}

		// Token: 0x06002A22 RID: 10786 RVA: 0x000B5FDC File Offset: 0x000B41DC
		private void OnPurchase(Interactor interactor, PurchaseInteraction purchaseInteraction)
		{
			bool flag = false;
			this.Networkavailable = false;
			for (int i = 0; i < this._terminalGameObjects.Length; i++)
			{
				GameObject gameObject = this._terminalGameObjects[i];
				PurchaseInteraction component = gameObject.GetComponent<PurchaseInteraction>();
				if (purchaseInteraction == component)
				{
					component.Networkavailable = false;
					gameObject.GetComponent<ShopTerminalBehavior>().SetNoPickup();
					flag = this.doCloseOnTerminalPurchase[i];
				}
				this.Networkavailable = (this.available || component.available);
			}
			if (flag)
			{
				this.Networkavailable = false;
				foreach (GameObject gameObject2 in this._terminalGameObjects)
				{
					gameObject2.GetComponent<PurchaseInteraction>().Networkavailable = false;
					gameObject2.GetComponent<ShopTerminalBehavior>().SetNoPickup();
				}
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000B608B File Offset: 0x000B428B
		public bool ShouldDisplayHologram(GameObject viewer)
		{
			return this.available;
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x0007647A File Offset: 0x0007467A
		public GameObject GetHologramContentPrefab()
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/CostHologramContent");
		}

		// Token: 0x06002A25 RID: 10789 RVA: 0x000B6094 File Offset: 0x000B4294
		public void UpdateHologramContent(GameObject hologramContentObject)
		{
			CostHologramContent component = hologramContentObject.GetComponent<CostHologramContent>();
			if (component)
			{
				component.displayValue = this.cost;
				component.costType = this.costType;
			}
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000B60C8 File Offset: 0x000B42C8
		public void SetCloseOnTerminalPurchase(PurchaseInteraction terminalPurchaseInteraction, bool doCloseMultiShop)
		{
			for (int i = 0; i < this._terminalGameObjects.Length; i++)
			{
				if (this._terminalGameObjects[i].GetComponent<PurchaseInteraction>() == terminalPurchaseInteraction)
				{
					this.doCloseOnTerminalPurchase[i] = doCloseMultiShop;
				}
			}
		}

		// Token: 0x06002A28 RID: 10792 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x000B612C File Offset: 0x000B432C
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x000B613F File Offset: 0x000B433F
		public bool Networkavailable
		{
			get
			{
				return this.available;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this.available, 1U);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000B6154 File Offset: 0x000B4354
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x000B6167 File Offset: 0x000B4367
		public int Networkcost
		{
			get
			{
				return this.cost;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.cost, 2U);
			}
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x000B617C File Offset: 0x000B437C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.available);
				writer.WritePackedUInt32((uint)this.cost);
				return true;
			}
			bool flag = false;
			if ((base.syncVarDirtyBits & 1U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.available);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.cost);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002A2E RID: 10798 RVA: 0x000B6228 File Offset: 0x000B4428
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.available = reader.ReadBoolean();
				this.cost = (int)reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.available = reader.ReadBoolean();
			}
			if ((num & 2) != 0)
			{
				this.cost = (int)reader.ReadPackedUInt32();
			}
		}

		// Token: 0x06002A2F RID: 10799 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002D64 RID: 11620
		[Tooltip("The shop terminal prefab to instantiate.")]
		public GameObject terminalPrefab;

		// Token: 0x04002D65 RID: 11621
		[Tooltip("The positions at which to instantiate shop terminals.")]
		public Transform[] terminalPositions;

		// Token: 0x04002D66 RID: 11622
		[Tooltip("The number of terminals guaranteed to have their item revealed")]
		public int revealCount = 1;

		// Token: 0x04002D67 RID: 11623
		[Tooltip("The percentage chance that terminals after the reveal count will be hidden")]
		public float hiddenChance = 0.2f;

		// Token: 0x04002D68 RID: 11624
		[Tooltip("The tier of items to drop")]
		[Header("Deprecated")]
		public ItemTier itemTier;

		// Token: 0x04002D69 RID: 11625
		public bool doEquipmentInstead;

		// Token: 0x04002D6A RID: 11626
		[Tooltip("Whether or not there's a chance the item contents are replaced with a '?'")]
		private bool hideDisplayContent = true;

		// Token: 0x04002D6B RID: 11627
		private bool[] doCloseOnTerminalPurchase;

		// Token: 0x04002D6C RID: 11628
		private GameObject[] _terminalGameObjects;

		// Token: 0x04002D6D RID: 11629
		public ReadOnlyArray<GameObject> terminalGameObjects;

		// Token: 0x04002D6E RID: 11630
		[SyncVar]
		private bool available = true;

		// Token: 0x04002D6F RID: 11631
		public int baseCost;

		// Token: 0x04002D70 RID: 11632
		public CostTypeIndex costType;

		// Token: 0x04002D71 RID: 11633
		[SyncVar]
		private int cost;

		// Token: 0x04002D72 RID: 11634
		private Xoroshiro128Plus rng;
	}
}
