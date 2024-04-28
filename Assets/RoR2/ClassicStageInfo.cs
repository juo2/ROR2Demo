using System;
using System.Collections;
using System.Runtime.CompilerServices;
using HG;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000658 RID: 1624
	[RequireComponent(typeof(SceneInfo))]
	public class ClassicStageInfo : MonoBehaviour
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00087302 File Offset: 0x00085502
		// (set) Token: 0x06001F80 RID: 8064 RVA: 0x0008730A File Offset: 0x0008550A
		public WeightedSelection<DirectorCard> monsterSelection { get; private set; }

		// Token: 0x06001F81 RID: 8065 RVA: 0x00087314 File Offset: 0x00085514
		private static void HandleSingleMonsterTypeArtifact(DirectorCardCategorySelection monsterCategories, Xoroshiro128Plus rng)
		{
			ClassicStageInfo.<>c__DisplayClass9_0 CS$<>8__locals1 = new ClassicStageInfo.<>c__DisplayClass9_0();
			CS$<>8__locals1.monsterCategories = monsterCategories;
			ScriptableObject.CreateInstance<DirectorCardCategorySelection>().CopyFrom(CS$<>8__locals1.monsterCategories);
			CS$<>8__locals1.baseCredits = 40f * Run.instance.difficultyCoefficient;
			CS$<>8__locals1.candidatesSelection = new WeightedSelection<DirectorCard>(8);
			CS$<>8__locals1.<HandleSingleMonsterTypeArtifact>g__AddCardsWhichPassCondition|2(new Predicate<DirectorCard>(CS$<>8__locals1.<HandleSingleMonsterTypeArtifact>g__CardIsAffordable|0));
			if (CS$<>8__locals1.candidatesSelection.Count == 0)
			{
				CS$<>8__locals1.<HandleSingleMonsterTypeArtifact>g__AddCardsWhichPassCondition|2(new Predicate<DirectorCard>(ClassicStageInfo.<>c.<>9.<HandleSingleMonsterTypeArtifact>g__ReturnTrue|9_1));
			}
			if (CS$<>8__locals1.candidatesSelection.Count == 0)
			{
				Debug.LogWarning("Could not collapse director card selection down to one, no cards passed the filters!");
				return;
			}
			ClassicStageInfo.<>c__DisplayClass9_1 CS$<>8__locals2 = new ClassicStageInfo.<>c__DisplayClass9_1();
			DirectorCard directorCard = CS$<>8__locals1.candidatesSelection.Evaluate(rng.nextNormalizedFloat);
			CS$<>8__locals1.monsterCategories.Clear();
			int categoryIndex = CS$<>8__locals1.monsterCategories.AddCategory("Basic Monsters", 1f);
			CS$<>8__locals1.monsterCategories.AddCard(categoryIndex, directorCard);
			CS$<>8__locals2.bodyIndex = directorCard.spawnCard.prefab.GetComponent<CharacterMaster>().bodyPrefab.GetComponent<CharacterBody>().bodyIndex;
			if (Stage.instance)
			{
				CS$<>8__locals2.<HandleSingleMonsterTypeArtifact>g__SetStageSingleMonsterType|3(Stage.instance);
				return;
			}
			Stage.onServerStageBegin += CS$<>8__locals2.<HandleSingleMonsterTypeArtifact>g__SetStageSingleMonsterType|3;
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00087444 File Offset: 0x00085644
		private static void HandleMixEnemyArtifact(DirectorCardCategorySelection monsterCategories, Xoroshiro128Plus rng)
		{
			ClassicStageInfo.<>c__DisplayClass10_0 CS$<>8__locals1;
			CS$<>8__locals1.rng = rng;
			CS$<>8__locals1.monsterCategories = monsterCategories;
			CS$<>8__locals1.monsterCategories.CopyFrom(RoR2Content.mixEnemyMonsterCards);
			if (CS$<>8__locals1.monsterCategories.categories.Length == 0)
			{
				Debug.LogError("MixEnemy monster cards are size 0!");
			}
			ClassicStageInfo.<HandleMixEnemyArtifact>g__TrimCategory|10_1("Basic Monsters", 3, ref CS$<>8__locals1);
			ClassicStageInfo.<HandleMixEnemyArtifact>g__TrimCategory|10_1("Minibosses", 3, ref CS$<>8__locals1);
			ClassicStageInfo.<HandleMixEnemyArtifact>g__TrimCategory|10_1("Champions", 3, ref CS$<>8__locals1);
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x000874B0 File Offset: 0x000856B0
		private static bool DirectorCardDoesNotForbidElite(DirectorCard directorCard)
		{
			CharacterSpawnCard characterSpawnCard = directorCard.spawnCard as CharacterSpawnCard;
			return !characterSpawnCard || !characterSpawnCard.noElites;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001F84 RID: 8068 RVA: 0x000874DC File Offset: 0x000856DC
		// (set) Token: 0x06001F85 RID: 8069 RVA: 0x000874E3 File Offset: 0x000856E3
		public static ClassicStageInfo instance { get; private set; }

		// Token: 0x06001F86 RID: 8070 RVA: 0x000874EB File Offset: 0x000856EB
		private void Awake()
		{
			if (NetworkServer.active)
			{
				this.seedServer = Run.instance.stageRng.nextUlong;
				this.rng = new Xoroshiro128Plus(this.seedServer);
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x0008751A File Offset: 0x0008571A
		private void Start()
		{
			this.RebuildCards();
			RunArtifactManager.onArtifactEnabledGlobal += this.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal += this.OnArtifactDisabled;
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00087544 File Offset: 0x00085744
		private void OnDestroy()
		{
			RunArtifactManager.onArtifactEnabledGlobal -= this.OnArtifactEnabled;
			RunArtifactManager.onArtifactDisabledGlobal -= this.OnArtifactDisabled;
			if (this.modifiableMonsterCategories)
			{
				UnityEngine.Object.Destroy(this.modifiableMonsterCategories);
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x00087580 File Offset: 0x00085780
		public IEnumerator BroadcastFamilySelection(string familySelectionChatString)
		{
			yield return new WaitForSeconds(1f);
			Chat.SendBroadcastChat(new Chat.SimpleChatMessage
			{
				baseToken = familySelectionChatString
			});
			yield break;
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x0008758F File Offset: 0x0008578F
		private void OnEnable()
		{
			ClassicStageInfo.instance = this;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x00087597 File Offset: 0x00085797
		private void OnDisable()
		{
			ClassicStageInfo.instance = null;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000875A0 File Offset: 0x000857A0
		private static float CalculateTotalWeight(DirectorCard[] cards)
		{
			float num = 0f;
			foreach (DirectorCard directorCard in cards)
			{
				num += (float)directorCard.selectionWeight;
			}
			return num;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x000875D4 File Offset: 0x000857D4
		private void RebuildCards()
		{
			Xoroshiro128Plus xoroshiro128Plus = new Xoroshiro128Plus(this.seedServer);
			Xoroshiro128Plus xoroshiro128Plus2 = new Xoroshiro128Plus(xoroshiro128Plus.nextUlong);
			Xoroshiro128Plus xoroshiro128Plus3 = new Xoroshiro128Plus(xoroshiro128Plus.nextUlong);
			Xoroshiro128Plus xoroshiro128Plus4 = new Xoroshiro128Plus(xoroshiro128Plus.nextUlong);
			Xoroshiro128Plus xoroshiro128Plus5 = new Xoroshiro128Plus(xoroshiro128Plus.nextUlong);
			if (this.interactableDccsPool)
			{
				DirectorCardCategorySelection directorCardCategorySelection = this.interactableDccsPool.GenerateWeightedSelection().Evaluate(xoroshiro128Plus5.nextNormalizedFloat);
				if (directorCardCategorySelection != null)
				{
					directorCardCategorySelection.OnSelected(this);
					this.interactableCategories = directorCardCategorySelection;
				}
			}
			DirectorCardCategorySelection exists = null;
			if (this.monsterDccsPool)
			{
				DirectorCardCategorySelection directorCardCategorySelection2 = this.monsterDccsPool.GenerateWeightedSelection().Evaluate(xoroshiro128Plus5.nextNormalizedFloat);
				if (directorCardCategorySelection2 != null)
				{
					directorCardCategorySelection2.OnSelected(this);
					exists = UnityEngine.Object.Instantiate<DirectorCardCategorySelection>(directorCardCategorySelection2);
				}
			}
			else if (this.monsterCategories)
			{
				exists = UnityEngine.Object.Instantiate<DirectorCardCategorySelection>(this.monsterCategories);
			}
			if (!exists)
			{
				return;
			}
			UnityEngine.Object.Destroy(this.modifiableMonsterCategories);
			RunArtifactManager instance = RunArtifactManager.instance;
			bool flag = instance != null && instance.IsArtifactEnabled(RoR2Content.Artifacts.singleMonsterTypeArtifactDef);
			RunArtifactManager instance2 = RunArtifactManager.instance;
			bool flag2 = instance2 != null && instance2.IsArtifactEnabled(RoR2Content.Artifacts.mixEnemyArtifactDef);
			RunArtifactManager instance3 = RunArtifactManager.instance;
			bool flag3 = instance3 != null && instance3.IsArtifactEnabled(RoR2Content.Artifacts.eliteOnlyArtifactDef);
			this.modifiableMonsterCategories = exists;
			if (flag2)
			{
				ClassicStageInfo.HandleMixEnemyArtifact(this.modifiableMonsterCategories, xoroshiro128Plus3);
			}
			else if (!this.monsterDccsPool && xoroshiro128Plus4.nextNormalizedFloat <= ClassicStageInfo.monsterFamilyChance && this.possibleMonsterFamilies != null)
			{
				Run instance4 = Run.instance;
				if (instance4 == null || instance4.canFamilyEventTrigger)
				{
					Debug.Log("Trying to find family selection...");
					WeightedSelection<ClassicStageInfo.MonsterFamily> weightedSelection = new WeightedSelection<ClassicStageInfo.MonsterFamily>(8);
					for (int i = 0; i < this.possibleMonsterFamilies.Length; i++)
					{
						ClassicStageInfo.MonsterFamily monsterFamily = this.possibleMonsterFamilies[i];
						if (Run.instance != null && monsterFamily.minimumStageCompletion <= Run.instance.stageClearCount && monsterFamily.maximumStageCompletion > Run.instance.stageClearCount)
						{
							weightedSelection.AddChoice(monsterFamily, monsterFamily.selectionWeight);
						}
					}
					if (weightedSelection.Count > 0)
					{
						ClassicStageInfo.MonsterFamily monsterFamily2 = weightedSelection.Evaluate(xoroshiro128Plus.nextNormalizedFloat);
						this.modifiableMonsterCategories.CopyFrom(monsterFamily2.monsterFamilyCategories);
						base.StartCoroutine("BroadcastFamilySelection", monsterFamily2.familySelectionChatString);
					}
				}
			}
			if (flag3)
			{
				this.modifiableMonsterCategories.RemoveCardsThatFailFilter(new Predicate<DirectorCard>(ClassicStageInfo.DirectorCardDoesNotForbidElite));
			}
			if (flag)
			{
				ClassicStageInfo.HandleSingleMonsterTypeArtifact(this.modifiableMonsterCategories, xoroshiro128Plus2);
			}
			this.monsterSelection = this.modifiableMonsterCategories.GenerateDirectorCardWeightedSelection();
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x0008786A File Offset: 0x00085A6A
		private void OnArtifactDisabled([NotNull] RunArtifactManager runArtifactManager, [NotNull] ArtifactDef artifactDef)
		{
			if (artifactDef == RoR2Content.Artifacts.mixEnemyArtifactDef || artifactDef == RoR2Content.Artifacts.singleMonsterTypeArtifactDef)
			{
				this.RebuildCards();
			}
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0008786A File Offset: 0x00085A6A
		private void OnArtifactEnabled([NotNull] RunArtifactManager runArtifactManager, [NotNull] ArtifactDef artifactDef)
		{
			if (artifactDef == RoR2Content.Artifacts.mixEnemyArtifactDef || artifactDef == RoR2Content.Artifacts.singleMonsterTypeArtifactDef)
			{
				this.RebuildCards();
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000878AC File Offset: 0x00085AAC
		[CompilerGenerated]
		internal static void <HandleMixEnemyArtifact>g__TrimSelection|10_0(ref DirectorCard[] cards, int requiredCount, ref ClassicStageInfo.<>c__DisplayClass10_0 A_2)
		{
			if (cards.Length <= requiredCount)
			{
				return;
			}
			DirectorCard[] array = ArrayUtils.Clone<DirectorCard>(cards);
			Util.ShuffleArray<DirectorCard>(array, A_2.rng);
			int num = array.Length - 1;
			while (num >= 0 && array.Length > requiredCount)
			{
				if (!array[num].IsAvailable())
				{
					ArrayUtils.ArrayRemoveAtAndResize<DirectorCard>(ref array, num, 1);
				}
				num--;
			}
			if (array.Length > requiredCount)
			{
				Array.Resize<DirectorCard>(ref array, requiredCount);
			}
			cards = array;
			foreach (DirectorCard directorCard in cards)
			{
				Debug.LogFormat("Selected {0}", new object[]
				{
					directorCard.spawnCard.name
				});
			}
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x00087944 File Offset: 0x00085B44
		[CompilerGenerated]
		internal static void <HandleMixEnemyArtifact>g__TrimCategory|10_1(string categoryName, int requiredCount, ref ClassicStageInfo.<>c__DisplayClass10_0 A_2)
		{
			DirectorCardCategorySelection.Category[] categories = A_2.monsterCategories.categories;
			for (int i = 0; i < categories.Length; i++)
			{
				if (string.CompareOrdinal(categoryName, categories[i].name) == 0)
				{
					Debug.LogFormat("Trimming {0} from {1} to {2}", new object[]
					{
						categoryName,
						categories[i].cards.Length,
						requiredCount
					});
					ClassicStageInfo.<HandleMixEnemyArtifact>g__TrimSelection|10_0(ref categories[i].cards, requiredCount, ref A_2);
				}
			}
		}

		// Token: 0x040024FF RID: 9471
		private DirectorCardCategorySelection modifiableMonsterCategories;

		// Token: 0x04002500 RID: 9472
		[Tooltip("We'll select a single DCCS from this pool when we enter the stage to determine which monsters can spawn.")]
		[SerializeField]
		private DccsPool monsterDccsPool;

		// Token: 0x04002501 RID: 9473
		[Tooltip("We'll select a single DCCS from this pool when we enter the stage to determine which interactables can spawn.")]
		[SerializeField]
		private DccsPool interactableDccsPool;

		// Token: 0x04002502 RID: 9474
		private ulong seedServer;

		// Token: 0x04002503 RID: 9475
		private Xoroshiro128Plus rng;

		// Token: 0x04002505 RID: 9477
		public int sceneDirectorInteractibleCredits = 200;

		// Token: 0x04002506 RID: 9478
		public int sceneDirectorMonsterCredits = 20;

		// Token: 0x04002507 RID: 9479
		public ClassicStageInfo.BonusInteractibleCreditObject[] bonusInteractibleCreditObjects;

		// Token: 0x04002509 RID: 9481
		public static float monsterFamilyChance = 0.02f;

		// Token: 0x0400250A RID: 9482
		[HideInInspector]
		[SerializeField]
		private DirectorCard[] monsterCards;

		// Token: 0x0400250B RID: 9483
		[HideInInspector]
		[SerializeField]
		public DirectorCard[] interactableCards;

		// Token: 0x0400250C RID: 9484
		[CanBeNull]
		[Tooltip("Deprecated.  Use MonsterDccsPool instead.")]
		[ShowFieldObsolete]
		public DirectorCardCategorySelection interactableCategories;

		// Token: 0x0400250D RID: 9485
		[SerializeField]
		[CanBeNull]
		[ShowFieldObsolete]
		[Tooltip("Deprecated.  Use MonsterDccsPool instead.")]
		private DirectorCardCategorySelection monsterCategories;

		// Token: 0x0400250E RID: 9486
		[ShowFieldObsolete]
		[Tooltip("Deprecated.  Use MonsterDccsPool instead.")]
		public ClassicStageInfo.MonsterFamily[] possibleMonsterFamilies;

		// Token: 0x02000659 RID: 1625
		[Serializable]
		public struct BonusInteractibleCreditObject
		{
			// Token: 0x0400250F RID: 9487
			public GameObject objectThatGrantsPointsIfEnabled;

			// Token: 0x04002510 RID: 9488
			public int points;
		}

		// Token: 0x0200065A RID: 1626
		[Serializable]
		public struct MonsterFamily
		{
			// Token: 0x04002511 RID: 9489
			[SerializeField]
			public DirectorCardCategorySelection monsterFamilyCategories;

			// Token: 0x04002512 RID: 9490
			public string familySelectionChatString;

			// Token: 0x04002513 RID: 9491
			public float selectionWeight;

			// Token: 0x04002514 RID: 9492
			public int minimumStageCompletion;

			// Token: 0x04002515 RID: 9493
			public int maximumStageCompletion;
		}
	}
}
