using System;
using System.Linq;
using RoR2;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.GrandParentBoss
{
	// Token: 0x02000355 RID: 853
	public class PortalJump : BaseState
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x000422B4 File Offset: 0x000404B4
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				this.animator = this.modelTransform.GetComponent<Animator>();
				this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
				this.hurtboxGroup = this.modelTransform.GetComponent<HurtBoxGroup>();
				base.PlayAnimation("Body", "Retreat", "retreat.playbackRate", PortalJump.retreatDuration);
				EffectData effectData = new EffectData
				{
					origin = base.characterBody.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild("Portal").position
				};
				EffectManager.SpawnEffect(PortalJump.jumpInEffectPrefab, effectData, true);
			}
			this.FXController = base.characterBody.GetComponent<GrandparentEnergyFXController>();
			if (this.FXController)
			{
				this.FXController.portalObject = base.characterBody.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild("Portal").GetComponentInChildren<EffectComponent>().gameObject;
			}
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x000423C0 File Offset: 0x000405C0
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= PortalJump.retreatDuration && !this.retreatDone)
			{
				this.retreatDone = true;
				if (this.FXController)
				{
					this.ScaleObject(this.FXController.portalObject, false);
				}
			}
			if (this.stopwatch >= PortalJump.retreatDuration + PortalJump.portalScaleDuration && !this.teleported)
			{
				this.teleported = true;
				this.canMoveDuringTeleport = true;
				if (this.FXController)
				{
					this.FXController.portalObject.GetComponent<ObjectScaleCurve>().enabled = false;
				}
				this.DoTeleport();
			}
			if (base.characterMotor && base.characterDirection)
			{
				base.characterMotor.velocity = Vector3.zero;
			}
			if (this.canMoveDuringTeleport)
			{
				this.SetPosition(Vector3.Lerp(this.startPressence, this.destinationPressence, this.stopwatch / PortalJump.duration));
			}
			if (this.stopwatch >= PortalJump.retreatDuration + PortalJump.portalScaleDuration + PortalJump.duration && this.canMoveDuringTeleport)
			{
				this.canMoveDuringTeleport = false;
				if (this.FXController)
				{
					this.FXController.portalObject.transform.position = base.characterBody.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild("Portal").position;
					this.ScaleObject(this.FXController.portalObject, true);
				}
			}
			if (this.stopwatch >= PortalJump.retreatDuration + PortalJump.portalScaleDuration * 2f + PortalJump.duration && !this.hasEmerged)
			{
				this.hasEmerged = true;
				if (this.FXController)
				{
					this.FXController.portalObject.GetComponent<ObjectScaleCurve>().enabled = false;
				}
				this.modelTransform = base.GetModelTransform();
				if (this.modelTransform)
				{
					base.PlayAnimation("Body", "Emerge", "emerge.playbackRate", PortalJump.duration);
					EffectData effectData = new EffectData
					{
						origin = base.characterBody.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild("Portal").position
					};
					EffectManager.SpawnEffect(PortalJump.jumpOutEffectPrefab, effectData, true);
					if (this.characterModel)
					{
						this.characterModel.invisibilityCount--;
					}
					if (this.hurtboxGroup)
					{
						HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
						int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
						hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
					}
					if (base.characterMotor)
					{
						base.characterMotor.enabled = true;
					}
				}
			}
			if (this.stopwatch >= PortalJump.retreatDuration + PortalJump.portalScaleDuration * 2f + PortalJump.duration + PortalJump.emergeDuration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x000426A4 File Offset: 0x000408A4
		private void DoTeleport()
		{
			Ray aimRay = base.GetAimRay();
			this.enemyFinder = new BullseyeSearch();
			this.enemyFinder.maxDistanceFilter = PortalJump.skillDistance;
			this.enemyFinder.searchOrigin = aimRay.origin;
			this.enemyFinder.searchDirection = aimRay.direction;
			this.enemyFinder.filterByLoS = false;
			this.enemyFinder.sortMode = BullseyeSearch.SortMode.Distance;
			this.enemyFinder.teamMaskFilter = TeamMask.allButNeutral;
			if (base.teamComponent)
			{
				this.enemyFinder.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
			}
			this.enemyFinder.RefreshCandidates();
			this.foundBullseye = this.enemyFinder.GetResults().LastOrDefault<HurtBox>();
			this.modelTransform = base.GetModelTransform();
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount++;
			}
			if (this.hurtboxGroup)
			{
				HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
			if (base.characterMotor)
			{
				base.characterMotor.enabled = false;
			}
			Vector3 b = base.inputBank.moveVector * PortalJump.skillDistance;
			this.destinationPressence = base.transform.position;
			this.startPressence = base.transform.position;
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			Vector3 position = this.startPressence + b;
			if (this.foundBullseye)
			{
				position = this.foundBullseye.transform.position;
			}
			NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNode(position, base.characterBody.hullClassification, float.PositiveInfinity);
			groundNodes.GetNodePosition(nodeIndex, out this.destinationPressence);
			this.destinationPressence += base.transform.position - base.characterBody.footPosition;
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0002837E File Offset: 0x0002657E
		private void SetPosition(Vector3 newPosition)
		{
			if (base.characterMotor)
			{
				base.characterMotor.Motor.SetPositionAndRotation(newPosition, Quaternion.identity, true);
			}
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x00042890 File Offset: 0x00040A90
		private void ScaleObject(GameObject objectToScaleDown, bool scaleUp)
		{
			float valueEnd = scaleUp ? 1f : 0f;
			float valueStart = scaleUp ? 0f : 1f;
			ObjectScaleCurve component = objectToScaleDown.GetComponent<ObjectScaleCurve>();
			component.timeMax = PortalJump.portalScaleDuration;
			component.curveX = AnimationCurve.Linear(0f, valueStart, 1f, valueEnd);
			component.curveY = AnimationCurve.Linear(0f, valueStart, 1f, valueEnd);
			component.curveZ = AnimationCurve.Linear(0f, valueStart, 1f, valueEnd);
			component.overallCurve = AnimationCurve.EaseInOut(0f, valueStart, 1f, valueEnd);
			component.enabled = true;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x0400133B RID: 4923
		private BullseyeSearch enemyFinder;

		// Token: 0x0400133C RID: 4924
		public static float duration = 3f;

		// Token: 0x0400133D RID: 4925
		public static float retreatDuration = 2.433f;

		// Token: 0x0400133E RID: 4926
		public static float emergeDuration = 2.933f;

		// Token: 0x0400133F RID: 4927
		public static float portalScaleDuration = 2f;

		// Token: 0x04001340 RID: 4928
		public static float effectsDuration = 2f;

		// Token: 0x04001341 RID: 4929
		private bool retreatDone;

		// Token: 0x04001342 RID: 4930
		private bool teleported;

		// Token: 0x04001343 RID: 4931
		private bool canMoveDuringTeleport;

		// Token: 0x04001344 RID: 4932
		private bool hasEmerged;

		// Token: 0x04001345 RID: 4933
		private HurtBox foundBullseye;

		// Token: 0x04001346 RID: 4934
		public static float telezoneRadius;

		// Token: 0x04001347 RID: 4935
		public static float skillDistance = 2000f;

		// Token: 0x04001348 RID: 4936
		private float stopwatch;

		// Token: 0x04001349 RID: 4937
		private Vector3 destinationPressence = Vector3.zero;

		// Token: 0x0400134A RID: 4938
		private Vector3 startPressence = Vector3.zero;

		// Token: 0x0400134B RID: 4939
		private Transform modelTransform;

		// Token: 0x0400134C RID: 4940
		private Animator animator;

		// Token: 0x0400134D RID: 4941
		private CharacterModel characterModel;

		// Token: 0x0400134E RID: 4942
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x0400134F RID: 4943
		public static GameObject jumpInEffectPrefab;

		// Token: 0x04001350 RID: 4944
		public static GameObject jumpOutEffectPrefab;

		// Token: 0x04001351 RID: 4945
		public static Vector3 teleportOffset;

		// Token: 0x04001352 RID: 4946
		private GrandparentEnergyFXController FXController;
	}
}
