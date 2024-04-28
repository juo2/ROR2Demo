using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RoR2;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.GrandParentBoss
{
	// Token: 0x02000357 RID: 855
	public class SpiritPull : BaseState
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x00042C14 File Offset: 0x00040E14
		public override void OnEnter()
		{
			base.OnEnter();
			this.Startpositions = new List<Vector3>();
			this.Endpositions = new List<Vector3>();
			Ray aimRay = base.GetAimRay();
			this.modelTransform = base.GetModelTransform();
			this.enemyFinder = new BullseyeSearch();
			this.enemyFinder.maxDistanceFilter = 2000f;
			this.enemyFinder.maxAngleFilter = SpiritPull.lockOnAngle;
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
			Vector3 zero = Vector3.zero;
			if (this.foundBullseye)
			{
				this.teleporterIndicatorInstance = this.foundBullseye.transform.position;
				RaycastHit raycastHit;
				if (Physics.Raycast(this.teleporterIndicatorInstance, Vector3.down, out raycastHit, 500f, LayerIndex.world.mask))
				{
					this.teleporterIndicatorInstance = raycastHit.point;
					Vector3 vector = this.teleporterIndicatorInstance;
				}
				EffectData effectData = new EffectData
				{
					origin = this.teleporterIndicatorInstance,
					rotation = SpiritPull.teleportZoneEffect.transform.rotation,
					scale = SpiritPull.zoneRadius * 2f
				};
				EffectManager.SpawnEffect(SpiritPull.teleportZoneEffect, effectData, true);
				if (this.spiritPullLocationObject == null)
				{
					this.spiritPullLocationObject = new GameObject();
				}
				this.spiritPullLocationObject.transform.position = effectData.origin;
				Util.PlaySound(SpiritPull.indicatorOnPlayerSoundLoop, this.spiritPullLocationObject);
			}
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00042E00 File Offset: 0x00041000
		private Transform FindTargetFarthest(Vector3 point, TeamIndex enemyTeam)
		{
			ReadOnlyCollection<TeamComponent> teamMembers = TeamComponent.GetTeamMembers(enemyTeam);
			float num = 0f;
			Transform result = null;
			for (int i = 0; i < teamMembers.Count; i++)
			{
				float num2 = Vector3.SqrMagnitude(teamMembers[i].transform.position - point);
				if (num2 > num && num2 < SpiritPull.maxRange)
				{
					num = num2;
					result = teamMembers[i].transform;
				}
			}
			return result;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00042E6C File Offset: 0x0004106C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			SpiritPull.pullTimer -= Time.deltaTime;
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= SpiritPull.effectsDuration && !this.gatheredVictims)
			{
				this.effectsDone = true;
				this.gatheredVictims = true;
				this.GetPlayersInsideTeleportZone();
			}
			if (this.stopwatch >= SpiritPull.effectsDuration + SpiritPull.playerTeleportEffectsDuration && !this.teleported)
			{
				this.teleported = true;
				this.TeleportPlayers();
			}
			if (base.characterMotor && base.characterDirection)
			{
				base.characterMotor.velocity = Vector3.zero;
			}
			if (this.effectsDone)
			{
				this.SetPositions();
			}
			if (this.stopwatch >= SpiritPull.effectsDuration + SpiritPull.playerTeleportEffectsDuration + this.duration && base.isAuthority)
			{
				this.effectsDone = false;
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00042F64 File Offset: 0x00041164
		private void GetPlayersInsideTeleportZone()
		{
			this.Startpositions.Clear();
			this.Endpositions.Clear();
			if (this.foundBullseye)
			{
				for (int i = 0; i < SpiritPull.stacks; i++)
				{
					this.search = new BullseyeSearch();
					this.search.filterByLoS = false;
					this.search.maxDistanceFilter = SpiritPull.zoneRadius;
					this.search.searchOrigin = new Vector3(this.teleporterIndicatorInstance.x, this.teleporterIndicatorInstance.y + SpiritPull.zoneRadius * (float)i, this.teleporterIndicatorInstance.z);
					this.search.sortMode = BullseyeSearch.SortMode.Distance;
					this.search.teamMaskFilter = TeamMask.allButNeutral;
					this.search.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
					this.search.RefreshCandidates();
					this.search.queryTriggerInteraction = QueryTriggerInteraction.Collide;
					for (int j = 0; j < this.search.GetResults().ToList<HurtBox>().Count; j++)
					{
						if (!this.results.Contains(this.search.GetResults().ToList<HurtBox>()[j]))
						{
							this.results.Add(this.search.GetResults().ToList<HurtBox>()[j]);
						}
					}
				}
				if (this.results.Count > 0)
				{
					for (int k = 0; k < this.results.Count; k++)
					{
						HurtBox hurtBox = this.results[k];
						Transform transform = hurtBox.healthComponent.body.modelLocator.modelTransform;
						EffectData effectData = new EffectData
						{
							origin = hurtBox.healthComponent.body.footPosition
						};
						EffectManager.SpawnEffect(SpiritPull.playerTeleportEffect, effectData, true);
						if (transform)
						{
							TemporaryOverlay temporaryOverlay = transform.gameObject.AddComponent<TemporaryOverlay>();
							temporaryOverlay.duration = 0.6f;
							temporaryOverlay.animateShaderAlpha = true;
							temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
							temporaryOverlay.destroyComponentOnEnd = true;
							temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matGrandparentTeleportFlash");
							temporaryOverlay.AddToCharacerModel(transform.GetComponent<CharacterModel>());
						}
					}
				}
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x000431A4 File Offset: 0x000413A4
		private void TeleportPlayers()
		{
			if (this.results.Count > 0)
			{
				for (int i = 0; i < this.results.Count; i++)
				{
					HurtBox hurtBox = this.results[i];
					CharacterModel component = hurtBox.healthComponent.body.modelLocator.modelTransform.GetComponent<CharacterModel>();
					HurtBoxGroup component2 = hurtBox.healthComponent.body.modelLocator.modelTransform.GetComponent<HurtBoxGroup>();
					this.Startpositions.Add(hurtBox.transform.position);
					if (component2)
					{
						HurtBoxGroup hurtBoxGroup = component2;
						int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
						hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
					}
					if (component)
					{
						component.invisibilityCount++;
					}
					CharacterMotor component3 = hurtBox.healthComponent.gameObject.GetComponent<CharacterMotor>();
					if (component3)
					{
						component3.enabled = false;
					}
					this.duration = SpiritPull.initialDelay;
					GameObject teleportEffectPrefab = Run.instance.GetTeleportEffectPrefab(base.gameObject);
					if (teleportEffectPrefab)
					{
						UnityEngine.Object.Instantiate<GameObject>(teleportEffectPrefab, base.gameObject.transform.position, Quaternion.identity);
					}
					Util.PlaySound(SpiritPull.teleportedPlayerSound, hurtBox.gameObject);
					base.characterMotor.velocity = Vector3.zero;
					Vector3 vector = base.characterBody.modelLocator.modelTransform.GetComponent<ChildLocator>().FindChild(SpiritPull.teleportZoneString).position;
					NodeGraph groundNodes = SceneInfo.instance.groundNodes;
					NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNode(vector, base.characterBody.hullClassification, float.PositiveInfinity);
					groundNodes.GetNodePosition(nodeIndex, out vector);
					vector += hurtBox.healthComponent.body.transform.position - hurtBox.healthComponent.body.footPosition;
					vector = new Vector3(vector.x, vector.y + 0.1f, vector.z);
					this.Endpositions.Add(vector);
				}
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000433A4 File Offset: 0x000415A4
		private void SetPositions()
		{
			if (this.results.Count > 0)
			{
				for (int i = 0; i < this.results.Count; i++)
				{
					CharacterMotor component = this.results[i].healthComponent.gameObject.GetComponent<CharacterMotor>();
					if (component)
					{
						Vector3 position = Vector3.Lerp(this.Startpositions[i], this.Endpositions[i], this.stopwatch / this.duration);
						component.Motor.SetPositionAndRotation(position, Quaternion.identity, true);
					}
				}
			}
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00043438 File Offset: 0x00041638
		public override void OnExit()
		{
			base.OnExit();
			Util.PlaySound(SpiritPull.indicatorOnPlayerSoundStop, this.spiritPullLocationObject);
			if (this.results.Count > 0)
			{
				for (int i = 0; i < this.results.Count; i++)
				{
					HurtBox hurtBox = this.results[i];
					CharacterModel component = hurtBox.healthComponent.body.modelLocator.modelTransform.GetComponent<CharacterModel>();
					HurtBoxGroup component2 = hurtBox.healthComponent.body.modelLocator.modelTransform.GetComponent<HurtBoxGroup>();
					Transform transform = hurtBox.healthComponent.body.modelLocator.modelTransform;
					if (transform)
					{
						TemporaryOverlay temporaryOverlay = transform.gameObject.AddComponent<TemporaryOverlay>();
						temporaryOverlay.duration = 0.6f;
						temporaryOverlay.animateShaderAlpha = true;
						temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
						temporaryOverlay.destroyComponentOnEnd = true;
						temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matGrandparentTeleportFlash");
						temporaryOverlay.AddToCharacerModel(transform.GetComponent<CharacterModel>());
					}
					EffectData effectData = new EffectData
					{
						origin = hurtBox.healthComponent.body.footPosition
					};
					EffectManager.SpawnEffect(SpiritPull.playerTeleportEffect, effectData, true);
					if (component2)
					{
						HurtBoxGroup hurtBoxGroup = component2;
						int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
						hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
					}
					if (component)
					{
						component.invisibilityCount--;
					}
					CharacterMotor component3 = hurtBox.healthComponent.gameObject.GetComponent<CharacterMotor>();
					if (component3)
					{
						component3.enabled = true;
					}
				}
			}
		}

		// Token: 0x04001362 RID: 4962
		private BullseyeSearch enemyFinder;

		// Token: 0x04001363 RID: 4963
		public static float lockOnAngle;

		// Token: 0x04001364 RID: 4964
		private Vector3 teleporterIndicatorInstance;

		// Token: 0x04001365 RID: 4965
		private Transform modelTransform;

		// Token: 0x04001366 RID: 4966
		public static float pullTimer;

		// Token: 0x04001367 RID: 4967
		public static float zoneRadius;

		// Token: 0x04001368 RID: 4968
		public static float initialDelay;

		// Token: 0x04001369 RID: 4969
		private float duration = 4f;

		// Token: 0x0400136A RID: 4970
		public static float maxRange;

		// Token: 0x0400136B RID: 4971
		public static string teleportZoneString;

		// Token: 0x0400136C RID: 4972
		public static GameObject teleportZoneEffect;

		// Token: 0x0400136D RID: 4973
		public static GameObject playerTeleportEffect;

		// Token: 0x0400136E RID: 4974
		public static float effectsDuration = 2f;

		// Token: 0x0400136F RID: 4975
		public static float playerTeleportEffectsDuration = 1f;

		// Token: 0x04001370 RID: 4976
		private bool effectsDone;

		// Token: 0x04001371 RID: 4977
		private bool gatheredVictims;

		// Token: 0x04001372 RID: 4978
		private bool teleported;

		// Token: 0x04001373 RID: 4979
		private float stopwatch;

		// Token: 0x04001374 RID: 4980
		private List<Vector3> Startpositions;

		// Token: 0x04001375 RID: 4981
		private List<Vector3> Endpositions;

		// Token: 0x04001376 RID: 4982
		public static int stacks;

		// Token: 0x04001377 RID: 4983
		private List<HurtBox> results = new List<HurtBox>();

		// Token: 0x04001378 RID: 4984
		private HurtBox foundBullseye;

		// Token: 0x04001379 RID: 4985
		private BullseyeSearch search;

		// Token: 0x0400137A RID: 4986
		private GameObject spiritPullLocationObject;

		// Token: 0x0400137B RID: 4987
		public static string indicatorOnPlayerSoundLoop;

		// Token: 0x0400137C RID: 4988
		public static string indicatorOnPlayerSoundStop;

		// Token: 0x0400137D RID: 4989
		public static string teleportedPlayerSound;
	}
}
