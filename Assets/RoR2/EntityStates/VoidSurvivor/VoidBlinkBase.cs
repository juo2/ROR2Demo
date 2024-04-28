using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.VoidSurvivor
{
	// Token: 0x020000EC RID: 236
	public class VoidBlinkBase : BaseState
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x000117D4 File Offset: 0x0000F9D4
		public override void OnEnter()
		{
			base.OnEnter();
			this.soundID = Util.PlaySound(this.beginSoundString, base.gameObject);
			this.forwardVector = ((base.inputBank.moveVector == Vector3.zero) ? base.characterDirection.forward : base.inputBank.moveVector).normalized;
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
			if (NetworkServer.active)
			{
				Util.CleanseBody(base.characterBody, true, false, false, true, true, false);
			}
			this.blinkVfxInstance = UnityEngine.Object.Instantiate<GameObject>(this.blinkVfxPrefab);
			this.blinkVfxInstance.transform.SetParent(base.transform, false);
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00011909 File Offset: 0x0000FB09
		protected virtual Vector3 GetBlinkVector()
		{
			return base.inputBank.aimDirection;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00011918 File Offset: 0x0000FB18
		private void CreateBlinkEffect(Vector3 origin)
		{
			EffectData effectData = new EffectData();
			effectData.rotation = Util.QuaternionSafeLookRotation(this.GetVelocity());
			effectData.origin = origin;
			EffectManager.SpawnEffect(this.blinkEffectPrefab, effectData, false);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00011950 File Offset: 0x0000FB50
		private Vector3 GetVelocity()
		{
			float time = base.fixedAge / this.duration;
			Vector3 a = this.forwardSpeed.Evaluate(time) * this.forwardVector;
			Vector3 b = this.upSpeed.Evaluate(time) * Vector3.up;
			return (a + b) * this.speedCoefficient * this.moveSpeedStat;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000119B8 File Offset: 0x0000FBB8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.characterMotor && base.characterDirection)
			{
				if (base.characterMotor)
				{
					base.characterMotor.Motor.ForceUnground();
				}
				Vector3 velocity = this.GetVelocity();
				base.characterMotor.velocity = velocity;
				if (this.blinkVfxInstance)
				{
					this.blinkVfxInstance.transform.forward = velocity;
				}
			}
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00011A54 File Offset: 0x0000FC54
		public override void OnExit()
		{
			AkSoundEngine.StopPlayingID(this.soundID);
			if (!this.outer.destroying)
			{
				Util.PlaySound(this.endSoundString, base.gameObject);
				this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
			}
			if (this.blinkVfxInstance)
			{
				VfxKillBehavior.KillVfxObject(this.blinkVfxInstance);
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
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = this.overlayDuration;
				temporaryOverlay.animateShaderAlpha = true;
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = this.overlayMaterial;
				temporaryOverlay.AddToCharacerModel(this.modelTransform.GetComponent<CharacterModel>());
			}
			base.OnExit();
		}

		// Token: 0x04000447 RID: 1095
		private Transform modelTransform;

		// Token: 0x04000448 RID: 1096
		[SerializeField]
		public GameObject blinkEffectPrefab;

		// Token: 0x04000449 RID: 1097
		[SerializeField]
		public float duration = 0.3f;

		// Token: 0x0400044A RID: 1098
		[SerializeField]
		public float speedCoefficient = 25f;

		// Token: 0x0400044B RID: 1099
		[SerializeField]
		public string beginSoundString;

		// Token: 0x0400044C RID: 1100
		[SerializeField]
		public string endSoundString;

		// Token: 0x0400044D RID: 1101
		[SerializeField]
		public AnimationCurve forwardSpeed;

		// Token: 0x0400044E RID: 1102
		[SerializeField]
		public AnimationCurve upSpeed;

		// Token: 0x0400044F RID: 1103
		[SerializeField]
		public GameObject blinkVfxPrefab;

		// Token: 0x04000450 RID: 1104
		[SerializeField]
		public float overlayDuration;

		// Token: 0x04000451 RID: 1105
		[SerializeField]
		public Material overlayMaterial;

		// Token: 0x04000452 RID: 1106
		private CharacterModel characterModel;

		// Token: 0x04000453 RID: 1107
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x04000454 RID: 1108
		private Vector3 forwardVector;

		// Token: 0x04000455 RID: 1109
		private GameObject blinkVfxInstance;

		// Token: 0x04000456 RID: 1110
		private uint soundID;

		// Token: 0x020000ED RID: 237
		public class VoidBlinkUp : VoidBlinkBase
		{
		}

		// Token: 0x020000EE RID: 238
		public class VoidBlinkDown : VoidBlinkBase
		{
		}
	}
}
