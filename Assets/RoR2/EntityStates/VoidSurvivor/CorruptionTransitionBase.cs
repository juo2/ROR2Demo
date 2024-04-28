using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor
{
	// Token: 0x020000E9 RID: 233
	public class CorruptionTransitionBase : BaseState
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x00011488 File Offset: 0x0000F688
		public override void OnEnter()
		{
			base.OnEnter();
			this.voidSurvivorController = base.GetComponent<VoidSurvivorController>();
			base.PlayCrossfade(this.animationLayerName, base.characterMotor.isGrounded ? this.animationGroundStateName : this.animationAirStateName, this.animationPlaybackParameterName, this.duration, this.animationCrossfadeDuration);
			Util.PlaySound(this.entrySound, base.gameObject);
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
			this.StartCameraParamsOverride(this.duration);
			Transform transform = base.FindModelChild(this.effectmuzzle);
			if (transform && this.chargeEffectPrefab)
			{
				this.chargeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(this.chargeEffectPrefab, transform.position, transform.rotation);
				this.chargeEffectInstance.transform.parent = transform;
				ScaleParticleSystemDuration component = this.chargeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
				if (component)
				{
					component.newDuration = this.duration;
				}
			}
			if (base.isAuthority && this.voidSurvivorController)
			{
				this.voidSurvivorController.weaponStateMachine.SetNextStateToMain();
			}
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000115AC File Offset: 0x0000F7AC
		public override void OnExit()
		{
			this.EndCameraParamsOverride(0f);
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
			if (this.chargeEffectInstance)
			{
				EntityState.Destroy(this.chargeEffectInstance);
			}
			base.OnExit();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000115FC File Offset: 0x0000F7FC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				base.characterMotor.velocity -= base.characterMotor.velocity * this.dampingCoefficient;
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.OnFinishAuthority();
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0001166A File Offset: 0x0000F86A
		public virtual void OnFinishAuthority()
		{
			EffectManager.SimpleMuzzleFlash(this.completionEffectPrefab, base.gameObject, this.effectmuzzle, true);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00011684 File Offset: 0x0000F884
		protected void StartCameraParamsOverride(float transitionDuration)
		{
			if (this.cameraParamsOverrideHandle.isValid)
			{
				return;
			}
			this.cameraParamsOverrideHandle = base.cameraTargetParams.AddParamsOverride(new CameraTargetParams.CameraParamsOverrideRequest
			{
				cameraParamsData = this.cameraParams.data
			}, transitionDuration);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000116CC File Offset: 0x0000F8CC
		protected void EndCameraParamsOverride(float transitionDuration)
		{
			if (this.cameraParamsOverrideHandle.isValid)
			{
				base.cameraTargetParams.RemoveParamsOverride(this.cameraParamsOverrideHandle, transitionDuration);
				this.cameraParamsOverrideHandle = default(CameraTargetParams.CameraParamsOverrideHandle);
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x04000437 RID: 1079
		[SerializeField]
		public float duration;

		// Token: 0x04000438 RID: 1080
		[SerializeField]
		public string animationLayerName;

		// Token: 0x04000439 RID: 1081
		[SerializeField]
		public string animationGroundStateName;

		// Token: 0x0400043A RID: 1082
		[SerializeField]
		public string animationAirStateName;

		// Token: 0x0400043B RID: 1083
		[SerializeField]
		public string animationPlaybackParameterName;

		// Token: 0x0400043C RID: 1084
		[SerializeField]
		public float animationCrossfadeDuration;

		// Token: 0x0400043D RID: 1085
		[SerializeField]
		public string entrySound;

		// Token: 0x0400043E RID: 1086
		[SerializeField]
		public GameObject chargeEffectPrefab;

		// Token: 0x0400043F RID: 1087
		[SerializeField]
		public GameObject completionEffectPrefab;

		// Token: 0x04000440 RID: 1088
		[SerializeField]
		public string effectmuzzle;

		// Token: 0x04000441 RID: 1089
		[SerializeField]
		public CharacterCameraParams cameraParams;

		// Token: 0x04000442 RID: 1090
		[SerializeField]
		public float dampingCoefficient;

		// Token: 0x04000443 RID: 1091
		protected VoidSurvivorController voidSurvivorController;

		// Token: 0x04000444 RID: 1092
		private GameObject chargeEffectInstance;

		// Token: 0x04000445 RID: 1093
		private CameraTargetParams.CameraParamsOverrideHandle cameraParamsOverrideHandle;
	}
}
