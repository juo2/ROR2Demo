using System;
using EntityStates.GummyClone;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace RoR2.Projectile
{
	// Token: 0x02000B79 RID: 2937
	[RequireComponent(typeof(ProjectileController))]
	public class GummyCloneProjectile : MonoBehaviour, IProjectileImpactBehavior
	{
		// Token: 0x060042F2 RID: 17138 RVA: 0x00115571 File Offset: 0x00113771
		public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
		{
			if (this.isAlive)
			{
				this.SpawnGummyClone();
				this.isAlive = false;
			}
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x00115588 File Offset: 0x00113788
		protected void FixedUpdate()
		{
			this.stopwatch += Time.fixedDeltaTime;
			if (NetworkServer.active && (!this.isAlive || this.stopwatch > this.maxLifetime))
			{
				UnityEngine.Object.Destroy(base.gameObject);
			}
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x001155C4 File Offset: 0x001137C4
		public void SpawnGummyClone()
		{
			ProjectileController component = base.GetComponent<ProjectileController>();
			if (!component || !component.owner)
			{
				return;
			}
			CharacterBody component2 = component.owner.GetComponent<CharacterBody>();
			if (!component2)
			{
				return;
			}
			CharacterMaster characterMaster = component2.master;
			if (!characterMaster || characterMaster.IsDeployableLimited(DeployableSlot.GummyClone))
			{
				return;
			}
			MasterCopySpawnCard masterCopySpawnCard = MasterCopySpawnCard.FromMaster(characterMaster, false, false, null);
			if (!masterCopySpawnCard)
			{
				return;
			}
			masterCopySpawnCard.GiveItem(DLC1Content.Items.GummyCloneIdentifier, 1);
			masterCopySpawnCard.GiveItem(RoR2Content.Items.BoostDamage, this.damageBoostCount);
			masterCopySpawnCard.GiveItem(RoR2Content.Items.BoostHp, this.hpBoostCount);
			Transform transform = base.transform;
			DirectorCore.MonsterSpawnDistance input = DirectorCore.MonsterSpawnDistance.Close;
			DirectorPlacementRule directorPlacementRule = new DirectorPlacementRule
			{
				spawnOnTarget = transform,
				placementMode = DirectorPlacementRule.PlacementMode.Direct
			};
			DirectorCore.GetMonsterSpawnDistance(input, out directorPlacementRule.minDistance, out directorPlacementRule.maxDistance);
			DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(masterCopySpawnCard, directorPlacementRule, new Xoroshiro128Plus(Run.instance.seed + (ulong)Run.instance.fixedTime));
			directorSpawnRequest.teamIndexOverride = new TeamIndex?(characterMaster.teamIndex);
			directorSpawnRequest.ignoreTeamMemberLimit = true;
			directorSpawnRequest.summonerBodyObject = component2.gameObject;
			DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
			directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(delegate(SpawnCard.SpawnResult result)
			{
				CharacterMaster component3 = result.spawnedInstance.GetComponent<CharacterMaster>();
				Deployable deployable = result.spawnedInstance.AddComponent<Deployable>();
				characterMaster.AddDeployable(deployable, DeployableSlot.GummyClone);
				deployable.onUndeploy = (deployable.onUndeploy ?? new UnityEvent());
				deployable.onUndeploy.AddListener(new UnityAction(component3.TrueKill));
				GameObject bodyObject = component3.GetBodyObject();
				if (bodyObject)
				{
					foreach (EntityStateMachine entityStateMachine in bodyObject.GetComponents<EntityStateMachine>())
					{
						if (entityStateMachine.customName == "Body")
						{
							entityStateMachine.SetState(new GummyCloneSpawnState());
							return;
						}
					}
				}
			}));
			DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
			UnityEngine.Object.Destroy(masterCopySpawnCard);
		}

		// Token: 0x040040E9 RID: 16617
		[SerializeField]
		private int damageBoostCount = 10;

		// Token: 0x040040EA RID: 16618
		[SerializeField]
		private int hpBoostCount = 10;

		// Token: 0x040040EB RID: 16619
		[SerializeField]
		private float maxLifetime = 10f;

		// Token: 0x040040EC RID: 16620
		private float stopwatch;

		// Token: 0x040040ED RID: 16621
		private bool isAlive = true;
	}
}
