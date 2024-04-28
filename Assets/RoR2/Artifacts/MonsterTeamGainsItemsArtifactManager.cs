using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Artifacts
{
	// Token: 0x02000E6C RID: 3692
	public static class MonsterTeamGainsItemsArtifactManager
	{
		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600547D RID: 21629 RVA: 0x0015C6B1 File Offset: 0x0015A8B1
		private static ArtifactDef myArtifact
		{
			get
			{
				return RoR2Content.Artifacts.monsterTeamGainsItemsArtifactDef;
			}
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x0015C6B8 File Offset: 0x0015A8B8
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog),
			typeof(ItemCatalog)
		})]
		private static void Init()
		{
			RunArtifactManager.onArtifactEnabledGlobal += MonsterTeamGainsItemsArtifactManager.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal += MonsterTeamGainsItemsArtifactManager.OnArtifactDisabled;
			Run.onRunStartGlobal += MonsterTeamGainsItemsArtifactManager.OnRunStartGlobal;
			Run.onRunDestroyGlobal += MonsterTeamGainsItemsArtifactManager.OnRunDestroyGlobal;
			Stage.onServerStageBegin += MonsterTeamGainsItemsArtifactManager.OnServerStageBegin;
			SceneDirector.onPrePopulateSceneServer += MonsterTeamGainsItemsArtifactManager.OnPrePopulateSceneServer;
			MonsterTeamGainsItemsArtifactManager.networkedInventoryPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MonsterTeamGainsItemsArtifactInventory");
			PickupDropTable pickupDropTable = LegacyResourcesAPI.Load<PickupDropTable>("DropTables/dtMonsterTeamTier1Item");
			PickupDropTable pickupDropTable2 = LegacyResourcesAPI.Load<PickupDropTable>("DropTables/dtMonsterTeamTier2Item");
			PickupDropTable pickupDropTable3 = LegacyResourcesAPI.Load<PickupDropTable>("DropTables/dtMonsterTeamTier3Item");
			MonsterTeamGainsItemsArtifactManager.dropPattern = new PickupDropTable[]
			{
				pickupDropTable,
				pickupDropTable,
				pickupDropTable2,
				pickupDropTable2,
				pickupDropTable3
			};
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x0015C77C File Offset: 0x0015A97C
		private static void OnRunStartGlobal(Run run)
		{
			if (NetworkServer.active)
			{
				MonsterTeamGainsItemsArtifactManager.currentItemIterator = 0;
				MonsterTeamGainsItemsArtifactManager.treasureRng.ResetSeed(run.seed);
				MonsterTeamGainsItemsArtifactManager.monsterTeamInventory = UnityEngine.Object.Instantiate<GameObject>(MonsterTeamGainsItemsArtifactManager.networkedInventoryPrefab).GetComponent<Inventory>();
				MonsterTeamGainsItemsArtifactManager.monsterTeamInventory.GetComponent<TeamFilter>().teamIndex = TeamIndex.Monster;
				NetworkServer.Spawn(MonsterTeamGainsItemsArtifactManager.monsterTeamInventory.gameObject);
				MonsterTeamGainsItemsArtifactManager.EnsureMonsterItemCountMatchesStageCount();
			}
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0015C7DE File Offset: 0x0015A9DE
		private static void OnRunDestroyGlobal(Run run)
		{
			MonsterTeamGainsItemsArtifactManager.treasureRng.ResetSeed(0UL);
			if (MonsterTeamGainsItemsArtifactManager.monsterTeamInventory)
			{
				NetworkServer.Destroy(MonsterTeamGainsItemsArtifactManager.monsterTeamInventory.gameObject);
			}
			MonsterTeamGainsItemsArtifactManager.monsterTeamInventory = null;
		}

		// Token: 0x06005481 RID: 21633 RVA: 0x0015C810 File Offset: 0x0015AA10
		private static void GrantMonsterTeamItem()
		{
			PickupDropTable pickupDropTable = MonsterTeamGainsItemsArtifactManager.dropPattern[MonsterTeamGainsItemsArtifactManager.currentItemIterator++ % MonsterTeamGainsItemsArtifactManager.dropPattern.Length];
			if (!pickupDropTable)
			{
				return;
			}
			PickupIndex pickupIndex = pickupDropTable.GenerateDrop(MonsterTeamGainsItemsArtifactManager.treasureRng);
			if (pickupIndex != PickupIndex.none)
			{
				PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
				if (pickupDef != null)
				{
					MonsterTeamGainsItemsArtifactManager.monsterTeamInventory.GiveItem(pickupDef.itemIndex, 1);
				}
			}
		}

		// Token: 0x06005482 RID: 21634 RVA: 0x0015C876 File Offset: 0x0015AA76
		private static void EnsureMonsterItemCountMatchesStageCount()
		{
			MonsterTeamGainsItemsArtifactManager.EnsureMonsterTeamItemCount(Run.instance.stageClearCount + 1);
		}

		// Token: 0x06005483 RID: 21635 RVA: 0x0015C889 File Offset: 0x0015AA89
		private static void OnServerStageBegin(Stage stage)
		{
			MonsterTeamGainsItemsArtifactManager.EnsureMonsterItemCountMatchesStageCount();
		}

		// Token: 0x06005484 RID: 21636 RVA: 0x0015C889 File Offset: 0x0015AA89
		private static void OnPrePopulateSceneServer(SceneDirector sceneDirector)
		{
			MonsterTeamGainsItemsArtifactManager.EnsureMonsterItemCountMatchesStageCount();
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0015C890 File Offset: 0x0015AA90
		private static void EnsureMonsterTeamItemCount(int itemCount)
		{
			while (MonsterTeamGainsItemsArtifactManager.currentItemIterator < itemCount)
			{
				MonsterTeamGainsItemsArtifactManager.GrantMonsterTeamItem();
			}
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0015C8A1 File Offset: 0x0015AAA1
		private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != MonsterTeamGainsItemsArtifactManager.myArtifact)
			{
				return;
			}
			if (NetworkServer.active)
			{
				SpawnCard.onSpawnedServerGlobal += MonsterTeamGainsItemsArtifactManager.OnServerCardSpawnedGlobal;
			}
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0015C8C9 File Offset: 0x0015AAC9
		private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != MonsterTeamGainsItemsArtifactManager.myArtifact)
			{
				return;
			}
			SpawnCard.onSpawnedServerGlobal -= MonsterTeamGainsItemsArtifactManager.OnServerCardSpawnedGlobal;
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x0015C8EC File Offset: 0x0015AAEC
		private static void OnServerCardSpawnedGlobal(SpawnCard.SpawnResult spawnResult)
		{
			CharacterMaster characterMaster = spawnResult.spawnedInstance ? spawnResult.spawnedInstance.GetComponent<CharacterMaster>() : null;
			if (!characterMaster)
			{
				return;
			}
			if (characterMaster.teamIndex != TeamIndex.Monster)
			{
				return;
			}
			characterMaster.inventory.AddItemsFrom(MonsterTeamGainsItemsArtifactManager.monsterTeamInventory);
		}

		// Token: 0x0400502C RID: 20524
		private static GameObject networkedInventoryPrefab;

		// Token: 0x0400502D RID: 20525
		private static Inventory monsterTeamInventory;

		// Token: 0x0400502E RID: 20526
		private static readonly Xoroshiro128Plus treasureRng = new Xoroshiro128Plus(0UL);

		// Token: 0x0400502F RID: 20527
		private static PickupDropTable[] dropPattern;

		// Token: 0x04005030 RID: 20528
		private static int currentItemIterator = 0;
	}
}
