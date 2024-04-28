using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates
{
	// Token: 0x020000C0 RID: 192
	public class GenericCharacterDeath : BaseState
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000DF12 File Offset: 0x0000C112
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000DF1A File Offset: 0x0000C11A
		private protected Transform cachedModelTransform { protected get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000DF23 File Offset: 0x0000C123
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000DF2B File Offset: 0x0000C12B
		private protected bool isBrittle { protected get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000DF34 File Offset: 0x0000C134
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000DF3C File Offset: 0x0000C13C
		private protected bool isVoidDeath { protected get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000DF45 File Offset: 0x0000C145
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000DF4D File Offset: 0x0000C14D
		private protected bool isPlayerDeath { protected get; private set; }

		// Token: 0x06000377 RID: 887 RVA: 0x0000DF56 File Offset: 0x0000C156
		protected virtual float GetDeathAnimationCrossFadeDuration()
		{
			return 0.1f;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000DF60 File Offset: 0x0000C160
		public override void OnEnter()
		{
			base.OnEnter();
			this.bodyMarkedForDestructionServer = false;
			this.cachedModelTransform = (base.modelLocator ? base.modelLocator.modelTransform : null);
			this.isBrittle = (base.characterBody && base.characterBody.isGlass);
			this.isVoidDeath = (base.healthComponent && (base.healthComponent.killingDamageType & DamageType.VoidDeath) > DamageType.Generic);
			this.isPlayerDeath = (base.characterBody.master && base.characterBody.master.GetComponent<PlayerCharacterMasterController>() != null);
			if (this.isVoidDeath)
			{
				if (base.characterBody && base.isAuthority)
				{
					EffectManager.SpawnEffect(GenericCharacterDeath.voidDeathEffect, new EffectData
					{
						origin = base.characterBody.corePosition,
						scale = base.characterBody.bestFitRadius
					}, true);
				}
				if (this.cachedModelTransform)
				{
					EntityState.Destroy(this.cachedModelTransform.gameObject);
					this.cachedModelTransform = null;
				}
			}
			if (this.isPlayerDeath && base.characterBody)
			{
				UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/TemporaryVisualEffects/PlayerDeathEffect"), base.characterBody.corePosition, Quaternion.identity).GetComponent<LocalCameraEffect>().targetCharacter = base.characterBody.gameObject;
			}
			if (this.cachedModelTransform)
			{
				if (this.isBrittle)
				{
					TemporaryOverlay temporaryOverlay = this.cachedModelTransform.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = 0.5f;
					temporaryOverlay.destroyObjectOnEnd = true;
					temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matShatteredGlass");
					temporaryOverlay.destroyEffectPrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BrittleDeath");
					temporaryOverlay.destroyEffectChildString = "Chest";
					temporaryOverlay.inspectorCharacterModel = this.cachedModelTransform.gameObject.GetComponent<CharacterModel>();
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
					temporaryOverlay.animateShaderAlpha = true;
				}
				if (base.cameraTargetParams)
				{
					ChildLocator component = this.cachedModelTransform.GetComponent<ChildLocator>();
					if (component)
					{
						Transform transform = component.FindChild("Chest");
						if (transform)
						{
							base.cameraTargetParams.cameraPivotTransform = transform;
							this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
							base.cameraTargetParams.dontRaycastToPivot = true;
						}
					}
				}
			}
			if (!this.isVoidDeath)
			{
				this.PlayDeathSound();
				this.PlayDeathAnimation(0.1f);
				this.CreateDeathEffects();
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		protected virtual void PlayDeathSound()
		{
			if (base.sfxLocator && base.sfxLocator.deathSound != "")
			{
				Util.PlaySound(base.sfxLocator.deathSound, base.gameObject);
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000E234 File Offset: 0x0000C434
		protected virtual void PlayDeathAnimation(float crossfadeDuration = 0.1f)
		{
			Animator modelAnimator = base.GetModelAnimator();
			if (modelAnimator)
			{
				modelAnimator.CrossFadeInFixedTime("Death", crossfadeDuration);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void CreateDeathEffects()
		{
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000E25C File Offset: 0x0000C45C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (NetworkServer.active)
			{
				bool flag = false;
				bool flag2 = true;
				if (base.characterMotor)
				{
					flag = base.characterMotor.isGrounded;
					flag2 = base.characterMotor.atRest;
				}
				else if (base.rigidbodyMotor)
				{
					flag = false;
					flag2 = false;
				}
				this.fallingStopwatch = (flag ? 0f : (this.fallingStopwatch + Time.fixedDeltaTime));
				this.restStopwatch = ((!flag2) ? 0f : (this.restStopwatch + Time.fixedDeltaTime));
				if (base.fixedAge >= GenericCharacterDeath.minTimeToKeepBodyForNetworkMessages)
				{
					if (this.bodyMarkedForDestructionServer)
					{
						this.OnPreDestroyBodyServer();
						EntityState.Destroy(base.gameObject);
						return;
					}
					if ((this.restStopwatch >= GenericCharacterDeath.bodyPreservationDuration || this.fallingStopwatch >= GenericCharacterDeath.maxFallDuration || base.fixedAge > GenericCharacterDeath.hardCutoffDuration) && this.shouldAutoDestroy)
					{
						this.DestroyBodyAsapServer();
						return;
					}
				}
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000E34A File Offset: 0x0000C54A
		protected void DestroyBodyAsapServer()
		{
			this.bodyMarkedForDestructionServer = true;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000B4B7 File Offset: 0x000096B7
		protected virtual bool shouldAutoDestroy
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000026ED File Offset: 0x000008ED
		protected virtual void OnPreDestroyBodyServer()
		{
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000E353 File Offset: 0x0000C553
		protected void DestroyModel()
		{
			if (this.cachedModelTransform)
			{
				EntityState.Destroy(this.cachedModelTransform.gameObject);
				this.cachedModelTransform = null;
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000E379 File Offset: 0x0000C579
		public override void OnExit()
		{
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			if (this.shouldAutoDestroy && this.fallingStopwatch >= GenericCharacterDeath.maxFallDuration)
			{
				this.DestroyModel();
			}
			base.OnExit();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000382 RID: 898
		private static readonly float bodyPreservationDuration = 1f;

		// Token: 0x04000383 RID: 899
		private static readonly float hardCutoffDuration = 10f;

		// Token: 0x04000384 RID: 900
		private static readonly float maxFallDuration = 4f;

		// Token: 0x04000385 RID: 901
		private static readonly float minTimeToKeepBodyForNetworkMessages = 0.5f;

		// Token: 0x04000386 RID: 902
		public static GameObject voidDeathEffect;

		// Token: 0x04000387 RID: 903
		private float restStopwatch;

		// Token: 0x04000388 RID: 904
		private float fallingStopwatch;

		// Token: 0x04000389 RID: 905
		private bool bodyMarkedForDestructionServer;

		// Token: 0x0400038A RID: 906
		private CameraTargetParams.AimRequest aimRequest;
	}
}
