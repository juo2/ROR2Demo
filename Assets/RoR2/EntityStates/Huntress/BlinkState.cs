using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Huntress
{
	// Token: 0x0200031B RID: 795
	public class BlinkState : BaseState
	{
		// Token: 0x06000E35 RID: 3637 RVA: 0x0003CEF4 File Offset: 0x0003B0F4
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(this.beginSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
				this.hurtboxGroup = this.modelTransform.GetComponent<HurtBoxGroup>();
			}
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
			this.blinkVector = this.GetBlinkVector();
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00011909 File Offset: 0x0000FB09
		protected virtual Vector3 GetBlinkVector()
		{
			return base.inputBank.aimDirection;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0003CFB4 File Offset: 0x0003B1B4
		private void CreateBlinkEffect(Vector3 origin)
		{
			EffectData effectData = new EffectData();
			effectData.rotation = Util.QuaternionSafeLookRotation(this.blinkVector);
			effectData.origin = origin;
			EffectManager.SpawnEffect(BlinkState.blinkPrefab, effectData, false);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0003CFEC File Offset: 0x0003B1EC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (base.characterMotor && base.characterDirection)
			{
				base.characterMotor.velocity = Vector3.zero;
				base.characterMotor.rootMotion += this.blinkVector * (this.moveSpeedStat * this.speedCoefficient * Time.fixedDeltaTime);
			}
			if (this.stopwatch >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0003D090 File Offset: 0x0003B290
		public override void OnExit()
		{
			if (!this.outer.destroying)
			{
				Util.PlaySound(this.endSoundString, base.gameObject);
				this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
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
			if (base.characterMotor)
			{
				base.characterMotor.disableAirControlUntilCollision = false;
			}
			base.OnExit();
		}

		// Token: 0x040011AC RID: 4524
		private Transform modelTransform;

		// Token: 0x040011AD RID: 4525
		public static GameObject blinkPrefab;

		// Token: 0x040011AE RID: 4526
		private float stopwatch;

		// Token: 0x040011AF RID: 4527
		private Vector3 blinkVector = Vector3.zero;

		// Token: 0x040011B0 RID: 4528
		[SerializeField]
		public float duration = 0.3f;

		// Token: 0x040011B1 RID: 4529
		[SerializeField]
		public float speedCoefficient = 25f;

		// Token: 0x040011B2 RID: 4530
		[SerializeField]
		public string beginSoundString;

		// Token: 0x040011B3 RID: 4531
		[SerializeField]
		public string endSoundString;

		// Token: 0x040011B4 RID: 4532
		private CharacterModel characterModel;

		// Token: 0x040011B5 RID: 4533
		private HurtBoxGroup hurtboxGroup;
	}
}
