using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Facepunch.Steamworks;
using RoR2.ExpansionManagement;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x020006F0 RID: 1776
	public class WeeklyRun : Run
	{
		// Token: 0x060023FA RID: 9210 RVA: 0x0009A6B4 File Offset: 0x000988B4
		public static uint GetCurrentSeedCycle()
		{
			return (uint)((WeeklyRun.now - WeeklyRun.startDate).Days / 3);
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x0009A6DC File Offset: 0x000988DC
		public static DateTime GetSeedCycleStartDateTime(uint seedCycle)
		{
			return WeeklyRun.startDate.AddDays(seedCycle * 3U);
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x0009A6FB File Offset: 0x000988FB
		public static DateTime GetSeedCycleStartDateTime()
		{
			return WeeklyRun.GetSeedCycleStartDateTime(WeeklyRun.GetCurrentSeedCycle());
		}

		// Token: 0x060023FD RID: 9213 RVA: 0x0009A707 File Offset: 0x00098907
		public static DateTime GetSeedCycleEndDateTime()
		{
			return WeeklyRun.GetSeedCycleStartDateTime(WeeklyRun.GetCurrentSeedCycle() + 1U);
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x0009A715 File Offset: 0x00098915
		public static DateTime now
		{
			get
			{
				return Util.UnixTimeStampToDateTimeUtc(Client.Instance.Utils.GetServerRealTime());
			}
		}

		// Token: 0x060023FF RID: 9215 RVA: 0x0009A72C File Offset: 0x0009892C
		protected new void Start()
		{
			base.Start();
			if (NetworkServer.active)
			{
				this.bossAffixRng = new Xoroshiro128Plus(this.runRNG.nextUlong);
				this.NetworkserverSeedCycle = WeeklyRun.GetCurrentSeedCycle();
			}
			this.bossAffixes = new EquipmentIndex[]
			{
				RoR2Content.Equipment.AffixRed.equipmentIndex,
				RoR2Content.Equipment.AffixBlue.equipmentIndex
			};
		}

		// Token: 0x06002400 RID: 9216 RVA: 0x0009A790 File Offset: 0x00098990
		protected override void OnFixedUpdate()
		{
			base.OnFixedUpdate();
			if (TeleporterInteraction.instance)
			{
				bool flag = this.crystalsRequiredToKill > this.crystalsKilled;
				if (flag != TeleporterInteraction.instance.locked)
				{
					if (flag)
					{
						if (NetworkServer.active)
						{
							TeleporterInteraction.instance.locked = true;
							return;
						}
					}
					else
					{
						if (NetworkServer.active)
						{
							TeleporterInteraction.instance.locked = false;
						}
						ChildLocator component = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
						if (component)
						{
							Transform transform = component.FindChild("TimeCrystalBeaconBlocker");
							EffectManager.SpawnEffect(LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/TimeCrystalDeath"), new EffectData
							{
								origin = transform.transform.position
							}, false);
							transform.gameObject.SetActive(false);
						}
					}
				}
			}
		}

		// Token: 0x06002401 RID: 9217 RVA: 0x0009A854 File Offset: 0x00098A54
		public override ulong GenerateSeedForNewRun()
		{
			return (ulong)WeeklyRun.GetCurrentSeedCycle() << 32;
		}

		// Token: 0x06002402 RID: 9218 RVA: 0x000026ED File Offset: 0x000008ED
		public override void HandlePlayerFirstEntryAnimation(CharacterBody body, Vector3 spawnPosition, Quaternion spawnRotation)
		{
		}

		// Token: 0x06002403 RID: 9219 RVA: 0x0009A85F File Offset: 0x00098A5F
		public override void AdvanceStage(SceneDef nextScene)
		{
			if (this.stageClearCount == 1 && SceneInfo.instance.countsAsStage)
			{
				base.BeginGameOver(RoR2Content.GameEndings.PrismaticTrialEnding);
				return;
			}
			base.AdvanceStage(nextScene);
		}

		// Token: 0x06002404 RID: 9220 RVA: 0x0009A889 File Offset: 0x00098A89
		public override void OnClientGameOver(RunReport runReport)
		{
			base.OnClientGameOver(runReport);
			this.ClientSubmitLeaderboardScore(runReport);
		}

		// Token: 0x06002405 RID: 9221 RVA: 0x0009A89C File Offset: 0x00098A9C
		public unsafe override void OnServerBossAdded(BossGroup bossGroup, CharacterMaster characterMaster)
		{
			base.OnServerBossAdded(bossGroup, characterMaster);
			if (this.stageClearCount >= 1)
			{
				if (characterMaster.inventory.GetEquipmentIndex() == EquipmentIndex.None)
				{
					characterMaster.inventory.SetEquipmentIndex(*this.bossAffixRng.NextElementUniform<EquipmentIndex>(this.bossAffixes));
				}
				characterMaster.inventory.GiveItem(RoR2Content.Items.BoostHp, 5);
				characterMaster.inventory.GiveItem(RoR2Content.Items.BoostDamage, 1);
			}
		}

		// Token: 0x06002406 RID: 9222 RVA: 0x0009A907 File Offset: 0x00098B07
		public override void OnServerBossDefeated(BossGroup bossGroup)
		{
			base.OnServerBossDefeated(bossGroup);
			if (TeleporterInteraction.instance)
			{
				TeleporterInteraction.instance.holdoutZoneController.FullyChargeHoldoutZone();
			}
		}

		// Token: 0x06002407 RID: 9223 RVA: 0x0009A92B File Offset: 0x00098B2B
		public override GameObject GetTeleportEffectPrefab(GameObject objectToTeleport)
		{
			return LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/TeleportOutCrystalBoom");
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0009A937 File Offset: 0x00098B37
		public uint crystalsKilled
		{
			get
			{
				return (uint)((ulong)this.crystalCount - (ulong)((long)this.crystalActiveList.Count));
			}
		}

		// Token: 0x06002409 RID: 9225 RVA: 0x0009A950 File Offset: 0x00098B50
		public override void OnServerTeleporterPlaced(SceneDirector sceneDirector, GameObject teleporter)
		{
			base.OnServerTeleporterPlaced(sceneDirector, teleporter);
			DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule();
			directorPlacementRule.placementMode = DirectorPlacementRule.PlacementMode.Random;
			int num = 0;
			while ((long)num < (long)((ulong)this.crystalCount))
			{
				GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.crystalSpawnCard, directorPlacementRule, this.stageRng));
				if (gameObject)
				{
					DeathRewards component3 = gameObject.GetComponent<DeathRewards>();
					if (component3)
					{
						component3.goldReward = this.crystalRewardValue;
					}
				}
				this.crystalActiveList.Add(OnDestroyCallback.AddCallback(gameObject, delegate(OnDestroyCallback component)
				{
					this.crystalActiveList.Remove(component);
				}));
				num++;
			}
			if (TeleporterInteraction.instance)
			{
				ChildLocator component2 = TeleporterInteraction.instance.GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>();
				if (component2)
				{
					component2.FindChild("TimeCrystalProps").gameObject.SetActive(true);
					component2.FindChild("TimeCrystalBeaconBlocker").gameObject.SetActive(true);
				}
			}
		}

		// Token: 0x0600240A RID: 9226 RVA: 0x0009AA3C File Offset: 0x00098C3C
		public override void OnPlayerSpawnPointsPlaced(SceneDirector sceneDirector)
		{
			if (this.stageClearCount == 0)
			{
				SpawnPoint spawnPoint = SpawnPoint.readOnlyInstancesList[0];
				if (spawnPoint)
				{
					float num = 360f / this.equipmentBarrelCount;
					int num2 = 0;
					while ((long)num2 < (long)((ulong)this.equipmentBarrelCount))
					{
						Vector3 b = Quaternion.AngleAxis(num * (float)num2, Vector3.up) * (Vector3.forward * this.equipmentBarrelRadius);
						DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule();
						directorPlacementRule.minDistance = 0f;
						directorPlacementRule.maxDistance = 3f;
						directorPlacementRule.placementMode = DirectorPlacementRule.PlacementMode.NearestNode;
						directorPlacementRule.position = spawnPoint.transform.position + b;
						DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.equipmentBarrelSpawnCard, directorPlacementRule, this.stageRng));
						num2++;
					}
				}
			}
		}

		// Token: 0x0600240B RID: 9227 RVA: 0x0009AB14 File Offset: 0x00098D14
		public static string GetLeaderboardName(int playerCount, uint seedCycle)
		{
			if (Console.sessionCheatsEnabled)
			{
				return null;
			}
			return string.Format(CultureInfo.InvariantCulture, "weekly{0}p{1}", playerCount, seedCycle);
		}

		// Token: 0x0600240C RID: 9228 RVA: 0x0009AB3C File Offset: 0x00098D3C
		protected void ClientSubmitLeaderboardScore(RunReport runReport)
		{
			Debug.LogFormat("Attempting to submit leaderboard score.", Array.Empty<object>());
			Debug.Log(runReport.gameEnding.cachedName);
			if (!runReport.gameEnding.isWin)
			{
				Debug.Log("Didn't win - aborting");
				return;
			}
			bool flag = false;
			using (IEnumerator<NetworkUser> enumerator = NetworkUser.readOnlyLocalPlayersList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.isParticipating)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				return;
			}
			int num = PlayerCharacterMasterController.instances.Count;
			if (num <= 0)
			{
				return;
			}
			if (num >= 3)
			{
				if (num > 4)
				{
					return;
				}
				num = 4;
			}
			string text = WeeklyRun.GetLeaderboardName(num, this.serverSeedCycle);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			int[] subScores = new int[64];
			GameObject bodyPrefab = BodyCatalog.GetBodyPrefab(NetworkUser.readOnlyLocalPlayersList[0].bodyIndexPreference);
			if (!bodyPrefab)
			{
				return;
			}
			SurvivorDef survivorDef = SurvivorCatalog.FindSurvivorDefFromBody(bodyPrefab);
			if (survivorDef == null)
			{
				return;
			}
			subScores[1] = (int)survivorDef.survivorIndex;
			Leaderboard leaderboard = Client.Instance.GetLeaderboard(text, Client.LeaderboardSortMethod.Ascending, Client.LeaderboardDisplayType.TimeMilliSeconds);
			leaderboard.OnBoardInformation = delegate()
			{
				leaderboard.AddScore(true, (int)Math.Ceiling((double)runReport.runStopwatchValue * 1000.0), subScores);
			};
		}

		// Token: 0x0600240D RID: 9229 RVA: 0x0009AC90 File Offset: 0x00098E90
		public override void OverrideRuleChoices(RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
		{
			base.OverrideRuleChoices(mustInclude, mustExclude, base.seed);
			base.ForceChoice(mustInclude, mustExclude, "Difficulty.Normal");
			base.ForceChoice(mustInclude, mustExclude, "Misc.StartingMoney.50");
			base.ForceChoice(mustInclude, mustExclude, "Misc.StageOrder.Random");
			base.ForceChoice(mustInclude, mustExclude, "Misc.KeepMoneyBetweenStages.Off");
			for (int i = 0; i < ArtifactCatalog.artifactCount; i++)
			{
				base.ForceChoice(mustInclude, mustExclude, WeeklyRun.<OverrideRuleChoices>g__FindRuleForArtifact|35_0((ArtifactIndex)i).FindChoice("Off"));
			}
			Xoroshiro128Plus xoroshiro128Plus = new Xoroshiro128Plus(runSeed);
			Debug.LogFormat("Weekly Run Seed: {0}", new object[]
			{
				runSeed
			});
			if (xoroshiro128Plus.nextNormalizedFloat < 1f)
			{
				int num = xoroshiro128Plus.RangeInt(2, 7);
				ArtifactIndex[] array = new ArtifactIndex[ArtifactCatalog.artifactCount];
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = (ArtifactIndex)j;
				}
				Util.ShuffleArray<ArtifactIndex>(array, xoroshiro128Plus);
				for (int k = 0; k < num; k++)
				{
					if (ArtifactCatalog.GetArtifactDef(array[k]) != RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef)
					{
						base.ForceChoice(mustInclude, mustExclude, WeeklyRun.<OverrideRuleChoices>g__FindRuleForArtifact|35_0(array[k]).FindChoice("On"));
					}
				}
			}
			ItemIndex itemIndex = (ItemIndex)0;
			ItemIndex itemCount = (ItemIndex)ItemCatalog.itemCount;
			while (itemIndex < itemCount)
			{
				ItemDef itemDef = ItemCatalog.GetItemDef(itemIndex);
				RuleDef ruleDef = RuleCatalog.FindRuleDef("Items." + itemDef.name);
				RuleChoiceDef ruleChoiceDef = (ruleDef != null) ? ruleDef.FindChoice("On") : null;
				if (ruleChoiceDef != null)
				{
					base.ForceChoice(mustInclude, mustExclude, ruleChoiceDef);
				}
				itemIndex++;
			}
			EquipmentIndex equipmentIndex = (EquipmentIndex)0;
			EquipmentIndex equipmentCount = (EquipmentIndex)EquipmentCatalog.equipmentCount;
			while (equipmentIndex < equipmentCount)
			{
				EquipmentDef equipmentDef = EquipmentCatalog.GetEquipmentDef(equipmentIndex);
				RuleDef ruleDef2 = RuleCatalog.FindRuleDef("Equipment." + equipmentDef.name);
				RuleChoiceDef ruleChoiceDef2 = (ruleDef2 != null) ? ruleDef2.FindChoice("On") : null;
				if (ruleChoiceDef2 != null)
				{
					base.ForceChoice(mustInclude, mustExclude, ruleChoiceDef2);
				}
				equipmentIndex++;
			}
			foreach (ExpansionDef expansionDef in ExpansionCatalog.expansionDefs)
			{
				RuleDef ruleDef3 = RuleCatalog.FindRuleDef("Expansions." + expansionDef.name);
				RuleChoiceDef ruleChoiceDef3 = (ruleDef3 != null) ? ruleDef3.FindChoice("On") : null;
				if (ruleChoiceDef3 != null)
				{
					base.ForceChoice(mustInclude, mustExclude, ruleChoiceDef3);
				}
			}
		}

		// Token: 0x0600240E RID: 9230 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool IsUnlockableUnlocked(UnlockableDef unlockableDef)
		{
			return true;
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override bool CanUnlockableBeGrantedThisRun(UnlockableDef unlockableDef)
		{
			return false;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool DoesEveryoneHaveThisUnlockableUnlocked(UnlockableDef unlockableDef)
		{
			return true;
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00096D77 File Offset: 0x00094F77
		protected override void HandlePostRunDestination()
		{
			Console.instance.SubmitCmd(null, "transition_command \"disconnect\";", false);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x0009AED4 File Offset: 0x000990D4
		protected override bool ShouldUpdateRunStopwatch()
		{
			return base.livingPlayerCount > 0;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override bool ShouldAllowNonChampionBossSpawn()
		{
			return true;
		}

		// Token: 0x06002417 RID: 9239 RVA: 0x0009AF5C File Offset: 0x0009915C
		[CompilerGenerated]
		internal static RuleDef <OverrideRuleChoices>g__FindRuleForArtifact|35_0(ArtifactIndex artifactIndex)
		{
			ArtifactDef artifactDef = ArtifactCatalog.GetArtifactDef(artifactIndex);
			return RuleCatalog.FindRuleDef("Artifacts." + artifactDef.cachedName);
		}

		// Token: 0x06002418 RID: 9240 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06002419 RID: 9241 RVA: 0x0009AF88 File Offset: 0x00099188
		// (set) Token: 0x0600241A RID: 9242 RVA: 0x0009AF9B File Offset: 0x0009919B
		public uint NetworkserverSeedCycle
		{
			get
			{
				return this.serverSeedCycle;
			}
			[param: In]
			set
			{
				base.SetSyncVar<uint>(value, ref this.serverSeedCycle, 64U);
			}
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x0009AFB0 File Offset: 0x000991B0
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool flag = base.OnSerialize(writer, forceAll);
			if (forceAll)
			{
				writer.WritePackedUInt32(this.serverSeedCycle);
				return true;
			}
			bool flag2 = false;
			if ((base.syncVarDirtyBits & 64U) != 0U)
			{
				if (!flag2)
				{
					writer.WritePackedUInt32(base.syncVarDirtyBits);
					flag2 = true;
				}
				writer.WritePackedUInt32(this.serverSeedCycle);
			}
			if (!flag2)
			{
				writer.WritePackedUInt32(base.syncVarDirtyBits);
			}
			return flag2 || flag;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x0009B028 File Offset: 0x00099228
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			base.OnDeserialize(reader, initialState);
			if (initialState)
			{
				this.serverSeedCycle = reader.ReadPackedUInt32();
				return;
			}
			int num = (int)reader.ReadPackedUInt32();
			if ((num & 64) != 0)
			{
				this.serverSeedCycle = reader.ReadPackedUInt32();
			}
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x0007597B File Offset: 0x00073B7B
		public override void PreStartClient()
		{
			base.PreStartClient();
		}

		// Token: 0x04002874 RID: 10356
		private Xoroshiro128Plus bossAffixRng;

		// Token: 0x04002875 RID: 10357
		public static readonly DateTime startDate = new DateTime(2018, 8, 27, 0, 0, 0, 0, DateTimeKind.Utc);

		// Token: 0x04002876 RID: 10358
		public const int cycleLength = 3;

		// Token: 0x04002877 RID: 10359
		private string leaderboardName;

		// Token: 0x04002878 RID: 10360
		[SyncVar]
		private uint serverSeedCycle;

		// Token: 0x04002879 RID: 10361
		private EquipmentIndex[] bossAffixes = Array.Empty<EquipmentIndex>();

		// Token: 0x0400287A RID: 10362
		public SpawnCard crystalSpawnCard;

		// Token: 0x0400287B RID: 10363
		public uint crystalCount = 3U;

		// Token: 0x0400287C RID: 10364
		public uint crystalRewardValue = 50U;

		// Token: 0x0400287D RID: 10365
		public uint crystalsRequiredToKill = 3U;

		// Token: 0x0400287E RID: 10366
		private List<OnDestroyCallback> crystalActiveList = new List<OnDestroyCallback>();

		// Token: 0x0400287F RID: 10367
		public SpawnCard equipmentBarrelSpawnCard;

		// Token: 0x04002880 RID: 10368
		public uint equipmentBarrelCount = 3U;

		// Token: 0x04002881 RID: 10369
		public float equipmentBarrelRadius = 10f;
	}
}
