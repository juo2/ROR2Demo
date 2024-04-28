using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x02000529 RID: 1321
	[CreateAssetMenu(menuName = "RoR2/SpawnCards/CharacterSpawnCard")]
	public class CharacterSpawnCard : SpawnCard, MasterSummon.IInventorySetupCallback
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x0006970F File Offset: 0x0006790F
		// (set) Token: 0x06001802 RID: 6146 RVA: 0x00069717 File Offset: 0x00067917
		[CanBeNull]
		public SerializableLoadout loadout
		{
			get
			{
				return this._loadout;
			}
			set
			{
				if (this._loadout == value)
				{
					return;
				}
				this._loadout = value;
				if (CharacterSpawnCard.loadoutAvailability.available)
				{
					this.SetLoadoutFromSerializedLoadout();
					return;
				}
				CharacterSpawnCard.loadoutAvailability.CallWhenAvailable(new Action(this.SetLoadoutFromSerializedLoadout));
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x00069753 File Offset: 0x00067953
		public void Awake()
		{
			if (!CharacterSpawnCard.loadoutAvailability.available)
			{
				CharacterSpawnCard.loadoutAvailability.CallWhenAvailable(new Action(this.SetLoadoutFromSerializedLoadout));
				return;
			}
			this.SetLoadoutFromSerializedLoadout();
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0006977E File Offset: 0x0006797E
		private void SetLoadoutFromSerializedLoadout()
		{
			this.SetLoadout(this.loadout);
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0006978C File Offset: 0x0006798C
		private void SetLoadout([CanBeNull] SerializableLoadout serializableLoadout)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.runtimeLoadout = ((serializableLoadout != null && !serializableLoadout.isEmpty) ? new Loadout() : null);
			if (this.runtimeLoadout != null)
			{
				serializableLoadout.Apply(this.runtimeLoadout);
			}
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x000697C3 File Offset: 0x000679C3
		[CanBeNull]
		protected virtual Loadout GetRuntimeLoadout()
		{
			return this.runtimeLoadout;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00003BE8 File Offset: 0x00001DE8
		[CanBeNull]
		protected virtual Action<CharacterMaster> GetPreSpawnSetupCallback()
		{
			return null;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000697CC File Offset: 0x000679CC
		protected override void Spawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest directorSpawnRequest, ref SpawnCard.SpawnResult result)
		{
			MasterSummon masterSummon = new MasterSummon
			{
				masterPrefab = this.prefab,
				position = position,
				rotation = rotation,
				summonerBodyObject = directorSpawnRequest.summonerBodyObject,
				teamIndexOverride = directorSpawnRequest.teamIndexOverride,
				ignoreTeamMemberLimit = directorSpawnRequest.ignoreTeamMemberLimit,
				loadout = this.GetRuntimeLoadout(),
				inventoryToCopy = this.inventoryToCopy,
				inventoryItemCopyFilter = this.inventoryItemCopyFilter,
				inventorySetupCallback = this,
				preSpawnSetupCallback = this.GetPreSpawnSetupCallback(),
				useAmbientLevel = new bool?(true)
			};
			CharacterMaster characterMaster = masterSummon.Perform();
			result.spawnedInstance = ((characterMaster != null) ? characterMaster.gameObject : null);
			result.success = result.spawnedInstance;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0006988C File Offset: 0x00067A8C
		[SystemInitializer(new Type[]
		{
			typeof(Loadout)
		})]
		private static void Init()
		{
			CharacterSpawnCard.loadoutAvailability.MakeAvailable();
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00069898 File Offset: 0x00067A98
		void MasterSummon.IInventorySetupCallback.SetupSummonedInventory(MasterSummon masterSummon, Inventory summonedInventory)
		{
			this.SetupSummonedInventory(masterSummon, summonedInventory);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x000698A4 File Offset: 0x00067AA4
		protected virtual void SetupSummonedInventory(MasterSummon masterSummon, Inventory summonedInventory)
		{
			for (int i = 0; i < this.equipmentToGrant.Length; i++)
			{
				EquipmentState equipment = summonedInventory.GetEquipment((uint)i);
				EquipmentDef equipmentDef = this.equipmentToGrant[i];
				summonedInventory.SetEquipment(new EquipmentState((equipmentDef != null) ? equipmentDef.equipmentIndex : EquipmentIndex.None, equipment.chargeFinishTime, equipment.charges), (uint)i);
			}
			for (int j = 0; j < this.itemsToGrant.Length; j++)
			{
				ItemCountPair itemCountPair = this.itemsToGrant[j];
				summonedInventory.GiveItem(itemCountPair.itemDef, itemCountPair.count);
			}
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0006992A File Offset: 0x00067B2A
		public void OnValidate()
		{
			if (this.occupyPosition)
			{
				Debug.LogErrorFormat("{0} OccupyPosition=1.This is only intended for limited spawns, and will prevent spawning on this node in the future! Are ya sure? ", new object[]
				{
					this
				});
			}
		}

		// Token: 0x04001DAE RID: 7598
		public bool noElites;

		// Token: 0x04001DAF RID: 7599
		public bool forbiddenAsBoss;

		// Token: 0x04001DB0 RID: 7600
		[CanBeNull]
		[Tooltip("The loadout for any summoned character to use.")]
		[FormerlySerializedAs("loadout")]
		[SerializeField]
		private SerializableLoadout _loadout;

		// Token: 0x04001DB1 RID: 7601
		[Tooltip("The inventory from which to initially paste into any summoned character's inventory. Will skip certain non-bequeathable items. This is usually not a good idea to set up in the editor, and is more reserved for runtime.")]
		[CanBeNull]
		public Inventory inventoryToCopy;

		// Token: 0x04001DB2 RID: 7602
		[CanBeNull]
		public Func<ItemIndex, bool> inventoryItemCopyFilter;

		// Token: 0x04001DB3 RID: 7603
		[NotNull]
		[Tooltip("The set of equipment to grant to any summoned character, after inventory copy.")]
		public EquipmentDef[] equipmentToGrant = Array.Empty<EquipmentDef>();

		// Token: 0x04001DB4 RID: 7604
		[NotNull]
		[Tooltip("The set of items to grant to any summoned character, after inventory copy.")]
		public ItemCountPair[] itemsToGrant = Array.Empty<ItemCountPair>();

		// Token: 0x04001DB5 RID: 7605
		[CanBeNull]
		protected Loadout runtimeLoadout;

		// Token: 0x04001DB6 RID: 7606
		private static ResourceAvailability loadoutAvailability;
	}
}
