using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Artifacts
{
	// Token: 0x02000E6D RID: 3693
	public static class SacrificeArtifactManager
	{
		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600548A RID: 21642 RVA: 0x0015C94C File Offset: 0x0015AB4C
		private static ArtifactDef myArtifact
		{
			get
			{
				return RoR2Content.Artifacts.sacrificeArtifactDef;
			}
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x0015C954 File Offset: 0x0015AB54
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			SacrificeArtifactManager.dropTable = LegacyResourcesAPI.Load<PickupDropTable>("DropTables/dtSacrificeArtifact");
			RunArtifactManager.onArtifactEnabledGlobal += SacrificeArtifactManager.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal += SacrificeArtifactManager.OnArtifactDisabled;
			Stage.onServerStageBegin += SacrificeArtifactManager.OnServerStageBegin;
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x0015C9A3 File Offset: 0x0015ABA3
		private static void OnServerStageBegin(Stage stage)
		{
			SacrificeArtifactManager.treasureRng.ResetSeed(Run.instance.treasureRng.nextUlong);
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x0015C9C0 File Offset: 0x0015ABC0
		private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (!NetworkServer.active)
			{
				return;
			}
			if (artifactDef != SacrificeArtifactManager.myArtifact)
			{
				return;
			}
			GlobalEventManager.onCharacterDeathGlobal += SacrificeArtifactManager.OnServerCharacterDeath;
			SceneDirector.onPrePopulateSceneServer += SacrificeArtifactManager.OnPrePopulateSceneServer;
			SceneDirector.onGenerateInteractableCardSelection += SacrificeArtifactManager.OnGenerateInteractableCardSelection;
			DirectorCardCategorySelection.calcCardWeight += SacrificeArtifactManager.CalcCardWeight;
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x0015CA28 File Offset: 0x0015AC28
		private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != SacrificeArtifactManager.myArtifact)
			{
				return;
			}
			SceneDirector.onGenerateInteractableCardSelection -= SacrificeArtifactManager.OnGenerateInteractableCardSelection;
			SceneDirector.onPrePopulateSceneServer -= SacrificeArtifactManager.OnPrePopulateSceneServer;
			GlobalEventManager.onCharacterDeathGlobal -= SacrificeArtifactManager.OnServerCharacterDeath;
			DirectorCardCategorySelection.calcCardWeight -= SacrificeArtifactManager.CalcCardWeight;
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x0015CA88 File Offset: 0x0015AC88
		private static void CalcCardWeight(DirectorCard card, ref float weight)
		{
			InteractableSpawnCard interactableSpawnCard = card.spawnCard as InteractableSpawnCard;
			if (interactableSpawnCard != null)
			{
				weight *= interactableSpawnCard.weightScalarWhenSacrificeArtifactEnabled;
			}
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x0015CAB5 File Offset: 0x0015ACB5
		private static void OnGenerateInteractableCardSelection(SceneDirector sceneDirector, DirectorCardCategorySelection dccs)
		{
			dccs.RemoveCardsThatFailFilter(new Predicate<DirectorCard>(SacrificeArtifactManager.<>c.<>9.<OnGenerateInteractableCardSelection>g__IsNotChest|9_0));
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0015CACD File Offset: 0x0015ACCD
		private static void OnPrePopulateSceneServer(SceneDirector sceneDirector)
		{
			sceneDirector.onPopulateCreditMultiplier *= 0.5f;
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0015CAE4 File Offset: 0x0015ACE4
		private static void OnServerCharacterDeath(DamageReport damageReport)
		{
			if (!damageReport.victimMaster)
			{
				return;
			}
			if (damageReport.attackerTeamIndex == damageReport.victimTeamIndex && damageReport.victimMaster.minionOwnership.ownerMaster)
			{
				return;
			}
			float expAdjustedDropChancePercent = Util.GetExpAdjustedDropChancePercent(5f, damageReport.victim.gameObject);
			Debug.LogFormat("Drop chance from {0}: {1}", new object[]
			{
				damageReport.victimBody,
				expAdjustedDropChancePercent
			});
			if (Util.CheckRoll(expAdjustedDropChancePercent, 0f, null))
			{
				PickupIndex pickupIndex = SacrificeArtifactManager.dropTable.GenerateDrop(SacrificeArtifactManager.treasureRng);
				if (pickupIndex != PickupIndex.none)
				{
					PickupDropletController.CreatePickupDroplet(pickupIndex, damageReport.victimBody.corePosition, Vector3.up * 20f);
				}
			}
		}

		// Token: 0x04005031 RID: 20529
		private static PickupDropTable dropTable;

		// Token: 0x04005032 RID: 20530
		private static readonly Xoroshiro128Plus treasureRng = new Xoroshiro128Plus(0UL);
	}
}
