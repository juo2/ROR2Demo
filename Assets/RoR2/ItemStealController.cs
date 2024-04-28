using System;
using System.Collections.Generic;
using HG;
using RoR2.Orbs;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000789 RID: 1929
	public class ItemStealController : NetworkBehaviour
	{
		// Token: 0x14000068 RID: 104
		// (add) Token: 0x06002869 RID: 10345 RVA: 0x000AF84C File Offset: 0x000ADA4C
		// (remove) Token: 0x0600286A RID: 10346 RVA: 0x000AF884 File Offset: 0x000ADA84
		public event Action onStealBeginClient;

		// Token: 0x14000069 RID: 105
		// (add) Token: 0x0600286B RID: 10347 RVA: 0x000AF8BC File Offset: 0x000ADABC
		// (remove) Token: 0x0600286C RID: 10348 RVA: 0x000AF8F4 File Offset: 0x000ADAF4
		public event Action onStealFinishClient;

		// Token: 0x1400006A RID: 106
		// (add) Token: 0x0600286D RID: 10349 RVA: 0x000AF92C File Offset: 0x000ADB2C
		// (remove) Token: 0x0600286E RID: 10350 RVA: 0x000AF964 File Offset: 0x000ADB64
		public event Action onLendingBeginClient;

		// Token: 0x1400006B RID: 107
		// (add) Token: 0x0600286F RID: 10351 RVA: 0x000AF99C File Offset: 0x000ADB9C
		// (remove) Token: 0x06002870 RID: 10352 RVA: 0x000AF9D4 File Offset: 0x000ADBD4
		public event Action onLendingFinishClient;

		// Token: 0x06002871 RID: 10353 RVA: 0x000AFA09 File Offset: 0x000ADC09
		private void Awake()
		{
			this.networkIdentity = base.GetComponent<NetworkIdentity>();
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x00002A4D File Offset: 0x00000C4D
		private void OnEnable()
		{
			if (!NetworkServer.active)
			{
				base.enabled = false;
			}
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000AFA17 File Offset: 0x000ADC17
		private void OnDisable()
		{
			this.inItemSteal = false;
			this.ForceReclaimAllItemsImmediately();
		}

		// Token: 0x06002874 RID: 10356 RVA: 0x000AFA28 File Offset: 0x000ADC28
		private void OnDestroy()
		{
			ItemStealController.StolenInventoryInfo[] array = this.stolenInventoryInfos;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Dispose();
			}
			this.stolenInventoryInfos = Array.Empty<ItemStealController.StolenInventoryInfo>();
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x000AFA60 File Offset: 0x000ADC60
		private void FixedUpdate()
		{
			if (this.inItemSteal)
			{
				this.stealTimer -= Time.fixedDeltaTime;
				if (this.stealTimer <= 0f)
				{
					this.stealTimer = this.stealInterval;
					this.StepSteal();
				}
			}
			if (this.inLending)
			{
				this.lendTimer -= Time.fixedDeltaTime;
				if (this.lendTimer <= 0f)
				{
					this.lendTimer = this.stealInterval;
					this.StepLend();
				}
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06002876 RID: 10358 RVA: 0x000AFADF File Offset: 0x000ADCDF
		// (set) Token: 0x06002877 RID: 10359 RVA: 0x000AFAE7 File Offset: 0x000ADCE7
		public Inventory lendeeInventory { get; private set; }

		// Token: 0x06002878 RID: 10360 RVA: 0x000AFAF0 File Offset: 0x000ADCF0
		public static bool DefaultItemFilter(ItemIndex itemIndex)
		{
			ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
			return itemDef.canRemove && itemDef.DoesNotContainTag(ItemTag.CannotSteal);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x000AFB18 File Offset: 0x000ADD18
		public static bool AIItemFilter(ItemIndex itemIndex)
		{
			ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
			return itemDef.canRemove && itemDef.DoesNotContainTag(ItemTag.AIBlacklist);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000AFB40 File Offset: 0x000ADD40
		public static bool BrotherItemFilter(ItemIndex itemIndex)
		{
			ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
			return itemDef.canRemove && itemDef.DoesNotContainTag(ItemTag.AIBlacklist) && itemDef.DoesNotContainTag(ItemTag.BrotherBlacklist);
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x0600287B RID: 10363 RVA: 0x000AFB6F File Offset: 0x000ADD6F
		// (set) Token: 0x0600287C RID: 10364 RVA: 0x000AFB78 File Offset: 0x000ADD78
		public bool inItemSteal
		{
			get
			{
				return this._inItemSteal;
			}
			private set
			{
				if (this._inItemSteal == value)
				{
					return;
				}
				if (this._inItemSteal)
				{
					this.hasStolen = true;
				}
				this._inItemSteal = value;
				UnityEvent unityEvent = this._inItemSteal ? this.onStealBeginServer : this.onStealFinishServer;
				if (unityEvent != null)
				{
					unityEvent.Invoke();
				}
				if (this._inItemSteal)
				{
					this.CallRpcOnStealBeginClient();
					return;
				}
				this.CallRpcOnStealFinishClient();
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x0600287D RID: 10365 RVA: 0x000AFBDB File Offset: 0x000ADDDB
		// (set) Token: 0x0600287E RID: 10366 RVA: 0x000AFBE3 File Offset: 0x000ADDE3
		public bool hasStolen { get; private set; }

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x000AFBEC File Offset: 0x000ADDEC
		// (set) Token: 0x06002880 RID: 10368 RVA: 0x000AFBF4 File Offset: 0x000ADDF4
		public bool inLending
		{
			get
			{
				return this._inLending;
			}
			private set
			{
				if (this._inLending == value)
				{
					return;
				}
				this._inLending = value;
				UnityEvent unityEvent = this._inLending ? this.onLendingBeginServer : this.onLendingFinishServer;
				if (unityEvent != null)
				{
					unityEvent.Invoke();
				}
				if (this._inItemSteal)
				{
					this.CallRpcOnLendingBeginClient();
					return;
				}
				this.CallRpcOnLendingFinishClient();
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06002881 RID: 10369 RVA: 0x000AFC48 File Offset: 0x000ADE48
		private Either<NetworkIdentity, HurtBox> orbTarget
		{
			get
			{
				if (this.orbDestinationHurtBoxOverride)
				{
					return this.orbDestinationHurtBoxOverride;
				}
				return this.networkIdentity;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x000AFC6E File Offset: 0x000ADE6E
		private Transform orbTargetTransform
		{
			get
			{
				if (this.orbDestinationHurtBoxOverride)
				{
					return this.orbDestinationHurtBoxOverride.transform;
				}
				return this.networkIdentity.transform;
			}
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x000AFC94 File Offset: 0x000ADE94
		public void StartLending(Inventory newLendeeInventory)
		{
			if (this.lendeeInventory != newLendeeInventory)
			{
				ItemStealController.StolenInventoryInfo[] array = this.stolenInventoryInfos;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].TakeBackItemsFromLendee();
				}
			}
			this.lendeeInventory = newLendeeInventory;
			this.inLending = true;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000AFCDC File Offset: 0x000ADEDC
		public void LendImmediately(Inventory newLendeeInventory)
		{
			Debug.LogFormat("LendImmediately({0})", new object[]
			{
				newLendeeInventory
			});
			this.StartLending(newLendeeInventory);
			ItemStealController.StolenInventoryInfo[] array = this.stolenInventoryInfos;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].LendAllItems(false, int.MaxValue);
			}
			this.inLending = false;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x000AFD30 File Offset: 0x000ADF30
		public void StartStealingFromInventory(Inventory victimInventory)
		{
			if (this.GetStolenInventoryInfo(victimInventory) != null)
			{
				return;
			}
			ItemStealController.StolenInventoryInfo stolenInventoryInfo = new ItemStealController.StolenInventoryInfo(this, victimInventory);
			if (victimInventory.GetComponent<MinionOwnership>().ownerMaster)
			{
				stolenInventoryInfo.allowLending = false;
				stolenInventoryInfo.showStealOrbs = false;
			}
			ArrayUtils.ArrayAppend<ItemStealController.StolenInventoryInfo>(ref this.stolenInventoryInfos, stolenInventoryInfo);
			this.inItemSteal = true;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x000AFD84 File Offset: 0x000ADF84
		public void StepSteal()
		{
			if (this.stealMasterFilter != null)
			{
				foreach (CharacterMaster characterMaster in CharacterMaster.readOnlyInstancesList)
				{
					if (this.stealMasterFilter(characterMaster))
					{
						this.StartStealingFromInventory(characterMaster.inventory);
					}
				}
			}
			bool flag = false;
			foreach (ItemStealController.StolenInventoryInfo stolenInventoryInfo in this.stolenInventoryInfos)
			{
				flag |= stolenInventoryInfo.hasOrbsInFlight;
				flag |= stolenInventoryInfo.StealNewestItem(int.MaxValue, null);
			}
			if (!flag)
			{
				this.inItemSteal = false;
			}
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x000AFE3C File Offset: 0x000AE03C
		public void StepLend()
		{
			bool flag = false;
			foreach (ItemStealController.StolenInventoryInfo stolenInventoryInfo in this.stolenInventoryInfos)
			{
				flag |= stolenInventoryInfo.hasOrbsInFlight;
				flag |= stolenInventoryInfo.LendNewestStolenItem(true, int.MaxValue);
			}
			if (!flag)
			{
				this.inLending = false;
			}
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000AFE88 File Offset: 0x000AE088
		public int GetStolenItemCount(Inventory victimInventory)
		{
			ItemStealController.StolenInventoryInfo stolenInventoryInfo = this.GetStolenInventoryInfo(victimInventory);
			if (stolenInventoryInfo != null)
			{
				return stolenInventoryInfo.stolenItemCount;
			}
			return 0;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000AFEA8 File Offset: 0x000AE0A8
		private ItemStealController.StolenInventoryInfo GetStolenInventoryInfo(Inventory victimInventory)
		{
			for (int i = 0; i < this.stolenInventoryInfos.Length; i++)
			{
				if (this.stolenInventoryInfos[i].victimInventory == victimInventory)
				{
					return this.stolenInventoryInfos[i];
				}
			}
			return null;
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000AFEE8 File Offset: 0x000AE0E8
		private void ForceReclaimAllItemsImmediately()
		{
			ItemStealController.StolenInventoryInfo[] array = this.stolenInventoryInfos;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ForceReclaimAllItemsImmediately();
			}
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x000AFF14 File Offset: 0x000AE114
		public void ReclaimAllItems()
		{
			ItemStealController.StolenInventoryInfo[] array = this.stolenInventoryInfos;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].ReclaimAllItems(null);
			}
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x000AFF47 File Offset: 0x000AE147
		public void StartSteal(Func<CharacterMaster, bool> filter)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			this.stealMasterFilter = filter;
			this.inItemSteal = true;
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000AFF5F File Offset: 0x000AE15F
		public bool ReclaimItemForInventory(Inventory inventory, int maxStack = 2147483647)
		{
			ItemStealController.StolenInventoryInfo stolenInventoryInfo = this.GetStolenInventoryInfo(inventory);
			return stolenInventoryInfo != null && stolenInventoryInfo.ReclaimNewestLentItem(maxStack);
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x000AFF74 File Offset: 0x000AE174
		public void FindDeadOwnersOfStolenItems(List<Inventory> dest)
		{
			List<Inventory> list = CollectionPool<Inventory, List<Inventory>>.RentCollection();
			List<Inventory> list2 = CollectionPool<Inventory, List<Inventory>>.RentCollection();
			for (int i = 0; i < this.stolenInventoryInfos.Length; i++)
			{
				ref ItemStealController.StolenInventoryInfo ptr = ref this.stolenInventoryInfos[i];
				if (ptr.victimInventory)
				{
					CharacterMaster component = ptr.victimInventory.GetComponent<CharacterMaster>();
					if (component && !component.hasBody && ptr.hasItemsToReclaim)
					{
						(ptr.allowLending ? list : list2).Add(ptr.victimInventory);
					}
				}
			}
			dest.AddRange(list);
			dest.AddRange(list2);
			CollectionPool<Inventory, List<Inventory>>.ReturnCollection(list2);
			CollectionPool<Inventory, List<Inventory>>.ReturnCollection(list);
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x000B001C File Offset: 0x000AE21C
		public void AddValidStolenInventoriesToList(List<Inventory> list)
		{
			if (list != null)
			{
				foreach (ItemStealController.StolenInventoryInfo stolenInventoryInfo in this.stolenInventoryInfos)
				{
					if (stolenInventoryInfo.victimInventory)
					{
						list.Add(stolenInventoryInfo.victimInventory);
					}
				}
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x000B005E File Offset: 0x000AE25E
		[ClientRpc]
		private void RpcOnStealBeginClient()
		{
			Action action = this.onStealBeginClient;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x000B0070 File Offset: 0x000AE270
		[ClientRpc]
		private void RpcOnStealFinishClient()
		{
			Action action = this.onStealFinishClient;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x000B0082 File Offset: 0x000AE282
		[ClientRpc]
		private void RpcOnLendingBeginClient()
		{
			Action action = this.onLendingBeginClient;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x000B0094 File Offset: 0x000AE294
		[ClientRpc]
		private void RpcOnLendingFinishClient()
		{
			Action action = this.onLendingFinishClient;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x000B00F5 File Offset: 0x000AE2F5
		protected static void InvokeRpcRpcOnStealBeginClient(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcOnStealBeginClient called on server.");
				return;
			}
			((ItemStealController)obj).RpcOnStealBeginClient();
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x000B0118 File Offset: 0x000AE318
		protected static void InvokeRpcRpcOnStealFinishClient(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcOnStealFinishClient called on server.");
				return;
			}
			((ItemStealController)obj).RpcOnStealFinishClient();
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x000B013B File Offset: 0x000AE33B
		protected static void InvokeRpcRpcOnLendingBeginClient(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcOnLendingBeginClient called on server.");
				return;
			}
			((ItemStealController)obj).RpcOnLendingBeginClient();
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x000B015E File Offset: 0x000AE35E
		protected static void InvokeRpcRpcOnLendingFinishClient(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcOnLendingFinishClient called on server.");
				return;
			}
			((ItemStealController)obj).RpcOnLendingFinishClient();
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x000B0184 File Offset: 0x000AE384
		public void CallRpcOnStealBeginClient()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcOnStealBeginClient called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)ItemStealController.kRpcRpcOnStealBeginClient);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcOnStealBeginClient");
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x000B01F0 File Offset: 0x000AE3F0
		public void CallRpcOnStealFinishClient()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcOnStealFinishClient called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)ItemStealController.kRpcRpcOnStealFinishClient);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcOnStealFinishClient");
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x000B025C File Offset: 0x000AE45C
		public void CallRpcOnLendingBeginClient()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcOnLendingBeginClient called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)ItemStealController.kRpcRpcOnLendingBeginClient);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcOnLendingBeginClient");
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x000B02C8 File Offset: 0x000AE4C8
		public void CallRpcOnLendingFinishClient()
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcOnLendingFinishClient called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)ItemStealController.kRpcRpcOnLendingFinishClient);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			this.SendRPCInternal(networkWriter, 0, "RpcOnLendingFinishClient");
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x000B0334 File Offset: 0x000AE534
		static ItemStealController()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(ItemStealController), ItemStealController.kRpcRpcOnStealBeginClient, new NetworkBehaviour.CmdDelegate(ItemStealController.InvokeRpcRpcOnStealBeginClient));
			ItemStealController.kRpcRpcOnStealFinishClient = -1161796992;
			NetworkBehaviour.RegisterRpcDelegate(typeof(ItemStealController), ItemStealController.kRpcRpcOnStealFinishClient, new NetworkBehaviour.CmdDelegate(ItemStealController.InvokeRpcRpcOnStealFinishClient));
			ItemStealController.kRpcRpcOnLendingBeginClient = -1214064562;
			NetworkBehaviour.RegisterRpcDelegate(typeof(ItemStealController), ItemStealController.kRpcRpcOnLendingBeginClient, new NetworkBehaviour.CmdDelegate(ItemStealController.InvokeRpcRpcOnLendingBeginClient));
			ItemStealController.kRpcRpcOnLendingFinishClient = -1007329532;
			NetworkBehaviour.RegisterRpcDelegate(typeof(ItemStealController), ItemStealController.kRpcRpcOnLendingFinishClient, new NetworkBehaviour.CmdDelegate(ItemStealController.InvokeRpcRpcOnLendingFinishClient));
			NetworkCRC.RegisterBehaviour("ItemStealController", 0);
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x000B03F8 File Offset: 0x000AE5F8
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002C19 RID: 11289
		public UnityEvent onStealBeginServer;

		// Token: 0x04002C1A RID: 11290
		public UnityEvent onStealFinishServer;

		// Token: 0x04002C1B RID: 11291
		public UnityEvent onLendingBeginServer;

		// Token: 0x04002C1C RID: 11292
		public UnityEvent onLendingFinishServer;

		// Token: 0x04002C1D RID: 11293
		public float stealInterval = 0.2f;

		// Token: 0x04002C1E RID: 11294
		public HurtBox orbDestinationHurtBoxOverride;

		// Token: 0x04002C23 RID: 11299
		private NetworkIdentity networkIdentity;

		// Token: 0x04002C25 RID: 11301
		public Func<ItemIndex, bool> itemStealFilter = new Func<ItemIndex, bool>(ItemStealController.DefaultItemFilter);

		// Token: 0x04002C26 RID: 11302
		public Func<ItemIndex, bool> itemLendFilter = new Func<ItemIndex, bool>(ItemStealController.AIItemFilter);

		// Token: 0x04002C27 RID: 11303
		private ItemStealController.StolenInventoryInfo[] stolenInventoryInfos = Array.Empty<ItemStealController.StolenInventoryInfo>();

		// Token: 0x04002C28 RID: 11304
		private bool _inItemSteal;

		// Token: 0x04002C29 RID: 11305
		private bool _inLending;

		// Token: 0x04002C2A RID: 11306
		private float stealTimer;

		// Token: 0x04002C2B RID: 11307
		private float lendTimer;

		// Token: 0x04002C2D RID: 11309
		private Func<CharacterMaster, bool> stealMasterFilter;

		// Token: 0x04002C2E RID: 11310
		private static int kRpcRpcOnStealBeginClient = 166425938;

		// Token: 0x04002C2F RID: 11311
		private static int kRpcRpcOnStealFinishClient;

		// Token: 0x04002C30 RID: 11312
		private static int kRpcRpcOnLendingBeginClient;

		// Token: 0x04002C31 RID: 11313
		private static int kRpcRpcOnLendingFinishClient;

		// Token: 0x0200078A RID: 1930
		[Serializable]
		private class StolenInventoryInfo : IDisposable
		{
			// Token: 0x17000397 RID: 919
			// (get) Token: 0x060028A2 RID: 10402 RVA: 0x000B0406 File Offset: 0x000AE606
			public int stolenItemCount
			{
				get
				{
					return this.itemAcquisitionOrderCount;
				}
			}

			// Token: 0x17000398 RID: 920
			// (get) Token: 0x060028A3 RID: 10403 RVA: 0x000B040E File Offset: 0x000AE60E
			[SerializeField]
			private Inventory lendeeInventory
			{
				get
				{
					return this.owner.lendeeInventory;
				}
			}

			// Token: 0x17000399 RID: 921
			// (get) Token: 0x060028A4 RID: 10404 RVA: 0x000B041B File Offset: 0x000AE61B
			public bool hasOrbsInFlight
			{
				get
				{
					return this.inFlightOrbs.Count > 0;
				}
			}

			// Token: 0x1700039A RID: 922
			// (get) Token: 0x060028A5 RID: 10405 RVA: 0x000B042B File Offset: 0x000AE62B
			// (set) Token: 0x060028A6 RID: 10406 RVA: 0x000B0433 File Offset: 0x000AE633
			public bool hasItemsToReclaim { get; private set; }

			// Token: 0x060028A7 RID: 10407 RVA: 0x000B043C File Offset: 0x000AE63C
			public StolenInventoryInfo(ItemStealController owner, Inventory victimInventory)
			{
				this.owner = owner;
				this.victimInventory = victimInventory;
				this.stolenItemStacks = ItemCatalog.RequestItemStackArray();
				this.lentItemStacks = ItemCatalog.RequestItemStackArray();
				this.itemAcqusitionSet = ItemCatalog.RequestItemStackArray();
				this.itemAcquisitionOrder = ItemCatalog.RequestItemOrderBuffer();
				this.itemAcquisitionOrderCount = 0;
				this.inFlightOrbs = new List<ItemTransferOrb>();
			}

			// Token: 0x060028A8 RID: 10408 RVA: 0x000B04AC File Offset: 0x000AE6AC
			public void Dispose()
			{
				this.victimInventory = null;
				if (this.stolenItemStacks != null)
				{
					ItemCatalog.ReturnItemStackArray(this.stolenItemStacks);
					this.stolenItemStacks = null;
				}
				if (this.lentItemStacks != null)
				{
					ItemCatalog.ReturnItemStackArray(this.lentItemStacks);
					this.lentItemStacks = null;
				}
				if (this.itemAcqusitionSet != null)
				{
					ItemCatalog.ReturnItemStackArray(this.itemAcqusitionSet);
					this.itemAcqusitionSet = null;
				}
				if (this.itemAcquisitionOrder != null)
				{
					ItemCatalog.ReturnItemOrderBuffer(this.itemAcquisitionOrder);
					this.itemAcquisitionOrder = null;
				}
				this.inFlightOrbs = null;
			}

			// Token: 0x060028A9 RID: 10409 RVA: 0x000B0530 File Offset: 0x000AE730
			public bool StealNewestItem(int maxStackToSteal = 2147483647, bool? useOrbOverride = null)
			{
				if (!this.victimInventory)
				{
					return false;
				}
				List<ItemIndex> list = this.victimInventory.itemAcquisitionOrder;
				for (int i = list.Count - 1; i >= 0; i--)
				{
					ItemIndex itemIndex = list[i];
					if (this.StealItem(itemIndex, maxStackToSteal, useOrbOverride) > 0)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060028AA RID: 10410 RVA: 0x000B0584 File Offset: 0x000AE784
			private int StealItem(ItemIndex itemIndex, int maxStackToSteal, bool? useOrbOverride = null)
			{
				if (!this.owner.itemStealFilter(itemIndex))
				{
					return 0;
				}
				int itemCount = this.victimInventory.GetItemCount(itemIndex);
				int itemsToSteal = Math.Min(itemCount, maxStackToSteal);
				if (itemsToSteal > 0)
				{
					this.victimInventory.RemoveItem(itemIndex, itemsToSteal);
					Vector3? vector = (useOrbOverride ?? this.showStealOrbs) ? ItemStealController.StolenInventoryInfo.FindInventoryOrbOrigin(this.victimInventory) : null;
					if (vector != null)
					{
						ItemTransferOrb item = ItemTransferOrb.DispatchItemTransferOrb(vector.Value, null, itemIndex, itemsToSteal, delegate(ItemTransferOrb orb)
						{
							this.GiveItemToSelf(itemIndex, itemsToSteal);
							this.inFlightOrbs.Remove(orb);
						}, this.owner.networkIdentity);
						this.inFlightOrbs.Add(item);
					}
					else
					{
						this.GiveItemToSelf(itemIndex, itemsToSteal);
					}
				}
				return itemsToSteal;
			}

			// Token: 0x060028AB RID: 10411 RVA: 0x000B069C File Offset: 0x000AE89C
			private void RegisterItemAsAcquired(ItemIndex itemIndex)
			{
				ref int ptr = ref this.itemAcqusitionSet[(int)itemIndex];
				if (ptr == 0)
				{
					ptr = 1;
					ItemIndex[] array = this.itemAcquisitionOrder;
					int num = this.itemAcquisitionOrderCount;
					this.itemAcquisitionOrderCount = num + 1;
					array[num] = itemIndex;
				}
			}

			// Token: 0x060028AC RID: 10412 RVA: 0x000B06D8 File Offset: 0x000AE8D8
			public bool LendNewestStolenItem(bool useOrb, int maxStackToGive = 2147483647)
			{
				for (int i = this.itemAcquisitionOrderCount - 1; i >= 0; i--)
				{
					ItemIndex itemIndex = this.itemAcquisitionOrder[i];
					if (this.LendStolenItem(itemIndex, useOrb, maxStackToGive) > 0)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060028AD RID: 10413 RVA: 0x000B0710 File Offset: 0x000AE910
			public void LendAllItems(bool useOrb, int maxStackToGive = 2147483647)
			{
				Debug.Log("StolenInventoryInfo.LendAllItems()");
				for (int i = this.itemAcquisitionOrderCount - 1; i >= 0; i--)
				{
					ItemIndex itemIndex = this.itemAcquisitionOrder[i];
					this.LendStolenItem(itemIndex, useOrb, maxStackToGive);
				}
			}

			// Token: 0x060028AE RID: 10414 RVA: 0x000B0750 File Offset: 0x000AE950
			private int LendStolenItem(ItemIndex itemIndex, bool useOrb, int maxStackToGive = 2147483647)
			{
				if (!this.lendeeInventory)
				{
					return 0;
				}
				int num = this.TakeItemFromSelf(itemIndex, maxStackToGive);
				if (num > 0)
				{
					if (useOrb)
					{
						ItemTransferOrb item = ItemTransferOrb.DispatchItemTransferOrb(this.owner.orbTargetTransform.position, this.lendeeInventory, itemIndex, num, delegate(ItemTransferOrb orb)
						{
							this.GiveItemToLendee(orb.itemIndex, orb.stack);
							this.inFlightOrbs.Remove(orb);
						}, default(Either<NetworkIdentity, HurtBox>));
						this.inFlightOrbs.Add(item);
					}
					else
					{
						this.GiveItemToLendee(itemIndex, num);
					}
				}
				this.hasItemsToReclaim = true;
				return num;
			}

			// Token: 0x060028AF RID: 10415 RVA: 0x000B07D0 File Offset: 0x000AE9D0
			private int ReclaimLentItem(ItemIndex itemToReclaim, int maxStackToReclaim = 2147483647, bool? useOrbOverride = null)
			{
				int num = this.TakeItemFromLendee(itemToReclaim, maxStackToReclaim);
				if (num > 0)
				{
					Vector3? orbOrigin = (useOrbOverride ?? this.showStealOrbs) ? ItemStealController.StolenInventoryInfo.FindInventoryOrbOrigin(this.lendeeInventory) : null;
					this.DispatchOrbOrGiveItem(this.victimInventory, itemToReclaim, num, orbOrigin, useOrbOverride);
				}
				return num;
			}

			// Token: 0x060028B0 RID: 10416 RVA: 0x000B0830 File Offset: 0x000AEA30
			private int ReclaimStolenItem(ItemIndex itemToReclaim, int maxStacksToReclaim = 2147483647, bool? useOrbOverride = null)
			{
				int num = this.TakeItemFromSelf(itemToReclaim, maxStacksToReclaim);
				if (num > 0)
				{
					this.DispatchOrbOrGiveItem(this.victimInventory, itemToReclaim, num, new Vector3?(this.owner.orbTargetTransform.position), useOrbOverride);
				}
				return num;
			}

			// Token: 0x060028B1 RID: 10417 RVA: 0x000B0870 File Offset: 0x000AEA70
			private void DispatchOrbOrGiveItem(Inventory inventoryToGrantTo, ItemIndex itemToReclaim, int stacks, Vector3? orbOrigin, bool? useOrbOverride = null)
			{
				if (!inventoryToGrantTo)
				{
					return;
				}
				if (orbOrigin != null && (useOrbOverride ?? this.showStealOrbs))
				{
					ItemTransferOrb item = ItemTransferOrb.DispatchItemTransferOrb(orbOrigin.Value, this.victimInventory, itemToReclaim, stacks, delegate(ItemTransferOrb orb)
					{
						ItemTransferOrb.DefaultOnArrivalBehavior(orb);
						this.inFlightOrbs.Remove(orb);
					}, default(Either<NetworkIdentity, HurtBox>));
					this.inFlightOrbs.Add(item);
					return;
				}
				if (inventoryToGrantTo)
				{
					inventoryToGrantTo.GiveItem(itemToReclaim, stacks);
				}
			}

			// Token: 0x060028B2 RID: 10418 RVA: 0x000B08F4 File Offset: 0x000AEAF4
			public bool ReclaimNewestLentItem(int maxStackToReclaim = 2147483647)
			{
				for (int i = this.itemAcquisitionOrderCount - 1; i >= 0; i--)
				{
					ItemIndex itemToReclaim = this.itemAcquisitionOrder[i];
					if (this.ReclaimLentItem(itemToReclaim, maxStackToReclaim, null) > 0)
					{
						return true;
					}
				}
				this.hasItemsToReclaim = false;
				return false;
			}

			// Token: 0x060028B3 RID: 10419 RVA: 0x000B093C File Offset: 0x000AEB3C
			public bool ReclaimOldestLentItem(int maxStackToReclaim = 2147483647)
			{
				for (int i = 0; i < this.itemAcquisitionOrderCount; i++)
				{
					ItemIndex itemToReclaim = this.itemAcquisitionOrder[i];
					if (this.ReclaimLentItem(itemToReclaim, maxStackToReclaim, null) > 0)
					{
						return true;
					}
				}
				this.hasItemsToReclaim = false;
				return false;
			}

			// Token: 0x060028B4 RID: 10420 RVA: 0x000B0981 File Offset: 0x000AEB81
			private void ForceAllOrbsToFinish()
			{
				while (this.inFlightOrbs.Count > 0)
				{
					OrbManager.instance.ForceImmediateArrival(this.inFlightOrbs[0]);
				}
			}

			// Token: 0x060028B5 RID: 10421 RVA: 0x000B09A9 File Offset: 0x000AEBA9
			private void GiveItemToSelf(ItemIndex itemIndex, int stack)
			{
				this.stolenItemStacks[(int)itemIndex] += stack;
				this.RegisterItemAsAcquired(itemIndex);
			}

			// Token: 0x060028B6 RID: 10422 RVA: 0x000B09C4 File Offset: 0x000AEBC4
			private int TakeItemFromSelf(ItemIndex itemIndex, int maxStackToTake)
			{
				int[] array = this.stolenItemStacks;
				int num = Math.Min(array[(int)itemIndex], maxStackToTake);
				array[(int)itemIndex] -= num;
				return num;
			}

			// Token: 0x060028B7 RID: 10423 RVA: 0x000B09EC File Offset: 0x000AEBEC
			private void GiveItemToLendee(ItemIndex itemIndex, int stack)
			{
				this.lentItemStacks[(int)itemIndex] += stack;
				if (this.lendeeInventory && this.allowLending && this.owner.itemLendFilter(itemIndex))
				{
					this.lendeeInventory.GiveItem(itemIndex, stack);
				}
			}

			// Token: 0x060028B8 RID: 10424 RVA: 0x000B0A40 File Offset: 0x000AEC40
			private int TakeItemFromLendee(ItemIndex itemIndex, int maxStackToTake)
			{
				int[] array = this.lentItemStacks;
				int num = Math.Min(array[(int)itemIndex], maxStackToTake);
				if (this.lendeeInventory && this.owner.itemLendFilter(itemIndex))
				{
					this.lendeeInventory.RemoveItem(itemIndex, num);
				}
				array[(int)itemIndex] -= num;
				return num;
			}

			// Token: 0x060028B9 RID: 10425 RVA: 0x000B0A98 File Offset: 0x000AEC98
			public void ForceReclaimAllItemsImmediately()
			{
				this.ForceAllOrbsToFinish();
				for (int i = this.itemAcquisitionOrderCount - 1; i >= 0; i--)
				{
					ItemIndex itemIndex = this.itemAcquisitionOrder[i];
					int num = 0;
					num += this.TakeItemFromSelf(itemIndex, int.MaxValue);
					num += this.TakeItemFromLendee(itemIndex, int.MaxValue);
					if (this.victimInventory)
					{
						this.victimInventory.GiveItem(itemIndex, num);
					}
				}
			}

			// Token: 0x060028BA RID: 10426 RVA: 0x000B0B04 File Offset: 0x000AED04
			public void ReclaimAllItems(bool? useOrbOverride)
			{
				for (int i = this.itemAcquisitionOrderCount - 1; i >= 0; i--)
				{
					ItemIndex itemToReclaim = this.itemAcquisitionOrder[i];
					this.ReclaimLentItem(itemToReclaim, int.MaxValue, useOrbOverride);
					this.ReclaimStolenItem(itemToReclaim, int.MaxValue, useOrbOverride);
				}
				this.hasItemsToReclaim = false;
			}

			// Token: 0x060028BB RID: 10427 RVA: 0x000B0B50 File Offset: 0x000AED50
			public void TakeBackItemsFromLendee()
			{
				Vector3? vector = ItemStealController.StolenInventoryInfo.FindInventoryOrbOrigin(this.lendeeInventory);
				for (int i = this.itemAcquisitionOrderCount - 1; i >= 0; i--)
				{
					ItemIndex itemIndex = this.itemAcquisitionOrder[i];
					int itemStackToTake = this.TakeItemFromLendee(itemIndex, int.MaxValue);
					if (vector != null)
					{
						ItemTransferOrb item = ItemTransferOrb.DispatchItemTransferOrb(vector.Value, null, itemIndex, itemStackToTake, delegate(ItemTransferOrb orb)
						{
							this.GiveItemToSelf(itemIndex, itemStackToTake);
							this.inFlightOrbs.Remove(orb);
						}, this.owner.orbTarget);
						this.inFlightOrbs.Add(item);
					}
					else
					{
						this.GiveItemToSelf(itemIndex, itemStackToTake);
					}
				}
			}

			// Token: 0x060028BC RID: 10428 RVA: 0x000B0C10 File Offset: 0x000AEE10
			private static Vector3? FindInventoryOrbOrigin(Inventory inventory)
			{
				if (inventory)
				{
					CharacterMaster component = inventory.GetComponent<CharacterMaster>();
					if (component)
					{
						CharacterBody body = component.GetBody();
						if (body)
						{
							return new Vector3?(body.corePosition);
						}
					}
				}
				return null;
			}

			// Token: 0x04002C32 RID: 11314
			[SerializeField]
			private ItemStealController owner;

			// Token: 0x04002C33 RID: 11315
			public Inventory victimInventory;

			// Token: 0x04002C34 RID: 11316
			public bool allowLending = true;

			// Token: 0x04002C35 RID: 11317
			public bool showStealOrbs = true;

			// Token: 0x04002C36 RID: 11318
			private int[] stolenItemStacks;

			// Token: 0x04002C37 RID: 11319
			private int[] lentItemStacks;

			// Token: 0x04002C38 RID: 11320
			private int[] itemAcqusitionSet;

			// Token: 0x04002C39 RID: 11321
			private ItemIndex[] itemAcquisitionOrder;

			// Token: 0x04002C3A RID: 11322
			[SerializeField]
			private int itemAcquisitionOrderCount;

			// Token: 0x04002C3B RID: 11323
			[SerializeField]
			private List<ItemTransferOrb> inFlightOrbs;
		}
	}
}
