using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200088E RID: 2190
	public class ShopTerminalBehavior : NetworkBehaviour
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x000CCB45 File Offset: 0x000CAD45
		public bool pickupIndexIsHidden
		{
			get
			{
				return this.hidden;
			}
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000CCB4D File Offset: 0x000CAD4D
		public void SetHasBeenPurchased(bool newHasBeenPurchased)
		{
			if (this.hasBeenPurchased != newHasBeenPurchased)
			{
				this.NetworkhasBeenPurchased = newHasBeenPurchased;
			}
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000CCB5F File Offset: 0x000CAD5F
		private void OnSyncHidden(bool newHidden)
		{
			this.SetPickupIndex(this.pickupIndex, newHidden);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000CCB6E File Offset: 0x000CAD6E
		private void OnSyncPickupIndex(PickupIndex newPickupIndex)
		{
			this.SetPickupIndex(newPickupIndex, this.hidden);
			if (NetworkClient.active)
			{
				this.UpdatePickupDisplayAndAnimations();
			}
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000CCB8C File Offset: 0x000CAD8C
		public void Start()
		{
			this.hasStarted = true;
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
				if (this.selfGeneratePickup)
				{
					this.GenerateNewPickupServer();
				}
			}
			if (NetworkClient.active)
			{
				this.UpdatePickupDisplayAndAnimations();
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000CCBDC File Offset: 0x000CADDC
		[Server]
		public void GenerateNewPickupServer()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShopTerminalBehavior::GenerateNewPickupServer()' called on client");
				return;
			}
			this.GenerateNewPickupServer(this.hidden);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x000CCC00 File Offset: 0x000CAE00
		[Server]
		public void GenerateNewPickupServer(bool newHidden)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShopTerminalBehavior::GenerateNewPickupServer(System.Boolean)' called on client");
				return;
			}
			if (this.hasBeenPurchased)
			{
				return;
			}
			PickupIndex newPickupIndex = PickupIndex.none;
			if (this.dropTable)
			{
				newPickupIndex = this.dropTable.GenerateDrop(this.rng);
			}
			else
			{
				List<PickupIndex> list;
				switch (this.itemTier)
				{
				case ItemTier.Tier1:
					list = Run.instance.availableTier1DropList;
					goto IL_FC;
				case ItemTier.Tier2:
					list = Run.instance.availableTier2DropList;
					goto IL_FC;
				case ItemTier.Tier3:
					list = Run.instance.availableTier3DropList;
					goto IL_FC;
				case ItemTier.Lunar:
					list = Run.instance.availableLunarCombinedDropList;
					goto IL_FC;
				case ItemTier.Boss:
					list = Run.instance.availableBossDropList;
					goto IL_FC;
				case ItemTier.VoidTier1:
					list = Run.instance.availableVoidTier1DropList;
					goto IL_FC;
				case ItemTier.VoidTier2:
					list = Run.instance.availableVoidTier2DropList;
					goto IL_FC;
				case ItemTier.VoidTier3:
					list = Run.instance.availableVoidTier3DropList;
					goto IL_FC;
				case ItemTier.VoidBoss:
					list = Run.instance.availableVoidBossDropList;
					goto IL_FC;
				}
				throw new ArgumentOutOfRangeException();
				IL_FC:
				newPickupIndex = this.<GenerateNewPickupServer>g__Pick|21_1(list);
			}
			this.SetPickupIndex(newPickupIndex, newHidden);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000CCD19 File Offset: 0x000CAF19
		public void SetPickupIndex(PickupIndex newPickupIndex, bool newHidden = false)
		{
			if (this.pickupIndex != newPickupIndex || this.hidden != newHidden)
			{
				this.NetworkpickupIndex = newPickupIndex;
				this.Networkhidden = newHidden;
			}
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x000CCB5F File Offset: 0x000CAD5F
		public void SetHidden(bool newHidden)
		{
			this.SetPickupIndex(this.pickupIndex, newHidden);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000CCD40 File Offset: 0x000CAF40
		private void UpdatePickupDisplayAndAnimations()
		{
			if (this.pickupDisplay)
			{
				this.pickupDisplay.SetPickupIndex(this.pickupIndex, this.hidden);
			}
			if (this.hasStarted)
			{
				if (this.pickupIndex == PickupIndex.none)
				{
					Util.PlaySound("Play_UI_tripleChestShutter", base.gameObject);
					if (this.animator)
					{
						int layerIndex = this.animator.GetLayerIndex("Body");
						this.animator.PlayInFixedTime(this.hasBeenPurchased ? "Open" : "Closing", layerIndex);
						return;
					}
				}
				else if (this.animator && !this.hasBeenPurchased)
				{
					int layerIndex2 = this.animator.GetLayerIndex("Body");
					this.animator.PlayInFixedTime("Idle", layerIndex2);
				}
			}
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000CCE15 File Offset: 0x000CB015
		public PickupIndex CurrentPickupIndex()
		{
			return this.pickupIndex;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000CCE1D File Offset: 0x000CB01D
		[Server]
		public void SetNoPickup()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShopTerminalBehavior::SetNoPickup()' called on client");
				return;
			}
			this.SetPickupIndex(PickupIndex.none, false);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000CCE40 File Offset: 0x000CB040
		[Server]
		public void DropPickup()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ShopTerminalBehavior::DropPickup()' called on client");
				return;
			}
			this.SetHasBeenPurchased(true);
			PickupDropletController.CreatePickupDroplet(this.pickupIndex, (this.dropTransform ? this.dropTransform : base.transform).position, base.transform.TransformVector(this.dropVelocity));
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000CCEC0 File Offset: 0x000CB0C0
		[CompilerGenerated]
		private bool <GenerateNewPickupServer>g__PassesFilter|21_0(PickupIndex pickupIndex)
		{
			if (this.bannedItemTag == ItemTag.Any)
			{
				return true;
			}
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			return pickupDef.itemIndex == ItemIndex.None || !ItemCatalog.GetItemDef(pickupDef.itemIndex).ContainsTag(this.bannedItemTag);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000CCF02 File Offset: 0x000CB102
		[CompilerGenerated]
		private PickupIndex <GenerateNewPickupServer>g__Pick|21_1(List<PickupIndex> list)
		{
			return this.rng.NextElementUniform<PickupIndex>(list.Where(new Func<PickupIndex, bool>(this.<GenerateNewPickupServer>g__PassesFilter|21_0)).ToList<PickupIndex>());
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x0600302E RID: 12334 RVA: 0x000CCF28 File Offset: 0x000CB128
		// (set) Token: 0x0600302F RID: 12335 RVA: 0x000CCF3B File Offset: 0x000CB13B
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
					this.OnSyncPickupIndex(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<PickupIndex>(value, ref this.pickupIndex, 1U);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x000CCF7C File Offset: 0x000CB17C
		// (set) Token: 0x06003031 RID: 12337 RVA: 0x000CCF8F File Offset: 0x000CB18F
		public bool Networkhidden
		{
			get
			{
				return this.hidden;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.OnSyncHidden(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this.hidden, 2U);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06003032 RID: 12338 RVA: 0x000CCFD0 File Offset: 0x000CB1D0
		// (set) Token: 0x06003033 RID: 12339 RVA: 0x000CCFE3 File Offset: 0x000CB1E3
		public bool NetworkhasBeenPurchased
		{
			get
			{
				return this.hasBeenPurchased;
			}
			[param: In]
			set
			{
				if (NetworkServer.localClientActive && !base.syncVarHookGuard)
				{
					base.syncVarHookGuard = true;
					this.SetHasBeenPurchased(value);
					base.syncVarHookGuard = false;
				}
				base.SetSyncVar<bool>(value, ref this.hasBeenPurchased, 4U);
			}
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000CD024 File Offset: 0x000CB224
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			if (forceAll)
			{
				GeneratedNetworkCode._WritePickupIndex_None(writer, this.pickupIndex);
				writer.Write(this.hidden);
				writer.Write(this.hasBeenPurchased);
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
				writer.Write(this.hidden);
			}
			if ((base.syncVarDirtyBits & 4U) != 0U)
			{
				if (!flag)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag = true;
				}
				writer.Write(this.hasBeenPurchased);
			}
			if (!flag)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag;
		}

		// Token: 0x06003035 RID: 12341 RVA: 0x000CD110 File Offset: 0x000CB310
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (initialState)
			{
				this.pickupIndex = GeneratedNetworkCode._ReadPickupIndex_None(reader);
				this.hidden = reader.ReadBoolean();
				this.hasBeenPurchased = reader.ReadBoolean();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 1) != 0)
			{
				this.OnSyncPickupIndex(GeneratedNetworkCode._ReadPickupIndex_None(reader));
			}
			if ((num & 2) != 0)
			{
				this.OnSyncHidden(reader.ReadBoolean());
			}
			if ((num & 4) != 0)
			{
				this.SetHasBeenPurchased(reader.ReadBoolean());
			}
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040031CC RID: 12748
		[SyncVar(hook = "OnSyncPickupIndex")]
		private PickupIndex pickupIndex = PickupIndex.none;

		// Token: 0x040031CD RID: 12749
		[SyncVar(hook = "OnSyncHidden")]
		private bool hidden;

		// Token: 0x040031CE RID: 12750
		[SyncVar(hook = "SetHasBeenPurchased")]
		private bool hasBeenPurchased;

		// Token: 0x040031CF RID: 12751
		[Tooltip("The PickupDisplay component that should show which item this shop terminal is offering.")]
		public PickupDisplay pickupDisplay;

		// Token: 0x040031D0 RID: 12752
		[Tooltip("The position from which the drop will be emitted")]
		public Transform dropTransform;

		// Token: 0x040031D1 RID: 12753
		[Tooltip("The drop table to select a pickup index from - only works if the pickup generates itself")]
		public PickupDropTable dropTable;

		// Token: 0x040031D2 RID: 12754
		[Tooltip("The velocity with which the drop will be emitted. Rotates with this object.")]
		public Vector3 dropVelocity;

		// Token: 0x040031D3 RID: 12755
		public Animator animator;

		// Token: 0x040031D4 RID: 12756
		[Tooltip("The tier of items to drop - only works if the pickup generates itself and the dropTable field is empty.")]
		[Header("Deprecated")]
		public ItemTier itemTier;

		// Token: 0x040031D5 RID: 12757
		public ItemTag bannedItemTag;

		// Token: 0x040031D6 RID: 12758
		[Tooltip("Whether or not the shop terminal should drive itself")]
		public bool selfGeneratePickup = true;

		// Token: 0x040031D7 RID: 12759
		private Xoroshiro128Plus rng;

		// Token: 0x040031D8 RID: 12760
		private bool hasStarted;

		// Token: 0x040031D9 RID: 12761
		[NonSerialized]
		public MultiShopController serverMultiShopController;
	}
}
