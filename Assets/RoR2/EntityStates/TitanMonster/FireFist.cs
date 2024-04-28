using System;
using System.Linq;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.TitanMonster
{
	// Token: 0x02000360 RID: 864
	public class FireFist : BaseState
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x00044228 File Offset: 0x00042428
		public override void OnEnter()
		{
			base.OnEnter();
			this.stopwatch = 0f;
			if (base.modelLocator)
			{
				ChildLocator component = base.modelLocator.modelTransform.GetComponent<ChildLocator>();
				this.aimAnimator = base.modelLocator.modelTransform.GetComponent<AimAnimator>();
				if (this.aimAnimator)
				{
					this.aimAnimator.enabled = true;
				}
				if (component)
				{
					this.fistTransform = component.FindChild("RightFist");
					if (this.fistTransform)
					{
						this.chargeEffect = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, this.fistTransform);
					}
				}
			}
			this.subState = FireFist.SubState.Prep;
			base.PlayCrossfade("Body", "PrepFist", "PrepFist.playbackRate", FireFist.entryDuration, 0.1f);
			Util.PlayAttackSpeedSound(FireFist.chargeFistAttackSoundString, base.gameObject, this.attackSpeedStat);
			if (NetworkServer.active)
			{
				BullseyeSearch bullseyeSearch = new BullseyeSearch();
				bullseyeSearch.teamMaskFilter = TeamMask.allButNeutral;
				if (base.teamComponent)
				{
					bullseyeSearch.teamMaskFilter.RemoveTeam(base.teamComponent.teamIndex);
				}
				bullseyeSearch.maxDistanceFilter = FireFist.maxDistance;
				bullseyeSearch.maxAngleFilter = 90f;
				Ray aimRay = base.GetAimRay();
				bullseyeSearch.searchOrigin = aimRay.origin;
				bullseyeSearch.searchDirection = aimRay.direction;
				bullseyeSearch.filterByLoS = false;
				bullseyeSearch.sortMode = BullseyeSearch.SortMode.Angle;
				bullseyeSearch.RefreshCandidates();
				HurtBox hurtBox = bullseyeSearch.GetResults().FirstOrDefault<HurtBox>();
				if (hurtBox)
				{
					this.predictor = new FireFist.Predictor(base.transform);
					this.predictor.SetTargetTransform(hurtBox.transform);
				}
			}
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x000443CC File Offset: 0x000425CC
		public override void OnExit()
		{
			base.OnExit();
			if (this.chargeEffect)
			{
				EntityState.Destroy(this.chargeEffect);
			}
			EntityState.Destroy(this.predictorDebug);
			this.predictorDebug = null;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00044400 File Offset: 0x00042600
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			switch (this.subState)
			{
			case FireFist.SubState.Prep:
				if (this.predictor != null)
				{
					this.predictor.Update();
				}
				if (this.stopwatch <= FireFist.trackingDuration)
				{
					if (this.predictor != null)
					{
						this.predictionOk = this.predictor.GetPredictedTargetPosition(FireFist.entryDuration - FireFist.trackingDuration, out this.predictedTargetPosition);
						if (this.predictionOk && this.predictorDebug)
						{
							this.predictorDebug.transform.position = this.predictedTargetPosition;
						}
					}
				}
				else if (!this.hasShownPrediction)
				{
					this.hasShownPrediction = true;
					this.PlacePredictedAttack();
				}
				if (this.stopwatch >= FireFist.entryDuration)
				{
					this.predictor = null;
					this.subState = FireFist.SubState.FireFist;
					this.stopwatch = 0f;
					this.PlayAnimation("Body", "FireFist");
					if (this.chargeEffect)
					{
						EntityState.Destroy(this.chargeEffect);
					}
					UnityEngine.Object.Instantiate<GameObject>(this.fireEffectPrefab, this.fistTransform.position, Quaternion.identity, this.fistTransform);
					return;
				}
				break;
			case FireFist.SubState.FireFist:
				if (this.stopwatch >= FireFist.fireDuration)
				{
					this.subState = FireFist.SubState.Exit;
					this.stopwatch = 0f;
					base.PlayCrossfade("Body", "ExitFist", "ExitFist.playbackRate", FireFist.exitDuration, 0.3f);
					return;
				}
				break;
			case FireFist.SubState.Exit:
				if (this.stopwatch >= FireFist.exitDuration && base.isAuthority)
				{
					this.outer.SetNextStateToMain();
					return;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0004459F File Offset: 0x0004279F
		protected virtual void PlacePredictedAttack()
		{
			this.PlaceSingleDelayBlast(this.predictedTargetPosition, 0f);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000445B4 File Offset: 0x000427B4
		protected void PlaceSingleDelayBlast(Vector3 position, float delay)
		{
			if (!base.isAuthority)
			{
				return;
			}
			FireProjectileInfo fireProjectileInfo = default(FireProjectileInfo);
			fireProjectileInfo.projectilePrefab = this.fistProjectilePrefab;
			fireProjectileInfo.position = position;
			fireProjectileInfo.rotation = Quaternion.identity;
			fireProjectileInfo.owner = base.gameObject;
			fireProjectileInfo.damage = this.damageStat * FireFist.fistDamageCoefficient;
			fireProjectileInfo.force = FireFist.fistForce;
			fireProjectileInfo.crit = base.characterBody.RollCrit();
			fireProjectileInfo.fuseOverride = FireFist.entryDuration - FireFist.trackingDuration + delay;
			ProjectileManager.instance.FireProjectile(fireProjectileInfo);
		}

		// Token: 0x040013B1 RID: 5041
		public static float entryDuration = 1f;

		// Token: 0x040013B2 RID: 5042
		public static float fireDuration = 2f;

		// Token: 0x040013B3 RID: 5043
		public static float exitDuration = 1f;

		// Token: 0x040013B4 RID: 5044
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x040013B5 RID: 5045
		[SerializeField]
		public GameObject fistEffectPrefab;

		// Token: 0x040013B6 RID: 5046
		[SerializeField]
		public GameObject fireEffectPrefab;

		// Token: 0x040013B7 RID: 5047
		[SerializeField]
		public GameObject fistProjectilePrefab;

		// Token: 0x040013B8 RID: 5048
		public static float maxDistance = 40f;

		// Token: 0x040013B9 RID: 5049
		public static float trackingDuration = 0.5f;

		// Token: 0x040013BA RID: 5050
		public static float fistDamageCoefficient = 2f;

		// Token: 0x040013BB RID: 5051
		public static float fistForce = 2000f;

		// Token: 0x040013BC RID: 5052
		public static string chargeFistAttackSoundString;

		// Token: 0x040013BD RID: 5053
		private bool hasShownPrediction;

		// Token: 0x040013BE RID: 5054
		private bool predictionOk;

		// Token: 0x040013BF RID: 5055
		protected Vector3 predictedTargetPosition;

		// Token: 0x040013C0 RID: 5056
		private AimAnimator aimAnimator;

		// Token: 0x040013C1 RID: 5057
		private GameObject chargeEffect;

		// Token: 0x040013C2 RID: 5058
		private Transform fistTransform;

		// Token: 0x040013C3 RID: 5059
		private float stopwatch;

		// Token: 0x040013C4 RID: 5060
		private FireFist.SubState subState;

		// Token: 0x040013C5 RID: 5061
		private FireFist.Predictor predictor;

		// Token: 0x040013C6 RID: 5062
		private GameObject predictorDebug;

		// Token: 0x02000361 RID: 865
		private enum SubState
		{
			// Token: 0x040013C8 RID: 5064
			Prep,
			// Token: 0x040013C9 RID: 5065
			FireFist,
			// Token: 0x040013CA RID: 5066
			Exit
		}

		// Token: 0x02000362 RID: 866
		private class Predictor
		{
			// Token: 0x06000F93 RID: 3987 RVA: 0x000446A3 File Offset: 0x000428A3
			public Predictor(Transform bodyTransform)
			{
				this.bodyTransform = bodyTransform;
			}

			// Token: 0x170000EC RID: 236
			// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000446B2 File Offset: 0x000428B2
			public bool hasTargetTransform
			{
				get
				{
					return this.targetTransform;
				}
			}

			// Token: 0x170000ED RID: 237
			// (get) Token: 0x06000F95 RID: 3989 RVA: 0x000446BF File Offset: 0x000428BF
			public bool isPredictionReady
			{
				get
				{
					return this.collectedPositions > 2;
				}
			}

			// Token: 0x06000F96 RID: 3990 RVA: 0x000446CA File Offset: 0x000428CA
			private void PushTargetPosition(Vector3 newTargetPosition)
			{
				this.targetPosition2 = this.targetPosition1;
				this.targetPosition1 = this.targetPosition0;
				this.targetPosition0 = newTargetPosition;
				this.collectedPositions++;
			}

			// Token: 0x06000F97 RID: 3991 RVA: 0x000446FC File Offset: 0x000428FC
			public void SetTargetTransform(Transform newTargetTransform)
			{
				this.targetTransform = newTargetTransform;
				this.targetPosition2 = (this.targetPosition1 = (this.targetPosition0 = newTargetTransform.position));
				this.collectedPositions = 1;
			}

			// Token: 0x06000F98 RID: 3992 RVA: 0x00044735 File Offset: 0x00042935
			public void Update()
			{
				if (this.targetTransform)
				{
					this.PushTargetPosition(this.targetTransform.position);
				}
			}

			// Token: 0x06000F99 RID: 3993 RVA: 0x00044758 File Offset: 0x00042958
			public bool GetPredictedTargetPosition(float time, out Vector3 predictedPosition)
			{
				Vector3 lhs = this.targetPosition1 - this.targetPosition2;
				Vector3 vector = this.targetPosition0 - this.targetPosition1;
				lhs.y = 0f;
				vector.y = 0f;
				FireFist.Predictor.ExtrapolationType extrapolationType;
				if (lhs == Vector3.zero || vector == Vector3.zero)
				{
					extrapolationType = FireFist.Predictor.ExtrapolationType.None;
				}
				else
				{
					Vector3 normalized = lhs.normalized;
					Vector3 normalized2 = vector.normalized;
					if (Vector3.Dot(normalized, normalized2) > 0.98f)
					{
						extrapolationType = FireFist.Predictor.ExtrapolationType.Linear;
					}
					else
					{
						extrapolationType = FireFist.Predictor.ExtrapolationType.Polar;
					}
				}
				float num = 1f / Time.fixedDeltaTime;
				predictedPosition = this.targetPosition0;
				switch (extrapolationType)
				{
				case FireFist.Predictor.ExtrapolationType.Linear:
					predictedPosition = this.targetPosition0 + vector * (time * num);
					break;
				case FireFist.Predictor.ExtrapolationType.Polar:
				{
					Vector3 position = this.bodyTransform.position;
					Vector3 v = Util.Vector3XZToVector2XY(this.targetPosition2 - position);
					Vector3 v2 = Util.Vector3XZToVector2XY(this.targetPosition1 - position);
					Vector3 v3 = Util.Vector3XZToVector2XY(this.targetPosition0 - position);
					float magnitude = v.magnitude;
					float magnitude2 = v2.magnitude;
					float magnitude3 = v3.magnitude;
					float num2 = Vector2.SignedAngle(v, v2) * num;
					float num3 = Vector2.SignedAngle(v2, v3) * num;
					float num4 = (magnitude2 - magnitude) * num;
					float num5 = (magnitude3 - magnitude2) * num;
					float num6 = (num2 + num3) * 0.5f;
					float num7 = (num4 + num5) * 0.5f;
					float num8 = magnitude3 + num7 * time;
					if (num8 < 0f)
					{
						num8 = 0f;
					}
					Vector2 vector2 = Util.RotateVector2(v3, num6 * time);
					vector2 *= num8 * magnitude3;
					predictedPosition = position;
					predictedPosition.x += vector2.x;
					predictedPosition.z += vector2.y;
					break;
				}
				}
				RaycastHit raycastHit;
				if (Physics.Raycast(new Ray(predictedPosition + Vector3.up * 1f, Vector3.down), out raycastHit, 200f, LayerIndex.world.mask, QueryTriggerInteraction.Ignore))
				{
					predictedPosition = raycastHit.point;
					return true;
				}
				return false;
			}

			// Token: 0x040013CB RID: 5067
			private Transform bodyTransform;

			// Token: 0x040013CC RID: 5068
			private Transform targetTransform;

			// Token: 0x040013CD RID: 5069
			private Vector3 targetPosition0;

			// Token: 0x040013CE RID: 5070
			private Vector3 targetPosition1;

			// Token: 0x040013CF RID: 5071
			private Vector3 targetPosition2;

			// Token: 0x040013D0 RID: 5072
			private int collectedPositions;

			// Token: 0x02000363 RID: 867
			private enum ExtrapolationType
			{
				// Token: 0x040013D2 RID: 5074
				None,
				// Token: 0x040013D3 RID: 5075
				Linear,
				// Token: 0x040013D4 RID: 5076
				Polar
			}
		}
	}
}
