using System;
using System.Runtime.InteropServices;
using RoR2.Items;
using RoR2.Networking;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006FF RID: 1791
	public sealed class GenericPickupController : NetworkBehaviour, IInteractable, IDisplayNameProvider
	{
		// Token: 0x0600249E RID: 9374 RVA: 0x0009C2D1 File Offset: 0x0009A4D1
		private void SyncPickupIndex(PickupIndex newPickupIndex)
		{
			this.NetworkpickupIndex = newPickupIndex;
			this.UpdatePickupDisplay();
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0009C2E0 File Offset: 0x0009A4E0
		private void SyncRecycled(bool isRecycled)
		{
			this.NetworkRecycled = isRecycled;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0009C2EC File Offset: 0x0009A4EC
		[Server]
		public static void SendPickupMessage(CharacterMaster master, PickupIndex pickupIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.GenericPickupController::SendPickupMessage(RoR2.CharacterMaster,RoR2.PickupIndex)' called on client");
				return;
			}
			uint pickupQuantity = 1U;
			if (master.inventory)
			{
				PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
				ItemIndex itemIndex = (pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None;
				if (itemIndex != ItemIndex.None)
				{
					pickupQuantity = (uint)master.inventory.GetItemCount(itemIndex);
				}
			}
			GenericPickupController.PickupMessage msg = new GenericPickupController.PickupMessage
			{
				masterGameObject = master.gameObject,
				pickupIndex = pickupIndex,
				pickupQuantity = pickupQuantity
			};
			NetworkServer.SendByChannelToAll(57, msg, QosChannelIndex.chat.intVal);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x0009C374 File Offset: 0x0009A574
		[NetworkMessageHandler(msgType = 57, client = true)]
		private static void HandlePickupMessage(NetworkMessage netMsg)
		{
			GenericPickupController.PickupMessage pickupMessage = GenericPickupController.pickupMessageInstance;
			netMsg.ReadMessage<GenericPickupController.PickupMessage>(pickupMessage);
			GameObject masterGameObject = pickupMessage.masterGameObject;
			PickupIndex a = pickupMessage.pickupIndex;
			PickupDef pickupDef = PickupCatalog.GetPickupDef(a);
			uint pickupQuantity = pickupMessage.pickupQuantity;
			pickupMessage.Reset();
			if (!masterGameObject)
			{
				return;
			}
			CharacterMaster component = masterGameObject.GetComponent<CharacterMaster>();
			if (!component)
			{
				return;
			}
			PlayerCharacterMasterController component2 = component.GetComponent<PlayerCharacterMasterController>();
			if (component2)
			{
				NetworkUser networkUser = component2.networkUser;
				if (networkUser)
				{
					LocalUser localUser = networkUser.localUser;
					if (localUser != null)
					{
						localUser.userProfile.DiscoverPickup(a);
					}
				}
			}
			CharacterBody body = component.GetBody();
			body;
			ItemDef itemDef = ItemCatalog.GetItemDef((pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None);
			if (itemDef != null && itemDef.hidden)
			{
				return;
			}
			if (a != PickupIndex.none)
			{
				ItemIndex transformedItemIndex = ContagiousItemManager.GetTransformedItemIndex((itemDef != null) ? itemDef.itemIndex : ItemIndex.None);
				if (itemDef == null || transformedItemIndex == ItemIndex.None || component.inventory.GetItemCount(transformedItemIndex) <= 0)
				{
					CharacterMasterNotificationQueue.PushPickupNotification(component, a);
				}
			}
			Chat.AddPickupMessage(body, ((pickupDef != null) ? pickupDef.nameToken : null) ?? PickupCatalog.invalidPickupToken, (pickupDef != null) ? pickupDef.baseColor : Color.black, pickupQuantity);
			if (body)
			{
				Util.PlaySound("Play_UI_item_pickup", body.gameObject);
			}
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x0009C4D8 File Offset: 0x0009A6D8
		public void StartWaitTime()
		{
			this.waitStartTime = Run.FixedTimeStamp.now;
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0009C4E8 File Offset: 0x0009A6E8
		private void OnTriggerStay(Collider other)
		{
			if (NetworkServer.active && this.waitStartTime.timeSince >= this.waitDuration && !this.consumed)
			{
				CharacterBody component = other.GetComponent<CharacterBody>();
				if (component)
				{
					PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
					ItemIndex itemIndex = (pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None;
					if (itemIndex != ItemIndex.None)
					{
						ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(ItemCatalog.GetItemDef(itemIndex).tier);
						if (itemTierDef && (itemTierDef.pickupRules == ItemTierDef.PickupRules.ConfirmAll || (itemTierDef.pickupRules == ItemTierDef.PickupRules.ConfirmFirst && component.inventory && component.inventory.GetItemCount(itemIndex) <= 0)))
						{
							return;
						}
					}
					EquipmentIndex equipmentIndex = (pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None;
					if (equipmentIndex != EquipmentIndex.None)
					{
						if (EquipmentCatalog.GetEquipmentDef(equipmentIndex).isLunar)
						{
							return;
						}
						if (component.inventory && component.inventory.currentEquipmentIndex != EquipmentIndex.None)
						{
							return;
						}
					}
					if (pickupDef != null && pickupDef.coinValue != 0U)
					{
						return;
					}
					if (GenericPickupController.BodyHasPickupPermission(component))
					{
						this.AttemptGrant(component);
					}
				}
			}
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x0009C5F1 File Offset: 0x0009A7F1
		private static bool BodyHasPickupPermission(CharacterBody body)
		{
			return (body.masterObject ? body.masterObject.GetComponent<PlayerCharacterMasterController>() : null) && body.inventory;
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0009C622 File Offset: 0x0009A822
		public string GetContextString(Interactor activator)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
			return string.Format(Language.GetString(((pickupDef != null) ? pickupDef.interactContextToken : null) ?? string.Empty), this.GetDisplayName());
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0009C654 File Offset: 0x0009A854
		private void UpdatePickupDisplay()
		{
			if (!this.pickupDisplay)
			{
				return;
			}
			this.pickupDisplay.SetPickupIndex(this.pickupIndex, false);
			if (this.pickupDisplay.modelRenderer)
			{
				Highlight component = base.GetComponent<Highlight>();
				if (component)
				{
					component.targetRenderer = this.pickupDisplay.modelRenderer;
				}
			}
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0009C6B4 File Offset: 0x0009A8B4
		[Server]
		private void AttemptGrant(CharacterBody body)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.GenericPickupController::AttemptGrant(RoR2.CharacterBody)' called on client");
				return;
			}
			TeamComponent component = body.GetComponent<TeamComponent>();
			if (component && component.teamIndex == TeamIndex.Player)
			{
				PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
				if (body.inventory && pickupDef != null)
				{
					PickupDef.GrantContext grantContext = new PickupDef.GrantContext
					{
						body = body,
						controller = this
					};
					PickupDef.AttemptGrantDelegate attemptGrant = pickupDef.attemptGrant;
					if (attemptGrant != null)
					{
						attemptGrant(ref grantContext);
					}
					if (grantContext.shouldNotify)
					{
						GenericPickupController.SendPickupMessage(body.master, pickupDef.pickupIndex);
					}
					if (grantContext.shouldDestroy)
					{
						this.consumed = true;
						UnityEngine.Object.Destroy(base.gameObject);
					}
				}
			}
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0009C76D File Offset: 0x0009A96D
		private void Start()
		{
			this.waitStartTime = Run.FixedTimeStamp.now;
			this.consumed = false;
			this.UpdatePickupDisplay();
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0009C787 File Offset: 0x0009A987
		private void OnEnable()
		{
			InstanceTracker.Add<GenericPickupController>(this);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x0009C78F File Offset: 0x0009A98F
		private void OnDisable()
		{
			InstanceTracker.Remove<GenericPickupController>(this);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0009C798 File Offset: 0x0009A998
		public Interactability GetInteractability(Interactor activator)
		{
			if (!base.enabled)
			{
				return Interactability.Disabled;
			}
			if (this.waitStartTime.timeSince < this.waitDuration || this.consumed)
			{
				return Interactability.Disabled;
			}
			CharacterBody component = activator.GetComponent<CharacterBody>();
			if (!component)
			{
				return Interactability.Disabled;
			}
			if (!GenericPickupController.BodyHasPickupPermission(component))
			{
				return Interactability.Disabled;
			}
			return Interactability.Available;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0009C7E8 File Offset: 0x0009A9E8
		public void OnInteractionBegin(Interactor activator)
		{
			this.AttemptGrant(activator.GetComponent<CharacterBody>());
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool ShouldShowOnScanner()
		{
			return true;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0009C7F6 File Offset: 0x0009A9F6
		public string GetDisplayName()
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
			return Language.GetString(((pickupDef != null) ? pickupDef.nameToken : null) ?? PickupCatalog.invalidPickupToken);
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x0009C820 File Offset: 0x0009AA20
		public void SetPickupIndexFromString(string pickupString)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			PickupIndex networkpickupIndex = PickupCatalog.FindPickupIndex(pickupString);
			this.NetworkpickupIndex = networkpickupIndex;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x0009C843 File Offset: 0x0009AA43
		[SystemInitializer(new Type[]
		{

		})]
		private static void Init()
		{
			GenericPickupController.pickupPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/GenericPickup");
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x0009C854 File Offset: 0x0009AA54
		public static GenericPickupController CreatePickup(in GenericPickupController.CreatePickupInfo createPickupInfo)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(createPickupInfo.prefabOverride ?? GenericPickupController.pickupPrefab, createPickupInfo.position, createPickupInfo.rotation);
			GenericPickupController component = gameObject.GetComponent<GenericPickupController>();
			if (component)
			{
				GenericPickupController genericPickupController = component;
				GenericPickupController.CreatePickupInfo createPickupInfo2 = createPickupInfo;
				genericPickupController.NetworkpickupIndex = createPickupInfo2.pickupIndex;
			}
			PickupIndexNetworker component2 = gameObject.GetComponent<PickupIndexNetworker>();
			if (component2)
			{
				PickupIndexNetworker pickupIndexNetworker = component2;
				GenericPickupController.CreatePickupInfo createPickupInfo2 = createPickupInfo;
				pickupIndexNetworker.NetworkpickupIndex = createPickupInfo2.pickupIndex;
			}
			PickupPickerController component3 = gameObject.GetComponent<PickupPickerController>();
			if (component3 && createPickupInfo.pickerOptions != null)
			{
				component3.SetOptionsServer(createPickupInfo.pickerOptions);
			}
			NetworkServer.Spawn(gameObject);
			return component;
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x0009C8F0 File Offset: 0x0009AAF0
		[ContextMenu("Print Pickup Index")]
		private void PrintPickupIndex()
		{
			string format = "pickupIndex={0}";
			object[] array = new object[1];
			int num = 0;
			PickupDef pickupDef = PickupCatalog.GetPickupDef(this.pickupIndex);
			array[num] = (((pickupDef != null) ? pickupDef.internalName : null) ?? "Invalid");
			Debug.LogFormat(format, array);
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x0009C950 File Offset: 0x0009AB50
		// (set) Token: 0x060024B8 RID: 9400 RVA: 0x0009C963 File Offset: 0x0009AB63
		public PickupIndex NetworkpickupIndex
		{
			get
			{
				return this.pickupIndex;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SyncPickupIndex(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<PickupIndex>(value, ref this.pickupIndex, 1U);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x0009C9A4 File Offset: 0x0009ABA4
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x0009C9B7 File Offset: 0x0009ABB7
		public bool NetworkRecycled
		{
			get
			{
				return this.Recycled;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SyncRecycled(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this.Recycled, 2U);
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0009C9F8 File Offset: 0x0009ABF8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.pickupIndex);
				writer.Write(this.Recycled);
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
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.pickupIndex);
			}
			if ((base.syncVarDirtyBits & 2U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.Recycled);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0009CAA4 File Offset: 0x0009ACA4
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.pickupIndex = GeneratedNetworkCode._ReadPickupIndex_None(reader);
				this.Recycled = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.SyncPickupIndex(GeneratedNetworkCode._ReadPickupIndex_None(reader));
			}
			if ((num & 2) != 0)
			{
				this.SyncRecycled(reader.ReadBoolean());
			}
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040028B5 RID: 10421
		public PickupDisplay pickupDisplay;

		// Token: 0x040028B6 RID: 10422
		[SyncVar(hook = "SyncPickupIndex")]
		public PickupIndex pickupIndex = PickupIndex.none;

		// Token: 0x040028B7 RID: 10423
		[SyncVar(hook = "SyncRecycled")]
		public bool Recycled;

		// Token: 0x040028B8 RID: 10424
		public bool selfDestructIfPickupIndexIsNotIdeal;

		// Token: 0x040028B9 RID: 10425
		public SerializablePickupIndex idealPickupIndex;

		// Token: 0x040028BA RID: 10426
		private static readonly GenericPickupController.PickupMessage pickupMessageInstance = new GenericPickupController.PickupMessage();

		// Token: 0x040028BB RID: 10427
		public float waitDuration = 0.5f;

		// Token: 0x040028BC RID: 10428
		private Run.FixedTimeStamp waitStartTime;

		// Token: 0x040028BD RID: 10429
		private bool consumed;

		// Token: 0x040028BE RID: 10430
		public const string pickupSoundString = "Play_UI_item_pickup";

		// Token: 0x040028BF RID: 10431
		private static GameObject pickupPrefab;

		// Token: 0x02000700 RID: 1792
		private class PickupMessage : MessageBase
		{
			// Token: 0x060024BE RID: 9406 RVA: 0x0009CB0A File Offset: 0x0009AD0A
			public void Reset()
			{
				this.masterGameObject = null;
				this.pickupIndex = PickupIndex.none;
				this.pickupQuantity = 0U;
			}

			// Token: 0x060024C0 RID: 9408 RVA: 0x0009CB25 File Offset: 0x0009AD25
			public override void Serialize(NetworkWriter writer)
			{
				writer.Write(this.masterGameObject);
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.pickupIndex);
				writer.WritePackedUInt32(this.pickupQuantity);
			}

			// Token: 0x060024C1 RID: 9409 RVA: 0x0009CB4B File Offset: 0x0009AD4B
			public override void Deserialize(NetworkReader reader)
			{
				this.masterGameObject = reader.ReadGameObject();
				this.pickupIndex = GeneratedNetworkCode._ReadPickupIndex_None(reader);
				this.pickupQuantity = reader.ReadPackedUInt32();
			}

			// Token: 0x040028C0 RID: 10432
			public GameObject masterGameObject;

			// Token: 0x040028C1 RID: 10433
			public PickupIndex pickupIndex;

			// Token: 0x040028C2 RID: 10434
			public uint pickupQuantity;
		}

		// Token: 0x02000701 RID: 1793
		public struct CreatePickupInfo
		{
			// Token: 0x17000310 RID: 784
			// (get) Token: 0x060024C2 RID: 9410 RVA: 0x0009CB71 File Offset: 0x0009AD71
			// (set) Token: 0x060024C3 RID: 9411 RVA: 0x0009CB91 File Offset: 0x0009AD91
			public PickupIndex pickupIndex
			{
				get
				{
					if (this._pickupIndex == null)
					{
						return PickupIndex.none;
					}
					return this._pickupIndex.Value;
				}
				set
				{
					this._pickupIndex = new PickupIndex?(value);
				}
			}

			// Token: 0x040028C3 RID: 10435
			public Vector3 position;

			// Token: 0x040028C4 RID: 10436
			public Quaternion rotation;

			// Token: 0x040028C5 RID: 10437
			private PickupIndex? _pickupIndex;

			// Token: 0x040028C6 RID: 10438
			public PickupPickerController.Option[] pickerOptions;

			// Token: 0x040028C7 RID: 10439
			public GameObject prefabOverride;
		}
	}
}
