using System;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000BB RID: 187
	public class EntityState
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000CF32 File Offset: 0x0000B132
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000CF3A File Offset: 0x0000B13A
		protected float age { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000CF43 File Offset: 0x0000B143
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000CF4B File Offset: 0x0000B14B
		protected float fixedAge { get; set; }

		// Token: 0x06000322 RID: 802 RVA: 0x0000CF54 File Offset: 0x0000B154
		public EntityState()
		{
			EntityStateCatalog.InitializeStateFields(this);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnEnter()
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnExit()
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void ModifyNextState(EntityState nextState)
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000CF62 File Offset: 0x0000B162
		public virtual void Update()
		{
			this.age += Time.deltaTime;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000CF76 File Offset: 0x0000B176
		public virtual void FixedUpdate()
		{
			this.fixedAge += Time.fixedDeltaTime;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnSerialize(NetworkWriter writer)
		{
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000026ED File Offset: 0x000008ED
		public virtual void OnDeserialize(NetworkReader reader)
		{
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public virtual InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000CF8D File Offset: 0x0000B18D
		protected GameObject gameObject
		{
			get
			{
				return this.outer.gameObject;
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000CF9A File Offset: 0x0000B19A
		protected static void Destroy(UnityEngine.Object obj)
		{
			UnityEngine.Object.Destroy(obj);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000CFA2 File Offset: 0x0000B1A2
		protected T GetComponent<T>() where T : Component
		{
			return this.outer.GetComponent<T>();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000CFAF File Offset: 0x0000B1AF
		protected Component GetComponent(Type type)
		{
			return this.outer.GetComponent(type);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000CFBD File Offset: 0x0000B1BD
		protected Component GetComponent(string type)
		{
			return this.outer.GetComponent(type);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000CFCB File Offset: 0x0000B1CB
		protected bool isLocalPlayer
		{
			get
			{
				return this.outer.networker && this.outer.networker.isLocalPlayer;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000CFF1 File Offset: 0x0000B1F1
		protected bool localPlayerAuthority
		{
			get
			{
				return this.outer.networker && this.outer.networker.localPlayerAuthority;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000D017 File Offset: 0x0000B217
		protected bool isAuthority
		{
			get
			{
				return Util.HasEffectiveAuthority(this.outer.networkIdentity);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000D029 File Offset: 0x0000B229
		protected Transform transform
		{
			get
			{
				return this.outer.commonComponents.transform;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000D03B File Offset: 0x0000B23B
		protected CharacterBody characterBody
		{
			get
			{
				return this.outer.commonComponents.characterBody;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000D04D File Offset: 0x0000B24D
		protected CharacterMotor characterMotor
		{
			get
			{
				return this.outer.commonComponents.characterMotor;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000D05F File Offset: 0x0000B25F
		protected CharacterDirection characterDirection
		{
			get
			{
				return this.outer.commonComponents.characterDirection;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000D071 File Offset: 0x0000B271
		protected Rigidbody rigidbody
		{
			get
			{
				return this.outer.commonComponents.rigidbody;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000D083 File Offset: 0x0000B283
		protected RigidbodyMotor rigidbodyMotor
		{
			get
			{
				return this.outer.commonComponents.rigidbodyMotor;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000D095 File Offset: 0x0000B295
		protected RigidbodyDirection rigidbodyDirection
		{
			get
			{
				return this.outer.commonComponents.rigidbodyDirection;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000D0A7 File Offset: 0x0000B2A7
		protected RailMotor railMotor
		{
			get
			{
				return this.outer.commonComponents.railMotor;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		protected ModelLocator modelLocator
		{
			get
			{
				return this.outer.commonComponents.modelLocator;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000D0CB File Offset: 0x0000B2CB
		protected InputBankTest inputBank
		{
			get
			{
				return this.outer.commonComponents.inputBank;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000D0DD File Offset: 0x0000B2DD
		protected TeamComponent teamComponent
		{
			get
			{
				return this.outer.commonComponents.teamComponent;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0000D0EF File Offset: 0x0000B2EF
		protected HealthComponent healthComponent
		{
			get
			{
				return this.outer.commonComponents.healthComponent;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000D101 File Offset: 0x0000B301
		protected SkillLocator skillLocator
		{
			get
			{
				return this.outer.commonComponents.skillLocator;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000D113 File Offset: 0x0000B313
		protected CharacterEmoteDefinitions characterEmoteDefinitions
		{
			get
			{
				return this.outer.commonComponents.characterEmoteDefinitions;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000D125 File Offset: 0x0000B325
		protected CameraTargetParams cameraTargetParams
		{
			get
			{
				return this.outer.commonComponents.cameraTargetParams;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000D137 File Offset: 0x0000B337
		protected SfxLocator sfxLocator
		{
			get
			{
				return this.outer.commonComponents.sfxLocator;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000D149 File Offset: 0x0000B349
		protected BodyAnimatorSmoothingParameters bodyAnimatorSmoothingParameters
		{
			get
			{
				return this.outer.commonComponents.bodyAnimatorSmoothingParameters;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000344 RID: 836 RVA: 0x0000D15B File Offset: 0x0000B35B
		protected ProjectileController projectileController
		{
			get
			{
				return this.outer.commonComponents.projectileController;
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000D16D File Offset: 0x0000B36D
		protected Transform GetModelBaseTransform()
		{
			if (!this.modelLocator)
			{
				return null;
			}
			return this.modelLocator.modelBaseTransform;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000D189 File Offset: 0x0000B389
		protected Transform GetModelTransform()
		{
			if (!this.modelLocator)
			{
				return null;
			}
			return this.modelLocator.modelTransform;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000D1A5 File Offset: 0x0000B3A5
		protected AimAnimator GetAimAnimator()
		{
			if (this.modelLocator && this.modelLocator.modelTransform)
			{
				return this.modelLocator.modelTransform.GetComponent<AimAnimator>();
			}
			return null;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		protected Animator GetModelAnimator()
		{
			if (this.modelLocator && this.modelLocator.modelTransform)
			{
				return this.modelLocator.modelTransform.GetComponent<Animator>();
			}
			return null;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000D20B File Offset: 0x0000B40B
		protected ChildLocator GetModelChildLocator()
		{
			if (this.modelLocator && this.modelLocator.modelTransform)
			{
				return this.modelLocator.modelTransform.GetComponent<ChildLocator>();
			}
			return null;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000D23E File Offset: 0x0000B43E
		protected RootMotionAccumulator GetModelRootMotionAccumulator()
		{
			if (this.modelLocator && this.modelLocator.modelTransform)
			{
				return this.modelLocator.modelTransform.GetComponent<RootMotionAccumulator>();
			}
			return null;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000D274 File Offset: 0x0000B474
		protected void PlayAnimation(string layerName, string animationStateName, string playbackRateParam, float duration)
		{
			if (duration <= 0f)
			{
				Debug.LogWarningFormat("EntityState.PlayAnimation: Zero duration is not allowed. type={0}", new object[]
				{
					base.GetType().Name
				});
				return;
			}
			Animator modelAnimator = this.GetModelAnimator();
			if (modelAnimator)
			{
				EntityState.PlayAnimationOnAnimator(modelAnimator, layerName, animationStateName, playbackRateParam, duration);
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
		protected static void PlayAnimationOnAnimator(Animator modelAnimator, string layerName, string animationStateName, string playbackRateParam, float duration)
		{
			modelAnimator.speed = 1f;
			modelAnimator.Update(0f);
			int layerIndex = modelAnimator.GetLayerIndex(layerName);
			if (layerIndex >= 0)
			{
				modelAnimator.SetFloat(playbackRateParam, 1f);
				modelAnimator.PlayInFixedTime(animationStateName, layerIndex, 0f);
				modelAnimator.Update(0f);
				float length = modelAnimator.GetCurrentAnimatorStateInfo(layerIndex).length;
				modelAnimator.SetFloat(playbackRateParam, length / duration);
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000D334 File Offset: 0x0000B534
		protected void PlayCrossfade(string layerName, string animationStateName, string playbackRateParam, float duration, float crossfadeDuration)
		{
			if (duration <= 0f)
			{
				Debug.LogWarningFormat("EntityState.PlayCrossfade: Zero duration is not allowed. type={0}", new object[]
				{
					base.GetType().Name
				});
				return;
			}
			Animator modelAnimator = this.GetModelAnimator();
			if (modelAnimator)
			{
				modelAnimator.speed = 1f;
				modelAnimator.Update(0f);
				int layerIndex = modelAnimator.GetLayerIndex(layerName);
				modelAnimator.SetFloat(playbackRateParam, 1f);
				modelAnimator.CrossFadeInFixedTime(animationStateName, crossfadeDuration, layerIndex);
				modelAnimator.Update(0f);
				float length = modelAnimator.GetNextAnimatorStateInfo(layerIndex).length;
				modelAnimator.SetFloat(playbackRateParam, length / duration);
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		protected void PlayCrossfade(string layerName, string animationStateName, float crossfadeDuration)
		{
			Animator modelAnimator = this.GetModelAnimator();
			if (modelAnimator)
			{
				modelAnimator.speed = 1f;
				modelAnimator.Update(0f);
				int layerIndex = modelAnimator.GetLayerIndex(layerName);
				modelAnimator.CrossFadeInFixedTime(animationStateName, crossfadeDuration, layerIndex);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000D418 File Offset: 0x0000B618
		public virtual void PlayAnimation(string layerName, string animationStateName)
		{
			Animator modelAnimator = this.GetModelAnimator();
			if (modelAnimator)
			{
				EntityState.PlayAnimationOnAnimator(modelAnimator, layerName, animationStateName);
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000D43C File Offset: 0x0000B63C
		protected static void PlayAnimationOnAnimator(Animator modelAnimator, string layerName, string animationStateName)
		{
			int layerIndex = modelAnimator.GetLayerIndex(layerName);
			modelAnimator.speed = 1f;
			modelAnimator.Update(0f);
			modelAnimator.PlayInFixedTime(animationStateName, layerIndex, 0f);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000D474 File Offset: 0x0000B674
		protected void GetBodyAnimatorSmoothingParameters(out BodyAnimatorSmoothingParameters.SmoothingParameters smoothingParameters)
		{
			if (this.bodyAnimatorSmoothingParameters)
			{
				smoothingParameters = this.bodyAnimatorSmoothingParameters.smoothingParameters;
				return;
			}
			smoothingParameters = BodyAnimatorSmoothingParameters.defaultParameters;
		}

		// Token: 0x04000356 RID: 854
		public EntityStateMachine outer;
	}
}
