using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using EntityStates;
using EntityStates.Barrel;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200086B RID: 2155
	public class ScavBackpackBehavior : NetworkBehaviour
	{
		// Token: 0x06002F27 RID: 12071 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x000C90C0 File Offset: 0x000C72C0
		[Server]
		private void PickFromList(List<PickupIndex> dropList)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ScavBackpackBehavior::PickFromList(System.Collections.Generic.List`1<RoR2.PickupIndex>)' called on client");
				return;
			}
			this.dropPickup = PickupIndex.none;
			if (dropList != null && dropList.Count > 0)
			{
				this.dropPickup = Run.instance.treasureRng.NextElementUniform<PickupIndex>(dropList);
			}
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x000C9110 File Offset: 0x000C7310
		[Server]
		public void RollItem()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ScavBackpackBehavior::RollItem()' called on client");
				return;
			}
			WeightedSelection<List<PickupIndex>> weightedSelection = new WeightedSelection<List<PickupIndex>>(8);
			weightedSelection.AddChoice(Run.instance.availableTier1DropList.Where(new Func<PickupIndex, bool>(this.<RollItem>g__RollItemTest|12_0)).ToList<PickupIndex>(), this.tier1Chance);
			weightedSelection.AddChoice(Run.instance.availableTier2DropList.Where(new Func<PickupIndex, bool>(this.<RollItem>g__RollItemTest|12_0)).ToList<PickupIndex>(), this.tier2Chance);
			weightedSelection.AddChoice(Run.instance.availableTier3DropList.Where(new Func<PickupIndex, bool>(this.<RollItem>g__RollItemTest|12_0)).ToList<PickupIndex>(), this.tier3Chance);
			weightedSelection.AddChoice(Run.instance.availableLunarCombinedDropList.Where(new Func<PickupIndex, bool>(this.<RollItem>g__RollItemTest|12_0)).ToList<PickupIndex>(), this.lunarChance);
			List<PickupIndex> dropList = weightedSelection.Evaluate(Run.instance.treasureRng.nextNormalizedFloat);
			this.PickFromList(dropList);
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x000C9204 File Offset: 0x000C7404
		[Server]
		public void RollEquipment()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ScavBackpackBehavior::RollEquipment()' called on client");
				return;
			}
			this.PickFromList(Run.instance.availableEquipmentDropList);
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x000C922B File Offset: 0x000C742B
		private void Start()
		{
			if (NetworkServer.active)
			{
				if (this.dropRoller != null)
				{
					this.dropRoller.Invoke();
					return;
				}
				Debug.LogFormat("Chest {0} has no item roller assigned!", Array.Empty<object>());
			}
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000C9258 File Offset: 0x000C7458
		[Server]
		public void Open()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ScavBackpackBehavior::Open()' called on client");
				return;
			}
			EntityStateMachine component = base.GetComponent<EntityStateMachine>();
			if (component)
			{
				component.SetNextState(EntityStateCatalog.InstantiateState(this.openState));
			}
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000C929C File Offset: 0x000C749C
		[Server]
		public void ItemDrop()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ScavBackpackBehavior::ItemDrop()' called on client");
				return;
			}
			if (this.dropPickup == PickupIndex.none)
			{
				return;
			}
			PickupDropletController.CreatePickupDroplet(this.dropPickup, base.transform.position + Vector3.up * 1.5f, Vector3.up * 20f + base.transform.forward * 2f);
			this.dropPickup = PickupIndex.none;
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x000C9384 File Offset: 0x000C7584
		[CompilerGenerated]
		private bool <RollItem>g__RollItemTest|12_0(PickupIndex pickupIndex)
		{
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			ItemDef itemDef = ItemCatalog.GetItemDef((pickupDef != null) ? pickupDef.itemIndex : ItemIndex.None);
			return itemDef != null && itemDef.ContainsTag(this.requiredItemTag);
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000C93B0 File Offset: 0x000C75B0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04003112 RID: 12562
		private PickupIndex dropPickup = PickupIndex.none;

		// Token: 0x04003113 RID: 12563
		public float tier1Chance = 0.8f;

		// Token: 0x04003114 RID: 12564
		public float tier2Chance = 0.2f;

		// Token: 0x04003115 RID: 12565
		public float tier3Chance = 0.01f;

		// Token: 0x04003116 RID: 12566
		public float lunarChance;

		// Token: 0x04003117 RID: 12567
		public int totalItems;

		// Token: 0x04003118 RID: 12568
		public float delayBetweenItems;

		// Token: 0x04003119 RID: 12569
		public ItemTag requiredItemTag;

		// Token: 0x0400311A RID: 12570
		public UnityEvent dropRoller;

		// Token: 0x0400311B RID: 12571
		public SerializableEntityStateType openState = new SerializableEntityStateType(typeof(Opening));
	}
}
