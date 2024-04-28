using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using RoR2.Stats;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000837 RID: 2103
	[RequireComponent(typeof(Highlight))]
	public sealed class PurchaseInteraction : NetworkBehaviour, IInteractable, IHologramContentProvider, IDisplayNameProvider
	{
		// Token: 0x06002DC4 RID: 11716 RVA: 0x000C2E10 File Offset: 0x000C1010
		private void Awake()
		{
			if (NetworkServer.active)
			{
				if (this.automaticallyScaleCostWithDifficulty)
				{
					this.Networkcost = Run.instance.GetDifficultyScaledCost(this.cost);
				}
				this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
			}
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000C2E5C File Offset: 0x000C105C
		[Server]
		public void SetAvailable(bool newAvailable)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PurchaseInteraction::SetAvailable(System.Boolean)' called on client");
				return;
			}
			this.Networkavailable = newAvailable;
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000C2E7A File Offset: 0x000C107A
		[Server]
		public void SetUnavailableTemporarily(float time)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PurchaseInteraction::SetUnavailableTemporarily(System.Single)' called on client");
				return;
			}
			this.Networkavailable = false;
			base.Invoke("SetAvailableTrue", time);
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000C2EA4 File Offset: 0x000C10A4
		private void SetAvailableTrue()
		{
			this.Networkavailable = true;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000C2EAD File Offset: 0x000C10AD
		public string GetDisplayName()
		{
			return Language.GetString(this.displayNameToken);
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000C2EBC File Offset: 0x000C10BC
		private static bool ActivatorHasUnlockable(Interactor activator, string unlockableName)
		{
			NetworkUser networkUser = Util.LookUpBodyNetworkUser(activator.gameObject);
			if (networkUser)
			{
				LocalUser localUser = networkUser.localUser;
				if (localUser != null)
				{
					return localUser.userProfile.HasUnlockable(unlockableName);
				}
			}
			return true;
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000C2EF8 File Offset: 0x000C10F8
		public string GetContextString(Interactor activator)
		{
			PurchaseInteraction.sharedStringBuilder.Clear();
			PurchaseInteraction.sharedStringBuilder.Append(Language.GetString(this.contextToken));
			if (this.costType != CostTypeIndex.None)
			{
				PurchaseInteraction.sharedStringBuilder.Append(" <nobr>(");
				CostTypeCatalog.GetCostTypeDef(this.costType).BuildCostStringStyled(this.cost, PurchaseInteraction.sharedStringBuilder, false, true);
				PurchaseInteraction.sharedStringBuilder.Append(")</nobr>");
			}
			return PurchaseInteraction.sharedStringBuilder.ToString();
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000C2F78 File Offset: 0x000C1178
		public Interactability GetInteractability(Interactor activator)
		{
			if (!string.IsNullOrEmpty(this.requiredUnlockable) && !PurchaseInteraction.ActivatorHasUnlockable(activator, this.requiredUnlockable))
			{
				return Interactability.Disabled;
			}
			if (!this.available || this.lockGameObject)
			{
				return Interactability.Disabled;
			}
			if (!this.CanBeAffordedByInteractor(activator))
			{
				return Interactability.ConditionsNotMet;
			}
			return Interactability.Available;
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000C2FC5 File Offset: 0x000C11C5
		public bool CanBeAffordedByInteractor(Interactor activator)
		{
			return CostTypeCatalog.GetCostTypeDef(this.costType).IsAffordable(this.cost, activator);
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06002DCD RID: 11725 RVA: 0x000C2FE0 File Offset: 0x000C11E0
		// (remove) Token: 0x06002DCE RID: 11726 RVA: 0x000C3014 File Offset: 0x000C1214
		public static event Action<PurchaseInteraction, Interactor> onItemSpentOnPurchase;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06002DCF RID: 11727 RVA: 0x000C3048 File Offset: 0x000C1248
		// (remove) Token: 0x06002DD0 RID: 11728 RVA: 0x000C307C File Offset: 0x000C127C
		public static event Action<PurchaseInteraction, Interactor, EquipmentIndex> onEquipmentSpentOnPurchase;

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000C30B0 File Offset: 0x000C12B0
		public void OnInteractionBegin(Interactor activator)
		{
			if (!this.CanBeAffordedByInteractor(activator))
			{
				return;
			}
			CharacterBody component = activator.GetComponent<CharacterBody>();
			CostTypeDef costTypeDef = CostTypeCatalog.GetCostTypeDef(this.costType);
			ItemIndex itemIndex = ItemIndex.None;
			ShopTerminalBehavior component2 = base.GetComponent<ShopTerminalBehavior>();
			if (component2)
			{
				PickupDef pickupDef = PickupCatalog.GetPickupDef(component2.CurrentPickupIndex());
				itemIndex = ((pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None);
			}
			CostTypeDef.PayCostResults payCostResults = costTypeDef.PayCost(this.cost, activator, base.gameObject, this.rng, itemIndex);
			foreach (ItemIndex itemIndex2 in payCostResults.itemsTaken)
			{
				PurchaseInteraction.CreateItemTakenOrb(component.corePosition, base.gameObject, itemIndex2);
				if (itemIndex2 != itemIndex)
				{
					Action<PurchaseInteraction, Interactor> action = PurchaseInteraction.onItemSpentOnPurchase;
					if (action != null)
					{
						action(this, activator);
					}
				}
			}
			foreach (EquipmentIndex arg in payCostResults.equipmentTaken)
			{
				Action<PurchaseInteraction, Interactor, EquipmentIndex> action2 = PurchaseInteraction.onEquipmentSpentOnPurchase;
				if (action2 != null)
				{
					action2(this, activator, arg);
				}
			}
			IEnumerable<StatDef> statDefsToIncrement = this.purchaseStatNames.Select(new Func<string, StatDef>(StatDef.Find));
			StatManager.OnPurchase<IEnumerable<StatDef>>(component, this.costType, statDefsToIncrement);
			this.onPurchase.Invoke(activator);
			this.lastActivator = activator;
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000C3214 File Offset: 0x000C1414
		[Server]
		public static void CreateItemTakenOrb(Vector3 effectOrigin, GameObject targetObject, ItemIndex itemIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PurchaseInteraction::CreateItemTakenOrb(UnityEngine.Vector3,UnityEngine.GameObject,RoR2.ItemIndex)' called on client");
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

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000C326F File Offset: 0x000C146F
		public bool ShouldDisplayHologram(GameObject viewer)
		{
			return this.available;
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x0007647A File Offset: 0x0007467A
		public GameObject GetHologramContentPrefab()
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/CostHologramContent");
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000C3278 File Offset: 0x000C1478
		public void UpdateHologramContent(GameObject hologramContentObject)
		{
			CostHologramContent component = hologramContentObject.GetComponent<CostHologramContent>();
			if (component)
			{
				component.displayValue = this.cost;
				component.costType = this.costType;
			}
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000C326F File Offset: 0x000C146F
		public bool ShouldShowOnScanner()
		{
			return this.available;
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000C32AC File Offset: 0x000C14AC
		public void ScaleCost(float scalar)
		{
			this.Networkcost = (int)Mathf.Floor((float)this.cost * scalar);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000C32C3 File Offset: 0x000C14C3
		private void OnEnable()
		{
			InstanceTracker.Add<PurchaseInteraction>(this);
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x000C32CB File Offset: 0x000C14CB
		private void OnDisable()
		{
			InstanceTracker.Remove<PurchaseInteraction>(this);
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x000C32D3 File Offset: 0x000C14D3
		[RuntimeInitializeOnLoadMethod]
		private static void Init()
		{
			TeleporterInteraction.onTeleporterBeginChargingGlobal += PurchaseInteraction.OnTeleporterBeginCharging;
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x000C32E8 File Offset: 0x000C14E8
		private static void OnTeleporterBeginCharging(TeleporterInteraction teleporterInteraction)
		{
			if (NetworkServer.active)
			{
				foreach (PurchaseInteraction purchaseInteraction in InstanceTracker.GetInstancesList<PurchaseInteraction>())
				{
					if (purchaseInteraction.setUnavailableOnTeleporterActivated)
					{
						purchaseInteraction.SetAvailable(false);
						purchaseInteraction.CancelInvoke("SetUnavailableTemporarily");
					}
				}
			}
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06002DE0 RID: 11744 RVA: 0x000C337C File Offset: 0x000C157C
		// (set) Token: 0x06002DE1 RID: 11745 RVA: 0x000C338F File Offset: 0x000C158F
		public string NetworkdisplayNameToken
		{
			get
			{
				return this.displayNameToken;
			}
			[param: In]
			set
			{
				base.SetSyncVar<string>(value, ref this.displayNameToken, 1U);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x000C33A4 File Offset: 0x000C15A4
		// (set) Token: 0x06002DE3 RID: 11747 RVA: 0x000C33B7 File Offset: 0x000C15B7
		public string NetworkcontextToken
		{
			get
			{
				return this.contextToken;
			}
			[param: In]
			set
			{
				base.SetSyncVar<string>(value, ref this.contextToken, 2U);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x000C33CC File Offset: 0x000C15CC
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x000C33DF File Offset: 0x000C15DF
		public bool Networkavailable
		{
			get
			{
				return this.available;
			}
			[param: In]
			set
			{
				base.SetSyncVar<bool>(value, ref this.available, 4U);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x000C33F4 File Offset: 0x000C15F4
		// (set) Token: 0x06002DE7 RID: 11751 RVA: 0x000C3407 File Offset: 0x000C1607
		public int Networkcost
		{
			get
			{
				return this.cost;
			}
			[param: In]
			set
			{
				base.SetSyncVar<int>(value, ref this.cost, 8U);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x000C341C File Offset: 0x000C161C
		// (set) Token: 0x06002DE9 RID: 11753 RVA: 0x000C342F File Offset: 0x000C162F
		public GameObject NetworklockGameObject
		{
			get
			{
				return this.lockGameObject;
			}
			[param: In]
			set
			{
				base.SetSyncVarGameObject(value, ref this.lockGameObject, 16U, ref this.___lockGameObjectNetId);
			}
		}

		// Token: 0x06002DEA RID: 11754 RVA: 0x000C344C File Offset: 0x000C164C
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				writer.Write(this.displayNameToken);
				writer.Write(this.contextToken);
				writer.Write(this.available);
				writer.WritePackedUInt32((uint)this.cost);
				writer.Write(this.lockGameObject);
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
				writer.Write(this.displayNameToken);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.contextToken);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.available);
			}
			if ((base.syncVarDirtyBits & 8U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.WritePackedUInt32((uint)this.cost);
			}
			if ((base.syncVarDirtyBits & 16U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.lockGameObject);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06002DEB RID: 11755 RVA: 0x000C35B4 File Offset: 0x000C17B4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.displayNameToken = reader.ReadString();
				this.contextToken = reader.ReadString();
				this.available = reader.ReadBoolean();
				this.cost = (int)reader.ReadPackedUInt32();
				this.___lockGameObjectNetId = reader.ReadNetworkId();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.displayNameToken = reader.ReadString();
			}
			if ((num & 2) != 0)
			{
				this.contextToken = reader.ReadString();
			}
			if ((num & 4) != 0)
			{
				this.available = reader.ReadBoolean();
			}
			if ((num & 8) != 0)
			{
				this.cost = (int)reader.ReadPackedUInt32();
			}
			if ((num & 16) != 0)
			{
				this.lockGameObject = reader.ReadGameObject();
			}
		}

		// Token: 0x06002DEC RID: 11756 RVA: 0x000C3689 File Offset: 0x000C1889
		public override void PreStartClient()
		{
			if (!this.___lockGameObjectNetId.IsEmpty())
			{
				this.NetworklockGameObject = ClientScene.FindLocalObject(this.___lockGameObjectNetId);
			}
		}

		// Token: 0x04002FC0 RID: 12224
		[SyncVar]
		public string displayNameToken;

		// Token: 0x04002FC1 RID: 12225
		[SyncVar]
		public string contextToken;

		// Token: 0x04002FC2 RID: 12226
		public CostTypeIndex costType;

		// Token: 0x04002FC3 RID: 12227
		[SyncVar]
		public bool available = true;

		// Token: 0x04002FC4 RID: 12228
		[SyncVar]
		public int cost;

		// Token: 0x04002FC5 RID: 12229
		public bool automaticallyScaleCostWithDifficulty;

		// Token: 0x04002FC6 RID: 12230
		[Tooltip("The unlockable that a player must have to be able to interact with this terminal.")]
		public string requiredUnlockable = "";

		// Token: 0x04002FC7 RID: 12231
		public bool ignoreSpherecastForInteractability;

		// Token: 0x04002FC8 RID: 12232
		public string[] purchaseStatNames;

		// Token: 0x04002FC9 RID: 12233
		public bool setUnavailableOnTeleporterActivated;

		// Token: 0x04002FCA RID: 12234
		public bool isShrine;

		// Token: 0x04002FCB RID: 12235
		public bool isGoldShrine;

		// Token: 0x04002FCC RID: 12236
		[HideInInspector]
		public Interactor lastActivator;

		// Token: 0x04002FCD RID: 12237
		[SyncVar]
		public GameObject lockGameObject;

		// Token: 0x04002FCE RID: 12238
		private Xoroshiro128Plus rng;

		// Token: 0x04002FCF RID: 12239
		private static readonly StringBuilder sharedStringBuilder = new StringBuilder();

		// Token: 0x04002FD2 RID: 12242
		public PurchaseEvent onPurchase;

		// Token: 0x04002FD3 RID: 12243
		private NetworkInstanceId ___lockGameObjectNetId;
	}
}
