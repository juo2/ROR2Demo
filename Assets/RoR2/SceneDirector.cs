using System;
using System.Collections.Generic;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000870 RID: 2160
	public class SceneDirector : MonoBehaviour
	{
		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000C965B File Offset: 0x000C785B
		// (set) Token: 0x06002F4C RID: 12108 RVA: 0x000C9663 File Offset: 0x000C7863
		public int interactableCredit { get; set; }

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000C966C File Offset: 0x000C786C
		// (set) Token: 0x06002F4E RID: 12110 RVA: 0x000C9674 File Offset: 0x000C7874
		public float onPopulateCreditMultiplier { get; set; } = 1f;

		// Token: 0x06002F4F RID: 12111 RVA: 0x000C9680 File Offset: 0x000C7880
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus((ulong)Run.instance.stageRng.nextUint);
				float num = 0.5f + (float)Run.instance.participatingPlayerCount * 0.5f;
				ClassicStageInfo component = SceneInfo.instance.GetComponent<ClassicStageInfo>();
				if (component)
				{
					this.interactableCredit = (int)((float)component.sceneDirectorInteractibleCredits * num);
					if (component.bonusInteractibleCreditObjects != null)
					{
						for (int i = 0; i < component.bonusInteractibleCreditObjects.Length; i++)
						{
							ClassicStageInfo.BonusInteractibleCreditObject bonusInteractibleCreditObject = component.bonusInteractibleCreditObjects[i];
							if (bonusInteractibleCreditObject.objectThatGrantsPointsIfEnabled && bonusInteractibleCreditObject.objectThatGrantsPointsIfEnabled.activeSelf)
							{
								this.interactableCredit += bonusInteractibleCreditObject.points;
							}
						}
					}
					Debug.LogFormat("Spending {0} credits on interactables...", new object[]
					{
						this.interactableCredit
					});
					this.monsterCredit = (int)((float)component.sceneDirectorMonsterCredits * Run.instance.difficultyCoefficient);
				}
				Action<SceneDirector> action = SceneDirector.onPrePopulateSceneServer;
				if (action != null)
				{
					action(this);
				}
				this.PopulateScene();
				Action<SceneDirector> action2 = SceneDirector.onPostPopulateSceneServer;
				if (action2 == null)
				{
					return;
				}
				action2(this);
			}
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000C97A4 File Offset: 0x000C79A4
		private void PlaceTeleporter()
		{
			if (!this.teleporterInstance && this.teleporterSpawnCard)
			{
				this.teleporterInstance = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(this.teleporterSpawnCard, new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.Random
				}, this.rng));
				Run.instance.OnServerTeleporterPlaced(this, this.teleporterInstance);
			}
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x000C980C File Offset: 0x000C7A0C
		private static bool IsNodeSuitableForPod(NodeGraph nodeGraph, NodeGraph.NodeIndex nodeIndex)
		{
			NodeFlags nodeFlags;
			return nodeGraph.GetNodeFlags(nodeIndex, out nodeFlags) && (nodeFlags & NodeFlags.NoCeiling) != NodeFlags.None;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000C982C File Offset: 0x000C7A2C
		private void PlacePlayerSpawnsViaNodegraph()
		{
			bool usePod = Stage.instance.usePod;
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			NodeFlags requiredFlags = NodeFlags.None;
			NodeFlags nodeFlags = NodeFlags.None;
			nodeFlags |= NodeFlags.NoCharacterSpawn;
			List<NodeGraph.NodeIndex> list = groundNodes.GetActiveNodesForHullMaskWithFlagConditions(HullMask.Golem, requiredFlags, nodeFlags);
			if (usePod)
			{
				int num = list.Count - 1;
				while (num >= 0 && list.Count > 1)
				{
					if (!SceneDirector.IsNodeSuitableForPod(groundNodes, list[num]))
					{
						list.RemoveAt(num);
					}
					num--;
				}
			}
			if (PlayerSpawnInhibitor.readOnlyInstancesList.Count > 0)
			{
				List<NodeGraph.NodeIndex> list2 = new List<NodeGraph.NodeIndex>();
				for (int i = 0; i < list.Count; i++)
				{
					bool flag = false;
					using (IEnumerator<PlayerSpawnInhibitor> enumerator = PlayerSpawnInhibitor.readOnlyInstancesList.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.IsInhibiting(groundNodes, list[i]))
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						list2.Add(list[i]);
					}
				}
				if (list2.Count > 0)
				{
					list = list2;
				}
			}
			NodeGraph.NodeIndex nodeIndex;
			if (this.teleporterInstance)
			{
				Vector3 position = this.teleporterInstance.transform.position;
				List<SceneDirector.NodeDistanceSqrPair> list3 = new List<SceneDirector.NodeDistanceSqrPair>();
				for (int j = 0; j < list.Count; j++)
				{
					Vector3 b2;
					groundNodes.GetNodePosition(list[j], out b2);
					list3.Add(new SceneDirector.NodeDistanceSqrPair
					{
						nodeIndex = list[j],
						distanceSqr = (position - b2).sqrMagnitude
					});
				}
				list3.Sort((SceneDirector.NodeDistanceSqrPair a, SceneDirector.NodeDistanceSqrPair b) => a.distanceSqr.CompareTo(b.distanceSqr));
				int index = this.rng.RangeInt(list3.Count * 3 / 4, list3.Count);
				nodeIndex = list3[index].nodeIndex;
			}
			else
			{
				nodeIndex = this.rng.NextElementUniform<NodeGraph.NodeIndex>(list);
			}
			NodeGraphSpider nodeGraphSpider = new NodeGraphSpider(groundNodes, HullMask.Human);
			nodeGraphSpider.AddNodeForNextStep(nodeIndex);
			while (nodeGraphSpider.PerformStep())
			{
				List<NodeGraphSpider.StepInfo> collectedSteps = nodeGraphSpider.collectedSteps;
				for (int k = collectedSteps.Count - 1; k >= 0; k--)
				{
					if ((RoR2Application.maxPlayers <= list.Count && !list.Contains(collectedSteps[k].node)) || (usePod && !SceneDirector.IsNodeSuitableForPod(groundNodes, collectedSteps[k].node)))
					{
						collectedSteps.RemoveAt(k);
					}
				}
				if (collectedSteps.Count >= RoR2Application.maxPlayers)
				{
					break;
				}
			}
			List<NodeGraphSpider.StepInfo> collectedSteps2 = nodeGraphSpider.collectedSteps;
			Util.ShuffleList<NodeGraphSpider.StepInfo>(collectedSteps2, Run.instance.stageRng);
			int num2 = Math.Min(nodeGraphSpider.collectedSteps.Count, RoR2Application.maxPlayers);
			for (int l = 0; l < num2; l++)
			{
				SpawnPoint.AddSpawnPoint(groundNodes, collectedSteps2[l].node, this.rng);
			}
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x000C9B28 File Offset: 0x000C7D28
		private void RemoveAllExistingSpawnPoints()
		{
			List<SpawnPoint> list = new List<SpawnPoint>(SpawnPoint.readOnlyInstancesList);
			for (int i = 0; i < list.Count; i++)
			{
				UnityEngine.Object.Destroy(list[i].gameObject);
			}
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x000C9B64 File Offset: 0x000C7D64
		private void CullExistingSpawnPoints()
		{
			List<SpawnPoint> list = new List<SpawnPoint>(SpawnPoint.readOnlyInstancesList);
			if (this.teleporterInstance)
			{
				Vector3 teleporterPosition = this.teleporterInstance.transform.position;
				list.Sort((SpawnPoint a, SpawnPoint b) => (teleporterPosition - a.transform.position).sqrMagnitude.CompareTo((teleporterPosition - b.transform.position).sqrMagnitude));
				Debug.Log("reorder list");
				for (int i = list.Count; i >= 0; i--)
				{
					if (i < list.Count - RoR2Application.maxPlayers)
					{
						UnityEngine.Object.Destroy(list[i].gameObject);
					}
				}
			}
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000C9BF4 File Offset: 0x000C7DF4
		private void DefaultPlayerSpawnPointGenerator()
		{
			bool flag = SpawnPoint.readOnlyInstancesList.Count == 0;
			bool flag2 = Run.instance.autoGenerateSpawnPoints && Stage.instance && !Stage.instance.usePod;
			if (flag || flag2)
			{
				this.RemoveAllExistingSpawnPoints();
				this.PlacePlayerSpawnsViaNodegraph();
				return;
			}
			this.CullExistingSpawnPoints();
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000C9C50 File Offset: 0x000C7E50
		private WeightedSelection<DirectorCard> GenerateInteractableCardSelection()
		{
			DirectorCardCategorySelection directorCardCategorySelection = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
			if (ClassicStageInfo.instance && ClassicStageInfo.instance.interactableCategories)
			{
				directorCardCategorySelection.CopyFrom(ClassicStageInfo.instance.interactableCategories);
			}
			Action<SceneDirector, DirectorCardCategorySelection> action = SceneDirector.onGenerateInteractableCardSelection;
			if (action != null)
			{
				action(this, directorCardCategorySelection);
			}
			WeightedSelection<DirectorCard> result = directorCardCategorySelection.GenerateDirectorCardWeightedSelection();
			UnityEngine.Object.Destroy(directorCardCategorySelection);
			return result;
		}

		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06002F57 RID: 12119 RVA: 0x000C9CB0 File Offset: 0x000C7EB0
		// (remove) Token: 0x06002F58 RID: 12120 RVA: 0x000C9CE4 File Offset: 0x000C7EE4
		public static event Action<SceneDirector, DirectorCardCategorySelection> onGenerateInteractableCardSelection;

		// Token: 0x06002F59 RID: 12121 RVA: 0x000C9D18 File Offset: 0x000C7F18
		private void PopulateScene()
		{
			WeightedSelection<DirectorCard> deck = this.GenerateInteractableCardSelection();
			this.PlaceTeleporter();
			Dictionary<DirectorCard, int> dictionary = new Dictionary<DirectorCard, int>();
			this.interactableCredit = (int)Mathf.Floor((float)this.interactableCredit * this.onPopulateCreditMultiplier);
			while (this.interactableCredit > 0)
			{
				DirectorCard directorCard = this.SelectCard(deck, this.interactableCredit);
				if (directorCard == null)
				{
					break;
				}
				if (directorCard.IsAvailable())
				{
					if (!dictionary.ContainsKey(directorCard))
					{
						InteractableSpawnCard interactableSpawnCard = directorCard.spawnCard as InteractableSpawnCard;
						if (interactableSpawnCard)
						{
							int value = int.MaxValue;
							if (interactableSpawnCard.maxSpawnsPerStage >= 0)
							{
								value = interactableSpawnCard.maxSpawnsPerStage;
							}
							dictionary[directorCard] = value;
						}
					}
					int num;
					if (dictionary.TryGetValue(directorCard, out num) && num > 0)
					{
						dictionary[directorCard] = num - 1;
						this.interactableCredit -= directorCard.cost;
						if (Run.instance)
						{
							int i = 0;
							while (i < 10)
							{
								DirectorPlacementRule placementRule = new DirectorPlacementRule
								{
									placementMode = DirectorPlacementRule.PlacementMode.Random
								};
								GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(directorCard.spawnCard, placementRule, this.rng));
								if (gameObject)
								{
									PurchaseInteraction component = gameObject.GetComponent<PurchaseInteraction>();
									if (component && component.costType == CostTypeIndex.Money)
									{
										component.Networkcost = Run.instance.GetDifficultyScaledCost(component.cost);
										break;
									}
									break;
								}
								else
								{
									i++;
								}
							}
						}
					}
				}
			}
			Action action = new Action(this.DefaultPlayerSpawnPointGenerator);
			SceneDirector.GenerateSpawnPointsDelegate generateSpawnPointsDelegate = SceneDirector.onPreGeneratePlayerSpawnPointsServer;
			if (generateSpawnPointsDelegate != null)
			{
				generateSpawnPointsDelegate(this, ref action);
			}
			if (action != null)
			{
				action();
			}
			Run.instance.OnPlayerSpawnPointsPlaced(this);
			Action<SceneDirector> action2 = SceneDirector.onPrePopulateMonstersSceneServer;
			if (action2 != null)
			{
				action2(this);
			}
			if (Run.instance && CombatDirector.cvDirectorCombatDisable.value)
			{
				this.monsterCredit = 0;
			}
			CombatDirector component2 = base.GetComponent<CombatDirector>();
			if (component2)
			{
				float num2 = component2.expRewardCoefficient;
				float num3 = component2.eliteBias;
				float num4 = component2.spawnDistanceMultiplier;
				component2.monsterCredit += (float)this.monsterCredit;
				component2.expRewardCoefficient = this.expRewardCoefficient;
				component2.eliteBias = this.eliteBias;
				component2.spawnDistanceMultiplier = this.spawnDistanceMultiplier;
				this.monsterCredit = 0;
				component2.onSpawnedServer.AddListener(new UnityAction<GameObject>(SceneDirector.<>c.<>9.<PopulateScene>g__OnMonsterSpawnedServer|27_0));
				component2.SpendAllCreditsOnMapSpawns(TeleporterInteraction.instance ? TeleporterInteraction.instance.transform : null);
				component2.onSpawnedServer.RemoveListener(new UnityAction<GameObject>(SceneDirector.<>c.<>9.<PopulateScene>g__OnMonsterSpawnedServer|27_0));
				component2.expRewardCoefficient = num2;
				component2.eliteBias = num3;
				component2.spawnDistanceMultiplier = num4;
			}
			if (SceneInfo.instance.countsAsStage)
			{
				Xoroshiro128Plus xoroshiro128Plus = new Xoroshiro128Plus(this.rng.nextUlong);
				int num5 = 0;
				using (IEnumerator<CharacterMaster> enumerator = CharacterMaster.readOnlyInstancesList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.inventory.GetItemCount(RoR2Content.Items.TreasureCache) > 0)
						{
							num5++;
						}
					}
				}
				for (int j = 0; j < num5; j++)
				{
					DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/iscLockbox"), new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Random
					}, xoroshiro128Plus));
				}
				Xoroshiro128Plus xoroshiro128Plus2 = new Xoroshiro128Plus(this.rng.nextUlong);
				int num6 = 0;
				using (IEnumerator<CharacterMaster> enumerator = CharacterMaster.readOnlyInstancesList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.inventory.GetItemCount(DLC1Content.Items.TreasureCacheVoid) > 0)
						{
							num6++;
						}
					}
				}
				for (int k = 0; k < num6; k++)
				{
					DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/iscLockboxVoid"), new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Random
					}, xoroshiro128Plus2));
				}
				Xoroshiro128Plus xoroshiro128Plus3 = new Xoroshiro128Plus(this.rng.nextUlong);
				int num7 = 0;
				using (IEnumerator<CharacterMaster> enumerator = CharacterMaster.readOnlyInstancesList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.inventory.GetItemCount(DLC1Content.Items.FreeChest) > 0)
						{
							num7++;
						}
					}
				}
				for (int l = 0; l < num7; l++)
				{
					DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/InteractableSpawnCard/iscFreeChest"), new DirectorPlacementRule
					{
						placementMode = DirectorPlacementRule.PlacementMode.Random
					}, xoroshiro128Plus3));
				}
			}
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000CA1B8 File Offset: 0x000C83B8
		private DirectorCard SelectCard(WeightedSelection<DirectorCard> deck, int maxCost)
		{
			SceneDirector.cardSelector.Clear();
			int i = 0;
			int count = deck.Count;
			while (i < count)
			{
				WeightedSelection<DirectorCard>.ChoiceInfo choice = deck.GetChoice(i);
				if (choice.value.cost <= maxCost)
				{
					SceneDirector.cardSelector.AddChoice(choice);
				}
				i++;
			}
			if (SceneDirector.cardSelector.Count == 0)
			{
				return null;
			}
			return SceneDirector.cardSelector.Evaluate(this.rng.nextNormalizedFloat);
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000CA226 File Offset: 0x000C8426
		public void ReduceMonsterCredits(int creditReduction)
		{
			this.monsterCredit = Mathf.Max(0, this.monsterCredit - creditReduction);
		}

		// Token: 0x14000097 RID: 151
		// (add) Token: 0x06002F5C RID: 12124 RVA: 0x000CA23C File Offset: 0x000C843C
		// (remove) Token: 0x06002F5D RID: 12125 RVA: 0x000CA270 File Offset: 0x000C8470
		public static event SceneDirector.GenerateSpawnPointsDelegate onPreGeneratePlayerSpawnPointsServer;

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06002F5E RID: 12126 RVA: 0x000CA2A4 File Offset: 0x000C84A4
		// (remove) Token: 0x06002F5F RID: 12127 RVA: 0x000CA2D8 File Offset: 0x000C84D8
		public static event Action<SceneDirector> onPrePopulateSceneServer;

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x06002F60 RID: 12128 RVA: 0x000CA30C File Offset: 0x000C850C
		// (remove) Token: 0x06002F61 RID: 12129 RVA: 0x000CA340 File Offset: 0x000C8540
		public static event Action<SceneDirector> onPrePopulateMonstersSceneServer;

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06002F62 RID: 12130 RVA: 0x000CA374 File Offset: 0x000C8574
		// (remove) Token: 0x06002F63 RID: 12131 RVA: 0x000CA3A8 File Offset: 0x000C85A8
		public static event Action<SceneDirector> onPostPopulateSceneServer;

		// Token: 0x04003128 RID: 12584
		public SpawnCard teleporterSpawnCard;

		// Token: 0x04003129 RID: 12585
		public float expRewardCoefficient;

		// Token: 0x0400312A RID: 12586
		public float eliteBias;

		// Token: 0x0400312B RID: 12587
		public float spawnDistanceMultiplier;

		// Token: 0x0400312E RID: 12590
		private int monsterCredit;

		// Token: 0x0400312F RID: 12591
		public GameObject teleporterInstance;

		// Token: 0x04003130 RID: 12592
		private Xoroshiro128Plus rng;

		// Token: 0x04003132 RID: 12594
		private static readonly WeightedSelection<DirectorCard> cardSelector = new WeightedSelection<DirectorCard>(8);

		// Token: 0x02000871 RID: 2161
		private struct NodeDistanceSqrPair
		{
			// Token: 0x04003137 RID: 12599
			public NodeGraph.NodeIndex nodeIndex;

			// Token: 0x04003138 RID: 12600
			public float distanceSqr;
		}

		// Token: 0x02000872 RID: 2162
		// (Invoke) Token: 0x06002F67 RID: 12135
		public delegate void GenerateSpawnPointsDelegate(SceneDirector sceneDirector, ref Action generationMethod);
	}
}
