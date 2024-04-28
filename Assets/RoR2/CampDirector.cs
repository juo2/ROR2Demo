using System;
using System.Runtime.CompilerServices;
using RoR2.Navigation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000617 RID: 1559
	public class CampDirector : MonoBehaviour
	{
		// Token: 0x06001CAC RID: 7340 RVA: 0x0007A019 File Offset: 0x00078219
		private void Start()
		{
			if (NetworkServer.active)
			{
				this.rng = new Xoroshiro128Plus((ulong)Run.instance.stageRng.nextUint);
				this.CalculateCredits();
				this.PopulateCamp();
			}
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0007A049 File Offset: 0x00078249
		private void OnEnable()
		{
			SceneDirector.onPrePopulateMonstersSceneServer += this.OnSceneDirectorPrePopulate;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0007A05C File Offset: 0x0007825C
		private void OnDisable()
		{
			SceneDirector.onPrePopulateMonstersSceneServer -= this.OnSceneDirectorPrePopulate;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0007A06F File Offset: 0x0007826F
		private void OnSceneDirectorPrePopulate(SceneDirector sceneDirector)
		{
			this.CalculateCredits();
			sceneDirector.ReduceMonsterCredits((int)((float)this.monsterCredit * this.monsterCreditPenaltyCoefficient));
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0007A08C File Offset: 0x0007828C
		private WeightedSelection<DirectorCard> GenerateInteractableCardSelection()
		{
			DirectorCardCategorySelection directorCardCategorySelection = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
			if (this.interactableDirectorCards)
			{
				directorCardCategorySelection.CopyFrom(this.interactableDirectorCards);
			}
			else if (ClassicStageInfo.instance && ClassicStageInfo.instance.interactableCategories)
			{
				directorCardCategorySelection.CopyFrom(ClassicStageInfo.instance.interactableCategories);
			}
			WeightedSelection<DirectorCard> result = directorCardCategorySelection.GenerateDirectorCardWeightedSelection();
			UnityEngine.Object.Destroy(directorCardCategorySelection);
			return result;
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0007A0F8 File Offset: 0x000782F8
		private void PopulateCamp()
		{
			WeightedSelection<DirectorCard> deck = this.GenerateInteractableCardSelection();
			while (this.baseInteractableCredit > 0)
			{
				DirectorCard directorCard = this.SelectCard(deck, this.baseInteractableCredit);
				if (directorCard == null)
				{
					break;
				}
				if (directorCard.IsAvailable())
				{
					this.baseInteractableCredit -= directorCard.cost;
					if (Run.instance)
					{
						int i = 0;
						while (i < 10)
						{
							DirectorPlacementRule placementRule = new DirectorPlacementRule
							{
								placementMode = DirectorPlacementRule.PlacementMode.Approximate,
								minDistance = this.campMinimumRadius,
								maxDistance = this.campMaximumRadius,
								position = this.campCenterTransform.position,
								spawnOnTarget = this.campCenterTransform
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
			if (Run.instance && CombatDirector.cvDirectorCombatDisable.value)
			{
				this.monsterCredit = 0;
			}
			if (this.combatDirector)
			{
				this.combatDirector.monsterCredit += (float)this.monsterCredit;
				this.monsterCredit = 0;
				this.combatDirector.onSpawnedServer.AddListener(new UnityAction<GameObject>(this.<PopulateCamp>g__OnMonsterSpawnedServer|18_0));
				this.combatDirector.SpendAllCreditsOnMapSpawns(this.campCenterTransform);
				this.combatDirector.onSpawnedServer.RemoveListener(new UnityAction<GameObject>(this.<PopulateCamp>g__OnMonsterSpawnedServer|18_0));
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x0007A2A8 File Offset: 0x000784A8
		private DirectorCard SelectCard(WeightedSelection<DirectorCard> deck, int maxCost)
		{
			CampDirector.cardSelector.Clear();
			int i = 0;
			int count = deck.Count;
			while (i < count)
			{
				WeightedSelection<DirectorCard>.ChoiceInfo choice = deck.GetChoice(i);
				if (choice.value.cost <= maxCost)
				{
					CampDirector.cardSelector.AddChoice(choice);
				}
				i++;
			}
			if (CampDirector.cardSelector.Count == 0)
			{
				return null;
			}
			return CampDirector.cardSelector.Evaluate(this.rng.nextNormalizedFloat);
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0007A316 File Offset: 0x00078516
		private void CalculateCredits()
		{
			if (this.scaleMonsterCreditWithDifficultyCoefficient)
			{
				this.monsterCredit = Mathf.CeilToInt((float)this.baseMonsterCredit * Run.instance.difficultyCoefficient);
				return;
			}
			this.monsterCredit = this.baseMonsterCredit;
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0007A36C File Offset: 0x0007856C
		[CompilerGenerated]
		private void <PopulateCamp>g__OnMonsterSpawnedServer|18_0(GameObject masterObject)
		{
			EliteDef eliteDef = this.eliteDef;
			EquipmentIndex? equipmentIndex;
			if (eliteDef == null)
			{
				equipmentIndex = null;
			}
			else
			{
				EquipmentDef eliteEquipmentDef = eliteDef.eliteEquipmentDef;
				equipmentIndex = ((eliteEquipmentDef != null) ? new EquipmentIndex?(eliteEquipmentDef.equipmentIndex) : null);
			}
			EquipmentIndex equipmentIndex2 = equipmentIndex ?? EquipmentIndex.None;
			CharacterMaster component = masterObject.GetComponent<CharacterMaster>();
			GameObject bodyObject = component.GetBodyObject();
			if (bodyObject)
			{
				foreach (EntityStateMachine entityStateMachine in bodyObject.GetComponents<EntityStateMachine>())
				{
					entityStateMachine.initialStateType = entityStateMachine.mainStateType;
				}
			}
			if (equipmentIndex2 != EquipmentIndex.None)
			{
				component.inventory.SetEquipmentIndex(equipmentIndex2);
			}
		}

		// Token: 0x04002284 RID: 8836
		[Header("Main Properties")]
		[Tooltip("Which interactables the camp can spawn. If left blank, will fall back to the stage's.")]
		public DirectorCardCategorySelection interactableDirectorCards;

		// Token: 0x04002285 RID: 8837
		public int baseMonsterCredit;

		// Token: 0x04002286 RID: 8838
		public int baseInteractableCredit;

		// Token: 0x04002287 RID: 8839
		public float campMinimumRadius;

		// Token: 0x04002288 RID: 8840
		public float campMaximumRadius;

		// Token: 0x04002289 RID: 8841
		public Transform campCenterTransform;

		// Token: 0x0400228A RID: 8842
		public CombatDirector combatDirector;

		// Token: 0x0400228B RID: 8843
		[Header("Combat Director Properties")]
		public EliteDef eliteDef;

		// Token: 0x0400228C RID: 8844
		[Header("Optional Properties")]
		public bool scaleMonsterCreditWithDifficultyCoefficient;

		// Token: 0x0400228D RID: 8845
		[Tooltip("The amount of credits to take away from the scenedirector's monster credits. A value of 1 takes away all the credits the camp spends - a value of 0 takes away none.")]
		[Range(0f, 1f)]
		public float monsterCreditPenaltyCoefficient = 0.5f;

		// Token: 0x0400228E RID: 8846
		private Xoroshiro128Plus rng;

		// Token: 0x0400228F RID: 8847
		private int monsterCredit;

		// Token: 0x04002290 RID: 8848
		private static readonly WeightedSelection<DirectorCard> cardSelector = new WeightedSelection<DirectorCard>(8);

		// Token: 0x02000618 RID: 1560
		private struct NodeDistanceSqrPair
		{
			// Token: 0x04002291 RID: 8849
			public NodeGraph.NodeIndex nodeIndex;

			// Token: 0x04002292 RID: 8850
			public float distanceSqr;
		}
	}
}
