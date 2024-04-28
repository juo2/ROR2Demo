using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2
{
	// Token: 0x02000773 RID: 1907
	public class InteractableSpawner : NetworkBehaviour
	{
		// Token: 0x0600279B RID: 10139 RVA: 0x000AC038 File Offset: 0x000AA238
		[Server]
		public void Spawn(Xoroshiro128Plus rng)
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InteractableSpawner::Spawn(Xoroshiro128Plus)' called on client");
				return;
			}
			WeightedSelection<DirectorCard> weightedSelection = this.interactableCards.GenerateDirectorCardWeightedSelection();
			float num = this.creditsToSpawn;
			while (num > 0f)
			{
				DirectorCard directorCard = weightedSelection.Evaluate(rng.nextNormalizedFloat);
				if (directorCard == null)
				{
					break;
				}
				if (!directorCard.IsAvailable())
				{
					num -= 1f;
				}
				else
				{
					num -= (float)directorCard.cost;
					for (int i = 0; i < 10; i++)
					{
						DirectorPlacementRule placementRule = new DirectorPlacementRule
						{
							placementMode = DirectorPlacementRule.PlacementMode.Random
						};
						GameObject gameObject = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(directorCard.spawnCard, placementRule, rng));
						if (gameObject)
						{
							PurchaseInteraction component = gameObject.GetComponent<PurchaseInteraction>();
							if (component && component.costType == CostTypeIndex.Money)
							{
								component.Networkcost = Run.instance.GetDifficultyScaledCost(component.cost);
							}
							this.spawnedObjects.Add(gameObject);
							break;
						}
					}
				}
			}
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000AC130 File Offset: 0x000AA330
		[Server]
		public void DestroySpawnedInteractables()
		{
			if (!NetworkServer.active)
			{
				Debug.LogWarning("[Server] function 'System.Void RoR2.InteractableSpawner::DestroySpawnedInteractables()' called on client");
				return;
			}
			foreach (GameObject obj in this.spawnedObjects)
			{
				UnityEngine.Object.Destroy(obj);
			}
			this.spawnedObjects.Clear();
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000026ED File Offset: 0x000008ED
		private void UNetVersion()
		{
		}

		// Token: 0x0600279F RID: 10143 RVA: 0x000AC1B4 File Offset: 0x000AA3B4
		public override bool OnSerialize(NetworkWriter writer, bool forceAll)
		{
			bool result;
			return result;
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x000026ED File Offset: 0x000008ED
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x000026ED File Offset: 0x000008ED
		public override void PreStartClient()
		{
		}

		// Token: 0x04002B90 RID: 11152
		[Tooltip("The selection of director cards to use when spawning stuff")]
		[SerializeField]
		private DirectorCardCategorySelection interactableCards;

		// Token: 0x04002B91 RID: 11153
		[Tooltip("How much stuff should this thing spawn")]
		[SerializeField]
		private float creditsToSpawn;

		// Token: 0x04002B92 RID: 11154
		private List<GameObject> spawnedObjects = new List<GameObject>();
	}
}
