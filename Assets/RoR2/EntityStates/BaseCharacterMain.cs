using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000B0 RID: 176
	public class BaseCharacterMain : BaseState
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B9C2 File Offset: 0x00009BC2
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000B9CA File Offset: 0x00009BCA
		private protected Animator modelAnimator { protected get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B9D3 File Offset: 0x00009BD3
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000B9DB File Offset: 0x00009BDB
		private protected Vector3 estimatedVelocity { protected get; private set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		public override void OnEnter()
		{
			base.OnEnter();
			this.modelAnimator = base.GetModelAnimator();
			this.rootMotionAccumulator = base.GetModelRootMotionAccumulator();
			if (this.rootMotionAccumulator)
			{
				this.rootMotionAccumulator.ExtractRootMotion();
			}
			base.GetBodyAnimatorSmoothingParameters(out this.smoothingParameters);
			this.previousPosition = base.transform.position;
			this.hasCharacterMotor = base.characterMotor;
			this.hasCharacterDirection = base.characterDirection;
			this.hasCharacterBody = base.characterBody;
			this.hasRailMotor = base.railMotor;
			this.hasCameraTargetParams = base.cameraTargetParams;
			this.hasSkillLocator = base.skillLocator;
			this.hasModelAnimator = this.modelAnimator;
			this.hasInputBank = base.inputBank;
			this.hasRootMotionAccumulator = this.rootMotionAccumulator;
			if (this.modelAnimator)
			{
				this.characterAnimParamAvailability = CharacterAnimParamAvailability.FromAnimator(this.modelAnimator);
				int layerIndex = this.modelAnimator.GetLayerIndex("Body");
				if (this.characterAnimParamAvailability.isGrounded)
				{
					this.wasGrounded = base.isGrounded;
					this.modelAnimator.SetBool(AnimationParameters.isGrounded, this.wasGrounded);
				}
				if (base.isGrounded || !this.hasCharacterMotor)
				{
					this.modelAnimator.CrossFadeInFixedTime("Idle", 0.1f, layerIndex);
				}
				else
				{
					this.modelAnimator.CrossFadeInFixedTime("AscendDescend", 0.1f, layerIndex);
				}
				this.modelAnimator.Update(0f);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000BB90 File Offset: 0x00009D90
		public override void OnExit()
		{
			if (this.rootMotionAccumulator)
			{
				this.rootMotionAccumulator.ExtractRootMotion();
			}
			if (this.modelAnimator)
			{
				if (this.characterAnimParamAvailability.isMoving)
				{
					this.modelAnimator.SetBool(AnimationParameters.isMoving, false);
				}
				if (this.characterAnimParamAvailability.turnAngle)
				{
					this.modelAnimator.SetFloat(AnimationParameters.turnAngle, 0f);
				}
			}
			base.OnExit();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BC0C File Offset: 0x00009E0C
		public override void Update()
		{
			base.Update();
			if (Time.deltaTime <= 0f)
			{
				return;
			}
			Vector3 position = base.transform.position;
			this.estimatedVelocity = (position - this.previousPosition) / Time.deltaTime;
			this.previousPosition = position;
			this.useRootMotion = ((base.characterBody && base.characterBody.rootMotionInMainState && base.isGrounded) || base.railMotor);
			this.UpdateAnimationParameters();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BC98 File Offset: 0x00009E98
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (this.hasCharacterMotor)
			{
				float num = this.estimatedVelocity.y - this.lastYSpeed;
				if (base.isGrounded && !this.wasGrounded && this.hasModelAnimator)
				{
					int layerIndex = this.modelAnimator.GetLayerIndex("Impact");
					if (layerIndex >= 0)
					{
						this.modelAnimator.SetLayerWeight(layerIndex, Mathf.Clamp01(Mathf.Max(new float[]
						{
							0.3f,
							num / 5f,
							this.modelAnimator.GetLayerWeight(layerIndex)
						})));
						this.modelAnimator.PlayInFixedTime("LightImpact", layerIndex, 0f);
					}
				}
				this.wasGrounded = base.isGrounded;
				this.lastYSpeed = this.estimatedVelocity.y;
			}
			if (this.hasRootMotionAccumulator)
			{
				Vector3 vector = this.rootMotionAccumulator.ExtractRootMotion();
				if (this.useRootMotion && vector != Vector3.zero && base.isAuthority)
				{
					if (base.characterMotor)
					{
						base.characterMotor.rootMotion += vector;
					}
					if (base.railMotor)
					{
						base.railMotor.rootMotion += vector;
					}
				}
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BDE0 File Offset: 0x00009FE0
		protected virtual void UpdateAnimationParameters()
		{
			if (this.hasRailMotor || !this.hasModelAnimator)
			{
				return;
			}
			Vector3 vector = base.inputBank ? base.inputBank.moveVector : Vector3.zero;
			bool value = vector != Vector3.zero && base.characterBody.moveSpeed > Mathf.Epsilon;
			this.animatorWalkParamCalculator.Update(vector, base.characterDirection ? base.characterDirection.animatorForward : base.transform.forward, this.smoothingParameters, Time.fixedDeltaTime);
			if (this.useRootMotion)
			{
				if (this.characterAnimParamAvailability.mainRootPlaybackRate)
				{
					float num = 1f;
					if (base.modelLocator && base.modelLocator.modelTransform)
					{
						num = base.modelLocator.modelTransform.localScale.x;
					}
					float value2 = base.characterBody.moveSpeed / (base.characterBody.mainRootSpeed * num);
					this.modelAnimator.SetFloat(AnimationParameters.mainRootPlaybackRate, value2);
				}
			}
			else if (this.characterAnimParamAvailability.walkSpeed)
			{
				this.modelAnimator.SetFloat(AnimationParameters.walkSpeed, base.characterBody.moveSpeed);
			}
			if (this.characterAnimParamAvailability.isGrounded)
			{
				this.modelAnimator.SetBool(AnimationParameters.isGrounded, base.isGrounded);
			}
			if (this.characterAnimParamAvailability.isMoving)
			{
				this.modelAnimator.SetBool(AnimationParameters.isMoving, value);
			}
			if (this.characterAnimParamAvailability.turnAngle)
			{
				this.modelAnimator.SetFloat(AnimationParameters.turnAngle, this.animatorWalkParamCalculator.remainingTurnAngle, this.smoothingParameters.turnAngleSmoothDamp, Time.fixedDeltaTime);
			}
			if (this.characterAnimParamAvailability.isSprinting)
			{
				this.modelAnimator.SetBool(AnimationParameters.isSprinting, base.characterBody.isSprinting);
			}
			if (this.characterAnimParamAvailability.forwardSpeed)
			{
				this.modelAnimator.SetFloat(AnimationParameters.forwardSpeed, this.animatorWalkParamCalculator.animatorWalkSpeed.x, this.smoothingParameters.forwardSpeedSmoothDamp, Time.deltaTime);
			}
			if (this.characterAnimParamAvailability.rightSpeed)
			{
				this.modelAnimator.SetFloat(AnimationParameters.rightSpeed, this.animatorWalkParamCalculator.animatorWalkSpeed.y, this.smoothingParameters.rightSpeedSmoothDamp, Time.deltaTime);
			}
			if (this.characterAnimParamAvailability.upSpeed)
			{
				this.modelAnimator.SetFloat(AnimationParameters.upSpeed, this.estimatedVelocity.y, 0.1f, Time.deltaTime);
			}
		}

		// Token: 0x0400030D RID: 781
		private RootMotionAccumulator rootMotionAccumulator;

		// Token: 0x0400030F RID: 783
		private Vector3 previousPosition;

		// Token: 0x04000311 RID: 785
		protected CharacterAnimParamAvailability characterAnimParamAvailability;

		// Token: 0x04000312 RID: 786
		private CharacterAnimatorWalkParamCalculator animatorWalkParamCalculator;

		// Token: 0x04000313 RID: 787
		protected BodyAnimatorSmoothingParameters.SmoothingParameters smoothingParameters;

		// Token: 0x04000314 RID: 788
		protected bool useRootMotion;

		// Token: 0x04000315 RID: 789
		private bool wasGrounded;

		// Token: 0x04000316 RID: 790
		private float lastYSpeed;

		// Token: 0x04000317 RID: 791
		protected bool hasCharacterMotor;

		// Token: 0x04000318 RID: 792
		protected bool hasCharacterDirection;

		// Token: 0x04000319 RID: 793
		protected bool hasCharacterBody;

		// Token: 0x0400031A RID: 794
		protected bool hasRailMotor;

		// Token: 0x0400031B RID: 795
		protected bool hasCameraTargetParams;

		// Token: 0x0400031C RID: 796
		protected bool hasSkillLocator;

		// Token: 0x0400031D RID: 797
		protected bool hasModelAnimator;

		// Token: 0x0400031E RID: 798
		protected bool hasInputBank;

		// Token: 0x0400031F RID: 799
		protected bool hasRootMotionAccumulator;
	}
}
