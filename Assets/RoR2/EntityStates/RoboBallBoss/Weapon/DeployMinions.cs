using System;
using System.Collections.ObjectModel;
using RoR2;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

namespace EntityStates.RoboBallBoss.Weapon
{
	// Token: 0x020001E3 RID: 483
	public class DeployMinions : BaseState
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x00024614 File Offset: 0x00022814
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			this.modelTransform = base.GetModelTransform();
			this.childLocator = this.modelTransform.GetComponent<ChildLocator>();
			this.duration = DeployMinions.baseDuration;
			base.PlayCrossfade("Gesture, Additive", "DeployMinions", "DeployMinions.playbackRate", this.duration, 0.1f);
			Util.PlaySound(DeployMinions.attackSoundString, base.gameObject);
			this.summonInterval = DeployMinions.summonDuration / (float)DeployMinions.maxSummonCount;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x000246A0 File Offset: 0x000228A0
		private Transform FindTargetClosest(Vector3 point, TeamIndex enemyTeam)
		{
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(enemyTeam);
			float num = 99999f;
			Transform result = null;
			for (int i = 0; i < teamMembers.Count; i++)
			{
				float num2 = Vector3.SqrMagnitude(teamMembers[i].transform.position - point);
				if (num2 < num)
				{
					num = num2;
					result = teamMembers[i].transform;
				}
			}
			return result;
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00024704 File Offset: 0x00022904
		private void SummonMinion()
		{
			if (!base.characterBody || !base.characterBody.master)
			{
				return;
			}
			if (base.characterBody.master.GetDeployableCount(DeployableSlot.RoboBallMini) < base.characterBody.master.GetDeployableSameSlotLimit(DeployableSlot.RoboBallMini))
			{
				Util.PlaySound(DeployMinions.summonSoundString, base.gameObject);
				if (!NetworkServer.active)
				{
					return;
				}
				Vector3 position = base.FindModelChild(DeployMinions.summonMuzzleString).position;
				DirectorSpawnRequest directorSpawnRequest = new DirectorSpawnRequest(LegacyResourcesAPI.Load<SpawnCard>(string.Format("SpawnCards/CharacterSpawnCards/{0}", DeployMinions.spawnCard)), new DirectorPlacementRule
				{
					placementMode = DirectorPlacementRule.PlacementMode.Direct,
					minDistance = 0f,
					maxDistance = 0f,
					position = position
				}, RoR2Application.rng);
				directorSpawnRequest.summonerBodyObject = base.gameObject;
				GameObject gameObject = DirectorCore.instance.TrySpawnObject(directorSpawnRequest);
				if (gameObject)
				{
					CharacterMaster component = gameObject.GetComponent<CharacterMaster>();
					gameObject.GetComponent<Inventory>().SetEquipmentIndex(base.characterBody.inventory.currentEquipmentIndex);
					Deployable deployable = gameObject.AddComponent<Deployable>();
					deployable.onUndeploy = new UnityEvent();
					deployable.onUndeploy.AddListener(new UnityAction(component.TrueKill));
					base.characterBody.master.AddDeployable(deployable, DeployableSlot.RoboBallMini);
				}
			}
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0002484C File Offset: 0x00022A4C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.animator)
			{
				bool flag = this.animator.GetFloat("DeployMinions.active") > 0.9f;
				if (this.isSummoning)
				{
					this.summonTimer += Time.fixedDeltaTime;
					if (NetworkServer.active && this.summonTimer > 0f && this.summonCount < DeployMinions.maxSummonCount)
					{
						this.summonCount++;
						this.summonTimer -= this.summonInterval;
						this.SummonMinion();
					}
				}
				this.isSummoning = flag;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000A20 RID: 2592
		public static float baseDuration = 3.5f;

		// Token: 0x04000A21 RID: 2593
		public static string attackSoundString;

		// Token: 0x04000A22 RID: 2594
		public static string summonSoundString;

		// Token: 0x04000A23 RID: 2595
		public static int maxSummonCount = 5;

		// Token: 0x04000A24 RID: 2596
		public static float summonDuration = 3.26f;

		// Token: 0x04000A25 RID: 2597
		public static string summonMuzzleString;

		// Token: 0x04000A26 RID: 2598
		public static string spawnCard;

		// Token: 0x04000A27 RID: 2599
		private Animator animator;

		// Token: 0x04000A28 RID: 2600
		private Transform modelTransform;

		// Token: 0x04000A29 RID: 2601
		private ChildLocator childLocator;

		// Token: 0x04000A2A RID: 2602
		private float duration;

		// Token: 0x04000A2B RID: 2603
		private float summonInterval;

		// Token: 0x04000A2C RID: 2604
		private float summonTimer;

		// Token: 0x04000A2D RID: 2605
		private int summonCount;

		// Token: 0x04000A2E RID: 2606
		private bool isSummoning;
	}
}
