using System;
using System.Runtime.CompilerServices;
using Rewired;
using UnityEngine;

namespace RoR2.CameraModes
{
	// Token: 0x02000E51 RID: 3665
	public class CameraModePlayerBasic : CameraModeBase
	{
		// Token: 0x060053F4 RID: 21492 RVA: 0x0015A0F2 File Offset: 0x001582F2
		protected override object CreateInstanceData(CameraRigController cameraRigController)
		{
			return new CameraModePlayerBasic.InstanceData();
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x0015A0F9 File Offset: 0x001582F9
		protected override void OnInstallInternal(object rawInstancedata, CameraRigController cameraRigController)
		{
			base.OnInstallInternal(rawInstancedata, cameraRigController);
			((CameraModePlayerBasic.InstanceData)rawInstancedata).neutralFov = cameraRigController.baseFov;
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x0015A114 File Offset: 0x00158314
		public override bool IsSpectating(CameraRigController cameraRigController)
		{
			return this.isSpectatorMode;
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x0015A11C File Offset: 0x0015831C
		protected override void UpdateInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, out CameraModeBase.UpdateResult result)
		{
			CameraModePlayerBasic.InstanceData instanceData = (CameraModePlayerBasic.InstanceData)rawInstanceData;
			CameraRigController cameraRigController = context.cameraInfo.cameraRigController;
			CameraTargetParams targetParams = context.targetInfo.targetParams;
			float fov = context.cameraInfo.baseFov;
			Quaternion rotation = context.cameraInfo.previousCameraState.rotation;
			Vector3 position = context.cameraInfo.previousCameraState.position;
			GameObject firstPersonTarget = null;
			float num = cameraRigController.baseFov;
			if (context.targetInfo.isSprinting)
			{
				num *= 1.3f;
			}
			instanceData.neutralFov = Mathf.SmoothDamp(instanceData.neutralFov, num, ref instanceData.neutralFovVelocity, 0.2f, float.PositiveInfinity, Time.deltaTime);
			CharacterCameraParamsData basic = CharacterCameraParamsData.basic;
			basic.fov = instanceData.neutralFov;
			Vector2 vector = Vector2.zero;
			if (targetParams)
			{
				CharacterCameraParamsData.Blend(targetParams.currentCameraParamsData, ref basic, 1f);
				fov = basic.fov.value;
				vector = targetParams.recoil;
			}
			if (basic.isFirstPerson.value)
			{
				firstPersonTarget = context.targetInfo.target;
			}
			instanceData.minPitch = basic.minPitch.value;
			instanceData.maxPitch = basic.maxPitch.value;
			float num2 = instanceData.pitchYaw.pitch;
			float num3 = instanceData.pitchYaw.yaw;
			num2 += vector.y;
			num3 += vector.x;
			num2 = Mathf.Clamp(num2, instanceData.minPitch, instanceData.maxPitch);
			num3 = Mathf.Repeat(num3, 360f);
			Vector3 vector2 = this.CalculateTargetPivotPosition(context);
			if (context.targetInfo.target)
			{
				rotation = Quaternion.Euler(num2, num3, 0f);
				Vector3 direction = vector2 + rotation * basic.idealLocalCameraPos.value - vector2;
				float num4 = direction.magnitude;
				float num5 = (1f + num2 / -90f) * 0.5f;
				num4 *= Mathf.Sqrt(1f - num5);
				if (num4 < 0.25f)
				{
					num4 = 0.25f;
				}
				Ray ray = new Ray(vector2, direction);
				float num6 = cameraRigController.Raycast(new Ray(vector2, direction), num4, basic.wallCushion.value - 0.01f);
				Debug.DrawRay(ray.origin, ray.direction * num4, Color.yellow, Time.deltaTime);
				Debug.DrawRay(ray.origin, ray.direction * num6, Color.red, Time.deltaTime);
				if (instanceData.currentCameraDistance >= num6)
				{
					instanceData.currentCameraDistance = num6;
					instanceData.cameraDistanceVelocity = 0f;
				}
				else
				{
					instanceData.currentCameraDistance = Mathf.SmoothDamp(instanceData.currentCameraDistance, num6, ref instanceData.cameraDistanceVelocity, 0.5f);
				}
				position = vector2 + direction.normalized * instanceData.currentCameraDistance;
			}
			result.cameraState.position = position;
			result.cameraState.rotation = rotation;
			result.cameraState.fov = fov;
			result.showSprintParticles = context.targetInfo.isSprinting;
			result.firstPersonTarget = firstPersonTarget;
			this.UpdateCrosshair(rawInstanceData, context, result.cameraState, vector2, out result.crosshairWorldPosition);
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x0015A45C File Offset: 0x0015865C
		protected override void CollectLookInputInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, out CameraModeBase.CollectLookInputResult output)
		{
			CameraModePlayerBasic.InstanceData instanceData = (CameraModePlayerBasic.InstanceData)rawInstanceData;
			ref CameraModeBase.CameraInfo ptr = ref context.cameraInfo;
			ref CameraModeBase.ViewerInfo ptr2 = ref context.viewerInfo;
			bool hasCursor = ptr2.hasCursor;
			Player inputPlayer = ptr2.inputPlayer;
			UserProfile userProfile = ptr2.userProfile;
			ICameraStateProvider overrideCam = ptr.overrideCam;
			output.lookInput = Vector3.zero;
			if (!hasCursor && ptr2.inputPlayer != null && userProfile != null && !ptr2.isUIFocused && (!(UnityEngine.Object)overrideCam || overrideCam.IsUserLookAllowed(ptr.cameraRigController)))
			{
				Vector2 a = new Vector2(inputPlayer.GetAxisRaw(2), inputPlayer.GetAxisRaw(3));
				Vector2 vector = new Vector2(inputPlayer.GetAxisRaw(16), inputPlayer.GetAxisRaw(17));
				CameraModePlayerBasic.<CollectLookInputInternal>g__ConditionalNegate|8_0(ref a.x, userProfile.mouseLookInvertX);
				CameraModePlayerBasic.<CollectLookInputInternal>g__ConditionalNegate|8_0(ref a.y, userProfile.mouseLookInvertY);
				CameraModePlayerBasic.<CollectLookInputInternal>g__ConditionalNegate|8_0(ref vector.x, userProfile.stickLookInvertX);
				CameraModePlayerBasic.<CollectLookInputInternal>g__ConditionalNegate|8_0(ref vector.y, userProfile.stickLookInvertY);
				this.PerformStickPostProcessing(instanceData, context, ref vector);
				float mouseLookSensitivity = userProfile.mouseLookSensitivity;
				float d = userProfile.stickLookSensitivity * CameraRigController.aimStickGlobalScale.value * 45f;
				Vector2 a2 = new Vector2(userProfile.mouseLookScaleX, userProfile.mouseLookScaleY);
				Vector2 a3 = new Vector2(userProfile.stickLookScaleX, userProfile.stickLookScaleY);
				a *= a2 * mouseLookSensitivity;
				vector *= a3 * d;
				this.PerformAimAssist(context, ref vector);
				vector *= Time.deltaTime;
				output.lookInput = a + vector;
			}
			output.lookInput *= ptr.previousCameraState.fov / ptr.baseFov;
			if (context.targetInfo.isSprinting && CameraRigController.enableSprintSensitivitySlowdown.value)
			{
				output.lookInput *= 0.5f;
			}
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x0015A664 File Offset: 0x00158864
		private void PerformStickPostProcessing(CameraModePlayerBasic.InstanceData instanceData, in CameraModeBase.CameraModeContext context, ref Vector2 aimStickVector)
		{
			float magnitude = aimStickVector.magnitude;
			float num = magnitude;
			Vector2 zero = Vector2.zero;
			Vector2 zero2 = Vector2.zero;
			Vector2 zero3 = Vector2.zero;
			if (CameraRigController.aimStickDualZoneSmoothing.value != 0f)
			{
				float maxDelta = Time.deltaTime / CameraRigController.aimStickDualZoneSmoothing.value;
				num = Mathf.Min(Mathf.MoveTowards(instanceData.stickAimPreviousAcceleratedMagnitude, magnitude, maxDelta), magnitude);
				instanceData.stickAimPreviousAcceleratedMagnitude = num;
				if (magnitude == 0f)
				{
					Vector2 zero4 = Vector2.zero;
				}
				else
				{
					aimStickVector * (num / magnitude);
				}
			}
			float num2 = num;
			float value = CameraRigController.aimStickDualZoneSlope.value;
			float num3;
			if (num2 <= CameraRigController.aimStickDualZoneThreshold.value)
			{
				num3 = 0f;
			}
			else
			{
				num3 = 1f - value;
			}
			num = value * num2 + num3;
			if (magnitude == 0f)
			{
				Vector2 zero5 = Vector2.zero;
			}
			else
			{
				aimStickVector * (num / magnitude);
			}
			num = Mathf.Pow(num, CameraRigController.aimStickExponent.value);
			if (magnitude == 0f)
			{
				Vector2 zero6 = Vector2.zero;
			}
			else
			{
				aimStickVector * (num / magnitude);
			}
			if (magnitude != 0f)
			{
				aimStickVector *= num / magnitude;
			}
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x0015A78C File Offset: 0x0015898C
		private void PerformAimAssist(in CameraModeBase.CameraModeContext context, ref Vector2 aimStickVector)
		{
			ref CameraModeBase.TargetInfo ptr = ref context.targetInfo;
			ref CameraModeBase.CameraInfo ptr2 = ref context.cameraInfo;
			if (!context.targetInfo.isViewerControlled || context.targetInfo.isSprinting)
			{
				return;
			}
			Camera sceneCam = ptr2.sceneCam;
			AimAssistTarget exists = null;
			AimAssistTarget exists2 = null;
			float value = CameraRigController.aimStickAssistMinSize.value;
			float num = value * CameraRigController.aimStickAssistMaxSize.value;
			float value2 = CameraRigController.aimStickAssistMaxSlowdownScale.value;
			float value3 = CameraRigController.aimStickAssistMinSlowdownScale.value;
			float num2 = 0f;
			float value4 = 0f;
			float num3 = 0f;
			Vector2 v = Vector2.zero;
			Vector2 zero = Vector2.zero;
			Vector2 normalized = aimStickVector.normalized;
			Vector2 vector = new Vector2(0.5f, 0.5f);
			for (int i = 0; i < AimAssistTarget.instancesList.Count; i++)
			{
				AimAssistTarget aimAssistTarget = AimAssistTarget.instancesList[i];
				if (aimAssistTarget.teamComponent.teamIndex != ptr.teamIndex)
				{
					Vector3 vector2 = sceneCam.WorldToViewportPoint(aimAssistTarget.point0.position);
					Vector3 vector3 = sceneCam.WorldToViewportPoint(aimAssistTarget.point1.position);
					float num4 = Mathf.Lerp(vector2.z, vector3.z, 0.5f);
					if (num4 > 3f)
					{
						float num5 = 1f / num4;
						Vector2 vector4 = Util.ClosestPointOnLine(vector2, vector3, vector) - vector;
						float num6 = Mathf.Clamp01(Util.Remap(vector4.magnitude, value * aimAssistTarget.assistScale * num5, num * aimAssistTarget.assistScale * num5, 1f, 0f));
						float num7 = Mathf.Clamp01(Vector3.Dot(vector4, normalized));
						float num8 = num7 * num6;
						if (num2 < num6)
						{
							num2 = num6;
							exists2 = aimAssistTarget;
						}
						if (num8 > num3)
						{
							num2 = num6;
							value4 = num7;
							exists = aimAssistTarget;
							v = vector4;
						}
					}
				}
			}
			Vector2 vector5 = aimStickVector;
			if (exists2)
			{
				float d = Mathf.Clamp01(Util.Remap(1f - num2, 0f, 1f, value2, value3));
				vector5 *= d;
			}
			if (exists)
			{
				vector5 = Vector3.RotateTowards(vector5, v, Util.Remap(value4, 1f, 0f, CameraRigController.aimStickAssistMaxDelta.value, CameraRigController.aimStickAssistMinDelta.value), 0f);
			}
			aimStickVector = vector5;
		}

		// Token: 0x060053FB RID: 21499 RVA: 0x0015AA00 File Offset: 0x00158C00
		protected override void ApplyLookInputInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraModeBase.ApplyLookInputArgs input)
		{
			CameraModePlayerBasic.InstanceData instanceData = (CameraModePlayerBasic.InstanceData)rawInstanceData;
			ref CameraModeBase.TargetInfo ptr = ref context.targetInfo;
			if (ptr.isViewerControlled)
			{
				float minPitch = instanceData.minPitch;
				float maxPitch = instanceData.maxPitch;
				instanceData.pitchYaw.pitch = Mathf.Clamp(instanceData.pitchYaw.pitch - input.lookInput.y, minPitch, maxPitch);
				CameraModePlayerBasic.InstanceData instanceData2 = instanceData;
				instanceData2.pitchYaw.yaw = instanceData2.pitchYaw.yaw + input.lookInput.x;
				if (ptr.networkedViewAngles && ptr.networkedViewAngles.hasEffectiveAuthority)
				{
					ptr.networkedViewAngles.viewAngles = instanceData.pitchYaw;
					return;
				}
			}
			else
			{
				if (ptr.networkedViewAngles)
				{
					instanceData.pitchYaw = ptr.networkedViewAngles.viewAngles;
					return;
				}
				if (ptr.inputBank)
				{
					instanceData.SetPitchYawFromLookVector(ptr.inputBank.aimDirection);
				}
			}
		}

		// Token: 0x060053FC RID: 21500 RVA: 0x0015AAE0 File Offset: 0x00158CE0
		protected void UpdateCrosshair(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraState cameraState, in Vector3 targetPivotPosition, out Vector3 crosshairWorldPosition)
		{
			CameraModePlayerBasic.InstanceData instanceData = (CameraModePlayerBasic.InstanceData)rawInstanceData;
			instanceData.lastAimAssist = instanceData.aimAssist;
			Ray crosshairRaycastRay = this.GetCrosshairRaycastRay(context, Vector2.zero, targetPivotPosition, cameraState);
			bool flag = false;
			instanceData.lastCrosshairHurtBox = null;
			RaycastHit raycastHit = default(RaycastHit);
			RaycastHit[] array = Physics.RaycastAll(crosshairRaycastRay, context.cameraInfo.cameraRigController.maxAimRaycastDistance, LayerIndex.world.mask | LayerIndex.entityPrecise.mask, QueryTriggerInteraction.Ignore);
			float num = float.PositiveInfinity;
			int num2 = -1;
			for (int i = 0; i < array.Length; i++)
			{
				RaycastHit raycastHit2 = array[i];
				HurtBox hurtBox = raycastHit2.collider.GetComponent<HurtBox>();
				EntityLocator component = raycastHit2.collider.GetComponent<EntityLocator>();
				float distance = raycastHit2.distance;
				if (distance > 3f && num > distance)
				{
					if (hurtBox)
					{
						if (hurtBox.teamIndex == context.targetInfo.teamIndex)
						{
							goto IL_166;
						}
						if (hurtBox.healthComponent && hurtBox.healthComponent.dontShowHealthbar)
						{
							hurtBox = null;
						}
					}
					if (component)
					{
						VehicleSeat vehicleSeat = component.entity ? component.entity.GetComponent<VehicleSeat>() : null;
						if (vehicleSeat && vehicleSeat.currentPassengerBody == context.targetInfo.body)
						{
							goto IL_166;
						}
					}
					num = distance;
					num2 = i;
					instanceData.lastCrosshairHurtBox = hurtBox;
				}
				IL_166:;
			}
			if (num2 != -1)
			{
				flag = true;
				raycastHit = array[num2];
			}
			instanceData.aimAssist.aimAssistHurtbox = null;
			if (flag)
			{
				crosshairWorldPosition = raycastHit.point;
				float num3 = 1000f;
				if (raycastHit.distance < num3)
				{
					HurtBox component2 = raycastHit.collider.GetComponent<HurtBox>();
					if (component2)
					{
						HealthComponent healthComponent = component2.healthComponent;
						if (healthComponent)
						{
							TeamComponent component3 = healthComponent.GetComponent<TeamComponent>();
							if (component3 && component3.teamIndex != context.targetInfo.teamIndex && component3.teamIndex != TeamIndex.None)
							{
								CharacterBody body = healthComponent.body;
								HurtBox hurtBox2 = (body != null) ? body.mainHurtBox : null;
								if (hurtBox2)
								{
									instanceData.aimAssist.aimAssistHurtbox = hurtBox2;
									instanceData.aimAssist.worldPosition = raycastHit.point;
									instanceData.aimAssist.localPositionOnHurtbox = hurtBox2.transform.InverseTransformPoint(raycastHit.point);
									return;
								}
							}
						}
					}
				}
			}
			else
			{
				crosshairWorldPosition = crosshairRaycastRay.GetPoint(context.cameraInfo.cameraRigController.maxAimRaycastDistance);
			}
		}

		// Token: 0x060053FD RID: 21501 RVA: 0x0015AD8C File Offset: 0x00158F8C
		private Ray GetCrosshairRaycastRay(in CameraModeBase.CameraModeContext context, Vector2 crosshairOffset, Vector3 raycastStartPlanePoint, in CameraState cameraState)
		{
			if (!context.cameraInfo.sceneCam)
			{
				return default(Ray);
			}
			float fov = cameraState.fov;
			float num = fov * context.cameraInfo.sceneCam.aspect;
			Quaternion quaternion = Quaternion.Euler(crosshairOffset.y * fov, crosshairOffset.x * num, 0f);
			quaternion = cameraState.rotation * quaternion;
			return new Ray(Vector3.ProjectOnPlane(cameraState.position - raycastStartPlanePoint, cameraState.rotation * Vector3.forward) + raycastStartPlanePoint, quaternion * Vector3.forward);
		}

		// Token: 0x060053FE RID: 21502 RVA: 0x0015AE34 File Offset: 0x00159034
		private Vector3 CalculateTargetPivotPosition(in CameraModeBase.CameraModeContext context)
		{
			CameraRigController cameraRigController = context.cameraInfo.cameraRigController;
			CameraTargetParams targetParams = context.targetInfo.targetParams;
			Vector3 result = context.cameraInfo.previousCameraState.position;
			if (targetParams)
			{
				Vector3 position = targetParams.transform.position;
				Vector3 vector = (targetParams.cameraPivotTransform ? targetParams.cameraPivotTransform.position : position) + new Vector3(0f, targetParams.currentCameraParamsData.pivotVerticalOffset.value, 0f);
				if (targetParams.dontRaycastToPivot)
				{
					result = vector;
				}
				else
				{
					Vector3 direction = vector - position;
					float magnitude = direction.magnitude;
					Ray ray = new Ray(position, direction);
					float num = cameraRigController.Raycast(ray, magnitude, targetParams.currentCameraParamsData.wallCushion.value);
					Debug.DrawRay(ray.origin, ray.direction * magnitude, Color.green, Time.deltaTime);
					Debug.DrawRay(ray.origin, ray.direction * num, Color.red, Time.deltaTime);
					result = ray.GetPoint(num);
				}
			}
			return result;
		}

		// Token: 0x060053FF RID: 21503 RVA: 0x0015AF60 File Offset: 0x00159160
		protected override void OnTargetChangedInternal(object rawInstanceData, CameraRigController cameraRigController, in CameraModeBase.OnTargetChangedArgs args)
		{
			CameraModePlayerBasic.InstanceData instanceData = (CameraModePlayerBasic.InstanceData)rawInstanceData;
			if (!instanceData.hasEverHadTarget && args.newTarget)
			{
				CharacterBody component = args.newTarget.GetComponent<CharacterBody>();
				if (component)
				{
					CharacterDirection characterDirection = component.characterDirection;
					if (characterDirection)
					{
						instanceData.pitchYaw = new PitchYawPair(0f, characterDirection.yaw);
					}
				}
				instanceData.hasEverHadTarget = true;
			}
		}

		// Token: 0x06005400 RID: 21504 RVA: 0x0015AFC9 File Offset: 0x001591C9
		protected override void MatchStateInternal(object rawInstanceData, in CameraModeBase.CameraModeContext context, in CameraState cameraStateToMatch)
		{
			((CameraModePlayerBasic.InstanceData)rawInstanceData).SetPitchYawFromLookVector(cameraStateToMatch.rotation * Vector3.forward);
		}

		// Token: 0x06005403 RID: 21507 RVA: 0x0015B00A File Offset: 0x0015920A
		[CompilerGenerated]
		internal static void <CollectLookInputInternal>g__ConditionalNegate|8_0(ref float value, bool condition)
		{
			value = (condition ? (-value) : value);
		}

		// Token: 0x04004FD6 RID: 20438
		public bool isSpectatorMode;

		// Token: 0x04004FD7 RID: 20439
		public static CameraModePlayerBasic playerBasic = new CameraModePlayerBasic
		{
			isSpectatorMode = false
		};

		// Token: 0x04004FD8 RID: 20440
		public static CameraModePlayerBasic spectator = new CameraModePlayerBasic
		{
			isSpectatorMode = true
		};

		// Token: 0x02000E52 RID: 3666
		protected class InstanceData
		{
			// Token: 0x06005404 RID: 21508 RVA: 0x0015B018 File Offset: 0x00159218
			public void SetPitchYawFromLookVector(Vector3 lookVector)
			{
				float x = Mathf.Sqrt(lookVector.x * lookVector.x + lookVector.z * lookVector.z);
				this.pitchYaw.pitch = Mathf.Atan2(-lookVector.y, x) * 57.29578f;
				this.pitchYaw.yaw = Mathf.Repeat(Mathf.Atan2(lookVector.x, lookVector.z) * 57.29578f, 360f);
			}

			// Token: 0x04004FD9 RID: 20441
			public float currentCameraDistance;

			// Token: 0x04004FDA RID: 20442
			public float cameraDistanceVelocity;

			// Token: 0x04004FDB RID: 20443
			public float stickAimPreviousAcceleratedMagnitude;

			// Token: 0x04004FDC RID: 20444
			public float minPitch;

			// Token: 0x04004FDD RID: 20445
			public float maxPitch;

			// Token: 0x04004FDE RID: 20446
			public PitchYawPair pitchYaw;

			// Token: 0x04004FDF RID: 20447
			public CameraRigController.AimAssistInfo lastAimAssist;

			// Token: 0x04004FE0 RID: 20448
			public CameraRigController.AimAssistInfo aimAssist;

			// Token: 0x04004FE1 RID: 20449
			public HurtBox lastCrosshairHurtBox;

			// Token: 0x04004FE2 RID: 20450
			public bool hasEverHadTarget;

			// Token: 0x04004FE3 RID: 20451
			public float neutralFov;

			// Token: 0x04004FE4 RID: 20452
			public float neutralFovVelocity;
		}
	}
}
