using System;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.GrandParentBoss
{
	// Token: 0x02000353 RID: 851
	public class Offspring : BaseState
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x00041CC4 File Offset: 0x0003FEC4
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			this.duration = Offspring.baseDuration;
			Util.PlaySound(Offspring.attackSoundString, base.gameObject);
			this.summonInterval = Offspring.summonDuration / (float)Offspring.maxSummonCount;
			this.isSummoning = true;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x00041D38 File Offset: 0x0003FF38
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.isSummoning)
			{
				this.summonTimer += Time.fixedDeltaTime;
				if (NetworkServer.active && this.summonTimer > 0f && this.summonCount < Offspring.maxSummonCount)
				{
					this.summonCount++;
					this.summonTimer -= this.summonInterval;
					this.SpawnPods();
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x00041DD0 File Offset: 0x0003FFD0
		private void SpawnPods()
		{
			Vector3 point = Vector3.zero;
			Ray aimRay = base.GetAimRay();
			aimRay.origin += UnityEngine.Random.insideUnitSphere * Offspring.randomRadius;
			RaycastHit raycastHit;
			if (Physics.Raycast(aimRay, out raycastHit, (float)LayerIndex.world.mask))
			{
				point = raycastHit.point;
			}
			point = base.transform.position;
			Transform transform = this.FindTargetFarthest(point, base.GetTeam());
			if (transform)
			{
				DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>("SpawnCards/CharacterSpawnCards/cscParentPod"), new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.Approximate,
					minDistance = 3f,
					maxDistance = 20f,
					spawnOnTarget = transform
				}, RoR2Application.rng);
				directorSpawnRequest.summonerBodyObject = base.gameObject;
				DirectorSpawnRequest directorSpawnRequest2 = directorSpawnRequest;
				directorSpawnRequest2.onSpawnedServer = (Action<SpawnCard.SpawnResult>)Delegate.Combine(directorSpawnRequest2.onSpawnedServer, new Action<SpawnCard.SpawnResult>(delegate(SpawnCard.SpawnResult spawnResult)
				{
					Inventory inventory = spawnResult.spawnedInstance.GetComponent<CharacterMaster>().inventory;
					Inventory inventory2 = base.characterBody.inventory;
					inventory.CopyEquipmentFrom(inventory2);
				}));
				DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
				base.PlayAnimation("Body", "SpawnPodWarn", "spawnPodWarn.playbackRate", this.duration);
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00041EF0 File Offset: 0x000400F0
		private Transform FindTargetFarthest(Vector3 point, TeamIndex myTeam)
		{
			float num = 0f;
			Transform result = null;
			TeamMask enemyTeams = TeamMask.GetEnemyTeams(myTeam);
			for (TeamIndex teamIndex = TeamIndex.Neutral; teamIndex < TeamIndex.Count; teamIndex += 1)
			{
				if (!enemyTeams.HasTeam(teamIndex))
				{
					ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(teamIndex);
					for (int i = 0; i < teamMembers.Count; i++)
					{
						float num2 = Vector3.Magnitude(teamMembers[i].transform.position - point);
						if (num2 > num && num2 < Offspring.maxRange)
						{
							num = num2;
							result = teamMembers[i].transform;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04001319 RID: 4889
		[SerializeField]
		public GameObject SpawnerPodsPrefab;

		// Token: 0x0400131A RID: 4890
		public static float randomRadius = 8f;

		// Token: 0x0400131B RID: 4891
		public static float maxRange = 9999f;

		// Token: 0x0400131C RID: 4892
		private Animator animator;

		// Token: 0x0400131D RID: 4893
		private ChildLocator childLocator;

		// Token: 0x0400131E RID: 4894
		private Transform modelTransform;

		// Token: 0x0400131F RID: 4895
		private float duration;

		// Token: 0x04001320 RID: 4896
		public static float baseDuration = 3.5f;

		// Token: 0x04001321 RID: 4897
		public static string attackSoundString;

		// Token: 0x04001322 RID: 4898
		private float summonInterval;

		// Token: 0x04001323 RID: 4899
		private static float summonDuration = 3.26f;

		// Token: 0x04001324 RID: 4900
		public static int maxSummonCount = 5;

		// Token: 0x04001325 RID: 4901
		private float summonTimer;

		// Token: 0x04001326 RID: 4902
		private bool isSummoning;

		// Token: 0x04001327 RID: 4903
		private int summonCount;

		// Token: 0x04001328 RID: 4904
		public static GameObject spawnEffect;
	}
}
