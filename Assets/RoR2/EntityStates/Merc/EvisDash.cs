using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Merc
{
	// Token: 0x02000278 RID: 632
	public class EvisDash : BaseState
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x0002DE2C File Offset: 0x0002C02C
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(EvisDash.beginSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
			if (this.modelTransform)
			{
				this.animator = this.modelTransform.GetComponent<Animator>();
				this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
			}
			if (base.isAuthority)
			{
				base.SmallHop(base.characterMotor, EvisDash.smallHopVelocity);
			}
			if (NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
			base.PlayAnimation("FullBody, Override", "EvisPrep", "EvisPrep.playbackRate", EvisDash.dashPrepDuration);
			this.dashVector = base.inputBank.aimDirection;
			base.characterDirection.forward = this.dashVector;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002DF18 File Offset: 0x0002C118
		private void CreateBlinkEffect(Vector3 origin)
		{
			EffectData effectData = new EffectData();
			effectData.rotation = Util.QuaternionSafeLookRotation(this.dashVector);
			effectData.origin = origin;
			EffectManager.SpawnEffect(EvisDash.blinkPrefab, effectData, false);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002DF50 File Offset: 0x0002C150
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch > EvisDash.dashPrepDuration && !this.isDashing)
			{
				this.isDashing = true;
				this.dashVector = base.inputBank.aimDirection;
				this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
				base.PlayCrossfade("FullBody, Override", "EvisLoop", 0.1f);
				if (this.modelTransform)
				{
					TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = 0.6f;
					temporaryOverlay.animateShaderAlpha = true;
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashBright");
					temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
					TemporaryOverlay temporaryOverlay2 = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay2.duration = 0.7f;
					temporaryOverlay2.animateShaderAlpha = true;
					temporaryOverlay2.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay2.destroyComponentOnEnd = true;
					temporaryOverlay2.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matHuntressFlashExpanded");
					temporaryOverlay2.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
				}
			}
			bool flag = this.stopwatch >= EvisDash.dashDuration + EvisDash.dashPrepDuration;
			if (this.isDashing)
			{
				if (base.characterMotor && base.characterDirection)
				{
					base.characterMotor.rootMotion += this.dashVector * (this.moveSpeedStat * EvisDash.speedCoefficient * Time.fixedDeltaTime);
				}
				if (base.isAuthority)
				{
					Collider[] array = Physics.OverlapSphere(base.transform.position, base.characterBody.radius + EvisDash.overlapSphereRadius * (flag ? EvisDash.lollypopFactor : 1f), LayerIndex.entityPrecise.mask);
					for (int i = 0; i < array.Length; i++)
					{
						HurtBox component = array[i].GetComponent<HurtBox>();
						if (component && component.healthComponent != base.healthComponent)
						{
							Evis nextState = new Evis();
							this.outer.SetNextState(nextState);
							return;
						}
					}
				}
			}
			if (flag && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002E1CC File Offset: 0x0002C3CC
		public override void OnExit()
		{
			Util.PlaySound(EvisDash.endSoundString, base.gameObject);
			base.characterMotor.velocity *= 0.1f;
			base.SmallHop(base.characterMotor, EvisDash.smallHopVelocity);
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			this.PlayAnimation("FullBody, Override", "EvisLoopExit");
			if (NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
			}
			base.OnExit();
		}

		// Token: 0x04000CD2 RID: 3282
		private Transform modelTransform;

		// Token: 0x04000CD3 RID: 3283
		public static GameObject blinkPrefab;

		// Token: 0x04000CD4 RID: 3284
		private float stopwatch;

		// Token: 0x04000CD5 RID: 3285
		private Vector3 dashVector = Vector3.zero;

		// Token: 0x04000CD6 RID: 3286
		public static float smallHopVelocity;

		// Token: 0x04000CD7 RID: 3287
		public static float dashPrepDuration;

		// Token: 0x04000CD8 RID: 3288
		public static float dashDuration = 0.3f;

		// Token: 0x04000CD9 RID: 3289
		public static float speedCoefficient = 25f;

		// Token: 0x04000CDA RID: 3290
		public static string beginSoundString;

		// Token: 0x04000CDB RID: 3291
		public static string endSoundString;

		// Token: 0x04000CDC RID: 3292
		public static float overlapSphereRadius;

		// Token: 0x04000CDD RID: 3293
		public static float lollypopFactor;

		// Token: 0x04000CDE RID: 3294
		private Animator animator;

		// Token: 0x04000CDF RID: 3295
		private CharacterModel characterModel;

		// Token: 0x04000CE0 RID: 3296
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x04000CE1 RID: 3297
		private bool isDashing;

		// Token: 0x04000CE2 RID: 3298
		private CameraTargetParams.AimRequest aimRequest;
	}
}
