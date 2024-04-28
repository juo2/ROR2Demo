using System;
using System.Collections.Generic;
using System.Linq;
using RoR2.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000803 RID: 2051
	[RequireComponent(typeof(NetworkUIPromptController))]
	public class PickupPickerController : NetworkBehaviour, IInteractable
	{
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06002C40 RID: 11328 RVA: 0x000BD2E1 File Offset: 0x000BB4E1
		// (set) Token: 0x06002C41 RID: 11329 RVA: 0x000BD2E9 File Offset: 0x000BB4E9
		public bool available { get; private set; } = true;

		// Token: 0x06002C42 RID: 11330 RVA: 0x000BD2F4 File Offset: 0x000BB4F4
		private void Awake()
		{
			this.networkUIPromptController = base.GetComponent<NetworkUIPromptController>();
			if (NetworkClient.active)
			{
				this.networkUIPromptController.onDisplayBegin += this.OnDisplayBegin;
				this.networkUIPromptController.onDisplayEnd += this.OnDisplayEnd;
			}
			if (NetworkServer.active)
			{
				this.networkUIPromptController.messageFromClientHandler = new Action<NetworkReader>(this.HandleClientMessage);
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000BD360 File Offset: 0x000BB560
		private void OnEnable()
		{
			InstanceTracker.Add<PickupPickerController>(this);
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000BD368 File Offset: 0x000BB568
		private void OnDisable()
		{
			InstanceTracker.Remove<PickupPickerController>(this);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000BD370 File Offset: 0x000BB570
		private void HandleClientMessage(NetworkReader reader)
		{
			byte b = reader.ReadByte();
			if (b == 0)
			{
				int choiceIndex = reader.ReadInt32();
				this.HandlePickupSelected(choiceIndex);
				return;
			}
			if (b != 1)
			{
				return;
			}
			this.networkUIPromptController.SetParticipantMaster(null);
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000BD3A7 File Offset: 0x000BB5A7
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.FixedUpdateServer();
			}
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000BD3B8 File Offset: 0x000BB5B8
		private void FixedUpdateServer()
		{
			CharacterMaster currentParticipantMaster = this.networkUIPromptController.currentParticipantMaster;
			if (currentParticipantMaster)
			{
				CharacterBody body = currentParticipantMaster.GetBody();
				if (!body || (body.inputBank.aimOrigin - base.transform.position).sqrMagnitude > this.cutoffDistance * this.cutoffDistance)
				{
					this.networkUIPromptController.SetParticipantMaster(null);
				}
			}
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000BD428 File Offset: 0x000BB628
		private void OnPanelDestroyed(OnDestroyCallback onDestroyCallback)
		{
			NetworkWriter networkWriter = this.networkUIPromptController.BeginMessageToServer();
			networkWriter.Write(1);
			this.networkUIPromptController.FinishMessageToServer(networkWriter);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000BD454 File Offset: 0x000BB654
		private void OnDisplayBegin(NetworkUIPromptController networkUIPromptController, LocalUser localUser, CameraRigController cameraRigController)
		{
			this.panelInstance = UnityEngine.Object.Instantiate<GameObject>(this.panelPrefab, cameraRigController.hud.mainContainer.transform);
			this.panelInstanceController = this.panelInstance.GetComponent<PickupPickerPanel>();
			this.panelInstanceController.pickerController = this;
			this.panelInstanceController.SetPickupOptions(this.options);
			OnDestroyCallback.AddCallback(this.panelInstance, new Action<OnDestroyCallback>(this.OnPanelDestroyed));
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000BD4C8 File Offset: 0x000BB6C8
		private void OnDisplayEnd(NetworkUIPromptController networkUIPromptController, LocalUser localUser, CameraRigController cameraRigController)
		{
			UnityEngine.Object.Destroy(this.panelInstance);
			this.panelInstance = null;
			this.panelInstanceController = null;
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000BD4E3 File Offset: 0x000BB6E3
		[Server]
		public void SetAvailable(bool newAvailable)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PickupPickerController::SetAvailable(System.Boolean)' called on client");
				return;
			}
			this.available = newAvailable;
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000BD504 File Offset: 0x000BB704
		public void SubmitChoice(int choiceIndex)
		{
			if (!NetworkServer.active)
			{
				NetworkWriter networkWriter = this.networkUIPromptController.BeginMessageToServer();
				networkWriter.Write(0);
				networkWriter.Write(choiceIndex);
				this.networkUIPromptController.FinishMessageToServer(networkWriter);
				return;
			}
			this.HandlePickupSelected(choiceIndex);
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000BD548 File Offset: 0x000BB748
		public bool IsChoiceAvailable(PickupIndex choice)
		{
			for (int i = 0; i < this.options.Length; i++)
			{
				ref PickupPickerController.Option ptr = ref this.options[i];
				if (ptr.pickupIndex == choice)
				{
					return ptr.available;
				}
			}
			return false;
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000BD58C File Offset: 0x000BB78C
		[Server]
		private void HandlePickupSelected(int choiceIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PickupPickerController::HandlePickupSelected(System.Int32)' called on client");
				return;
			}
			if ((ulong)choiceIndex >= (ulong)((long)this.options.Length))
			{
				return;
			}
			ref PickupPickerController.Option ptr = ref this.options[choiceIndex];
			if (!ptr.available)
			{
				return;
			}
			PickupPickerController.PickupIndexUnityEvent pickupIndexUnityEvent = this.onPickupSelected;
			if (pickupIndexUnityEvent == null)
			{
				return;
			}
			pickupIndexUnityEvent.Invoke(ptr.pickupIndex.value);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000BD5ED File Offset: 0x000BB7ED
		[Server]
		public void SetOptionsServer(PickupPickerController.Option[] newOptions)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PickupPickerController::SetOptionsServer(RoR2.PickupPickerController/Option[])' called on client");
				return;
			}
			this.SetOptionsInternal(newOptions);
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000BD60C File Offset: 0x000BB80C
		private void SetOptionsInternal(PickupPickerController.Option[] newOptions)
		{
			Array.Resize<PickupPickerController.Option>(ref this.options, newOptions.Length);
			Array.Copy(newOptions, this.options, newOptions.Length);
			if (this.panelInstanceController)
			{
				this.panelInstanceController.SetPickupOptions(this.options);
			}
			if (NetworkServer.active)
			{
				base.SetDirtyBit(PickupPickerController.optionsDirtyBit);
			}
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000BD668 File Offset: 0x000BB868
		[Server]
		public void SetTestOptions()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PickupPickerController::SetTestOptions()' called on client");
				return;
			}
			this.SetOptionsServer((from itemIndex in ItemCatalog.allItems
			select ItemCatalog.GetItemDef(itemIndex) into itemDef
			where itemDef.tier == ItemTier.Tier2
			select new PickupPickerController.Option
			{
				pickupIndex = PickupCatalog.FindPickupIndex(itemDef.itemIndex),
				available = true
			}).ToArray<PickupPickerController.Option>());
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000BD70C File Offset: 0x000BB90C
		public static PickupPickerController.Option[] GenerateOptionsFromDropTable(int numOptions, PickupDropTable dropTable, Xoroshiro128Plus rng)
		{
			PickupIndex[] array = dropTable.GenerateUniqueDrops(numOptions, rng);
			PickupPickerController.Option[] array2 = new PickupPickerController.Option[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new PickupPickerController.Option
				{
					available = true,
					pickupIndex = array[i]
				};
			}
			return array2;
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000BD760 File Offset: 0x000BB960
		private static PickupPickerController.Option[] GetOptionsFromPickupIndex(PickupIndex pickupIndex)
		{
			PickupIndex[] groupFromPickupIndex = PickupTransmutationManager.GetGroupFromPickupIndex(pickupIndex);
			if (groupFromPickupIndex == null)
			{
				return new PickupPickerController.Option[]
				{
					new PickupPickerController.Option
					{
						available = true,
						pickupIndex = pickupIndex
					}
				};
			}
			PickupPickerController.Option[] array = new PickupPickerController.Option[groupFromPickupIndex.Length];
			for (int i = 0; i < groupFromPickupIndex.Length; i++)
			{
				PickupIndex pickupIndex2 = groupFromPickupIndex[i];
				array[i] = new PickupPickerController.Option
				{
					available = Run.instance.IsPickupAvailable(pickupIndex2),
					pickupIndex = pickupIndex2
				};
			}
			return array;
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000BD7EC File Offset: 0x000BB9EC
		public static PickupPickerController.Option[] GenerateOptionsFromArray(PickupIndex[] drops)
		{
			PickupPickerController.Option[] array = new PickupPickerController.Option[drops.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new PickupPickerController.Option
				{
					available = true,
					pickupIndex = drops[i]
				};
			}
			return array;
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000BD837 File Offset: 0x000BBA37
		[Server]
		public void SetOptionsFromPickupForCommandArtifact(PickupIndex pickupIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PickupPickerController::SetOptionsFromPickupForCommandArtifact(RoR2.PickupIndex)' called on client");
				return;
			}
			this.SetOptionsServer(PickupPickerController.GetOptionsFromPickupIndex(pickupIndex));
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000BD85A File Offset: 0x000BBA5A
		[Server]
		public void CreatePickup(int intPickupIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PickupPickerController::CreatePickup(System.Int32)' called on client");
				return;
			}
			this.CreatePickup(new PickupIndex(intPickupIndex));
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000BD880 File Offset: 0x000BBA80
		[Server]
		public void CreatePickup(PickupIndex pickupIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.PickupPickerController::CreatePickup(RoR2.PickupIndex)' called on client");
				return;
			}
			GenericPickupController.CreatePickupInfo createPickupInfo = new GenericPickupController.CreatePickupInfo
			{
				rotation = Quaternion.identity,
				position = base.transform.position,
				pickupIndex = pickupIndex
			};
			GenericPickupController.CreatePickup(createPickupInfo);
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000BD8DC File Offset: 0x000BBADC
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint syncVarDirtyBits = base.syncVarDirtyBits;
			if (initialState)
			{
				syncVarDirtyBits = PickupPickerController.allDirtyBits;
			}
			bool flag = (syncVarDirtyBits & PickupPickerController.optionsDirtyBit) > 0U;
			writer.WritePackedUInt32(syncVarDirtyBits);
			if (flag)
			{
				writer.WritePackedUInt32((uint)this.options.Length);
				for (int i = 0; i < this.options.Length; i++)
				{
					ref PickupPickerController.Option ptr = ref this.options[i];
					writer.Write(ptr.pickupIndex);
					writer.Write(ptr.available);
				}
			}
			return syncVarDirtyBits > 0U;
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000BD958 File Offset: 0x000BBB58
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if ((reader.ReadPackedUInt32() & PickupPickerController.optionsDirtyBit) > 0U)
			{
				PickupPickerController.Option[] array = new PickupPickerController.Option[reader.ReadPackedUInt32()];
				for (int i = 0; i < array.Length; i++)
				{
					PickupPickerController.Option[] array2 = array;
					int num = i;
					array2[num].pickupIndex = reader.ReadPickupIndex();
					array2[num].available = reader.ReadBoolean();
				}
				this.SetOptionsInternal(array);
			}
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000BD9B5 File Offset: 0x000BBBB5
		public string GetContextString(Interactor activator)
		{
			return Language.GetString(this.contextString);
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000BD9C2 File Offset: 0x000BBBC2
		public Interactability GetInteractability(Interactor activator)
		{
			if (this.networkUIPromptController.inUse)
			{
				return Interactability.ConditionsNotMet;
			}
			if (!this.available)
			{
				return Interactability.Disabled;
			}
			return Interactability.Available;
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000BD9DE File Offset: 0x000BBBDE
		public void OnInteractionBegin(Interactor activator)
		{
			this.onServerInteractionBegin.Invoke(activator);
			this.networkUIPromptController.SetParticipantMasterFromInteractor(activator);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000BD9F8 File Offset: 0x000BBBF8
		public void SetOptionsFromInteractor(Interactor activator)
		{
			if (!activator)
			{
				Debug.Log("No activator.");
				return;
			}
			CharacterBody component = activator.GetComponent<CharacterBody>();
			if (!component)
			{
				Debug.Log("No body.");
				return;
			}
			Inventory inventory = component.inventory;
			if (!inventory)
			{
				Debug.Log("No inventory.");
				return;
			}
			List<PickupPickerController.Option> list = new List<PickupPickerController.Option>();
			for (int i = 0; i < inventory.itemAcquisitionOrder.Count; i++)
			{
				ItemIndex itemIndex = inventory.itemAcquisitionOrder[i];
				ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
				ItemTierDef itemTierDef = ItemTierCatalog.GetItemTierDef(itemDef.tier);
				PickupIndex pickupIndex = PickupCatalog.FindPickupIndex(itemIndex);
				if ((!itemTierDef || itemTierDef.canScrap) && itemDef.canRemove && !itemDef.hidden && itemDef.DoesNotContainTag(ItemTag.Scrap))
				{
					list.Add(new PickupPickerController.Option
					{
						available = true,
						pickupIndex = pickupIndex
					});
				}
			}
			Debug.Log(list.Count);
			this.SetOptionsServer(list.ToArray());
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public bool ShouldIgnoreSpherecastForInteractibility(Interactor activator)
		{
			return false;
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public bool ShouldShowOnScanner()
		{
			return true;
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002EA7 RID: 11943
		public GameObject panelPrefab;

		// Token: 0x04002EA8 RID: 11944
		public PickupPickerController.PickupIndexUnityEvent onPickupSelected;

		// Token: 0x04002EA9 RID: 11945
		public GenericInteraction.InteractorUnityEvent onServerInteractionBegin;

		// Token: 0x04002EAA RID: 11946
		public float cutoffDistance;

		// Token: 0x04002EAC RID: 11948
		public string contextString = "";

		// Token: 0x04002EAD RID: 11949
		private NetworkUIPromptController networkUIPromptController;

		// Token: 0x04002EAE RID: 11950
		private const byte msgSubmit = 0;

		// Token: 0x04002EAF RID: 11951
		private const byte msgCancel = 1;

		// Token: 0x04002EB0 RID: 11952
		private GameObject panelInstance;

		// Token: 0x04002EB1 RID: 11953
		private PickupPickerPanel panelInstanceController;

		// Token: 0x04002EB2 RID: 11954
		private PickupPickerController.Option[] options = Array.Empty<PickupPickerController.Option>();

		// Token: 0x04002EB3 RID: 11955
		private static readonly uint optionsDirtyBit = 1U;

		// Token: 0x04002EB4 RID: 11956
		private static readonly uint allDirtyBits = PickupPickerController.optionsDirtyBit;

		// Token: 0x02000804 RID: 2052
		public struct Option
		{
			// Token: 0x04002EB5 RID: 11957
			public PickupIndex pickupIndex;

			// Token: 0x04002EB6 RID: 11958
			public bool available;
		}

		// Token: 0x02000805 RID: 2053
		[Serializable]
		public class PickupIndexUnityEvent : UnityEvent<int>
		{
		}
	}
}
