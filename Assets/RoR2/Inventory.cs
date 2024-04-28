using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000778 RID: 1912
	public class Inventory : NetworkBehaviour
	{
		// Token: 0x14000062 RID: 98
		// (add) Token: 0x060027BF RID: 10175 RVA: 0x000AC8CC File Offset: 0x000AAACC
		// (remove) Token: 0x060027C0 RID: 10176 RVA: 0x000AC904 File Offset: 0x000AAB04
		public event Action onInventoryChanged;

		// Token: 0x14000063 RID: 99
		// (add) Token: 0x060027C1 RID: 10177 RVA: 0x000AC93C File Offset: 0x000AAB3C
		// (remove) Token: 0x060027C2 RID: 10178 RVA: 0x000AC974 File Offset: 0x000AAB74
		public event Action onEquipmentExternalRestockServer;

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000AC9A9 File Offset: 0x000AABA9
		public EquipmentIndex currentEquipmentIndex
		{
			get
			{
				return this.currentEquipmentState.equipmentIndex;
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x000AC9B6 File Offset: 0x000AABB6
		public EquipmentState currentEquipmentState
		{
			get
			{
				return this.GetEquipment((uint)this.activeEquipmentSlot);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x000AC9C4 File Offset: 0x000AABC4
		public EquipmentIndex alternateEquipmentIndex
		{
			get
			{
				return this.alternateEquipmentState.equipmentIndex;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x000AC9D4 File Offset: 0x000AABD4
		public EquipmentState alternateEquipmentState
		{
			get
			{
				uint num = 0U;
				while ((ulong)num < (ulong)((long)this.GetEquipmentSlotCount()))
				{
					if (num != (uint)this.activeEquipmentSlot)
					{
						return this.GetEquipment(num);
					}
					num += 1U;
				}
				return EquipmentState.empty;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000ACA0A File Offset: 0x000AAC0A
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x000ACA12 File Offset: 0x000AAC12
		public byte activeEquipmentSlot { get; private set; }

		// Token: 0x060027C9 RID: 10185 RVA: 0x000ACA1C File Offset: 0x000AAC1C
		private bool SetEquipmentInternal(EquipmentState equipmentState, uint slot)
		{
			if (Run.instance.IsEquipmentExpansionLocked(equipmentState.equipmentIndex))
			{
				return false;
			}
			if ((long)this.equipmentStateSlots.Length <= (long)((ulong)slot))
			{
				int num = this.equipmentStateSlots.Length;
				Array.Resize<EquipmentState>(ref this.equipmentStateSlots, (int)(slot + 1U));
				for (int i = num; i < this.equipmentStateSlots.Length; i++)
				{
					this.equipmentStateSlots[i] = EquipmentState.empty;
				}
			}
			if (this.equipmentStateSlots[(int)slot].Equals(equipmentState))
			{
				return false;
			}
			this.equipmentStateSlots[(int)slot] = equipmentState;
			return true;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000ACAA7 File Offset: 0x000AACA7
		[Server]
		public void SetEquipment(EquipmentState equipmentState, uint slot)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::SetEquipment(RoR2.EquipmentState,System.UInt32)' called on client");
				return;
			}
			if (this.SetEquipmentInternal(equipmentState, slot))
			{
				if (NetworkServer.active)
				{
					base.SetDirtyBit(16U);
				}
				this.HandleInventoryChanged();
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000ACADD File Offset: 0x000AACDD
		public EquipmentState GetEquipment(uint slot)
		{
			if ((ulong)slot >= (ulong)((long)this.equipmentStateSlots.Length))
			{
				return EquipmentState.empty;
			}
			return this.equipmentStateSlots[(int)slot];
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x000ACAFE File Offset: 0x000AACFE
		[Server]
		public void SetActiveEquipmentSlot(byte slotIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::SetActiveEquipmentSlot(System.Byte)' called on client");
				return;
			}
			this.activeEquipmentSlot = slotIndex;
			base.SetDirtyBit(16U);
			this.HandleInventoryChanged();
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x000ACB2A File Offset: 0x000AAD2A
		public int GetEquipmentSlotCount()
		{
			return this.equipmentStateSlots.Length;
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x000ACB34 File Offset: 0x000AAD34
		[Server]
		public void SetEquipmentIndex(EquipmentIndex newEquipmentIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::SetEquipmentIndex(RoR2.EquipmentIndex)' called on client");
				return;
			}
			this.SetEquipmentIndexForSlot(newEquipmentIndex, (uint)this.activeEquipmentSlot);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x000ACB58 File Offset: 0x000AAD58
		[Server]
		public void SetEquipmentIndexForSlot(EquipmentIndex newEquipmentIndex, uint slot)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::SetEquipmentIndexForSlot(RoR2.EquipmentIndex,System.UInt32)' called on client");
				return;
			}
			if (Run.instance.IsEquipmentExpansionLocked(newEquipmentIndex))
			{
				return;
			}
			EquipmentState equipment = this.GetEquipment(slot);
			if (equipment.equipmentIndex != newEquipmentIndex)
			{
				byte charges = equipment.charges;
				Run.FixedTimeStamp chargeFinishTime = equipment.chargeFinishTime;
				if (equipment.equipmentIndex == EquipmentIndex.None && chargeFinishTime.isNegativeInfinity)
				{
					charges = 1;
					chargeFinishTime = Run.FixedTimeStamp.now;
				}
				EquipmentState equipmentState = new EquipmentState(newEquipmentIndex, chargeFinishTime, charges);
				this.SetEquipment(equipmentState, slot);
			}
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x000ACBD3 File Offset: 0x000AADD3
		public EquipmentIndex GetEquipmentIndex()
		{
			return this.currentEquipmentIndex;
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x000ACBDC File Offset: 0x000AADDC
		[Server]
		public void DeductEquipmentCharges(byte slot, int deduction)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::DeductEquipmentCharges(System.Byte,System.Int32)' called on client");
				return;
			}
			EquipmentState equipment = this.GetEquipment((uint)slot);
			byte b = equipment.charges;
			Run.FixedTimeStamp chargeFinishTime = equipment.chargeFinishTime;
			if ((int)b < deduction)
			{
				b = 0;
			}
			else
			{
				b -= (byte)deduction;
			}
			this.SetEquipment(new EquipmentState(equipment.equipmentIndex, chargeFinishTime, b), (uint)slot);
			this.UpdateEquipment();
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000ACC3C File Offset: 0x000AAE3C
		public int GetEquipmentRestockableChargeCount(byte slot)
		{
			EquipmentState equipment = this.GetEquipment((uint)slot);
			if (equipment.equipmentIndex == EquipmentIndex.None)
			{
				return 0;
			}
			return (int)HGMath.ByteSafeSubtract((byte)this.GetEquipmentSlotMaxCharges(slot), equipment.charges);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000ACC70 File Offset: 0x000AAE70
		[Server]
		public void RestockEquipmentCharges(byte slot, int amount)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::RestockEquipmentCharges(System.Byte,System.Int32)' called on client");
				return;
			}
			amount = Math.Min(amount, this.GetEquipmentRestockableChargeCount(slot));
			if (amount <= 0)
			{
				return;
			}
			EquipmentState equipment = this.GetEquipment((uint)slot);
			byte charges = HGMath.ByteSafeAdd(equipment.charges, (byte)Math.Min(amount, 255));
			this.SetEquipment(new EquipmentState(equipment.equipmentIndex, equipment.chargeFinishTime, charges), (uint)slot);
			Action action = this.onEquipmentExternalRestockServer;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000ACCF0 File Offset: 0x000AAEF0
		[Server]
		public void DeductActiveEquipmentCooldown(float seconds)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::DeductActiveEquipmentCooldown(System.Single)' called on client");
				return;
			}
			EquipmentState equipment = this.GetEquipment((uint)this.activeEquipmentSlot);
			this.SetEquipment(new EquipmentState(equipment.equipmentIndex, equipment.chargeFinishTime - seconds, equipment.charges), (uint)this.activeEquipmentSlot);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x000ACD48 File Offset: 0x000AAF48
		public int GetEquipmentSlotMaxCharges(byte slot)
		{
			return Math.Min(1 + this.GetItemCount(RoR2Content.Items.EquipmentMagazine), 255);
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x000ACD61 File Offset: 0x000AAF61
		public int GetActiveEquipmentMaxCharges()
		{
			return this.GetEquipmentSlotMaxCharges(this.activeEquipmentSlot);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000ACD70 File Offset: 0x000AAF70
		[Server]
		private void UpdateEquipment()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::UpdateEquipment()' called on client");
				return;
			}
			Run.FixedTimeStamp now = Run.FixedTimeStamp.now;
			byte b = (byte)Mathf.Min(1 + this.GetItemCount(RoR2Content.Items.EquipmentMagazine), 255);
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.equipmentStateSlots.Length))
			{
				EquipmentState equipmentState = this.equipmentStateSlots[(int)num];
				if (equipmentState.equipmentIndex != EquipmentIndex.None)
				{
					if (equipmentState.charges < b)
					{
						Run.FixedTimeStamp a = equipmentState.chargeFinishTime;
						byte b2 = equipmentState.charges;
						if (!a.isPositiveInfinity)
						{
							b2 += 1;
						}
						if (a.isInfinity)
						{
							a = now;
						}
						if (a.hasPassed)
						{
							float b3 = equipmentState.equipmentDef.cooldown * this.CalculateEquipmentCooldownScale();
							this.SetEquipment(new EquipmentState(equipmentState.equipmentIndex, a + b3, b2), num);
						}
					}
					if (equipmentState.charges >= b && !equipmentState.chargeFinishTime.isPositiveInfinity)
					{
						this.SetEquipment(new EquipmentState(equipmentState.equipmentIndex, Run.FixedTimeStamp.positiveInfinity, b), num);
					}
				}
				num += 1U;
			}
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000ACE88 File Offset: 0x000AB088
		private float CalculateEquipmentCooldownScale()
		{
			int itemCount = this.GetItemCount(RoR2Content.Items.EquipmentMagazine);
			int itemCount2 = this.GetItemCount(RoR2Content.Items.AutoCastEquipment);
			int itemCount3 = this.GetItemCount(RoR2Content.Items.BoostEquipmentRecharge);
			float num = Mathf.Pow(0.85f, (float)itemCount);
			if (itemCount2 > 0)
			{
				num *= 0.5f * Mathf.Pow(0.85f, (float)(itemCount2 - 1));
			}
			if (itemCount3 > 0)
			{
				num *= Mathf.Pow(0.9f, (float)itemCount3);
			}
			return num;
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000ACEF5 File Offset: 0x000AB0F5
		private void HandleInventoryChanged()
		{
			Action action = this.onInventoryChanged;
			if (action != null)
			{
				action();
			}
			Action<Inventory> action2 = Inventory.onInventoryChangedGlobal;
			if (action2 == null)
			{
				return;
			}
			action2(this);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000ACF18 File Offset: 0x000AB118
		private void FixedUpdate()
		{
			if (NetworkServer.active)
			{
				this.UpdateEquipment();
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060027DB RID: 10203 RVA: 0x000ACF27 File Offset: 0x000AB127
		// (set) Token: 0x060027DC RID: 10204 RVA: 0x000ACF2F File Offset: 0x000AB12F
		public uint infusionBonus { get; private set; }

		// Token: 0x060027DD RID: 10205 RVA: 0x000ACF38 File Offset: 0x000AB138
		[Server]
		public void AddInfusionBonus(uint value)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::AddInfusionBonus(System.UInt32)' called on client");
				return;
			}
			if (value != 0U)
			{
				this.infusionBonus += value;
				base.SetDirtyBit(4U);
				this.HandleInventoryChanged();
			}
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x000ACF6D File Offset: 0x000AB16D
		[Server]
		public void GiveItemString(string itemString)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::GiveItemString(System.String)' called on client");
				return;
			}
			this.GiveItem(ItemCatalog.FindItemIndex(itemString), 1);
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x000ACF91 File Offset: 0x000AB191
		[Server]
		public void GiveItemString(string itemString, int count)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::GiveItemString(System.String,System.Int32)' called on client");
				return;
			}
			this.GiveItem(ItemCatalog.FindItemIndex(itemString), count);
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x000ACFB5 File Offset: 0x000AB1B5
		[Server]
		public void GiveEquipmentString(string equipmentString)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::GiveEquipmentString(System.String)' called on client");
				return;
			}
			this.SetEquipmentIndex(EquipmentCatalog.FindEquipmentIndex(equipmentString));
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x000ACFD8 File Offset: 0x000AB1D8
		[Server]
		public void GiveRandomItems(int count, bool lunarEnabled, bool voidEnabled)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::GiveRandomItems(System.Int32,System.Boolean,System.Boolean)' called on client");
				return;
			}
			try
			{
				if (count > 0)
				{
					WeightedSelection<List<PickupIndex>> weightedSelection = new WeightedSelection<List<PickupIndex>>(8);
					weightedSelection.AddChoice(Run.instance.availableTier1DropList, 100f);
					weightedSelection.AddChoice(Run.instance.availableTier2DropList, 60f);
					weightedSelection.AddChoice(Run.instance.availableTier3DropList, 4f);
					if (lunarEnabled)
					{
						weightedSelection.AddChoice(Run.instance.availableLunarItemDropList, 4f);
					}
					if (voidEnabled)
					{
						weightedSelection.AddChoice(Run.instance.availableVoidTier1DropList, 4f);
						weightedSelection.AddChoice(Run.instance.availableVoidTier1DropList, 2.3999999f);
						weightedSelection.AddChoice(Run.instance.availableVoidTier1DropList, 0.16f);
					}
					for (int i = 0; i < count; i++)
					{
						List<PickupIndex> list = weightedSelection.Evaluate(UnityEngine.Random.value);
						PickupDef pickupDef = PickupCatalog.GetPickupDef(list[UnityEngine.Random.Range(0, list.Count)]);
						this.GiveItem((pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None, 1);
					}
				}
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x000AD0FC File Offset: 0x000AB2FC
		[Server]
		public void GiveRandomEquipment()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::GiveRandomEquipment()' called on client");
				return;
			}
			PickupIndex pickupIndex = Run.instance.availableEquipmentDropList[UnityEngine.Random.Range(0, Run.instance.availableEquipmentDropList.Count)];
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			this.SetEquipmentIndex((pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x000AD15C File Offset: 0x000AB35C
		[Server]
		public void GiveRandomEquipment(Xoroshiro128Plus rng)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::GiveRandomEquipment(Xoroshiro128Plus)' called on client");
				return;
			}
			PickupIndex pickupIndex = rng.NextElementUniform<PickupIndex>(Run.instance.availableEquipmentDropList);
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			this.SetEquipmentIndex((pickupDef != null) ? pickupDef.equipmentIndex : EquipmentIndex.None);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x000AD1A8 File Offset: 0x000AB3A8
		[Server]
		public void GiveItem(ItemIndex itemIndex, int count = 1)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::GiveItem(RoR2.ItemIndex,System.Int32)' called on client");
				return;
			}
			if (Run.instance && Run.instance.IsItemExpansionLocked(itemIndex))
			{
				return;
			}
			if ((ulong)itemIndex >= (ulong)((long)this.itemStacks.Length))
			{
				return;
			}
			if (count <= 0)
			{
				if (count < 0)
				{
					this.RemoveItem(itemIndex, -count);
				}
				return;
			}
			base.SetDirtyBit(1U);
			if ((this.itemStacks[(int)itemIndex] += count) == count)
			{
				this.itemAcquisitionOrder.Add(itemIndex);
				base.SetDirtyBit(8U);
			}
			this.HandleInventoryChanged();
			Action<Inventory, ItemIndex, int> action = Inventory.onServerItemGiven;
			if (action != null)
			{
				action(this, itemIndex, count);
			}
			if (this.spawnedOverNetwork)
			{
				this.CallRpcItemAdded(itemIndex);
			}
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x000AD25D File Offset: 0x000AB45D
		[Server]
		public void GiveItem(ItemDef itemDef, int count = 1)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::GiveItem(RoR2.ItemDef,System.Int32)' called on client");
				return;
			}
			this.GiveItem((itemDef != null) ? itemDef.itemIndex : ItemIndex.None, count);
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x000AD287 File Offset: 0x000AB487
		private bool spawnedOverNetwork
		{
			get
			{
				return base.isServer;
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x060027E7 RID: 10215 RVA: 0x000AD290 File Offset: 0x000AB490
		// (remove) Token: 0x060027E8 RID: 10216 RVA: 0x000AD2C4 File Offset: 0x000AB4C4
		public static event Action<Inventory> onInventoryChangedGlobal;

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x060027E9 RID: 10217 RVA: 0x000AD2F8 File Offset: 0x000AB4F8
		// (remove) Token: 0x060027EA RID: 10218 RVA: 0x000AD32C File Offset: 0x000AB52C
		public static event Action<Inventory, ItemIndex, int> onServerItemGiven;

		// Token: 0x060027EB RID: 10219 RVA: 0x000AD35F File Offset: 0x000AB55F
		[ClientRpc]
		private void RpcItemAdded(ItemIndex itemIndex)
		{
			Action<ItemIndex> action = this.onItemAddedClient;
			if (action == null)
			{
				return;
			}
			action(itemIndex);
		}

		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060027EC RID: 10220 RVA: 0x000AD374 File Offset: 0x000AB574
		// (remove) Token: 0x060027ED RID: 10221 RVA: 0x000AD3AC File Offset: 0x000AB5AC
		public event Action<ItemIndex> onItemAddedClient;

		// Token: 0x060027EE RID: 10222 RVA: 0x000AD3E4 File Offset: 0x000AB5E4
		[Server]
		public void RemoveItem(ItemIndex itemIndex, int count = 1)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::RemoveItem(RoR2.ItemIndex,System.Int32)' called on client");
				return;
			}
			if ((ulong)itemIndex >= (ulong)((long)this.itemStacks.Length))
			{
				return;
			}
			if (count <= 0)
			{
				if (count < 0)
				{
					this.GiveItem(itemIndex, -count);
				}
				return;
			}
			int num = this.itemStacks[(int)itemIndex];
			count = Math.Min(count, num);
			if (count == 0)
			{
				return;
			}
			if ((this.itemStacks[(int)itemIndex] = num - count) == 0)
			{
				this.itemAcquisitionOrder.Remove(itemIndex);
				base.SetDirtyBit(8U);
			}
			base.SetDirtyBit(1U);
			this.HandleInventoryChanged();
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000AD46D File Offset: 0x000AB66D
		[Server]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveItem(ItemDef itemDef, int count = 1)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::RemoveItem(RoR2.ItemDef,System.Int32)' called on client");
				return;
			}
			this.RemoveItem((itemDef != null) ? itemDef.itemIndex : ItemIndex.None, count);
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000AD498 File Offset: 0x000AB698
		[Server]
		public void ResetItem(ItemIndex itemIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::ResetItem(RoR2.ItemIndex)' called on client");
				return;
			}
			if ((ulong)itemIndex >= (ulong)((long)this.itemStacks.Length))
			{
				return;
			}
			ref int ptr = ref this.itemStacks[(int)itemIndex];
			if (ptr <= 0)
			{
				return;
			}
			ptr = 0;
			base.SetDirtyBit(1U);
			base.SetDirtyBit(8U);
			this.HandleInventoryChanged();
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000AD4F2 File Offset: 0x000AB6F2
		[Server]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void ResetItem(ItemDef itemDef)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::ResetItem(RoR2.ItemDef)' called on client");
				return;
			}
			this.ResetItem((itemDef != null) ? itemDef.itemIndex : ItemIndex.None);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000AD51C File Offset: 0x000AB71C
		[Server]
		public void CopyEquipmentFrom(Inventory other)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::CopyEquipmentFrom(RoR2.Inventory)' called on client");
				return;
			}
			for (int i = 0; i < other.equipmentStateSlots.Length; i++)
			{
				this.SetEquipment(new EquipmentState(other.equipmentStateSlots[i].equipmentIndex, Run.FixedTimeStamp.negativeInfinity, 1), (uint)i);
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000AD574 File Offset: 0x000AB774
		private static bool DefaultItemCopyFilter(ItemIndex itemIndex)
		{
			return !ItemCatalog.GetItemDef(itemIndex).ContainsTag(ItemTag.CannotCopy);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000AD586 File Offset: 0x000AB786
		[Server]
		public void AddItemsFrom([NotNull] Inventory other)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::AddItemsFrom(RoR2.Inventory)' called on client");
				return;
			}
			this.AddItemsFrom(other, Inventory.defaultItemCopyFilterDelegate);
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000AD5A9 File Offset: 0x000AB7A9
		[Server]
		public void AddItemsFrom([NotNull] Inventory other, [NotNull] Func<ItemIndex, bool> filter)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::AddItemsFrom(RoR2.Inventory,System.Func`2<RoR2.ItemIndex,System.Boolean>)' called on client");
				return;
			}
			this.AddItemsFrom(other.itemStacks, filter);
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000AD5D0 File Offset: 0x000AB7D0
		[Server]
		public void AddItemsFrom([NotNull] int[] otherItemStacks, [NotNull] Func<ItemIndex, bool> filter)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::AddItemsFrom(System.Int32[],System.Func`2<RoR2.ItemIndex,System.Boolean>)' called on client");
				return;
			}
			for (ItemIndex itemIndex = (ItemIndex)0; itemIndex < (ItemIndex)this.itemStacks.Length; itemIndex++)
			{
				int num = otherItemStacks[(int)itemIndex];
				if (num > 0 && filter(itemIndex))
				{
					int[] array = this.itemStacks;
					ItemIndex itemIndex2 = itemIndex;
					if (array[(int)itemIndex2] == 0)
					{
						this.itemAcquisitionOrder.Add(itemIndex);
					}
					array[(int)itemIndex2] += num;
				}
			}
			base.SetDirtyBit(1U);
			base.SetDirtyBit(8U);
			this.HandleInventoryChanged();
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000AD64C File Offset: 0x000AB84C
		[Server]
		private void AddItemAcquisitionOrderFrom([NotNull] List<ItemIndex> otherItemAcquisitionOrder)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::AddItemAcquisitionOrderFrom(System.Collections.Generic.List`1<RoR2.ItemIndex>)' called on client");
				return;
			}
			int[] array = ItemCatalog.RequestItemStackArray();
			for (int i = 0; i < this.itemAcquisitionOrder.Count; i++)
			{
				ItemIndex itemIndex = this.itemAcquisitionOrder[i];
				array[(int)itemIndex] = 1;
			}
			int j = 0;
			int count = otherItemAcquisitionOrder.Count;
			while (j < count)
			{
				ItemIndex itemIndex2 = otherItemAcquisitionOrder[j];
				ref int ptr = ref array[(int)itemIndex2];
				if (ptr == 0)
				{
					ptr = 1;
					this.itemAcquisitionOrder.Add(itemIndex2);
				}
				j++;
			}
			ItemCatalog.ReturnItemStackArray(array);
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000AD6DF File Offset: 0x000AB8DF
		[Server]
		public void CopyItemsFrom([NotNull] Inventory other)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::CopyItemsFrom(RoR2.Inventory)' called on client");
				return;
			}
			this.CopyItemsFrom(other, Inventory.defaultItemCopyFilterDelegate);
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000AD704 File Offset: 0x000AB904
		[Server]
		public void CopyItemsFrom([NotNull] Inventory other, [NotNull] Func<ItemIndex, bool> filter)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::CopyItemsFrom(RoR2.Inventory,System.Func`2<RoR2.ItemIndex,System.Boolean>)' called on client");
				return;
			}
			this.itemAcquisitionOrder.Clear();
			int[] array = this.itemStacks;
			int num = 0;
			ArrayUtils.SetAll<int>(array, num);
			this.AddItemsFrom(other);
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000AD748 File Offset: 0x000AB948
		[Server]
		public void ShrineRestackInventory([NotNull] Xoroshiro128Plus rng)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.Inventory::ShrineRestackInventory(Xoroshiro128Plus)' called on client");
				return;
			}
			List<ItemIndex> list = new List<ItemIndex>();
			bool flag = false;
			foreach (ItemTierDef itemTierDef in ItemTierCatalog.allItemTierDefs)
			{
				if (itemTierDef.canRestack)
				{
					int num = 0;
					list.Clear();
					for (int i = 0; i < this.itemStacks.Length; i++)
					{
						if (this.itemStacks[i] > 0)
						{
							ItemIndex itemIndex = (ItemIndex)i;
							ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
							if (itemTierDef.tier == itemDef.tier)
							{
								num += this.itemStacks[i];
								list.Add(itemIndex);
								this.itemAcquisitionOrder.Remove(itemIndex);
								this.ResetItem(itemIndex);
							}
						}
					}
					if (list.Count > 0)
					{
						this.GiveItem(rng.NextElementUniform<ItemIndex>(list), num);
						flag = true;
					}
				}
			}
			if (flag)
			{
				base.SetDirtyBit(8U);
			}
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x000AD860 File Offset: 0x000ABA60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetItemCount(ItemIndex itemIndex)
		{
			return ArrayUtils.GetSafe<int>(this.itemStacks, (int)itemIndex);
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x000AD86E File Offset: 0x000ABA6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int GetItemCount(ItemDef itemDef)
		{
			return this.GetItemCount((itemDef != null) ? itemDef.itemIndex : ItemIndex.None);
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x000AD884 File Offset: 0x000ABA84
		public bool HasAtLeastXTotalItemsOfTier(ItemTier itemTier, int x)
		{
			int num = 0;
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				if (ItemCatalog.GetItemDef(itemIndex).tier == itemTier)
				{
					num += this.GetItemCount(itemIndex);
					if (num >= x)
					{
						return true;
					}
				}
				itemIndex++;
			}
			return false;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x000AD8C4 File Offset: 0x000ABAC4
		public int GetTotalItemCountOfTier(ItemTier itemTier)
		{
			int num = 0;
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				if (ItemCatalog.GetItemDef(itemIndex).tier == itemTier)
				{
					num += this.GetItemCount(itemIndex);
				}
				itemIndex++;
			}
			return num;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000AD8FE File Offset: 0x000ABAFE
		public void WriteItemStacks(int[] output)
		{
			Array.Copy(this.itemStacks, output, output.Length);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000AD910 File Offset: 0x000ABB10
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			byte b = reader.ReadByte();
			bool flag = (b & 1) > 0;
			bool flag2 = (b & 4) > 0;
			bool flag3 = (b & 8) > 0;
			bool flag4 = (b & 16) > 0;
			if (flag)
			{
				reader.ReadItemStacks(this.itemStacks);
			}
			if (flag2)
			{
				this.infusionBonus = reader.ReadPackedUInt32();
			}
			if (flag3)
			{
				uint num = reader.ReadPackedUInt32();
				this.itemAcquisitionOrder.Clear();
				this.itemAcquisitionOrder.Capacity = (int)num;
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					ItemIndex item = (ItemIndex)reader.ReadPackedUInt32();
					this.itemAcquisitionOrder.Add(item);
				}
			}
			if (flag4)
			{
				uint num3 = (uint)reader.ReadByte();
				for (uint num4 = 0U; num4 < num3; num4 += 1U)
				{
					this.SetEquipmentInternal(EquipmentState.Deserialize(reader), num4);
				}
				this.activeEquipmentSlot = reader.ReadByte();
			}
			if (flag || flag4 || flag2)
			{
				this.HandleInventoryChanged();
			}
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000AD9EC File Offset: 0x000ABBEC
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			uint num = base.syncVarDirtyBits;
			if (initialState)
			{
				num = 29U;
			}
			for (int i = 0; i < this.equipmentStateSlots.Length; i++)
			{
				if (this.equipmentStateSlots[i].dirty)
				{
					num |= 16U;
					break;
				}
			}
			bool flag = (num & 1U) > 0U;
			bool flag2 = (num & 4U) > 0U;
			bool flag3 = (num & 8U) > 0U;
			bool flag4 = (num & 16U) > 0U;
			writer.Write((byte)num);
			if (flag)
			{
				writer.WriteItemStacks(this.itemStacks);
			}
			if (flag2)
			{
				writer.WritePackedUInt32(this.infusionBonus);
			}
			if (flag3)
			{
				int count = this.itemAcquisitionOrder.Count;
				writer.WritePackedUInt32((uint)count);
				for (int j = 0; j < count; j++)
				{
					writer.WritePackedUInt32((uint)this.itemAcquisitionOrder[j]);
				}
			}
			if (flag4)
			{
				writer.Write((byte)this.equipmentStateSlots.Length);
				for (int k = 0; k < this.equipmentStateSlots.Length; k++)
				{
					EquipmentState.Serialize(writer, this.equipmentStateSlots[k]);
				}
				writer.Write(this.activeEquipmentSlot);
			}
			if (!initialState)
			{
				for (int l = 0; l < this.equipmentStateSlots.Length; l++)
				{
					this.equipmentStateSlots[l].dirty = false;
				}
			}
			return !initialState && num > 0U;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000ADB5C File Offset: 0x000ABD5C
		static Inventory()
		{
			NetworkBehaviour.RegisterRpcDelegate(typeof(Inventory), Inventory.kRpcRpcItemAdded, new NetworkBehaviour.CmdDelegate(Inventory.InvokeRpcRpcItemAdded));
			NetworkCRC.RegisterBehaviour("Inventory", 0);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000ADBB3 File Offset: 0x000ABDB3
		protected static void InvokeRpcRpcItemAdded(NetworkBehaviour obj, NetworkReader reader)
		{
			if (!NetworkClient.active)
			{
				Debug.LogError("RPC RpcItemAdded called on server.");
				return;
			}
			((Inventory)obj).RpcItemAdded((ItemIndex)reader.ReadInt32());
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x000ADBDC File Offset: 0x000ABDDC
		public void CallRpcItemAdded(ItemIndex itemIndex)
		{
			if (!NetworkServer.active)
			{
				Debug.LogError("RPC Function RpcItemAdded called on client.");
				return;
			}
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.Write(0);
			networkWriter.Write((short)((ushort)2));
			networkWriter.WritePackedUInt32((uint)Inventory.kRpcRpcItemAdded);
			networkWriter.Write(base.GetComponent<NetworkIdentity>().netId);
			networkWriter.Write((int)itemIndex);
			this.SendRPCInternal(networkWriter, 0, "RpcItemAdded");
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002BA2 RID: 11170
		private int[] itemStacks = ItemCatalog.RequestItemStackArray();

		// Token: 0x04002BA3 RID: 11171
		public readonly List<ItemIndex> itemAcquisitionOrder = new List<ItemIndex>();

		// Token: 0x04002BA4 RID: 11172
		private const uint itemListDirtyBit = 1U;

		// Token: 0x04002BA5 RID: 11173
		private const uint infusionBonusDirtyBit = 4U;

		// Token: 0x04002BA6 RID: 11174
		private const uint itemAcquisitionOrderDirtyBit = 8U;

		// Token: 0x04002BA7 RID: 11175
		private const uint equipmentDirtyBit = 16U;

		// Token: 0x04002BA8 RID: 11176
		private const uint allDirtyBits = 29U;

		// Token: 0x04002BAC RID: 11180
		private EquipmentState[] equipmentStateSlots = Array.Empty<EquipmentState>();

		// Token: 0x04002BB1 RID: 11185
		public static readonly Func<ItemIndex, bool> defaultItemCopyFilterDelegate = new Func<ItemIndex, bool>(Inventory.DefaultItemCopyFilter);

		// Token: 0x04002BB2 RID: 11186
		private static int kRpcRpcItemAdded = 1978705787;
	}
}
