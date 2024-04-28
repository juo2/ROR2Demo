using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.BeetleQueenMonster
{
	// Token: 0x02000469 RID: 1129
	public class SummonEggs : BaseState
	{
		// Token: 0x0600142D RID: 5165 RVA: 0x00059E90 File Offset: 0x00058090
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			this.duration = SummonEggs.baseDuration;
			base.PlayCrossfade("Gesture", "SummonEggs", 0.5f);
			Util.PlaySound(SummonEggs.attackSoundString, base.gameObject);
			if (NetworkServer.active)
			{
				this.enemySearch = new BullseyeSearch();
				this.enemySearch.filterByDistinctEntity = false;
				this.enemySearch.filterByLoS = false;
				this.enemySearch.maxDistanceFilter = float.PositiveInfinity;
				this.enemySearch.minDistanceFilter = 0f;
				this.enemySearch.minAngleFilter = 0f;
				this.enemySearch.maxAngleFilter = 180f;
				this.enemySearch.teamMaskFilter = TeamMask.GetEnemyTeams(base.GetTeam());
				this.enemySearch.sortMode = BullseyeSearch.SortMode.Distance;
				this.enemySearch.viewer = base.characterBody;
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x00059FA0 File Offset: 0x000581A0
		private void SummonEgg()
		{
			Vector3 searchOrigin = base.GetAimRay().origin;
			RaycastHit raycastHit;
			if (base.inputBank && base.inputBank.GetAimRaycast(float.PositiveInfinity, out raycastHit))
			{
				searchOrigin = raycastHit.point;
			}
			if (this.enemySearch != null)
			{
				this.enemySearch.searchOrigin = searchOrigin;
				this.enemySearch.RefreshCandidates();
				HurtBox hurtBox = this.enemySearch.GetResults().FirstOrDefault<HurtBox>();
				Transform transform = (hurtBox && hurtBox.healthComponent) ? hurtBox.healthComponent.body.coreTransform : base.characterBody.coreTransform;
				if (transform)
				{
					DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(SummonEggs.spawnCard, new DirectorPlacementRule
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
						if (spawnResult.success && spawnResult.spawnedInstance && base.characterBody)
						{
							Inventory component = spawnResult.spawnedInstance.GetComponent<Inventory>();
							if (component)
							{
								component.CopyEquipmentFrom(base.characterBody.inventory);
							}
						}
					}));
					DirectorCore instance = DirectorCore.instance;
					if (instance == null)
					{
						return;
					}
					instance.TrySpawnObject(directorSpawnRequest);
				}
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x0005A0D4 File Offset: 0x000582D4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			bool flag = this.animator.GetFloat("SummonEggs.active") > 0.9f;
			if (flag && !this.isSummoning)
			{
				string muzzleName = "Mouth";
				EffectManager.SimpleMuzzleFlash(SummonEggs.spitPrefab, base.gameObject, muzzleName, false);
			}
			if (this.isSummoning)
			{
				this.summonTimer += Time.fixedDeltaTime;
				if (NetworkServer.active && this.summonTimer > 0f && this.summonCount < SummonEggs.maxSummonCount)
				{
					this.summonCount++;
					this.summonTimer -= SummonEggs.summonInterval;
					this.SummonEgg();
				}
			}
			this.isSummoning = flag;
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x040019E2 RID: 6626
		public static float baseDuration = 3.5f;

		// Token: 0x040019E3 RID: 6627
		public static string attackSoundString;

		// Token: 0x040019E4 RID: 6628
		public static float randomRadius = 8f;

		// Token: 0x040019E5 RID: 6629
		public static GameObject spitPrefab;

		// Token: 0x040019E6 RID: 6630
		public static int maxSummonCount = 5;

		// Token: 0x040019E7 RID: 6631
		public static float summonInterval = 1f;

		// Token: 0x040019E8 RID: 6632
		private static float summonDuration = 3.26f;

		// Token: 0x040019E9 RID: 6633
		public static SpawnCard spawnCard;

		// Token: 0x040019EA RID: 6634
		private Animator animator;

		// Token: 0x040019EB RID: 6635
		private Transform modelTransform;

		// Token: 0x040019EC RID: 6636
		private ChildLocator childLocator;

		// Token: 0x040019ED RID: 6637
		private float duration;

		// Token: 0x040019EE RID: 6638
		private float summonTimer;

		// Token: 0x040019EF RID: 6639
		private int summonCount;

		// Token: 0x040019F0 RID: 6640
		private bool isSummoning;

		// Token: 0x040019F1 RID: 6641
		private BullseyeSearch enemySearch;
	}
}
