using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace RoR2
{
	// Token: 0x02000584 RID: 1412
	public class DeployableMinionSpawner : IDisposable
	{
		// Token: 0x06001951 RID: 6481 RVA: 0x0006D638 File Offset: 0x0006B838
		public DeployableMinionSpawner([CanBeNull] CharacterMaster ownerMaster, DeployableSlot deployableSlot, [NotNull] Xoroshiro128Plus rng)
		{
			this.ownerMaster = ownerMaster;
			this.deployableSlot = deployableSlot;
			this.rng = rng;
			RoR2Application.onFixedUpdate += this.FixedUpdate;
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x0006D69D File Offset: 0x0006B89D
		public void Dispose()
		{
			RoR2Application.onFixedUpdate -= this.FixedUpdate;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0006D6B0 File Offset: 0x0006B8B0
		[CanBeNull]
		public CharacterMaster ownerMaster { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06001954 RID: 6484 RVA: 0x0006D6B8 File Offset: 0x0006B8B8
		public DeployableSlot deployableSlot { get; }

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001955 RID: 6485 RVA: 0x0006D6C0 File Offset: 0x0006B8C0
		// (remove) Token: 0x06001956 RID: 6486 RVA: 0x0006D6F8 File Offset: 0x0006B8F8
		public event Action<SpawnCard.SpawnResult> onMinionSpawnedServer;

		// Token: 0x06001957 RID: 6487 RVA: 0x0006D730 File Offset: 0x0006B930
		private void FixedUpdate()
		{
			if (!this.ownerMaster)
			{
				return;
			}
			float fixedDeltaTime = Time.fixedDeltaTime;
			int deployableCount = this.ownerMaster.GetDeployableCount(this.deployableSlot);
			int deployableSameSlotLimit = this.ownerMaster.GetDeployableSameSlotLimit(this.deployableSlot);
			if (deployableCount < deployableSameSlotLimit)
			{
				this.respawnStopwatch += fixedDeltaTime;
				if (this.respawnStopwatch >= this.respawnInterval)
				{
					CharacterBody body = this.ownerMaster.GetBody();
					if (body && this.spawnCard)
					{
						try
						{
							this.SpawnMinion(this.spawnCard, body);
						}
						catch (Exception message)
						{
							Debug.LogError(message);
						}
						this.respawnStopwatch = 0f;
					}
				}
			}
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x0006D7E8 File Offset: 0x0006B9E8
		private void SpawnMinion([NotNull] SpawnCard spawnCard, [NotNull] CharacterBody spawnTarget)
		{
			DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(spawnCard, new DirectorPlacementRule
			{
				placementMode = DirectorPlacementRule.PlacementMode.Approximate,
				minDistance = this.minSpawnDistance,
				maxDistance = this.maxSpawnDistance,
				spawnOnTarget = spawnTarget.transform
			}, RoR2Application.rng);
			directorSpawnRequest.summonerBodyObject = this.ownerMaster.GetBodyObject();
			directorSpawnRequest.onSpawnedServer = new Action<SpawnCard.SpawnResult>(this.OnMinionSpawnedServerInternal);
			DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0006D860 File Offset: 0x0006BA60
		private void OnMinionSpawnedServerInternal([NotNull] SpawnCard.SpawnResult spawnResult)
		{
			GameObject spawnedInstance = spawnResult.spawnedInstance;
			if (spawnedInstance)
			{
				CharacterMaster component = spawnedInstance.GetComponent<CharacterMaster>();
				Deployable deployable = spawnedInstance.AddComponent<Deployable>();
				deployable.onUndeploy = (deployable.onUndeploy ?? new UnityEvent());
				if (component)
				{
					deployable.onUndeploy.AddListener(new UnityAction(component.TrueKill));
				}
				else
				{
					deployable.onUndeploy.AddListener(delegate()
					{
						UnityEngine.Object.Destroy(spawnedInstance);
					});
				}
				this.ownerMaster.AddDeployable(deployable, this.deployableSlot);
			}
			Action<SpawnCard.SpawnResult> action = this.onMinionSpawnedServer;
			if (action == null)
			{
				return;
			}
			action(spawnResult);
		}

		// Token: 0x04001FBA RID: 8122
		public float respawnInterval = 30f;

		// Token: 0x04001FBB RID: 8123
		[CanBeNull]
		public SpawnCard spawnCard;

		// Token: 0x04001FBD RID: 8125
		public float minSpawnDistance = 3f;

		// Token: 0x04001FBE RID: 8126
		public float maxSpawnDistance = 40f;

		// Token: 0x04001FBF RID: 8127
		[NotNull]
		private Xoroshiro128Plus rng;

		// Token: 0x04001FC0 RID: 8128
		private float respawnStopwatch = float.PositiveInfinity;
	}
}
