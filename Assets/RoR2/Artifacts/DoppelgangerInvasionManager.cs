using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Artifacts
{
	// Token: 0x02000E65 RID: 3685
	public class DoppelgangerInvasionManager : MonoBehaviour
	{
		// Token: 0x14000114 RID: 276
		// (add) Token: 0x06005458 RID: 21592 RVA: 0x0015BED8 File Offset: 0x0015A0D8
		// (remove) Token: 0x06005459 RID: 21593 RVA: 0x0015BF0C File Offset: 0x0015A10C
		public static event Action<DamageReport> onDoppelgangerDeath;

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x0600545A RID: 21594 RVA: 0x0015BF3F File Offset: 0x0015A13F
		private bool artifactIsEnabled
		{
			get
			{
				return RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.shadowCloneArtifactDef);
			}
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x0015BF50 File Offset: 0x0015A150
		[SystemInitializer(new Type[]
		{
			typeof(ArtifactCatalog)
		})]
		private static void Init()
		{
			Run.onRunStartGlobal += DoppelgangerInvasionManager.OnRunStartGlobal;
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x0015BF63 File Offset: 0x0015A163
		private static void OnRunStartGlobal(Run run)
		{
			if (NetworkServer.active)
			{
				run.gameObject.AddComponent<DoppelgangerInvasionManager>();
			}
		}

		// Token: 0x0600545D RID: 21597 RVA: 0x0015BF78 File Offset: 0x0015A178
		private void Start()
		{
			this.run = base.GetComponent<Run>();
			this.seed = this.run.seed;
			this.treasureRng = new Xoroshiro128Plus(this.seed);
			this.dropTable = LegacyResourcesAPI.Load<PickupDropTable>("DropTables/dtDoppelganger");
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x0015BFB8 File Offset: 0x0015A1B8
		private void OnEnable()
		{
			GlobalEventManager.onCharacterDeathGlobal += this.OnCharacterDeathGlobal;
			ArtifactTrialMissionController.onShellTakeDamageServer += this.OnArtifactTrialShellTakeDamageServer;
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x0015BFDC File Offset: 0x0015A1DC
		private void OnDisable()
		{
			ArtifactTrialMissionController.onShellTakeDamageServer -= this.OnArtifactTrialShellTakeDamageServer;
			GlobalEventManager.onCharacterDeathGlobal -= this.OnCharacterDeathGlobal;
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x0015C000 File Offset: 0x0015A200
		private void FixedUpdate()
		{
			int currentInvasionCycle = this.GetCurrentInvasionCycle();
			if (this.previousInvasionCycle < currentInvasionCycle)
			{
				this.previousInvasionCycle = currentInvasionCycle;
				if (this.artifactIsEnabled)
				{
					DoppelgangerInvasionManager.PerformInvasion(new Xoroshiro128Plus(this.seed + (ulong)((long)currentInvasionCycle)));
				}
			}
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x0015C040 File Offset: 0x0015A240
		private void OnCharacterDeathGlobal(DamageReport damageReport)
		{
			CharacterMaster victimMaster = damageReport.victimMaster;
			Inventory inventory = (victimMaster != null) ? victimMaster.inventory : null;
			if (inventory)
			{
				bool flag = damageReport.victimMaster.minionOwnership.ownerMaster;
				if (inventory.GetItemCount(RoR2Content.Items.InvadingDoppelganger) > 0 && inventory.GetItemCount(RoR2Content.Items.ExtraLife) == 0 && inventory.GetItemCount(DLC1Content.Items.ExtraLifeVoid) == 0 && !flag)
				{
					Action<DamageReport> action = DoppelgangerInvasionManager.onDoppelgangerDeath;
					if (action != null)
					{
						action(damageReport);
					}
					PickupIndex pickupIndex = this.dropTable.GenerateDrop(this.treasureRng);
					if (pickupIndex == PickupIndex.none)
					{
						return;
					}
					PickupDropletController.CreatePickupDroplet(pickupIndex, damageReport.victimBody.corePosition, Vector3.up * 20f);
				}
			}
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x0015C0FD File Offset: 0x0015A2FD
		private void OnArtifactTrialShellTakeDamageServer(ArtifactTrialMissionController missionController, DamageReport damageReport)
		{
			if (!this.artifactIsEnabled)
			{
				return;
			}
			if (!damageReport.victim.alive)
			{
				return;
			}
			DoppelgangerInvasionManager.PerformInvasion(new Xoroshiro128Plus((ulong)damageReport.victim.health));
		}

		// Token: 0x06005463 RID: 21603 RVA: 0x0015C12C File Offset: 0x0015A32C
		private int GetCurrentInvasionCycle()
		{
			return Mathf.FloorToInt(this.run.GetRunStopwatch() / this.invasionInterval);
		}

		// Token: 0x06005464 RID: 21604 RVA: 0x0015C148 File Offset: 0x0015A348
		public static void PerformInvasion(Xoroshiro128Plus rng)
		{
			for (int i = CharacterMaster.readOnlyInstancesList.Count - 1; i >= 0; i--)
			{
				CharacterMaster characterMaster = CharacterMaster.readOnlyInstancesList[i];
				if (characterMaster.teamIndex == TeamIndex.Player && characterMaster.playerCharacterMasterController)
				{
					DoppelgangerInvasionManager.CreateDoppelganger(characterMaster, rng);
				}
			}
		}

		// Token: 0x06005465 RID: 21605 RVA: 0x0015C198 File Offset: 0x0015A398
		private static void CreateDoppelganger(CharacterMaster srcCharacterMaster, Xoroshiro128Plus rng)
		{
			SpawnCard spawnCard = DoppelgangerSpawnCard.FromMaster(srcCharacterMaster);
			if (!spawnCard)
			{
				return;
			}
			Transform spawnOnTarget;
			DirectorCore.MonsterSpawnDistance input;
			if (TeleporterInteraction.instance)
			{
				spawnOnTarget = TeleporterInteraction.instance.transform;
				input = DirectorCore.MonsterSpawnDistance.Close;
			}
			else
			{
				spawnOnTarget = srcCharacterMaster.GetBody().coreTransform;
				input = DirectorCore.MonsterSpawnDistance.Far;
			}
			DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule
			{
				spawnOnTarget = spawnOnTarget,
				placementMode = DirectorPlacementRule.PlacementMode.NearestNode
			};
			DirectorCore.GetMonsterSpawnDistance(input, out directorPlacementRule.minDistance, out directorPlacementRule.maxDistance);
			DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(spawnCard, directorPlacementRule, rng);
			directorSpawnRequest.teamIndexOverride = new TeamIndex?(TeamIndex.Monster);
			directorSpawnRequest.ignoreTeamMemberLimit = true;
			CombatSquad combatSquad = null;
			DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
			directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(delegate(SpawnCard.SpawnResult result)
			{
				if (!combatSquad)
				{
					combatSquad = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Encounters/ShadowCloneEncounter")).GetComponent<CombatSquad>();
				}
				combatSquad.AddMember(result.spawnedInstance.GetComponent<CharacterMaster>());
			}));
			DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
			if (combatSquad)
			{
				NetworkServer.Spawn(combatSquad.gameObject);
			}
			UnityEngine.Object.Destroy(spawnCard);
		}

		// Token: 0x0400501E RID: 20510
		private readonly float invasionInterval = 600f;

		// Token: 0x0400501F RID: 20511
		private int previousInvasionCycle;

		// Token: 0x04005020 RID: 20512
		private ulong seed;

		// Token: 0x04005021 RID: 20513
		private Run run;

		// Token: 0x04005022 RID: 20514
		private Xoroshiro128Plus treasureRng;

		// Token: 0x04005023 RID: 20515
		private PickupDropTable dropTable;
	}
}
