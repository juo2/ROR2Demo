using System;
using System.Collections.Generic;
using EntityStates;
using EntityStates.Barrel;
using RoR2.Networking;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x0200064F RID: 1615
	public class ChestBehavior : NetworkBehaviour, IChestBehavior
	{
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x00086811 File Offset: 0x00084A11
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x00086819 File Offset: 0x00084A19
		public PickupIndex dropPickup { get; private set; } = PickupIndex.none;

		// Token: 0x06001F4D RID: 8013 RVA: 0x000756DA File Offset: 0x000738DA
		public override int GetNetworkChannel()
		{
			return QosChannelIndex.defaultReliable.intVal;
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x00086824 File Offset: 0x00084A24
		[Server]
		public void Roll()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ChestBehavior::Roll()' called on client");
				return;
			}
			if (this.dropTable)
			{
				this.dropPickup = this.dropTable.GenerateDrop(this.rng);
				return;
			}
			UnityEvent unityEvent = this.dropRoller;
			if (unityEvent == null)
			{
				return;
			}
			unityEvent.Invoke();
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0008687B File Offset: 0x00084A7B
		[Server]
		private void PickFromList(List<PickupIndex> dropList)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ChestBehavior::PickFromList(System.Collections.Generic.List`1<RoR2.PickupIndex>)' called on client");
				return;
			}
			this.dropPickup = PickupIndex.none;
			if (dropList != null && dropList.Count > 0)
			{
				this.dropPickup = this.rng.NextElementUniform<PickupIndex>(dropList);
			}
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x000868BC File Offset: 0x00084ABC
		[Server]
		[Obsolete("Just use a drop table")]
		public void RollItem()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ChestBehavior::RollItem()' called on client");
				return;
			}
			ChestBehavior.<>c__DisplayClass23_0 CS$<>8__locals1 = new ChestBehavior.<>c__DisplayClass23_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.selector = new WeightedSelection<List<PickupIndex>>(8);
			List<PickupIndex> list = new List<PickupIndex>();
			list.Add(PickupCatalog.FindPickupIndex(RoR2Content.MiscPickups.LunarCoin.miscPickupIndex));
			CS$<>8__locals1.<RollItem>g__Add|1(Run.instance.availableTier1DropList, this.tier1Chance);
			CS$<>8__locals1.<RollItem>g__Add|1(Run.instance.availableTier2DropList, this.tier2Chance);
			CS$<>8__locals1.<RollItem>g__Add|1(Run.instance.availableTier3DropList, this.tier3Chance);
			CS$<>8__locals1.<RollItem>g__Add|1(Run.instance.availableLunarCombinedDropList, this.lunarChance);
			CS$<>8__locals1.<RollItem>g__Add|1(list, this.lunarCoinChance);
			List<PickupIndex> dropList = CS$<>8__locals1.selector.Evaluate(this.rng.nextNormalizedFloat);
			this.PickFromList(dropList);
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x00086993 File Offset: 0x00084B93
		[Server]
		[Obsolete("Just use a drop table")]
		public void RollEquipment()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ChestBehavior::RollEquipment()' called on client");
				return;
			}
			this.PickFromList(Run.instance.availableEquipmentDropList);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x000869BA File Offset: 0x00084BBA
		private void Awake()
		{
			if (this.dropTransform == null)
			{
				this.dropTransform = base.transform;
			}
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x000869D8 File Offset: 0x00084BD8
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus(Run.instance.treasureRng.nextUlong);
				this.dropCount = this.rng.RangeInt(this.minDropCount, Math.Max(this.minDropCount + 1, this.maxDropCount + 1));
				this.Roll();
			}
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x00086A38 File Offset: 0x00084C38
		[Server]
		public void Open()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ChestBehavior::Open()' called on client");
				return;
			}
			EntityStateMachine component = base.GetComponent<EntityStateMachine>();
			if (component)
			{
				component.SetNextState(EntityStateCatalog.InstantiateState(this.openState));
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00086A7C File Offset: 0x00084C7C
		[Server]
		public void ItemDrop()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.ChestBehavior::ItemDrop()' called on client");
				return;
			}
			if (this.dropPickup == PickupIndex.none || this.dropCount < 1)
			{
				return;
			}
			float angle = 360f / (float)this.dropCount;
			Vector3 vector = Vector3.up * this.dropUpVelocityStrength + this.dropTransform.forward * this.dropForwardVelocityStrength;
			Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
			for (int i = 0; i < this.dropCount; i++)
			{
				PickupDropletController.CreatePickupDroplet(this.dropPickup, this.dropTransform.position + Vector3.up * 1.5f, vector);
				vector = rotation * vector;
				this.Roll();
			}
			this.dropPickup = PickupIndex.none;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00086B53 File Offset: 0x00084D53
		public bool HasRolledPickup(PickupIndex pickupIndex)
		{
			return this.dropPickup == pickupIndex;
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00086BDC File Offset: 0x00084DDC
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x040024D3 RID: 9427
		public PickupDropTable dropTable;

		// Token: 0x040024D4 RID: 9428
		public Transform dropTransform;

		// Token: 0x040024D5 RID: 9429
		public float dropUpVelocityStrength = 20f;

		// Token: 0x040024D6 RID: 9430
		public float dropForwardVelocityStrength = 2f;

		// Token: 0x040024D7 RID: 9431
		public int minDropCount = 1;

		// Token: 0x040024D8 RID: 9432
		public int maxDropCount = 1;

		// Token: 0x040024D9 RID: 9433
		public SerializableEntityStateType openState = new SerializableEntityStateType(typeof(Opening));

		// Token: 0x040024DA RID: 9434
		[Header("Deprecated")]
		[Tooltip("Deprecated.  Use DropTable instead.")]
		public float tier1Chance = 0.8f;

		// Token: 0x040024DB RID: 9435
		[Tooltip("Deprecated.  Use DropTable instead.")]
		public float tier2Chance = 0.2f;

		// Token: 0x040024DC RID: 9436
		[Tooltip("Deprecated.  Use DropTable instead.")]
		public float tier3Chance = 0.01f;

		// Token: 0x040024DD RID: 9437
		[Tooltip("Deprecated.  Use DropTable instead.")]
		public float lunarChance;

		// Token: 0x040024DE RID: 9438
		[Tooltip("Deprecated.  Use DropTable instead.")]
		public ItemTag requiredItemTag;

		// Token: 0x040024DF RID: 9439
		[Tooltip("Deprecated.  Use DropTable instead.")]
		public float lunarCoinChance;

		// Token: 0x040024E0 RID: 9440
		[Tooltip("Deprecated.  Use DropTable instead.")]
		public UnityEvent dropRoller;

		// Token: 0x040024E1 RID: 9441
		private Xoroshiro128Plus rng;

		// Token: 0x040024E2 RID: 9442
		private int dropCount;
	}
}
