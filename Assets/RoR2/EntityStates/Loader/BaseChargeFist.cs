using System;
using RoR2;
using RoR2.UI;
using UnityEngine;

namespace EntityStates.Loader
{
	// Token: 0x020002C0 RID: 704
	public class BaseChargeFist : BaseSkillState
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00034972 File Offset: 0x00032B72
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0003497A File Offset: 0x00032B7A
		private protected float chargeDuration { protected get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00034983 File Offset: 0x00032B83
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0003498B File Offset: 0x00032B8B
		private protected float charge { protected get; private set; }

		// Token: 0x06000C7E RID: 3198 RVA: 0x00034994 File Offset: 0x00032B94
		public override void OnEnter()
		{
			base.OnEnter();
			this.chargeDuration = this.baseChargeDuration / this.attackSpeedStat;
			Util.PlaySound(BaseChargeFist.enterSFXString, base.gameObject);
			this.soundID = Util.PlaySound(BaseChargeFist.startChargeLoopSFXString, base.gameObject);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000349E4 File Offset: 0x00032BE4
		public override void OnExit()
		{
			if (this.chargeVfxInstanceTransform)
			{
				EntityState.Destroy(this.chargeVfxInstanceTransform.gameObject);
				this.PlayAnimation("Gesture, Additive", "Empty");
				this.PlayAnimation("Gesture, Override", "Empty");
				CrosshairUtils.OverrideRequest overrideRequest = this.crosshairOverrideRequest;
				if (overrideRequest != null)
				{
					overrideRequest.Dispose();
				}
				this.chargeVfxInstanceTransform = null;
			}
			base.characterMotor.walkSpeedPenaltyCoefficient = 1f;
			Util.PlaySound(BaseChargeFist.endChargeLoopSFXString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00034A70 File Offset: 0x00032C70
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.charge = Mathf.Clamp01(base.fixedAge / this.chargeDuration);
			AkSoundEngine.SetRTPCValueByPlayingID("loaderShift_chargeAmount", this.charge * 100f, this.soundID);
			base.characterBody.SetSpreadBloom(this.charge, true);
			base.characterBody.SetAimTimer(3f);
			if (this.charge >= BaseChargeFist.minChargeForChargedAttack && !this.chargeVfxInstanceTransform && BaseChargeFist.chargeVfxPrefab)
			{
				if (BaseChargeFist.crosshairOverridePrefab && this.crosshairOverrideRequest == null)
				{
					this.crosshairOverrideRequest = CrosshairUtils.RequestOverrideForBody(base.characterBody, BaseChargeFist.crosshairOverridePrefab, CrosshairUtils.OverridePriority.Skill);
				}
				Transform transform = base.FindModelChild(BaseChargeFist.chargeVfxChildLocatorName);
				if (transform)
				{
					this.chargeVfxInstanceTransform = UnityEngine.Object.Instantiate<GameObject>(BaseChargeFist.chargeVfxPrefab, transform).transform;
					ScaleParticleSystemDuration component = this.chargeVfxInstanceTransform.GetComponent<ScaleParticleSystemDuration>();
					if (component)
					{
						component.newDuration = (1f - BaseChargeFist.minChargeForChargedAttack) * this.chargeDuration;
					}
				}
				base.PlayCrossfade("Gesture, Additive", "ChargePunchIntro", "ChargePunchIntro.playbackRate", this.chargeDuration, 0.1f);
				base.PlayCrossfade("Gesture, Override", "ChargePunchIntro", "ChargePunchIntro.playbackRate", this.chargeDuration, 0.1f);
			}
			if (this.chargeVfxInstanceTransform)
			{
				base.characterMotor.walkSpeedPenaltyCoefficient = BaseChargeFist.walkSpeedCoefficient;
			}
			if (base.isAuthority)
			{
				this.AuthorityFixedUpdate();
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00034BF5 File Offset: 0x00032DF5
		public override void Update()
		{
			base.Update();
			Mathf.Clamp01(base.age / this.chargeDuration);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00034C10 File Offset: 0x00032E10
		private void AuthorityFixedUpdate()
		{
			if (!this.ShouldKeepChargingAuthority())
			{
				this.outer.SetNextState(this.GetNextStateAuthority());
			}
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0000B381 File Offset: 0x00009581
		protected virtual bool ShouldKeepChargingAuthority()
		{
			return base.IsKeyDownAuthority();
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00034C2B File Offset: 0x00032E2B
		protected virtual EntityState GetNextStateAuthority()
		{
			return new SwingChargedFist
			{
				charge = this.charge
			};
		}

		// Token: 0x04000F3A RID: 3898
		public static GameObject arcVisualizerPrefab;

		// Token: 0x04000F3B RID: 3899
		public static float arcVisualizerSimulationLength;

		// Token: 0x04000F3C RID: 3900
		public static int arcVisualizerVertexCount;

		// Token: 0x04000F3D RID: 3901
		[SerializeField]
		public float baseChargeDuration = 1f;

		// Token: 0x04000F3E RID: 3902
		public static float minChargeForChargedAttack;

		// Token: 0x04000F3F RID: 3903
		public static GameObject chargeVfxPrefab;

		// Token: 0x04000F40 RID: 3904
		public static string chargeVfxChildLocatorName;

		// Token: 0x04000F41 RID: 3905
		public static GameObject crosshairOverridePrefab;

		// Token: 0x04000F42 RID: 3906
		public static float walkSpeedCoefficient;

		// Token: 0x04000F43 RID: 3907
		public static string startChargeLoopSFXString;

		// Token: 0x04000F44 RID: 3908
		public static string endChargeLoopSFXString;

		// Token: 0x04000F45 RID: 3909
		public static string enterSFXString;

		// Token: 0x04000F46 RID: 3910
		private CrosshairUtils.OverrideRequest crosshairOverrideRequest;

		// Token: 0x04000F48 RID: 3912
		private Transform chargeVfxInstanceTransform;

		// Token: 0x04000F4A RID: 3914
		private int gauntlet;

		// Token: 0x04000F4B RID: 3915
		private uint soundID;

		// Token: 0x020002C1 RID: 705
		private class ArcVisualizer : IDisposable
		{
			// Token: 0x06000C87 RID: 3207 RVA: 0x00034C54 File Offset: 0x00032E54
			public ArcVisualizer(GameObject arcVisualizerPrefab, float duration, int vertexCount)
			{
				this.arcVisualizerInstance = UnityEngine.Object.Instantiate<GameObject>(arcVisualizerPrefab);
				this.lineRenderer = this.arcVisualizerInstance.GetComponent<LineRenderer>();
				this.lineRenderer.positionCount = vertexCount;
				this.points = new Vector3[vertexCount];
				this.duration = duration;
			}

			// Token: 0x06000C88 RID: 3208 RVA: 0x00034CA3 File Offset: 0x00032EA3
			public void Dispose()
			{
				EntityState.Destroy(this.arcVisualizerInstance);
			}

			// Token: 0x06000C89 RID: 3209 RVA: 0x00034CB0 File Offset: 0x00032EB0
			public void SetParameters(Vector3 origin, Vector3 initialVelocity, float characterMaxSpeed, float characterAcceleration)
			{
				this.arcVisualizerInstance.transform.position = origin;
				if (!this.lineRenderer.useWorldSpace)
				{
					Vector3 eulerAngles = Quaternion.LookRotation(initialVelocity).eulerAngles;
					eulerAngles.x = 0f;
					eulerAngles.z = 0f;
					Quaternion rotation = Quaternion.Euler(eulerAngles);
					this.arcVisualizerInstance.transform.rotation = rotation;
					origin = Vector3.zero;
					initialVelocity = Quaternion.Inverse(rotation) * initialVelocity;
				}
				else
				{
					this.arcVisualizerInstance.transform.rotation = Quaternion.LookRotation(Vector3.Cross(initialVelocity, Vector3.up));
				}
				float y = Physics.gravity.y;
				float num = this.duration / (float)this.points.Length;
				Vector3 vector = origin;
				Vector3 vector2 = initialVelocity;
				float num2 = num;
				float num3 = y * num2;
				float maxDistanceDelta = characterAcceleration * num2;
				for (int i = 0; i < this.points.Length; i++)
				{
					this.points[i] = vector;
					Vector2 vector3 = Util.Vector3XZToVector2XY(vector2);
					vector3 = Vector2.MoveTowards(vector3, Vector3.zero, maxDistanceDelta);
					vector2.x = vector3.x;
					vector2.z = vector3.y;
					vector2.y += num3;
					vector += vector2 * num2;
				}
				this.lineRenderer.SetPositions(this.points);
			}

			// Token: 0x04000F4C RID: 3916
			private readonly Vector3[] points;

			// Token: 0x04000F4D RID: 3917
			private readonly float duration;

			// Token: 0x04000F4E RID: 3918
			private readonly GameObject arcVisualizerInstance;

			// Token: 0x04000F4F RID: 3919
			private readonly LineRenderer lineRenderer;
		}
	}
}
