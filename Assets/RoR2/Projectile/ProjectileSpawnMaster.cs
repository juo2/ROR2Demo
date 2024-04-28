using System;
using UnityEngine;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000BB3 RID: 2995
	[RequireComponent(typeof(ProjectileController))]
	public class ProjectileSpawnMaster : MonoBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x06004436 RID: 17462 RVA: 0x0011BFC4 File Offset: 0x0011A1C4
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			if (this.isAlive)
			{
				this.SpawnMaster();
				this.isAlive = false;
			}
		}

		// Token: 0x06004437 RID: 17463 RVA: 0x0011BFDB File Offset: 0x0011A1DB
		protected void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			if (NetworkServer.active && (!this.isAlive || this.stopwatch > this.maxLifetime))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x06004438 RID: 17464 RVA: 0x0011C018 File Offset: 0x0011A218
		public void SpawnMaster()
		{
			ProjectileController component = base.GetComponent<ProjectileController>();
			if (!component || !component.owner)
			{
				return;
			}
			CharacterBody component2 = component.owner.GetComponent<CharacterBody>();
			if (!component2 || !component2.master)
			{
				return;
			}
			if (!this.spawnCard)
			{
				return;
			}
			CharacterMaster characterMaster = component2.master;
			if (!characterMaster)
			{
				return;
			}
			if (this.deployableSlot != DeployableSlot.None && characterMaster.IsDeployableLimited(this.deployableSlot))
			{
				return;
			}
			Transform transform = base.transform;
			DirectorCore.MonsterSpawnDistance input = DirectorCore.MonsterSpawnDistance.Close;
			DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule
			{
				spawnOnTarget = transform,
				placementMode = DirectorPlacementRule.PlacementMode.Direct
			};
			DirectorCore.GetMonsterSpawnDistance(input, out directorPlacementRule.minDistance, out directorPlacementRule.maxDistance);
			DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(this.spawnCard, directorPlacementRule, new Xoroshiro128Plus(Run.instance.seed + (ulong)Run.instance.fixedTime));
			directorSpawnRequest.teamIndexOverride = new TeamIndex?(characterMaster.teamIndex);
			directorSpawnRequest.ignoreTeamMemberLimit = false;
			directorSpawnRequest.summonerBodyObject = component.owner;
			if (this.deployableSlot != DeployableSlot.None)
			{
				DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
				directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(delegate(SpawnCard.SpawnResult result)
				{
					if (result.success && result.spawnedInstance)
					{
						result.spawnedInstance.GetComponent<CharacterMaster>();
						Deployable deployable = result.spawnedInstance.AddComponent<Deployable>();
						characterMaster.AddDeployable(deployable, this.deployableSlot);
					}
				}));
			}
			DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
		}

		// Token: 0x040042B1 RID: 17073
		[SerializeField]
		private float maxLifetime = 10f;

		// Token: 0x040042B2 RID: 17074
		[SerializeField]
		private CharacterSpawnCard spawnCard;

		// Token: 0x040042B3 RID: 17075
		[SerializeField]
		public DeployableSlot deployableSlot = DeployableSlot.None;

		// Token: 0x040042B4 RID: 17076
		private float stopwatch;

		// Token: 0x040042B5 RID: 17077
		private bool isAlive = true;
	}
}
