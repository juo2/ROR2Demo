using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Artifacts
{
	// Token: 0x02000E63 RID: 3683
	public static class CommandArtifactManager
	{
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x0600544E RID: 21582 RVA: 0x0015BD50 File Offset: 0x00159F50
		private static ArtifactDef myArtifact
		{
			get
			{
				return RoR2Content.Artifacts.commandArtifactDef;
			}
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0015BD57 File Offset: 0x00159F57
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			CommandArtifactManager.commandCubePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/CommandCube");
			RunArtifactManager.onArtifactEnabledGlobal += CommandArtifactManager.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal += CommandArtifactManager.OnArtifactDisabled;
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x0015BD8A File Offset: 0x00159F8A
		private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != CommandArtifactManager.myArtifact)
			{
				return;
			}
			if (NetworkServer.active)
			{
				PickupDropletController.onDropletHitGroundServer += CommandArtifactManager.OnDropletHitGroundServer;
				SceneDirector.onGenerateInteractableCardSelection += CommandArtifactManager.OnGenerateInteractableCardSelection;
			}
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x0015BDC3 File Offset: 0x00159FC3
		private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
		{
			if (artifactDef != CommandArtifactManager.myArtifact)
			{
				return;
			}
			SceneDirector.onGenerateInteractableCardSelection -= CommandArtifactManager.OnGenerateInteractableCardSelection;
			PickupDropletController.onDropletHitGroundServer -= CommandArtifactManager.OnDropletHitGroundServer;
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x0015BDF8 File Offset: 0x00159FF8
		private static void OnDropletHitGroundServer(ref GenericPickupController.CreatePickupInfo createPickupInfo, ref bool shouldSpawn)
		{
			PickupIndex pickupIndex = createPickupInfo.pickupIndex;
			PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
			if (pickupDef == null || (pickupDef.itemIndex == ItemIndex.None && pickupDef.equipmentIndex == EquipmentIndex.None && pickupDef.itemTier == ItemTier.NoTier))
			{
				return;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(CommandArtifactManager.commandCubePrefab, createPickupInfo.position, createPickupInfo.rotation);
			gameObject.GetComponent<PickupIndexNetworker>().NetworkpickupIndex = pickupIndex;
			gameObject.GetComponent<PickupPickerController>().SetOptionsFromPickupForCommandArtifact(pickupIndex);
			NetworkServer.Spawn(gameObject);
			shouldSpawn = false;
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x0015BE68 File Offset: 0x0015A068
		private static void OnGenerateInteractableCardSelection(SceneDirector sceneDirector, DirectorCardCategorySelection dccs)
		{
			dccs.RemoveCardsThatFailFilter(new Predicate<DirectorCard>(CommandArtifactManager.<>c.<>9.<OnGenerateInteractableCardSelection>g__DoesNotOfferChoice|7_1));
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0015BE80 File Offset: 0x0015A080
		[CompilerGenerated]
		internal static bool <OnGenerateInteractableCardSelection>g__OffersChoice|7_0(DirectorCard card)
		{
			GameObject prefab = card.spawnCard.prefab;
			return prefab.GetComponent<ShopTerminalBehavior>() || prefab.GetComponent<MultiShopController>() || prefab.GetComponent<ScrapperController>();
		}

		// Token: 0x0400501B RID: 20507
		private static GameObject commandCubePrefab;
	}
}
