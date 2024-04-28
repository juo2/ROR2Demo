using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RoR2.ConVar;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

namespace RoR2
{
	// Token: 0x02000663 RID: 1635
	public class CombatDirector : MonoBehaviour
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x00087C4B File Offset: 0x00085E4B
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x00087C53 File Offset: 0x00085E53
		public float monsterSpawnTimer { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x00087C5C File Offset: 0x00085E5C
		// (set) Token: 0x06001FAA RID: 8106 RVA: 0x00087C64 File Offset: 0x00085E64
		public DirectorCard lastAttemptedMonsterCard { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001FAB RID: 8107 RVA: 0x00087C6D File Offset: 0x00085E6D
		// (set) Token: 0x06001FAC RID: 8108 RVA: 0x00087C75 File Offset: 0x00085E75
		public float totalCreditsSpent { get; private set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x00087C7E File Offset: 0x00085E7E
		private WeightedSelection<DirectorCard> finalMonsterCardsSelection
		{
			get
			{
				WeightedSelection<DirectorCard> monsterSelection;
				if ((monsterSelection = this.monsterCardsSelection) == null)
				{
					ClassicStageInfo instance = ClassicStageInfo.instance;
					if (instance == null)
					{
						return null;
					}
					monsterSelection = instance.monsterSelection;
				}
				return monsterSelection;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x00087C9A File Offset: 0x00085E9A
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x00087CA2 File Offset: 0x00085EA2
		private DirectorCardCategorySelection monsterCards
		{
			get
			{
				return this._monsterCards;
			}
			set
			{
				if (this._monsterCards != value)
				{
					this._monsterCards = value;
					DirectorCardCategorySelection monsterCards = this._monsterCards;
					this.monsterCardsSelection = ((monsterCards != null) ? monsterCards.GenerateDirectorCardWeightedSelection() : null);
				}
			}
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00087CD4 File Offset: 0x00085ED4
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus((ulong)Run.instance.stageRng.nextUint);
				this.moneyWaves = new CombatDirector.DirectorMoneyWave[this.moneyWaveIntervals.Length];
				for (int i = 0; i < this.moneyWaveIntervals.Length; i++)
				{
					this.moneyWaves[i] = new CombatDirector.DirectorMoneyWave
					{
						interval = this.rng.RangeFloat(this.moneyWaveIntervals[i].min, this.moneyWaveIntervals[i].max),
						multiplier = this.creditMultiplier
					};
				}
				DirectorCardCategorySelection monsterCards = this.monsterCards;
				this.monsterCardsSelection = ((monsterCards != null) ? monsterCards.GenerateDirectorCardWeightedSelection() : null);
			}
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00087D90 File Offset: 0x00085F90
		private void OnEnable()
		{
			CombatDirector.instancesList.Add(this);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00087DA0 File Offset: 0x00085FA0
		private void OnDisable()
		{
			CombatDirector.instancesList.Remove(this);
			if (NetworkServer.active && CombatDirector.instancesList.Count > 0)
			{
				float num = 0.4f;
				CombatDirector combatDirector = this.rng.NextElementUniform<CombatDirector>(CombatDirector.instancesList);
				this.monsterCredit *= num;
				combatDirector.monsterCredit += this.monsterCredit;
				Debug.LogFormat("Transfered {0} monster credits from {1} to {2}", new object[]
				{
					this.monsterCredit,
					base.gameObject,
					combatDirector.gameObject
				});
				this.monsterCredit = 0f;
			}
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00087E44 File Offset: 0x00086044
		private void GenerateAmbush(Vector3 victimPosition)
		{
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNode(victimPosition, HullClassification.Human, float.PositiveInfinity);
			NodeGraphSpider nodeGraphSpider = new NodeGraphSpider(groundNodes, HullMask.Human);
			nodeGraphSpider.AddNodeForNextStep(nodeIndex);
			List<NodeGraphSpider.StepInfo> list = new List<NodeGraphSpider.StepInfo>();
			int num = 0;
			List<NodeGraphSpider.StepInfo> collectedSteps = nodeGraphSpider.collectedSteps;
			while (nodeGraphSpider.PerformStep() && num < 8)
			{
				num++;
				for (int i = 0; i < collectedSteps.Count; i++)
				{
					if (CombatDirector.IsAcceptableAmbushSpiderStep(groundNodes, nodeIndex, collectedSteps[i]))
					{
						list.Add(collectedSteps[i]);
					}
				}
				collectedSteps.Clear();
			}
			for (int j = 0; j < list.Count; j++)
			{
				Vector3 position;
				groundNodes.GetNodePosition(list[j].node, out position);
				LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/scLemurian").DoSpawn(position, Quaternion.identity, null);
			}
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00087F20 File Offset: 0x00086120
		private static bool IsAcceptableAmbushSpiderStep(NodeGraph nodeGraph, NodeGraph.NodeIndex startNode, NodeGraphSpider.StepInfo stepInfo)
		{
			int num = 0;
			while (stepInfo.previousStep != null)
			{
				if (nodeGraph.TestNodeLineOfSight(startNode, stepInfo.node))
				{
					return false;
				}
				stepInfo = stepInfo.previousStep;
				num++;
				if (num > 2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00087F5D File Offset: 0x0008615D
		public void OverrideCurrentMonsterCard(DirectorCard overrideMonsterCard)
		{
			this.PrepareNewMonsterWave(overrideMonsterCard);
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x00087F68 File Offset: 0x00086168
		public void SetNextSpawnAsBoss()
		{
			WeightedSelection<DirectorCard> weightedSelection = new WeightedSelection<DirectorCard>(8);
			int i = 0;
			int count = this.finalMonsterCardsSelection.Count;
			while (i < count)
			{
				WeightedSelection<DirectorCard>.ChoiceInfo choice = this.finalMonsterCardsSelection.GetChoice(i);
				SpawnCard spawnCard = choice.value.spawnCard;
				bool isChampion = spawnCard.prefab.GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().isChampion;
				CharacterSpawnCard characterSpawnCard = spawnCard as CharacterSpawnCard;
				bool flag = characterSpawnCard != null && characterSpawnCard.forbiddenAsBoss;
				if (isChampion && !flag && choice.value.IsAvailable())
				{
					weightedSelection.AddChoice(choice);
				}
				i++;
			}
			if (weightedSelection.Count > 0)
			{
				DirectorCard directorCard = weightedSelection.Evaluate(this.rng.nextNormalizedFloat);
				Debug.Log(string.Format("Next boss spawn:  {0}", directorCard.spawnCard));
				this.PrepareNewMonsterWave(directorCard);
			}
			else
			{
				Debug.Log("Could not spawn boss");
			}
			this.monsterSpawnTimer = -600f;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x00088048 File Offset: 0x00086248
		private void PickPlayerAsSpawnTarget()
		{
			ReadOnlyCollection<PlayerCharacterMasterController> instances = PlayerCharacterMasterController.instances;
			List<PlayerCharacterMasterController> list = new List<PlayerCharacterMasterController>();
			foreach (PlayerCharacterMasterController playerCharacterMasterController in instances)
			{
				if (playerCharacterMasterController.master.hasBody)
				{
					list.Add(playerCharacterMasterController);
				}
			}
			if (list.Count > 0)
			{
				this.currentSpawnTarget = this.rng.NextElementUniform<PlayerCharacterMasterController>(list).master.GetBodyObject();
			}
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x000880CC File Offset: 0x000862CC
		public void SpendAllCreditsOnMapSpawns(Transform mapSpawnTarget)
		{
			int num = 0;
			int num2 = 10;
			while (this.monsterCredit > 0f)
			{
				this.PrepareNewMonsterWave(this.finalMonsterCardsSelection.Evaluate(this.rng.nextNormalizedFloat));
				bool flag;
				if (mapSpawnTarget)
				{
					flag = this.AttemptSpawnOnTarget(mapSpawnTarget, DirectorPlacementRule.PlacementMode.Approximate);
				}
				else
				{
					flag = this.AttemptSpawnOnTarget(null, SceneInfo.instance.approximateMapBoundMesh ? DirectorPlacementRule.PlacementMode.RandomNormalized : DirectorPlacementRule.PlacementMode.Random);
				}
				if (flag)
				{
					num = 0;
				}
				else
				{
					num++;
					if (num >= num2)
					{
						break;
					}
				}
			}
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x0008814C File Offset: 0x0008634C
		public void ToggleAllCombatDirectorsOnThisObject(bool newValue)
		{
			CombatDirector[] components = base.GetComponents<CombatDirector>();
			for (int i = 0; i < components.Length; i++)
			{
				components[i].enabled = newValue;
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00088178 File Offset: 0x00086378
		private void Simulate(float deltaTime)
		{
			if (this.targetPlayers)
			{
				this.playerRetargetTimer -= deltaTime;
				if (this.playerRetargetTimer <= 0f)
				{
					this.playerRetargetTimer = this.rng.RangeFloat(1f, 10f);
					this.PickPlayerAsSpawnTarget();
				}
			}
			this.monsterSpawnTimer -= deltaTime;
			if (this.monsterSpawnTimer <= 0f)
			{
				if (this.AttemptSpawnOnTarget(this.currentSpawnTarget ? this.currentSpawnTarget.transform : null, DirectorPlacementRule.PlacementMode.Approximate))
				{
					if (this.shouldSpawnOneWave)
					{
						Debug.Log("CombatDirector hasStartedwave = true");
						this.hasStartedWave = true;
					}
					this.monsterSpawnTimer += this.rng.RangeFloat(this.minSeriesSpawnInterval, this.maxSeriesSpawnInterval);
					return;
				}
				this.monsterSpawnTimer += this.rng.RangeFloat(this.minRerollSpawnInterval, this.maxRerollSpawnInterval);
				if (this.resetMonsterCardIfFailed)
				{
					this.currentMonsterCard = null;
				}
				if (this.shouldSpawnOneWave && this.hasStartedWave)
				{
					Debug.Log("CombatDirector wave complete");
					base.enabled = false;
					return;
				}
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x0008829E File Offset: 0x0008649E
		private static bool IsEliteOnlyArtifactActive()
		{
			return RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.eliteOnlyArtifactDef);
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x000882AF File Offset: 0x000864AF
		private static bool NotEliteOnlyArtifactActive()
		{
			return !CombatDirector.IsEliteOnlyArtifactActive();
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x000882BC File Offset: 0x000864BC
		[SystemInitializer(new Type[]
		{
			typeof(EliteCatalog)
		})]
		private static void Init()
		{
			CombatDirector.EliteTierDef[] array = new CombatDirector.EliteTierDef[5];
			int num = 0;
			CombatDirector.EliteTierDef eliteTierDef = new CombatDirector.EliteTierDef();
			eliteTierDef.costMultiplier = 1f;
			eliteTierDef.eliteTypes = new EliteDef[1];
			eliteTierDef.isAvailable = ((SpawnCard.EliteRules rules) => CombatDirector.NotEliteOnlyArtifactActive());
			eliteTierDef.canSelectWithoutAvailableEliteDef = true;
			array[num] = eliteTierDef;
			int num2 = 1;
			eliteTierDef = new CombatDirector.EliteTierDef();
			eliteTierDef.costMultiplier = CombatDirector.baseEliteCostMultiplier;
			eliteTierDef.eliteTypes = new EliteDef[]
			{
				RoR2Content.Elites.Lightning,
				RoR2Content.Elites.Ice,
				RoR2Content.Elites.Fire,
				DLC1Content.Elites.Earth
			};
			eliteTierDef.isAvailable = ((SpawnCard.EliteRules rules) => CombatDirector.NotEliteOnlyArtifactActive() && rules == SpawnCard.EliteRules.Default);
			eliteTierDef.canSelectWithoutAvailableEliteDef = false;
			array[num2] = eliteTierDef;
			int num3 = 2;
			eliteTierDef = new CombatDirector.EliteTierDef();
			eliteTierDef.costMultiplier = Mathf.LerpUnclamped(1f, CombatDirector.baseEliteCostMultiplier, 0.5f);
			eliteTierDef.eliteTypes = new EliteDef[]
			{
				RoR2Content.Elites.LightningHonor,
				RoR2Content.Elites.IceHonor,
				RoR2Content.Elites.FireHonor,
				DLC1Content.Elites.EarthHonor
			};
			eliteTierDef.isAvailable = ((SpawnCard.EliteRules rules) => CombatDirector.IsEliteOnlyArtifactActive());
			eliteTierDef.canSelectWithoutAvailableEliteDef = false;
			array[num3] = eliteTierDef;
			int num4 = 3;
			eliteTierDef = new CombatDirector.EliteTierDef();
			eliteTierDef.costMultiplier = CombatDirector.baseEliteCostMultiplier * 6f;
			eliteTierDef.eliteTypes = new EliteDef[]
			{
				RoR2Content.Elites.Poison,
				RoR2Content.Elites.Haunted
			};
			eliteTierDef.isAvailable = ((SpawnCard.EliteRules rules) => Run.instance.loopClearCount > 0 && rules == SpawnCard.EliteRules.Default);
			eliteTierDef.canSelectWithoutAvailableEliteDef = false;
			array[num4] = eliteTierDef;
			int num5 = 4;
			eliteTierDef = new CombatDirector.EliteTierDef();
			eliteTierDef.costMultiplier = CombatDirector.baseEliteCostMultiplier;
			eliteTierDef.eliteTypes = new EliteDef[]
			{
				RoR2Content.Elites.Lunar
			};
			eliteTierDef.isAvailable = ((SpawnCard.EliteRules rules) => rules == SpawnCard.EliteRules.Lunar);
			eliteTierDef.canSelectWithoutAvailableEliteDef = false;
			array[num5] = eliteTierDef;
			CombatDirector.eliteTiers = array;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x000884C4 File Offset: 0x000866C4
		public static float CalcHighestEliteCostMultiplier(SpawnCard.EliteRules eliteRules)
		{
			float num = 1f;
			for (int i = 1; i < CombatDirector.eliteTiers.Length; i++)
			{
				if (CombatDirector.eliteTiers[i].CanSelect(eliteRules))
				{
					num = Mathf.Max(num, CombatDirector.eliteTiers[i].costMultiplier);
				}
			}
			return num;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001FBF RID: 8127 RVA: 0x0008850C File Offset: 0x0008670C
		public static float lowestEliteCostMultiplier
		{
			get
			{
				return CombatDirector.eliteTiers[1].costMultiplier;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001FC0 RID: 8128 RVA: 0x0008851C File Offset: 0x0008671C
		private int mostExpensiveMonsterCostInDeck
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.finalMonsterCardsSelection.Count; i++)
				{
					DirectorCard value = this.finalMonsterCardsSelection.GetChoice(i).value;
					int num2 = value.cost;
					if (!(value.spawnCard as CharacterSpawnCard).noElites)
					{
						num2 = (int)((float)num2 * CombatDirector.CalcHighestEliteCostMultiplier(value.spawnCard.eliteRules));
					}
					num = Mathf.Max(num, num2);
				}
				return num;
			}
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0008858C File Offset: 0x0008678C
		private void ResetEliteType()
		{
			this.currentActiveEliteTier = CombatDirector.eliteTiers[0];
			for (int i = 0; i < CombatDirector.eliteTiers.Length; i++)
			{
				if (CombatDirector.eliteTiers[i].CanSelect(this.currentMonsterCard.spawnCard.eliteRules))
				{
					this.currentActiveEliteTier = CombatDirector.eliteTiers[i];
					break;
				}
			}
			this.currentActiveEliteDef = this.currentActiveEliteTier.GetRandomAvailableEliteDef(this.rng);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x000885FC File Offset: 0x000867FC
		private void PrepareNewMonsterWave(DirectorCard monsterCard)
		{
			if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
			{
				Debug.LogFormat("Preparing monster wave {0}", new object[]
				{
					monsterCard.spawnCard
				});
			}
			this.currentMonsterCard = monsterCard;
			this.ResetEliteType();
			if (!(this.currentMonsterCard.spawnCard as CharacterSpawnCard).noElites)
			{
				for (int i = 1; i < CombatDirector.eliteTiers.Length; i++)
				{
					CombatDirector.EliteTierDef eliteTierDef = CombatDirector.eliteTiers[i];
					if (!eliteTierDef.CanSelect(this.currentMonsterCard.spawnCard.eliteRules))
					{
						if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
						{
							Debug.LogFormat("Elite tier index {0} is unavailable", new object[]
							{
								i
							});
						}
					}
					else
					{
						float num = (float)this.currentMonsterCard.cost * eliteTierDef.costMultiplier * this.eliteBias;
						if (num <= this.monsterCredit)
						{
							this.currentActiveEliteTier = eliteTierDef;
							if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
							{
								Debug.LogFormat("Found valid elite tier index {0}", new object[]
								{
									i
								});
							}
						}
						else if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
						{
							Debug.LogFormat("Elite tier index {0} is too expensive ({1}/{2})", new object[]
							{
								i,
								num,
								this.monsterCredit
							});
						}
					}
				}
			}
			else if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
			{
				Debug.LogFormat("Card {0} cannot be elite. Skipping elite procedure.", new object[]
				{
					this.currentMonsterCard.spawnCard
				});
			}
			this.currentActiveEliteDef = this.currentActiveEliteTier.GetRandomAvailableEliteDef(this.rng);
			if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
			{
				Debug.LogFormat("Assigned elite index {0}", new object[]
				{
					this.currentActiveEliteDef
				});
			}
			this.lastAttemptedMonsterCard = this.currentMonsterCard;
			this.spawnCountInCurrentWave = 0;
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x000887C4 File Offset: 0x000869C4
		private bool AttemptSpawnOnTarget(Transform spawnTarget, DirectorPlacementRule.PlacementMode placementMode = DirectorPlacementRule.PlacementMode.Approximate)
		{
			if (this.currentMonsterCard == null)
			{
				if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
				{
					Debug.Log("Current monster card is null, pick new one.");
				}
				if (this.finalMonsterCardsSelection == null)
				{
					return false;
				}
				this.PrepareNewMonsterWave(this.finalMonsterCardsSelection.Evaluate(this.rng.nextNormalizedFloat));
			}
			if (this.spawnCountInCurrentWave >= this.maximumNumberToSpawnBeforeSkipping)
			{
				this.spawnCountInCurrentWave = 0;
				if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
				{
					Debug.LogFormat("Spawn count has hit the max ({0}/{1}). Aborting spawn.", new object[]
					{
						this.spawnCountInCurrentWave,
						this.maximumNumberToSpawnBeforeSkipping
					});
				}
				return false;
			}
			int num = this.currentMonsterCard.cost;
			int num2 = this.currentMonsterCard.cost;
			float num3 = 1f;
			EliteDef eliteDef = this.currentActiveEliteDef;
			num2 = (int)((float)num * this.currentActiveEliteTier.costMultiplier);
			if ((float)num2 <= this.monsterCredit)
			{
				num = num2;
				num3 = this.currentActiveEliteTier.costMultiplier;
			}
			else
			{
				this.ResetEliteType();
				eliteDef = this.currentActiveEliteDef;
			}
			if (!this.currentMonsterCard.IsAvailable())
			{
				if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
				{
					Debug.LogFormat("Spawn card {0} is invalid, aborting spawn.", new object[]
					{
						this.currentMonsterCard.spawnCard
					});
				}
				return false;
			}
			if (this.monsterCredit < (float)num)
			{
				if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
				{
					Debug.LogFormat("Spawn card {0} is too expensive, aborting spawn.", new object[]
					{
						this.currentMonsterCard.spawnCard
					});
				}
				return false;
			}
			if (this.skipSpawnIfTooCheap && this.consecutiveCheapSkips < this.maxConsecutiveCheapSkips && (float)(num2 * this.maximumNumberToSpawnBeforeSkipping) < this.monsterCredit)
			{
				if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
				{
					Debug.LogFormat("Card {0} seems too cheap ({1}/{2}). Comparing against most expensive possible ({3})", new object[]
					{
						this.currentMonsterCard.spawnCard,
						num * this.maximumNumberToSpawnBeforeSkipping,
						this.monsterCredit,
						this.mostExpensiveMonsterCostInDeck
					});
				}
				if (this.mostExpensiveMonsterCostInDeck > num)
				{
					this.consecutiveCheapSkips++;
					if (CombatDirector.cvDirectorCombatEnableInternalLogs.value)
					{
						Debug.LogFormat("Spawn card {0} is too cheap, aborting spawn.", new object[]
						{
							this.currentMonsterCard.spawnCard
						});
					}
					return false;
				}
			}
			SpawnCard spawnCard = this.currentMonsterCard.spawnCard;
			SpawnCard spawnCard2 = spawnCard;
			EliteDef eliteDef2 = eliteDef;
			float valueMultiplier = num3;
			bool preventOverhead = this.currentMonsterCard.preventOverhead;
			if (this.Spawn(spawnCard2, eliteDef2, spawnTarget, this.currentMonsterCard.spawnDistance, preventOverhead, valueMultiplier, placementMode))
			{
				this.monsterCredit -= (float)num;
				this.totalCreditsSpent += (float)num;
				this.spawnCountInCurrentWave++;
				this.consecutiveCheapSkips = 0;
				return true;
			}
			return false;
		}

		// Token: 0x06001FC4 RID: 8132 RVA: 0x00088A74 File Offset: 0x00086C74
		public bool Spawn(SpawnCard spawnCard, EliteDef eliteDef, Transform spawnTarget, DirectorCore.MonsterSpawnDistance spawnDistance, bool preventOverhead, float valueMultiplier = 1f, DirectorPlacementRule.PlacementMode placementMode = DirectorPlacementRule.PlacementMode.Approximate)
		{
			DirectorPlacementRule placementRule = new DirectorPlacementRule
			{
				placementMode = placementMode,
				spawnOnTarget = spawnTarget,
				preventOverhead = preventOverhead
			};
			float minDistance;
			float maxDistance;
			DirectorCore.GetMonsterSpawnDistance(spawnDistance, out minDistance, out maxDistance);
			placementRule.maxDistance = Mathf.Min(this.maxSpawnDistance, maxDistance * this.spawnDistanceMultiplier);
			placementRule.minDistance = Mathf.Max(0f, Mathf.Min(placementRule.maxDistance - this.minSpawnRange, minDistance * this.spawnDistanceMultiplier));
			DirectorSpawnRequest spawnRequest = new DirectorSpawnRequest(spawnCard, placementRule, this.rng)
			{
				ignoreTeamMemberLimit = this.ignoreTeamSizeLimit,
				teamIndexOverride = new TeamIndex?(this.teamIndex),
				onSpawnedServer = delegate(SpawnCard.SpawnResult result)
				{
					Debug.LogFormat("Spawn card {0} successfully spawned.", new object[]
					{
						spawnCard
					});
				}
			};
			Debug.Log("CombatDirector Spawn.......");
			if (!DirectorCore.instance.TrySpawnObject(spawnRequest))
			{
				Debug.LogFormat("Spawn card {0} failed to spawn. Aborting cost procedures.", new object[]
				{
					spawnCard
				});
				return false;
			}
			return true;
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x00088B94 File Offset: 0x00086D94
		private void FixedUpdate()
		{
			if (CombatDirector.cvDirectorCombatDisable.value)
			{
				return;
			}
			if (NetworkServer.active && Run.instance)
			{
				float compensatedDifficultyCoefficient = Run.instance.compensatedDifficultyCoefficient;
				for (int i = 0; i < this.moneyWaves.Length; i++)
				{
					float num = this.moneyWaves[i].Update(Time.fixedDeltaTime, compensatedDifficultyCoefficient);
					this.monsterCredit += num;
				}
				this.Simulate(Time.fixedDeltaTime);
			}
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00088C0C File Offset: 0x00086E0C
		public void CombatShrineActivation(Interactor interactor, float monsterCredit, DirectorCard chosenDirectorCard)
		{
			base.enabled = true;
			this.monsterCredit += monsterCredit;
			this.OverrideCurrentMonsterCard(chosenDirectorCard);
			this.monsterSpawnTimer = 0f;
			CharacterMaster component = chosenDirectorCard.spawnCard.prefab.GetComponent<CharacterMaster>();
			if (component)
			{
				CharacterBody component2 = component.bodyPrefab.GetComponent<CharacterBody>();
				if (component2)
				{
					Chat.SendBroadcastChat(new Chat.SubjectFormatChatMessage
					{
						subjectAsCharacterBody = interactor.GetComponent<CharacterBody>(),
						baseToken = "SHRINE_COMBAT_USE_MESSAGE",
						paramTokens = new string[]
						{
							component2.baseNameToken
						}
					});
				}
			}
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00088CA8 File Offset: 0x00086EA8
		public DirectorCard SelectMonsterCardForCombatShrine(float monsterCredit)
		{
			WeightedSelection<DirectorCard> weightedSelection = Util.CreateReasonableDirectorCardSpawnList(monsterCredit, this.maximumNumberToSpawnBeforeSkipping, 1);
			if (weightedSelection.Count == 0)
			{
				return null;
			}
			return weightedSelection.Evaluate(this.rng.nextNormalizedFloat);
		}

		// Token: 0x04002528 RID: 9512
		[Header("Core Director Values")]
		public string customName;

		// Token: 0x04002529 RID: 9513
		public float monsterCredit;

		// Token: 0x0400252A RID: 9514
		public float expRewardCoefficient = 0.2f;

		// Token: 0x0400252B RID: 9515
		public float goldRewardCoefficient = 1f;

		// Token: 0x0400252C RID: 9516
		public float minSeriesSpawnInterval = 0.1f;

		// Token: 0x0400252D RID: 9517
		public float maxSeriesSpawnInterval = 1f;

		// Token: 0x0400252E RID: 9518
		public float minRerollSpawnInterval = 2.3333333f;

		// Token: 0x0400252F RID: 9519
		public float maxRerollSpawnInterval = 4.3333335f;

		// Token: 0x04002530 RID: 9520
		public RangeFloat[] moneyWaveIntervals;

		// Token: 0x04002531 RID: 9521
		public TeamIndex teamIndex = TeamIndex.Monster;

		// Token: 0x04002532 RID: 9522
		[Tooltip("How much to multiply money wave yield by.")]
		[Header("Optional Behaviors")]
		public float creditMultiplier = 1f;

		// Token: 0x04002533 RID: 9523
		[Tooltip("The coefficient to multiply spawn distances. Used for combat shrines, to keep spawns nearby.")]
		public float spawnDistanceMultiplier = 1f;

		// Token: 0x04002534 RID: 9524
		[Tooltip("The maximum distance at which enemies will spawn.")]
		public float maxSpawnDistance = float.PositiveInfinity;

		// Token: 0x04002535 RID: 9525
		[Tooltip("Ensure that the minimum spawn distance is at least this many units away from the maxSpawnDistance")]
		public float minSpawnRange;

		// Token: 0x04002536 RID: 9526
		public bool shouldSpawnOneWave;

		// Token: 0x04002537 RID: 9527
		public bool targetPlayers = true;

		// Token: 0x04002538 RID: 9528
		public bool skipSpawnIfTooCheap = true;

		// Token: 0x04002539 RID: 9529
		[Tooltip("If skipSpawnIfTooCheap is true, we'll behave as though it's not set after this many consecutive skips")]
		public int maxConsecutiveCheapSkips = int.MaxValue;

		// Token: 0x0400253A RID: 9530
		public bool resetMonsterCardIfFailed = true;

		// Token: 0x0400253B RID: 9531
		public int maximumNumberToSpawnBeforeSkipping = 6;

		// Token: 0x0400253C RID: 9532
		public float eliteBias = 1f;

		// Token: 0x0400253D RID: 9533
		public CombatDirector.OnSpawnedServer onSpawnedServer;

		// Token: 0x0400253E RID: 9534
		[FormerlySerializedAs("_combatSquad")]
		public CombatSquad combatSquad;

		// Token: 0x0400253F RID: 9535
		[Tooltip("A special effect for when a monster appears will be instantiated at its position. Used for combat shrine.")]
		public GameObject spawnEffectPrefab;

		// Token: 0x04002540 RID: 9536
		public bool ignoreTeamSizeLimit;

		// Token: 0x04002541 RID: 9537
		[SerializeField]
		private DirectorCardCategorySelection _monsterCards;

		// Token: 0x04002542 RID: 9538
		public bool fallBackToStageMonsterCards = true;

		// Token: 0x04002546 RID: 9542
		public static readonly List<CombatDirector> instancesList = new List<CombatDirector>();

		// Token: 0x04002547 RID: 9543
		private bool hasStartedWave;

		// Token: 0x04002548 RID: 9544
		private Xoroshiro128Plus rng;

		// Token: 0x04002549 RID: 9545
		private DirectorCard currentMonsterCard;

		// Token: 0x0400254A RID: 9546
		private CombatDirector.EliteTierDef currentActiveEliteTier;

		// Token: 0x0400254B RID: 9547
		private EliteDef currentActiveEliteDef;

		// Token: 0x0400254C RID: 9548
		private int currentMonsterCardCost;

		// Token: 0x0400254D RID: 9549
		private WeightedSelection<DirectorCard> monsterCardsSelection;

		// Token: 0x0400254E RID: 9550
		private int consecutiveCheapSkips;

		// Token: 0x0400254F RID: 9551
		public GameObject currentSpawnTarget;

		// Token: 0x04002550 RID: 9552
		private float playerRetargetTimer;

		// Token: 0x04002551 RID: 9553
		private static readonly float baseEliteCostMultiplier = 6f;

		// Token: 0x04002552 RID: 9554
		private static CombatDirector.EliteTierDef[] eliteTiers;

		// Token: 0x04002553 RID: 9555
		private int spawnCountInCurrentWave;

		// Token: 0x04002554 RID: 9556
		public static readonly BoolConVar cvDirectorCombatDisable = new BoolConVar("director_combat_disable", ConVarFlags.SenderMustBeServer | ConVarFlags.Cheat, "0", "Disables all combat directors.");

		// Token: 0x04002555 RID: 9557
		private static readonly BoolConVar cvDirectorCombatEnableInternalLogs = new BoolConVar("director_combat_enable_internal_logs", ConVarFlags.None, "0", "Enables all combat directors to print internal logging.");

		// Token: 0x04002556 RID: 9558
		private CombatDirector.DirectorMoneyWave[] moneyWaves;

		// Token: 0x02000664 RID: 1636
		[Serializable]
		public class OnSpawnedServer : UnityEvent<GameObject>
		{
		}

		// Token: 0x02000665 RID: 1637
		public class EliteTierDef
		{
			// Token: 0x06001FCB RID: 8139 RVA: 0x00088DF6 File Offset: 0x00086FF6
			public bool CanSelect(SpawnCard.EliteRules rules)
			{
				return this.isAvailable(rules) && (this.canSelectWithoutAvailableEliteDef || this.HasAnyAvailableEliteDefs());
			}

			// Token: 0x06001FCC RID: 8140 RVA: 0x00088E18 File Offset: 0x00087018
			public bool HasAnyAvailableEliteDefs()
			{
				foreach (EliteDef eliteDef in this.eliteTypes)
				{
					if (eliteDef && eliteDef.IsAvailable())
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001FCD RID: 8141 RVA: 0x00088E54 File Offset: 0x00087054
			public EliteDef GetRandomAvailableEliteDef(Xoroshiro128Plus rng)
			{
				this.availableDefs.Clear();
				foreach (EliteDef eliteDef in this.eliteTypes)
				{
					if (eliteDef && eliteDef.IsAvailable())
					{
						this.availableDefs.Add(eliteDef);
					}
				}
				if (this.availableDefs.Count > 0)
				{
					return rng.NextElementUniform<EliteDef>(this.availableDefs);
				}
				return null;
			}

			// Token: 0x04002557 RID: 9559
			public float costMultiplier;

			// Token: 0x04002558 RID: 9560
			public EliteDef[] eliteTypes;

			// Token: 0x04002559 RID: 9561
			public Func<SpawnCard.EliteRules, bool> isAvailable = (SpawnCard.EliteRules rules) => true;

			// Token: 0x0400255A RID: 9562
			public bool canSelectWithoutAvailableEliteDef;

			// Token: 0x0400255B RID: 9563
			private List<EliteDef> availableDefs = new List<EliteDef>();
		}

		// Token: 0x02000667 RID: 1639
		private class DirectorMoneyWave
		{
			// Token: 0x06001FD2 RID: 8146 RVA: 0x00088F04 File Offset: 0x00087104
			public float Update(float deltaTime, float difficultyCoefficient)
			{
				this.timer += deltaTime;
				if (this.timer > this.interval)
				{
					float num = 0.5f + (float)Run.instance.participatingPlayerCount * 0.5f;
					this.timer -= this.interval;
					float num2 = 1f;
					float num3 = 0.4f;
					this.accumulatedAward += this.interval * this.multiplier * (num2 + num3 * difficultyCoefficient) * num;
				}
				float num4 = (float)Mathf.FloorToInt(this.accumulatedAward);
				this.accumulatedAward -= num4;
				return num4;
			}

			// Token: 0x0400255E RID: 9566
			public float interval;

			// Token: 0x0400255F RID: 9567
			public float timer;

			// Token: 0x04002560 RID: 9568
			public float multiplier;

			// Token: 0x04002561 RID: 9569
			private float accumulatedAward;
		}
	}
}
