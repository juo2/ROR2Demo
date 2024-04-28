using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoR2
{
	// Token: 0x02000785 RID: 1925
	public class VoidMegaCrabItemBehavior : CharacterBody.ItemBehavior
	{
		// Token: 0x06002850 RID: 10320 RVA: 0x000AEF85 File Offset: 0x000AD185
		public static int GetMaxProjectiles(Inventory inventory)
		{
			return 1 + (inventory.GetItemCount(DLC1Content.Items.VoidMegaCrabItem) - 1);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000AEF98 File Offset: 0x000AD198
		private void Awake()
		{
			base.enabled = false;
			ulong seed = Run.instance.seed ^ (ulong)((long)Run.instance.stageClearCount);
			this.rng = new Xoroshiro128Plus(seed);
			this.spawnSelection = new WeightedSelection<CharacterSpawnCard>(8);
			this.spawnSelection.AddChoice(Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC1/VoidJailer/cscVoidJailerAlly.asset").WaitForCompletion(), 15f);
			this.spawnSelection.AddChoice(Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/Base/Nullifier/cscNullifierAlly.asset").WaitForCompletion(), 15f);
			this.spawnSelection.AddChoice(Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrabAlly.asset").WaitForCompletion(), 1f);
			this.placementRule = new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.Approximate,
				minDistance = 3f,
				maxDistance = 40f,
				spawnOnTarget = base.transform
			};
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000AF078 File Offset: 0x000AD278
		private void FixedUpdate()
		{
			this.spawnTimer += Time.fixedDeltaTime;
			if (!this.body.master.IsDeployableLimited(DeployableSlot.VoidMegaCrabItem) && this.spawnTimer > 60f / (float)this.stack)
			{
				this.spawnTimer = 0f;
				DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.spawnSelection.Evaluate(this.rng.nextNormalizedFloat), this.placementRule, this.rng);
				directorSpawnRequest.summonerBodyObject = base.gameObject;
				directorSpawnRequest.onSpawnedServer = new Action<SpawnCard.SpawnResult>(this.OnMasterSpawned);
				directorSpawnRequest.summonerBodyObject = base.gameObject;
				DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
			}
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x000AF12C File Offset: 0x000AD32C
		private void OnMasterSpawned(SpawnCard.SpawnResult spawnResult)
		{
			GameObject spawnedInstance = spawnResult.spawnedInstance;
			if (!spawnedInstance)
			{
				return;
			}
			CharacterMaster component = spawnedInstance.GetComponent<CharacterMaster>();
			if (component)
			{
				Deployable component2 = component.GetComponent<Deployable>();
				if (component2)
				{
					this.body.master.AddDeployable(component2, DeployableSlot.VoidMegaCrabItem);
				}
			}
		}

		// Token: 0x04002BFA RID: 11258
		private const float baseSecondsPerSpawn = 60f;

		// Token: 0x04002BFB RID: 11259
		private const int baseMaxAllies = 1;

		// Token: 0x04002BFC RID: 11260
		private const int maxAlliesPerStack = 1;

		// Token: 0x04002BFD RID: 11261
		private const float minSpawnDist = 3f;

		// Token: 0x04002BFE RID: 11262
		private const float maxSpawnDist = 40f;

		// Token: 0x04002BFF RID: 11263
		private float spawnTimer;

		// Token: 0x04002C00 RID: 11264
		private Xoroshiro128Plus rng;

		// Token: 0x04002C01 RID: 11265
		private WeightedSelection<CharacterSpawnCard> spawnSelection;

		// Token: 0x04002C02 RID: 11266
		private DirectorPlacementRule placementRule;
	}
}
