using System;
using System.Collections.Generic;
using UnityEngine;

namespace RoR2
{
	// Token: 0x020005BE RID: 1470
	[RequireComponent(typeof(Animator))]
	public class AimAnimator : MonoBehaviour, ILifeBehavior
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001A9E RID: 6814 RVA: 0x00072176 File Offset: 0x00070376
		// (set) Token: 0x06001A9D RID: 6813 RVA: 0x0007216D File Offset: 0x0007036D
		public bool isOutsideOfRange { get; private set; }

		// Token: 0x06001A9F RID: 6815 RVA: 0x00072180 File Offset: 0x00070380
		public AimAnimator.DirectionOverrideRequest RequestDirectionOverride(Func<Vector3> getter)
		{
			AimAnimator.DirectionOverrideRequest directionOverrideRequest = new AimAnimator.DirectionOverrideRequest(getter, new Action<AimAnimator.DirectionOverrideRequest>(this.RemoveRequest));
			this.directionOverrideRequests.Add(directionOverrideRequest);
			return directionOverrideRequest;
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000721AD File Offset: 0x000703AD
		private void RemoveRequest(AimAnimator.DirectionOverrideRequest request)
		{
			this.directionOverrideRequests.Remove(request);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000721BC File Offset: 0x000703BC
		private void Awake()
		{
			this.animatorComponent = base.GetComponent<Animator>();
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000721CC File Offset: 0x000703CC
		private void Start()
		{
			int layerIndex = this.animatorComponent.GetLayerIndex("AimPitch");
			int layerIndex2 = this.animatorComponent.GetLayerIndex("AimYaw");
			this.animatorComponent.Play("PitchControl", layerIndex);
			this.animatorComponent.Play("YawControl", layerIndex2);
			this.animatorComponent.Update(0f);
			AnimatorClipInfo[] currentAnimatorClipInfo = this.animatorComponent.GetCurrentAnimatorClipInfo(layerIndex);
			AnimatorClipInfo[] currentAnimatorClipInfo2 = this.animatorComponent.GetCurrentAnimatorClipInfo(layerIndex2);
			if (currentAnimatorClipInfo.Length != 0)
			{
				AnimationClip clip = currentAnimatorClipInfo[0].clip;
				double num = (double)(clip.length * clip.frameRate);
				this.pitchClipCycleEnd = (float)((num - 1.0) / num);
			}
			if (currentAnimatorClipInfo2.Length != 0)
			{
				AnimationClip clip2 = currentAnimatorClipInfo2[0].clip;
				float length = clip2.length;
				float frameRate = clip2.frameRate;
				this.yawClipCycleEnd = 0.999f;
			}
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000722B0 File Offset: 0x000704B0
		private void Update()
		{
			if (Time.deltaTime <= 0f)
			{
				return;
			}
			this.UpdateLocalAnglesToAimVector();
			this.UpdateGiveup();
			this.ApproachDesiredAngles();
			this.UpdateAnimatorParameters(this.animatorComponent, this.pitchRangeMin, this.pitchRangeMax, this.yawRangeMin, this.yawRangeMax);
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00072300 File Offset: 0x00070500
		public void OnDeathStart()
		{
			base.enabled = false;
			this.currentLocalAngles = new AimAnimator.AimAngles
			{
				pitch = 0f,
				yaw = 0f
			};
			this.UpdateAnimatorParameters(this.animatorComponent, this.pitchRangeMin, this.pitchRangeMax, this.yawRangeMin, this.yawRangeMax);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x0007235F File Offset: 0x0007055F
		private static float Remap(float value, float inMin, float inMax, float outMin, float outMax)
		{
			return outMin + (value - inMin) / (inMax - inMin) * (outMax - outMin);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x0007236F File Offset: 0x0007056F
		private static float NormalizeAngle(float angle)
		{
			return Mathf.Repeat(angle + 180f, 360f) - 180f;
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x00072388 File Offset: 0x00070588
		private void UpdateLocalAnglesToAimVector()
		{
			Vector3 vector;
			if (this.directionOverrideRequests.Count > 0)
			{
				vector = this.directionOverrideRequests[this.directionOverrideRequests.Count - 1].directionGetter();
			}
			else
			{
				vector = (this.inputBank ? this.inputBank.aimDirection : base.transform.forward);
			}
			if (this.UseTransformedAimVector)
			{
				Vector3 eulerAngles = Util.QuaternionSafeLookRotation(base.transform.InverseTransformDirection(vector), base.transform.up).eulerAngles;
				this.localAnglesToAimVector = new AimAnimator.AimAngles
				{
					pitch = AimAnimator.NormalizeAngle(eulerAngles.x),
					yaw = AimAnimator.NormalizeAngle(eulerAngles.y)
				};
			}
			else
			{
				float y = this.directionComponent ? this.directionComponent.yaw : base.transform.eulerAngles.y;
				float x = this.directionComponent ? this.directionComponent.transform.eulerAngles.x : base.transform.eulerAngles.x;
				float z = this.directionComponent ? this.directionComponent.transform.eulerAngles.z : base.transform.eulerAngles.z;
				Vector3 eulerAngles2 = Util.QuaternionSafeLookRotation(vector, base.transform.up).eulerAngles;
				Vector3 vector2 = vector;
				Vector3 vector3 = new Vector3(x, y, z);
				vector2.y = 0f;
				this.localAnglesToAimVector = new AimAnimator.AimAngles
				{
					pitch = -Mathf.Atan2(vector.y, vector2.magnitude) * 57.29578f,
					yaw = AimAnimator.NormalizeAngle(eulerAngles2.y - vector3.y)
				};
			}
			this.overshootAngles = new AimAnimator.AimAngles
			{
				pitch = Mathf.Max(this.pitchRangeMin - this.localAnglesToAimVector.pitch, this.localAnglesToAimVector.pitch - this.pitchRangeMax),
				yaw = Mathf.Max(Mathf.DeltaAngle(this.localAnglesToAimVector.yaw, this.yawRangeMin), Mathf.DeltaAngle(this.yawRangeMax, this.localAnglesToAimVector.yaw))
			};
			this.clampedLocalAnglesToAimVector = new AimAnimator.AimAngles
			{
				pitch = Mathf.Clamp(this.localAnglesToAimVector.pitch, this.pitchRangeMin, this.pitchRangeMax),
				yaw = Mathf.Clamp(this.localAnglesToAimVector.yaw, this.yawRangeMin, this.yawRangeMax)
			};
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x00072638 File Offset: 0x00070838
		private void ApproachDesiredAngles()
		{
			AimAnimator.AimAngles aimAngles;
			float maxSpeed;
			if (this.shouldGiveup)
			{
				aimAngles = new AimAnimator.AimAngles
				{
					pitch = 0f,
					yaw = 0f
				};
				maxSpeed = this.loweredApproachSpeed;
			}
			else
			{
				aimAngles = this.clampedLocalAnglesToAimVector;
				maxSpeed = this.raisedApproachSpeed;
			}
			float yaw;
			if (this.fullYaw)
			{
				yaw = AimAnimator.NormalizeAngle(Mathf.SmoothDampAngle(this.currentLocalAngles.yaw, aimAngles.yaw, ref this.smoothingVelocity.yaw, this.smoothTime, maxSpeed, Time.deltaTime));
			}
			else
			{
				yaw = Mathf.SmoothDamp(this.currentLocalAngles.yaw, aimAngles.yaw, ref this.smoothingVelocity.yaw, this.smoothTime, maxSpeed, Time.deltaTime);
			}
			this.currentLocalAngles = new AimAnimator.AimAngles
			{
				pitch = Mathf.SmoothDampAngle(this.currentLocalAngles.pitch, aimAngles.pitch, ref this.smoothingVelocity.pitch, this.smoothTime, maxSpeed, Time.deltaTime),
				yaw = yaw
			};
		}

		// Token: 0x06001AA9 RID: 6825 RVA: 0x0007273C File Offset: 0x0007093C
		private void ResetGiveup()
		{
			this.giveupTimer = this.giveupDuration;
		}

		// Token: 0x06001AAA RID: 6826 RVA: 0x0007274C File Offset: 0x0007094C
		private void UpdateGiveup()
		{
			if (this.overshootAngles.pitch > this.pitchGiveupRange || (!this.fullYaw && this.overshootAngles.yaw > this.yawGiveupRange))
			{
				this.giveupTimer -= Time.deltaTime;
				this.isOutsideOfRange = true;
				return;
			}
			this.isOutsideOfRange = false;
			this.ResetGiveup();
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x000727AE File Offset: 0x000709AE
		private bool shouldGiveup
		{
			get
			{
				return this.giveupTimer <= 0f;
			}
		}

		// Token: 0x06001AAC RID: 6828 RVA: 0x000727C0 File Offset: 0x000709C0
		public void AimImmediate()
		{
			this.UpdateLocalAnglesToAimVector();
			this.ResetGiveup();
			this.currentLocalAngles = this.clampedLocalAnglesToAimVector;
			this.smoothingVelocity = new AimAnimator.AimAngles
			{
				pitch = 0f,
				yaw = 0f
			};
			this.UpdateAnimatorParameters(this.animatorComponent, this.pitchRangeMin, this.pitchRangeMax, this.yawRangeMin, this.yawRangeMax);
		}

		// Token: 0x06001AAD RID: 6829 RVA: 0x00072830 File Offset: 0x00070A30
		public void UpdateAnimatorParameters(Animator animator, float pitchRangeMin, float pitchRangeMax, float yawRangeMin, float yawRangeMax)
		{
			float num = 1f;
			if (this.enableAimWeight)
			{
				num = this.animatorComponent.GetFloat(AimAnimator.aimWeightHash);
			}
			animator.SetFloat(AimAnimator.aimPitchCycleHash, AimAnimator.Remap(this.currentLocalAngles.pitch * num, pitchRangeMin, pitchRangeMax, this.pitchClipCycleEnd, 0f));
			animator.SetFloat(AimAnimator.aimYawCycleHash, AimAnimator.Remap(this.currentLocalAngles.yaw * num, yawRangeMin, yawRangeMax, 0f, this.yawClipCycleEnd));
		}

		// Token: 0x040020A8 RID: 8360
		[Tooltip("The input bank component of the character.")]
		public InputBankTest inputBank;

		// Token: 0x040020A9 RID: 8361
		[Tooltip("The direction component of the character.")]
		public CharacterDirection directionComponent;

		// Token: 0x040020AA RID: 8362
		[Tooltip("The minimum pitch supplied by the aiming animation.")]
		public float pitchRangeMin;

		// Token: 0x040020AB RID: 8363
		[Tooltip("The maximum pitch supplied by the aiming animation.")]
		public float pitchRangeMax;

		// Token: 0x040020AC RID: 8364
		[Tooltip("The minimum yaw supplied by the aiming animation.")]
		public float yawRangeMin;

		// Token: 0x040020AD RID: 8365
		[Tooltip("The maximum yaw supplied by the aiming animation.")]
		public float yawRangeMax;

		// Token: 0x040020AE RID: 8366
		[Tooltip("If the pitch is this many degrees beyond the range the aiming animations support, the character will return to neutral pose after waiting the giveup duration.")]
		public float pitchGiveupRange;

		// Token: 0x040020AF RID: 8367
		[Tooltip("If the yaw is this many degrees beyond the range the aiming animations support, the character will return to neutral pose after waiting the giveup duration.")]
		public float yawGiveupRange;

		// Token: 0x040020B0 RID: 8368
		[Tooltip("If the pitch or yaw exceed the range supported by the aiming animations, the character will return to neutral pose after waiting this long.")]
		public float giveupDuration;

		// Token: 0x040020B1 RID: 8369
		[Tooltip("The speed in degrees/second to approach the desired pitch/yaw by while the weapon should be raised.")]
		public float raisedApproachSpeed = 720f;

		// Token: 0x040020B2 RID: 8370
		[Tooltip("The speed in degrees/second to approach the desired pitch/yaw by while the weapon should be lowered.")]
		public float loweredApproachSpeed = 360f;

		// Token: 0x040020B3 RID: 8371
		[Tooltip("The smoothing time for the motion.")]
		public float smoothTime = 0.1f;

		// Token: 0x040020B4 RID: 8372
		[Tooltip("Whether or not the character can do full 360 yaw turns.")]
		public bool fullYaw;

		// Token: 0x040020B5 RID: 8373
		[Tooltip("Switches between Direct (point straight at target) or Smart (only turn when outside angle range).")]
		public AimAnimator.AimType aimType;

		// Token: 0x040020B6 RID: 8374
		[Tooltip("Assigns the weight of the aim from the center as an animator value 'aimWeight' between 0-1.")]
		public bool enableAimWeight;

		// Token: 0x040020B8 RID: 8376
		public bool UseTransformedAimVector;

		// Token: 0x040020B9 RID: 8377
		private Animator animatorComponent;

		// Token: 0x040020BA RID: 8378
		private float pitchClipCycleEnd;

		// Token: 0x040020BB RID: 8379
		private float yawClipCycleEnd;

		// Token: 0x040020BC RID: 8380
		private float giveupTimer;

		// Token: 0x040020BD RID: 8381
		private AimAnimator.AimAngles localAnglesToAimVector;

		// Token: 0x040020BE RID: 8382
		private AimAnimator.AimAngles overshootAngles;

		// Token: 0x040020BF RID: 8383
		private AimAnimator.AimAngles clampedLocalAnglesToAimVector;

		// Token: 0x040020C0 RID: 8384
		private AimAnimator.AimAngles currentLocalAngles;

		// Token: 0x040020C1 RID: 8385
		private AimAnimator.AimAngles smoothingVelocity;

		// Token: 0x040020C2 RID: 8386
		private List<AimAnimator.DirectionOverrideRequest> directionOverrideRequests = new List<AimAnimator.DirectionOverrideRequest>();

		// Token: 0x040020C3 RID: 8387
		private static readonly int aimPitchCycleHash = Animator.StringToHash("aimPitchCycle");

		// Token: 0x040020C4 RID: 8388
		private static readonly int aimYawCycleHash = Animator.StringToHash("aimYawCycle");

		// Token: 0x040020C5 RID: 8389
		private static readonly int aimWeightHash = Animator.StringToHash("aimWeight");

		// Token: 0x020005BF RID: 1471
		public enum AimType
		{
			// Token: 0x040020C7 RID: 8391
			Direct,
			// Token: 0x040020C8 RID: 8392
			Smart
		}

		// Token: 0x020005C0 RID: 1472
		public class DirectionOverrideRequest : IDisposable
		{
			// Token: 0x06001AB0 RID: 6832 RVA: 0x00072915 File Offset: 0x00070B15
			public DirectionOverrideRequest(Func<Vector3> getter, Action<AimAnimator.DirectionOverrideRequest> onDispose)
			{
				this.disposeCallback = onDispose;
				this.directionGetter = getter;
			}

			// Token: 0x06001AB1 RID: 6833 RVA: 0x0007292B File Offset: 0x00070B2B
			public void Dispose()
			{
				Action<AimAnimator.DirectionOverrideRequest> action = this.disposeCallback;
				if (action != null)
				{
					action(this);
				}
				this.disposeCallback = null;
			}

			// Token: 0x040020C9 RID: 8393
			public readonly Func<Vector3> directionGetter;

			// Token: 0x040020CA RID: 8394
			private Action<AimAnimator.DirectionOverrideRequest> disposeCallback;
		}

		// Token: 0x020005C1 RID: 1473
		private struct AimAngles
		{
			// Token: 0x040020CB RID: 8395
			public float pitch;

			// Token: 0x040020CC RID: 8396
			public float yaw;
		}
	}
}
