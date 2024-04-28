using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Huntress
{
	// Token: 0x0200031A RID: 794
	public class BaseBeginArrowBarrage : BaseState
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x0003CADC File Offset: 0x0003ACDC
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(BaseBeginArrowBarrage.blinkSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
				this.hurtboxGroup = this.modelTransform.GetComponent<HurtBoxGroup>();
			}
			this.prepDuration = this.basePrepDuration / this.attackSpeedStat;
			base.PlayAnimation("FullBody, Override", "BeginArrowRain", "BeginArrowRain.playbackRate", this.prepDuration);
			if (base.characterMotor)
			{
				base.characterMotor.velocity = Vector3.zero;
			}
			if (base.cameraTargetParams)
			{
				this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
			}
			Vector3 direction = base.GetAimRay().direction;
			direction.y = 0f;
			direction.Normalize();
			Vector3 up = Vector3.up;
			this.worldBlinkVector = Matrix4x4.TRS(base.transform.position, Util.QuaternionSafeLookRotation(direction, up), new Vector3(1f, 1f, 1f)).MultiplyPoint3x4(this.blinkVector) - base.transform.position;
			this.worldBlinkVector.Normalize();
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0003CC28 File Offset: 0x0003AE28
		private void CreateBlinkEffect(Vector3 origin)
		{
			EffectData effectData = new EffectData();
			effectData.rotation = Util.QuaternionSafeLookRotation(this.worldBlinkVector);
			effectData.origin = origin;
			EffectManager.SpawnEffect(BaseBeginArrowBarrage.blinkPrefab, effectData, false);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0003CC60 File Offset: 0x0003AE60
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.prepDuration && !this.beginBlink)
			{
				this.beginBlink = true;
				this.CreateBlinkEffect(base.transform.position);
				if (this.characterModel)
				{
					this.characterModel.invisibilityCount++;
				}
				if (this.hurtboxGroup)
				{
					HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
					int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
					hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
				}
			}
			if (this.beginBlink && base.characterMotor)
			{
				base.characterMotor.velocity = Vector3.zero;
				base.characterMotor.rootMotion += this.worldBlinkVector * (base.characterBody.jumpPower * this.jumpCoefficient * Time.fixedDeltaTime);
			}
			if (base.fixedAge >= this.blinkDuration + this.prepDuration && base.isAuthority)
			{
				this.outer.SetNextState(this.InstantiateNextState());
			}
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x00003BE8 File Offset: 0x00001DE8
		protected virtual EntityState InstantiateNextState()
		{
			return null;
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003CD70 File Offset: 0x0003AF70
		public override void OnExit()
		{
			this.CreateBlinkEffect(base.transform.position);
			this.modelTransform = base.GetModelTransform();
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
			if (this.characterModel)
			{
				this.characterModel.invisibilityCount--;
			}
			if (this.hurtboxGroup)
			{
				HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
			CameraTargetParams.AimRequest aimRequest = this.aimRequest;
			if (aimRequest != null)
			{
				aimRequest.Dispose();
			}
			base.OnExit();
		}

		// Token: 0x0400119F RID: 4511
		private Transform modelTransform;

		// Token: 0x040011A0 RID: 4512
		[SerializeField]
		public float basePrepDuration;

		// Token: 0x040011A1 RID: 4513
		[SerializeField]
		public float blinkDuration = 0.3f;

		// Token: 0x040011A2 RID: 4514
		[SerializeField]
		public float jumpCoefficient = 25f;

		// Token: 0x040011A3 RID: 4515
		public static GameObject blinkPrefab;

		// Token: 0x040011A4 RID: 4516
		public static string blinkSoundString;

		// Token: 0x040011A5 RID: 4517
		[SerializeField]
		public Vector3 blinkVector;

		// Token: 0x040011A6 RID: 4518
		private Vector3 worldBlinkVector;

		// Token: 0x040011A7 RID: 4519
		private float prepDuration;

		// Token: 0x040011A8 RID: 4520
		private bool beginBlink;

		// Token: 0x040011A9 RID: 4521
		private CharacterModel characterModel;

		// Token: 0x040011AA RID: 4522
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x040011AB RID: 4523
		protected CameraTargetParams.AimRequest aimRequest;
	}
}
