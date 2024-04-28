using System;
using System.Collections.Generic;
using RoR2.Items;
using RoR2.Networking;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020008F7 RID: 2295
	[RequireComponent(typeof(PurchaseInteraction))]
	public class VoidSuppressorBehavior : NetworkBehaviour
	{
		// Token: 0x060033B5 RID: 13237 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x000D9C53 File Offset: 0x000D7E53
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
			}
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x000D9C76 File Offset: 0x000D7E76
		public override void OnStartClient()
		{
			this.nextItemsToSuppress.Callback = new SyncList<PickupIndex>.SyncListChanged(this.OnItemsUpdated);
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x000D9C90 File Offset: 0x000D7E90
		public void FixedUpdate()
		{
			if (this.purchaseCount < this.maxPurchaseCount)
			{
				if (this.timeUntilUseRefresh > 0f)
				{
					this.timeUntilUseRefresh -= Time.fixedDeltaTime;
					if (this.timeUntilUseRefresh <= 0f)
					{
						this.animator.SetBool(this.animatorSuppressName, false);
						this.purchaseInteraction.SetAvailable(true);
						this.purchaseInteraction.Networkcost = (int)((float)this.purchaseInteraction.cost * this.costMultiplierPerPurchase);
					}
				}
				if (this.timeUntilItemRefresh > 0f)
				{
					this.timeUntilItemRefresh -= Time.fixedDeltaTime;
					if (this.timeUntilItemRefresh <= 0f)
					{
						this.nextItemsToSuppress.Clear();
						this.RefreshItems();
					}
				}
			}
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x000D9D54 File Offset: 0x000D7F54
		[Server]
		public void OnInteraction(Interactor interactor)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoidSuppressorBehavior::OnInteraction(RoR2.Interactor)' called on client");
				return;
			}
			this.timeUntilUseRefresh = this.useRefreshDelay;
			if (this.hasRevealed)
			{
				this.timeUntilItemRefresh = this.itemRefreshDelay;
				if (this.nextItemsToSuppress != null)
				{
					CharacterBody component = interactor.GetComponent<CharacterBody>();
					int num = 0;
					while (num < this.itemsSuppressedPerPurchase && num < (int)this.nextItemsToSuppress.Count)
					{
						ItemIndex itemIndex = this.nextItemsToSuppress[num].itemIndex;
						if (SuppressedItemManager.SuppressItem(itemIndex, this.transformItemIndex))
						{
							ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
							ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(itemDef.tier);
							Chat.SendBroadcastChat(new ColoredTokenChatMessage
							{
								subjectAsCharacterBody = component,
								baseToken = "VOID_SUPPRESSOR_USE_MESSAGE",
								paramTokens = new string[]
								{
									itemDef.nameToken
								},
								paramColors = new Color32[]
								{
									ColorCatalog.GetColor(itemTierDef.colorIndex)
								}
							});
						}
						num++;
					}
				}
				EffectManager.SpawnEffect(this.effectPrefab, new EffectData
				{
					origin = base.transform.position,
					rotation = Quaternion.identity,
					scale = 1f,
					color = this.effectColor
				}, true);
				this.purchaseCount++;
				if (this.purchaseCount >= this.maxPurchaseCount)
				{
					if (this.symbolTransform)
					{
						this.symbolTransform.gameObject.SetActive(false);
					}
					this.animator.SetBool(this.animatorIsAvailableName, false);
				}
				this.animator.SetBool(this.animatorSuppressName, true);
				return;
			}
			this.hasRevealed = true;
			this.animator.SetBool(this.animatorIsAvailableName, true);
			this.purchaseInteraction.NetworkcontextToken = this.revealedContext;
			this.purchaseInteraction.NetworkdisplayNameToken = this.revealedName;
			this.RefreshItems();
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000D9F4C File Offset: 0x000D814C
		[Server]
		public void RefreshItems()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.VoidSuppressorBehavior::RefreshItems()' called on client");
				return;
			}
			if (this.timeUntilItemRefresh > 0f)
			{
				return;
			}
			for (int i = (int)(this.nextItemsToSuppress.Count - 1); i >= 0; i--)
			{
				if (SuppressedItemManager.HasItemBeenSuppressed(this.nextItemsToSuppress[i].itemIndex))
				{
					this.nextItemsToSuppress.RemoveAt(i);
				}
			}
			int num = this.itemsSuppressedPerPurchase - (int)this.nextItemsToSuppress.Count;
			if (num > 0)
			{
				List<PickupIndex> list = null;
				PickupIndex item = PickupIndex.none;
				switch (this.purchaseCount)
				{
				case 0:
					list = new List<PickupIndex>(Run.instance.availableTier1DropList);
					item = PickupCatalog.FindPickupIndex(DLC1Content.Items.ScrapWhiteSuppressed.itemIndex);
					this.transformItemIndex = (this.transformItems ? DLC1Content.Items.ScrapWhiteSuppressed.itemIndex : ItemIndex.None);
					break;
				case 1:
					list = new List<PickupIndex>(Run.instance.availableTier2DropList);
					item = PickupCatalog.FindPickupIndex(DLC1Content.Items.ScrapGreenSuppressed.itemIndex);
					this.transformItemIndex = (this.transformItems ? DLC1Content.Items.ScrapGreenSuppressed.itemIndex : ItemIndex.None);
					break;
				case 2:
					list = new List<PickupIndex>(Run.instance.availableTier3DropList);
					item = PickupCatalog.FindPickupIndex(DLC1Content.Items.ScrapRedSuppressed.itemIndex);
					this.transformItemIndex = (this.transformItems ? DLC1Content.Items.ScrapRedSuppressed.itemIndex : ItemIndex.None);
					break;
				}
				if (list != null && list.Count > 0)
				{
					list.Remove(item);
					foreach (PickupIndex item2 in this.nextItemsToSuppress)
					{
						list.Remove(item2);
					}
					Util.ShuffleList<PickupIndex>(list, this.rng);
					int num2 = list.Count - num;
					if (num2 > 0)
					{
						list.RemoveRange(num, num2);
					}
					foreach (PickupIndex item3 in list)
					{
						this.nextItemsToSuppress.Add(item3);
					}
				}
			}
			this.RefreshPickupDisplays();
			if (this.nextItemsToSuppress.Count == 0)
			{
				if (this.purchaseCount < this.maxPurchaseCount)
				{
					this.purchaseCount++;
					this.RefreshItems();
					return;
				}
				if (this.symbolTransform)
				{
					this.symbolTransform.gameObject.SetActive(false);
				}
				this.purchaseInteraction.SetAvailable(false);
				this.animator.SetBool(this.animatorIsAvailableName, false);
			}
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000DA1F4 File Offset: 0x000D83F4
		public void OnPlayerNearby()
		{
			this.animator.SetBool(this.animatorIsPlayerNearbyName, true);
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000DA208 File Offset: 0x000D8408
		public void OnPlayerFar()
		{
			this.animator.SetBool(this.animatorIsPlayerNearbyName, false);
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000DA21C File Offset: 0x000D841C
		private void OnItemsUpdated(SyncList<PickupIndex>.Operation op, int index)
		{
			this.RefreshPickupDisplays();
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000DA224 File Offset: 0x000D8424
		private void RefreshPickupDisplays()
		{
			for (int i = 0; i < this.pickupDisplays.Length; i++)
			{
				if (i < (int)this.nextItemsToSuppress.Count)
				{
					this.pickupDisplays[i].gameObject.SetActive(true);
					this.pickupDisplays[i].SetPickupIndex(this.nextItemsToSuppress[i], i >= this.numItemsToReveal);
				}
				else
				{
					this.pickupDisplays[i].gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000DA2C8 File Offset: 0x000D84C8
		protected static void InvokeSyncListnextItemsToSuppress(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("SyncList nextItemsToSuppress called on server.");
				return;
			}
			((VoidSuppressorBehavior)obj).nextItemsToSuppress.HandleMsg(reader);
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000DA2F1 File Offset: 0x000D84F1
		static VoidSuppressorBehavior()
		{
			NetworkBehaviour.RegisterSyncListDelegate(typeof(VoidSuppressorBehavior), VoidSuppressorBehavior.kListnextItemsToSuppress, new NetworkBehaviour.CmdDelegate(VoidSuppressorBehavior.InvokeSyncListnextItemsToSuppress));
			NetworkCRC.RegisterBehaviour("VoidSuppressorBehavior", 0);
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x000DA32C File Offset: 0x000D852C
		private void Awake()
		{
			this.nextItemsToSuppress.InitializeBehaviour(this, VoidSuppressorBehavior.kListnextItemsToSuppress);
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x000DA340 File Offset: 0x000D8540
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WriteStructSyncListPickupIndex_VoidSuppressorBehavior(writer, this.nextItemsToSuppress);
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
				GeneratedNetworkCode._WriteStructSyncListPickupIndex_VoidSuppressorBehavior(writer, this.nextItemsToSuppress);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x000DA3AC File Offset: 0x000D85AC
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				GeneratedNetworkCode._ReadStructSyncListPickupIndex_VoidSuppressorBehavior(reader, this.nextItemsToSuppress);
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				GeneratedNetworkCode._ReadStructSyncListPickupIndex_VoidSuppressorBehavior(reader, this.nextItemsToSuppress);
			}
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040034A6 RID: 13478
		[SerializeField]
		private int maxPurchaseCount;

		// Token: 0x040034A7 RID: 13479
		[SerializeField]
		private int itemsSuppressedPerPurchase;

		// Token: 0x040034A8 RID: 13480
		[SerializeField]
		private float costMultiplierPerPurchase;

		// Token: 0x040034A9 RID: 13481
		[SerializeField]
		private Transform symbolTransform;

		// Token: 0x040034AA RID: 13482
		[SerializeField]
		private PurchaseInteraction purchaseInteraction;

		// Token: 0x040034AB RID: 13483
		[SerializeField]
		private GameObject effectPrefab;

		// Token: 0x040034AC RID: 13484
		[SerializeField]
		private Color effectColor;

		// Token: 0x040034AD RID: 13485
		[SerializeField]
		private PickupDisplay[] pickupDisplays;

		// Token: 0x040034AE RID: 13486
		[SerializeField]
		private int numItemsToReveal;

		// Token: 0x040034AF RID: 13487
		[SerializeField]
		private bool transformItems;

		// Token: 0x040034B0 RID: 13488
		[SerializeField]
		private string animatorSuppressName;

		// Token: 0x040034B1 RID: 13489
		[SerializeField]
		private string animatorIsAvailableName;

		// Token: 0x040034B2 RID: 13490
		[SerializeField]
		private string animatorIsPlayerNearbyName;

		// Token: 0x040034B3 RID: 13491
		[SerializeField]
		private Animator animator;

		// Token: 0x040034B4 RID: 13492
		[SerializeField]
		private NetworkAnimator networkAnimator;

		// Token: 0x040034B5 RID: 13493
		[SerializeField]
		private string revealedName;

		// Token: 0x040034B6 RID: 13494
		[SerializeField]
		private string revealedContext;

		// Token: 0x040034B7 RID: 13495
		[SerializeField]
		private float useRefreshDelay = 2f;

		// Token: 0x040034B8 RID: 13496
		[SerializeField]
		private float itemRefreshDelay = 1f;

		// Token: 0x040034B9 RID: 13497
		private int purchaseCount;

		// Token: 0x040034BA RID: 13498
		private float timeUntilUseRefresh;

		// Token: 0x040034BB RID: 13499
		private float timeUntilItemRefresh;

		// Token: 0x040034BC RID: 13500
		private Xoroshiro128Plus rng;

		// Token: 0x040034BD RID: 13501
		private VoidSuppressorBehavior.SyncListPickupIndex nextItemsToSuppress = new VoidSuppressorBehavior.SyncListPickupIndex();

		// Token: 0x040034BE RID: 13502
		private ItemIndex transformItemIndex;

		// Token: 0x040034BF RID: 13503
		private bool hasRevealed;

		// Token: 0x040034C0 RID: 13504
		private static int kListnextItemsToSuppress = 164192093;

		// Token: 0x020008F8 RID: 2296
		public class SyncListPickupIndex : SyncListStruct<PickupIndex>
		{
			// Token: 0x060033C8 RID: 13256 RVA: 0x000DA3F5 File Offset: 0x000D85F5
			public override void SerializeItem(NetworkWriter writer, PickupIndex item)
			{
				writer.WritePackedUInt32((uint)item.value);
			}

			// Token: 0x060033C9 RID: 13257 RVA: 0x000DA404 File Offset: 0x000D8604
			public override PickupIndex DeserializeItem(NetworkReader reader)
			{
				return new PickupIndex
				{
					value = (int)reader.ReadPackedUInt32()
				};
			}
		}
	}
}
