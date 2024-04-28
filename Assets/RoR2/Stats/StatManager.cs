using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace RoR2.Stats
{
	// Token: 0x02000ABE RID: 2750
	internal class StatManager
	{
		// Token: 0x06003F3C RID: 16188 RVA: 0x00104CE0 File Offset: 0x00102EE0
		[SystemInitializer(new Type[]
		{
			typeof(BodyCatalog),
			typeof(ItemCatalog),
			typeof(EquipmentCatalog),
			typeof(PickupCatalog)
		})]
		private static void Init()
		{
			GlobalEventManager.onServerDamageDealt += StatManager.OnDamageDealt;
			GlobalEventManager.onCharacterDeathGlobal += StatManager.OnCharacterDeath;
			GlobalEventManager.onServerCharacterExecuted += StatManager.OnCharacterExecute;
			HealthComponent.onCharacterHealServer += StatManager.OnCharacterHeal;
			Run.onPlayerFirstCreatedServer += StatManager.OnPlayerFirstCreatedServer;
			Run.onServerGameOver += StatManager.OnServerGameOver;
			Stage.onServerStageComplete += StatManager.OnServerStageComplete;
			Stage.onServerStageBegin += StatManager.OnServerStageBegin;
			Inventory.onServerItemGiven += StatManager.OnServerItemGiven;
			RoR2Application.onFixedUpdate += StatManager.ProcessEvents;
			EquipmentSlot.onServerEquipmentActivated += StatManager.OnEquipmentActivated;
			InfiniteTowerRun.onWaveInitialized += StatManager.OnInfiniteTowerWaveInitialized;
			StatManager.crocoBodyIndex = BodyCatalog.FindBodyIndex("CrocoBody");
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x00104DC8 File Offset: 0x00102FC8
		private static void OnInfiniteTowerWaveInitialized(InfiniteTowerWaveController waveController)
		{
			InfiniteTowerRun infiniteTowerRun = Run.instance as InfiniteTowerRun;
			ulong statValue = (ulong)((long)((infiniteTowerRun != null) ? infiniteTowerRun.waveIndex : 0));
			foreach (PlayerStatsComponent playerStatsComponent in PlayerStatsComponent.instancesList)
			{
				if (playerStatsComponent.playerCharacterMasterController.isConnected)
				{
					PerBodyStatDef perBodyStatDef = null;
					switch (Run.instance.selectedDifficulty)
					{
					case DifficultyIndex.Easy:
						perBodyStatDef = PerBodyStatDef.highestInfiniteTowerWaveReachedEasy;
						break;
					case DifficultyIndex.Normal:
						perBodyStatDef = PerBodyStatDef.highestInfiniteTowerWaveReachedNormal;
						break;
					case DifficultyIndex.Hard:
						perBodyStatDef = PerBodyStatDef.highestInfiniteTowerWaveReachedHard;
						break;
					}
					StatSheet currentStats = playerStatsComponent.currentStats;
					currentStats.PushStatValue(StatDef.highestInfiniteTowerWaveReached, statValue);
					if (perBodyStatDef != null)
					{
						CharacterBody body = playerStatsComponent.characterMaster.GetBody();
						if (body)
						{
							string bodyName = BodyCatalog.GetBodyName(body.bodyIndex);
							currentStats.PushStatValue(perBodyStatDef.FindStatDef(bodyName ?? ""), statValue);
						}
					}
				}
			}
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x00104ED0 File Offset: 0x001030D0
		private static void OnServerGameOver(Run run, GameEndingDef gameEndingDef)
		{
			if (gameEndingDef.isWin && run.GetType() == typeof(Run))
			{
				foreach (PlayerStatsComponent playerStatsComponent in PlayerStatsComponent.instancesList)
				{
					if (playerStatsComponent.playerCharacterMasterController.isConnected)
					{
						StatSheet currentStats = playerStatsComponent.currentStats;
						PerBodyStatDef totalWins = PerBodyStatDef.totalWins;
						GameObject bodyPrefab = playerStatsComponent.characterMaster.bodyPrefab;
						currentStats.PushStatValue(totalWins.FindStatDef(((bodyPrefab != null) ? bodyPrefab.name : null) ?? ""), 1UL);
					}
				}
			}
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x00104F84 File Offset: 0x00103184
		private static void OnPlayerFirstCreatedServer(Run run, PlayerCharacterMasterController playerCharacterMasterController)
		{
			playerCharacterMasterController.master.onBodyStart += StatManager.OnBodyFirstStart;
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x00104FA0 File Offset: 0x001031A0
		private static void OnBodyFirstStart(CharacterBody body)
		{
			CharacterMaster master = body.master;
			if (master)
			{
				master.onBodyStart -= StatManager.OnBodyFirstStart;
				PlayerCharacterMasterController component = master.GetComponent<PlayerCharacterMasterController>();
				if (component)
				{
					PlayerStatsComponent component2 = component.GetComponent<PlayerStatsComponent>();
					if (component2)
					{
						StatSheet currentStats = component2.currentStats;
						currentStats.PushStatValue(PerBodyStatDef.timesPicked.FindStatDef(body.name), 1UL);
						currentStats.PushStatValue(StatDef.totalGamesPlayed, 1UL);
					}
				}
			}
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x00105016 File Offset: 0x00103216
		public static void ForceUpdate()
		{
			StatManager.ProcessEvents();
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x0010501D File Offset: 0x0010321D
		private static void ProcessEvents()
		{
			StatManager.ProcessDamageEvents();
			StatManager.ProcessDeathEvents();
			StatManager.ProcessHealingEvents();
			StatManager.ProcessGoldEvents();
			StatManager.ProcessItemCollectedEvents();
			StatManager.ProcessCharacterUpdateEvents();
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x00105040 File Offset: 0x00103240
		public static void OnCharacterHeal(HealthComponent healthComponent, float amount, ProcChainMask procChainMask)
		{
			StatManager.healingEvents.Enqueue(new StatManager.HealingEvent
			{
				healee = healthComponent.gameObject,
				healAmount = amount
			});
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x00105078 File Offset: 0x00103278
		public static void OnDamageDealt(DamageReport damageReport)
		{
			StatManager.damageEvents.Enqueue(new StatManager.DamageEvent
			{
				attackerMaster = damageReport.attackerMaster,
				attackerBodyIndex = damageReport.attackerBodyIndex,
				attackerOwnerMaster = damageReport.attackerOwnerMaster,
				attackerOwnerBodyIndex = damageReport.attackerOwnerBodyIndex,
				victimMaster = damageReport.victimMaster,
				victimBodyIndex = damageReport.victimBodyIndex,
				victimIsElite = damageReport.victimIsElite,
				damageDealt = damageReport.damageDealt,
				dotType = damageReport.dotType
			});
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x00105110 File Offset: 0x00103310
		public static void OnCharacterExecute(DamageReport damageReport, float executionHealthLost)
		{
			StatManager.damageEvents.Enqueue(new StatManager.DamageEvent
			{
				attackerMaster = damageReport.attackerMaster,
				attackerBodyIndex = damageReport.attackerBodyIndex,
				attackerOwnerMaster = damageReport.attackerOwnerMaster,
				attackerOwnerBodyIndex = damageReport.attackerOwnerBodyIndex,
				victimMaster = damageReport.victimMaster,
				victimBodyIndex = damageReport.victimBodyIndex,
				victimIsElite = damageReport.victimIsElite,
				damageDealt = executionHealthLost,
				dotType = damageReport.dotType
			});
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x001051A0 File Offset: 0x001033A0
		public static void OnCharacterDeath(DamageReport damageReport)
		{
			DotController dotController = DotController.FindDotController(damageReport.victim.gameObject);
			bool victimWasBurning = false;
			if (dotController)
			{
				victimWasBurning = (dotController.HasDotActive(DotController.DotIndex.Burn) | dotController.HasDotActive(DotController.DotIndex.PercentBurn) | dotController.HasDotActive(DotController.DotIndex.Helfire) | dotController.HasDotActive(DotController.DotIndex.StrongerBurn));
			}
			StatManager.deathEvents.Enqueue(new StatManager.DeathEvent
			{
				damageReport = damageReport,
				victimWasBurning = victimWasBurning
			});
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x0010520C File Offset: 0x0010340C
		private static void ProcessHealingEvents()
		{
			while (StatManager.healingEvents.Count > 0)
			{
				StatManager.HealingEvent healingEvent = StatManager.healingEvents.Dequeue();
				ulong statValue = (ulong)healingEvent.healAmount;
				StatSheet statSheet = PlayerStatsComponent.FindBodyStatSheet(healingEvent.healee);
				if (statSheet != null)
				{
					statSheet.PushStatValue(StatDef.totalHealthHealed, statValue);
				}
			}
		}

		// Token: 0x06003F48 RID: 16200 RVA: 0x00105254 File Offset: 0x00103454
		private static void ProcessDamageEvents()
		{
			while (StatManager.damageEvents.Count > 0)
			{
				StatManager.DamageEvent damageEvent = StatManager.damageEvents.Dequeue();
				ulong statValue = (ulong)damageEvent.damageDealt;
				StatSheet statSheet = PlayerStatsComponent.FindMasterStatSheet(damageEvent.victimMaster);
				StatSheet statSheet2 = PlayerStatsComponent.FindMasterStatSheet(damageEvent.attackerMaster);
				StatSheet statSheet3 = PlayerStatsComponent.FindMasterStatSheet(damageEvent.attackerOwnerMaster);
				if (statSheet != null)
				{
					statSheet.PushStatValue(StatDef.totalDamageTaken, statValue);
					if (damageEvent.attackerBodyIndex != BodyIndex.None)
					{
						statSheet.PushStatValue(PerBodyStatDef.damageTakenFrom, damageEvent.attackerBodyIndex, statValue);
					}
					if (damageEvent.victimBodyIndex != BodyIndex.None)
					{
						statSheet.PushStatValue(PerBodyStatDef.damageTakenAs, damageEvent.victimBodyIndex, statValue);
					}
				}
				if (statSheet2 != null)
				{
					statSheet2.PushStatValue(StatDef.totalDamageDealt, statValue);
					statSheet2.PushStatValue(StatDef.highestDamageDealt, statValue);
					if (damageEvent.attackerBodyIndex != BodyIndex.None)
					{
						statSheet2.PushStatValue(PerBodyStatDef.damageDealtAs, damageEvent.attackerBodyIndex, statValue);
					}
					if (damageEvent.victimBodyIndex != BodyIndex.None)
					{
						statSheet2.PushStatValue(PerBodyStatDef.damageDealtTo, damageEvent.victimBodyIndex, statValue);
					}
				}
				if (statSheet3 != null)
				{
					statSheet3.PushStatValue(StatDef.totalMinionDamageDealt, statValue);
					if (damageEvent.attackerOwnerBodyIndex != BodyIndex.None)
					{
						statSheet3.PushStatValue(PerBodyStatDef.minionDamageDealtAs, damageEvent.attackerOwnerBodyIndex, statValue);
					}
				}
			}
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x00105374 File Offset: 0x00103574
		private static void ProcessDeathEvents()
		{
			while (StatManager.deathEvents.Count > 0)
			{
				StatManager.DeathEvent deathEvent = StatManager.deathEvents.Dequeue();
				DamageReport damageReport = deathEvent.damageReport;
				StatSheet statSheet = PlayerStatsComponent.FindMasterStatSheet(damageReport.victimMaster);
				StatSheet statSheet2 = PlayerStatsComponent.FindMasterStatSheet(damageReport.attackerMaster);
				StatSheet statSheet3 = PlayerStatsComponent.FindMasterStatSheet(damageReport.attackerOwnerMaster);
				if (statSheet != null)
				{
					statSheet.PushStatValue(StatDef.totalDeaths, 1UL);
					statSheet.PushStatValue(PerBodyStatDef.deathsAs, damageReport.victimBodyIndex, 1UL);
					if (damageReport.attackerBodyIndex != BodyIndex.None)
					{
						statSheet.PushStatValue(PerBodyStatDef.deathsFrom, damageReport.attackerBodyIndex, 1UL);
					}
					if (damageReport.dotType != DotController.DotIndex.None)
					{
						DotController.DotIndex dotType = damageReport.dotType;
						if (dotType - DotController.DotIndex.Burn <= 2 || dotType == DotController.DotIndex.StrongerBurn)
						{
							statSheet.PushStatValue(StatDef.totalBurnDeaths, 1UL);
						}
					}
					if (deathEvent.victimWasBurning)
					{
						statSheet.PushStatValue(StatDef.totalDeathsWhileBurning, 1UL);
					}
				}
				if (statSheet2 != null)
				{
					statSheet2.PushStatValue(StatDef.totalKills, 1UL);
					statSheet2.PushStatValue(PerBodyStatDef.killsAs, damageReport.attackerBodyIndex, 1UL);
					if (damageReport.victimBodyIndex != BodyIndex.None)
					{
						statSheet2.PushStatValue(PerBodyStatDef.killsAgainst, damageReport.victimBodyIndex, 1UL);
						if (damageReport.victimIsElite)
						{
							statSheet2.PushStatValue(StatDef.totalEliteKills, 1UL);
							statSheet2.PushStatValue(PerBodyStatDef.killsAgainstElite, damageReport.victimBodyIndex, 1UL);
						}
					}
					if (damageReport.attackerBodyIndex == StatManager.crocoBodyIndex && damageReport.combinedHealthBeforeDamage <= 1f)
					{
						statSheet2.PushStatValue(StatDef.totalCrocoWeakEnemyKills, 1UL);
					}
					CharacterBody victimBody = damageReport.victimBody;
					string text = (victimBody != null) ? victimBody.customKillTotalStatName : null;
					if (!string.IsNullOrEmpty(text))
					{
						StatDef statDef = StatDef.Find(text);
						if (statDef == null)
						{
							Debug.LogWarningFormat("Stat def \"{0}\" could not be found.", new object[]
							{
								text
							});
						}
						else
						{
							statSheet2.PushStatValue(statDef, 1UL);
						}
					}
				}
				if (statSheet3 != null)
				{
					statSheet3.PushStatValue(StatDef.totalMinionKills, 1UL);
					if (damageReport.attackerOwnerBodyIndex != BodyIndex.None)
					{
						statSheet3.PushStatValue(PerBodyStatDef.minionKillsAs, damageReport.attackerOwnerBodyIndex, 1UL);
					}
				}
				if (damageReport.victimIsBoss)
				{
					int i = 0;
					int count = PlayerStatsComponent.instancesList.Count;
					while (i < count)
					{
						PlayerStatsComponent playerStatsComponent = PlayerStatsComponent.instancesList[i];
						if (playerStatsComponent.characterMaster.hasBody)
						{
							playerStatsComponent.currentStats.PushStatValue(StatDef.totalTeleporterBossKillsWitnessed, 1UL);
						}
						i++;
					}
				}
			}
		}

		// Token: 0x06003F4A RID: 16202 RVA: 0x001055AC File Offset: 0x001037AC
		public static void OnGoldCollected(CharacterMaster characterMaster, ulong amount)
		{
			StatManager.goldCollectedEvents.Enqueue(new StatManager.GoldEvent
			{
				characterMaster = characterMaster,
				amount = amount
			});
		}

		// Token: 0x06003F4B RID: 16203 RVA: 0x001055DC File Offset: 0x001037DC
		private static void ProcessGoldEvents()
		{
			while (StatManager.goldCollectedEvents.Count > 0)
			{
				StatManager.GoldEvent goldEvent = StatManager.goldCollectedEvents.Dequeue();
				CharacterMaster characterMaster = goldEvent.characterMaster;
				StatSheet statSheet;
				if (characterMaster == null)
				{
					statSheet = null;
				}
				else
				{
					PlayerStatsComponent component = characterMaster.GetComponent<PlayerStatsComponent>();
					statSheet = ((component != null) ? component.currentStats : null);
				}
				StatSheet statSheet2 = statSheet;
				if (statSheet2 != null)
				{
					statSheet2.PushStatValue(StatDef.goldCollected, goldEvent.amount);
					statSheet2.PushStatValue(StatDef.maxGoldCollected, statSheet2.GetStatValueULong(StatDef.goldCollected));
				}
			}
		}

		// Token: 0x06003F4C RID: 16204 RVA: 0x0010564C File Offset: 0x0010384C
		public static void OnPurchase<T>(CharacterBody characterBody, CostTypeIndex costType, T statDefsToIncrement) where T : IEnumerable<StatDef>
		{
			StatSheet statSheet = PlayerStatsComponent.FindBodyStatSheet(characterBody);
			if (statSheet == null)
			{
				return;
			}
			StatDef statDef = null;
			StatDef statDef2 = null;
			switch (costType)
			{
			case CostTypeIndex.Money:
				statDef = StatDef.totalGoldPurchases;
				statDef2 = StatDef.highestGoldPurchases;
				break;
			case CostTypeIndex.PercentHealth:
				statDef = StatDef.totalBloodPurchases;
				statDef2 = StatDef.highestBloodPurchases;
				break;
			case CostTypeIndex.LunarCoin:
				statDef = StatDef.totalLunarPurchases;
				statDef2 = StatDef.highestLunarPurchases;
				break;
			case CostTypeIndex.WhiteItem:
				statDef = StatDef.totalTier1Purchases;
				statDef2 = StatDef.highestTier1Purchases;
				break;
			case CostTypeIndex.GreenItem:
				statDef = StatDef.totalTier2Purchases;
				statDef2 = StatDef.highestTier2Purchases;
				break;
			case CostTypeIndex.RedItem:
				statDef = StatDef.totalTier3Purchases;
				statDef2 = StatDef.highestTier3Purchases;
				break;
			}
			statSheet.PushStatValue(StatDef.totalPurchases, 1UL);
			statSheet.PushStatValue(StatDef.highestPurchases, statSheet.GetStatValueULong(StatDef.totalPurchases));
			if (statDef != null)
			{
				statSheet.PushStatValue(statDef, 1UL);
				if (statDef2 != null)
				{
					statSheet.PushStatValue(statDef2, statSheet.GetStatValueULong(statDef));
				}
			}
			if (statDefsToIncrement != null)
			{
				foreach (StatDef statDef3 in statDefsToIncrement)
				{
					if (statDef3 != null)
					{
						statSheet.PushStatValue(statDef3, 1UL);
					}
				}
			}
		}

		// Token: 0x06003F4D RID: 16205 RVA: 0x00105770 File Offset: 0x00103970
		public static void OnEquipmentActivated(EquipmentSlot activator, EquipmentIndex equipmentIndex)
		{
			StatSheet statSheet = PlayerStatsComponent.FindBodyStatSheet(activator.characterBody);
			if (statSheet == null)
			{
				return;
			}
			statSheet.PushStatValue(PerEquipmentStatDef.totalTimesFired.FindStatDef(equipmentIndex), 1UL);
		}

		// Token: 0x06003F4E RID: 16206 RVA: 0x00105794 File Offset: 0x00103994
		public static void PushCharacterUpdateEvent(StatManager.CharacterUpdateEvent e)
		{
			StatManager.characterUpdateEvents.Enqueue(e);
		}

		// Token: 0x06003F4F RID: 16207 RVA: 0x001057A4 File Offset: 0x001039A4
		private static void ProcessCharacterUpdateEvents()
		{
			while (StatManager.characterUpdateEvents.Count > 0)
			{
				StatManager.CharacterUpdateEvent characterUpdateEvent = StatManager.characterUpdateEvents.Dequeue();
				if (characterUpdateEvent.statsComponent)
				{
					StatSheet currentStats = characterUpdateEvent.statsComponent.currentStats;
					if (currentStats != null)
					{
						CharacterBody body = characterUpdateEvent.statsComponent.characterMaster.GetBody();
						BodyIndex bodyIndex = (body != null) ? body.bodyIndex : BodyIndex.None;
						currentStats.PushStatValue(StatDef.totalTimeAlive, (double)characterUpdateEvent.additionalTimeAlive);
						currentStats.PushStatValue(StatDef.highestLevel, (ulong)((long)characterUpdateEvent.level));
						currentStats.PushStatValue(StatDef.totalDistanceTraveled, (double)characterUpdateEvent.additionalDistanceTraveled);
						if (bodyIndex != BodyIndex.None)
						{
							currentStats.PushStatValue(PerBodyStatDef.totalTimeAlive, bodyIndex, (double)characterUpdateEvent.additionalTimeAlive);
							currentStats.PushStatValue(PerBodyStatDef.longestRun, bodyIndex, (double)characterUpdateEvent.runTime);
						}
						EquipmentIndex currentEquipmentIndex = characterUpdateEvent.statsComponent.characterMaster.inventory.currentEquipmentIndex;
						if (currentEquipmentIndex != EquipmentIndex.None)
						{
							currentStats.PushStatValue(PerEquipmentStatDef.totalTimeHeld.FindStatDef(currentEquipmentIndex), (double)characterUpdateEvent.additionalTimeAlive);
						}
					}
				}
			}
		}

		// Token: 0x06003F50 RID: 16208 RVA: 0x001058A4 File Offset: 0x00103AA4
		private static void OnServerItemGiven(Inventory inventory, ItemIndex itemIndex, int quantity)
		{
			StatManager.itemCollectedEvents.Enqueue(new StatManager.ItemCollectedEvent
			{
				inventory = inventory,
				itemIndex = itemIndex,
				quantity = quantity,
				newCount = inventory.GetItemCount(itemIndex)
			});
		}

		// Token: 0x06003F51 RID: 16209 RVA: 0x001058EC File Offset: 0x00103AEC
		private static void ProcessItemCollectedEvents()
		{
			while (StatManager.itemCollectedEvents.Count > 0)
			{
				StatManager.ItemCollectedEvent itemCollectedEvent = StatManager.itemCollectedEvents.Dequeue();
				if (itemCollectedEvent.inventory)
				{
					PlayerStatsComponent component = itemCollectedEvent.inventory.GetComponent<PlayerStatsComponent>();
					StatSheet statSheet = (component != null) ? component.currentStats : null;
					if (statSheet != null)
					{
						statSheet.PushStatValue(StatDef.totalItemsCollected, (ulong)((long)itemCollectedEvent.quantity));
						statSheet.PushStatValue(StatDef.highestItemsCollected, statSheet.GetStatValueULong(StatDef.totalItemsCollected));
						statSheet.PushStatValue(PerItemStatDef.totalCollected.FindStatDef(itemCollectedEvent.itemIndex), (ulong)((long)itemCollectedEvent.quantity));
						statSheet.PushStatValue(PerItemStatDef.highestCollected.FindStatDef(itemCollectedEvent.itemIndex), (ulong)((long)itemCollectedEvent.newCount));
					}
				}
			}
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x001059A4 File Offset: 0x00103BA4
		private static void OnServerStageBegin(Stage stage)
		{
			foreach (PlayerStatsComponent playerStatsComponent in PlayerStatsComponent.instancesList)
			{
				if (playerStatsComponent.playerCharacterMasterController.isConnected)
				{
					StatSheet currentStats = playerStatsComponent.currentStats;
					StatDef statDef = PerStageStatDef.totalTimesVisited.FindStatDef(stage.sceneDef ? stage.sceneDef.baseSceneName : string.Empty);
					if (statDef != null)
					{
						currentStats.PushStatValue(statDef, 1UL);
					}
				}
			}
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x00105A3C File Offset: 0x00103C3C
		private static void OnServerStageComplete(Stage stage)
		{
			foreach (PlayerStatsComponent playerStatsComponent in PlayerStatsComponent.instancesList)
			{
				if (playerStatsComponent.playerCharacterMasterController.isConnected)
				{
					StatSheet currentStats = playerStatsComponent.currentStats;
					if (SceneInfo.instance.countsAsStage)
					{
						currentStats.PushStatValue(StatDef.totalStagesCompleted, 1UL);
						currentStats.PushStatValue(StatDef.highestStagesCompleted, currentStats.GetStatValueULong(StatDef.totalStagesCompleted));
					}
					StatDef statDef = PerStageStatDef.totalTimesCleared.FindStatDef(stage.sceneDef ? stage.sceneDef.baseSceneName : string.Empty);
					if (statDef != null)
					{
						currentStats.PushStatValue(statDef, 1UL);
					}
				}
			}
		}

		// Token: 0x04003DB2 RID: 15794
		private static BodyIndex crocoBodyIndex = BodyIndex.None;

		// Token: 0x04003DB3 RID: 15795
		private static readonly Queue<StatManager.DamageEvent> damageEvents = new Queue<StatManager.DamageEvent>();

		// Token: 0x04003DB4 RID: 15796
		private static readonly Queue<StatManager.DeathEvent> deathEvents = new Queue<StatManager.DeathEvent>();

		// Token: 0x04003DB5 RID: 15797
		private static readonly Queue<StatManager.HealingEvent> healingEvents = new Queue<StatManager.HealingEvent>();

		// Token: 0x04003DB6 RID: 15798
		private static readonly Queue<StatManager.GoldEvent> goldCollectedEvents = new Queue<StatManager.GoldEvent>();

		// Token: 0x04003DB7 RID: 15799
		private static readonly Queue<StatManager.PurchaseStatEvent> purchaseStatEvents = new Queue<StatManager.PurchaseStatEvent>();

		// Token: 0x04003DB8 RID: 15800
		private static readonly Queue<StatManager.CharacterUpdateEvent> characterUpdateEvents = new Queue<StatManager.CharacterUpdateEvent>();

		// Token: 0x04003DB9 RID: 15801
		private static readonly Queue<StatManager.ItemCollectedEvent> itemCollectedEvents = new Queue<StatManager.ItemCollectedEvent>();

		// Token: 0x02000ABF RID: 2751
		private struct DamageEvent
		{
			// Token: 0x04003DBA RID: 15802
			[CanBeNull]
			public CharacterMaster attackerMaster;

			// Token: 0x04003DBB RID: 15803
			public BodyIndex attackerBodyIndex;

			// Token: 0x04003DBC RID: 15804
			[CanBeNull]
			public CharacterMaster attackerOwnerMaster;

			// Token: 0x04003DBD RID: 15805
			public BodyIndex attackerOwnerBodyIndex;

			// Token: 0x04003DBE RID: 15806
			[CanBeNull]
			public CharacterMaster victimMaster;

			// Token: 0x04003DBF RID: 15807
			public BodyIndex victimBodyIndex;

			// Token: 0x04003DC0 RID: 15808
			public bool victimIsElite;

			// Token: 0x04003DC1 RID: 15809
			public float damageDealt;

			// Token: 0x04003DC2 RID: 15810
			public DotController.DotIndex dotType;
		}

		// Token: 0x02000AC0 RID: 2752
		private struct DeathEvent
		{
			// Token: 0x04003DC3 RID: 15811
			public DamageReport damageReport;

			// Token: 0x04003DC4 RID: 15812
			public bool victimWasBurning;
		}

		// Token: 0x02000AC1 RID: 2753
		private struct HealingEvent
		{
			// Token: 0x04003DC5 RID: 15813
			[CanBeNull]
			public GameObject healee;

			// Token: 0x04003DC6 RID: 15814
			public float healAmount;
		}

		// Token: 0x02000AC2 RID: 2754
		private struct GoldEvent
		{
			// Token: 0x04003DC7 RID: 15815
			[CanBeNull]
			public CharacterMaster characterMaster;

			// Token: 0x04003DC8 RID: 15816
			public ulong amount;
		}

		// Token: 0x02000AC3 RID: 2755
		private struct PurchaseStatEvent
		{
		}

		// Token: 0x02000AC4 RID: 2756
		public struct CharacterUpdateEvent
		{
			// Token: 0x04003DC9 RID: 15817
			public PlayerStatsComponent statsComponent;

			// Token: 0x04003DCA RID: 15818
			public float additionalDistanceTraveled;

			// Token: 0x04003DCB RID: 15819
			public float additionalTimeAlive;

			// Token: 0x04003DCC RID: 15820
			public int level;

			// Token: 0x04003DCD RID: 15821
			public float runTime;
		}

		// Token: 0x02000AC5 RID: 2757
		private struct ItemCollectedEvent
		{
			// Token: 0x04003DCE RID: 15822
			[CanBeNull]
			public Inventory inventory;

			// Token: 0x04003DCF RID: 15823
			public ItemIndex itemIndex;

			// Token: 0x04003DD0 RID: 15824
			public int quantity;

			// Token: 0x04003DD1 RID: 15825
			public int newCount;
		}
	}
}
