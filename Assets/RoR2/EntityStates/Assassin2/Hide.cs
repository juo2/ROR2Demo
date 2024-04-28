using System;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace EntityStates.Assassin2
{
	// Token: 0x02000489 RID: 1161
	public class Hide : BaseState
	{
		// Token: 0x060014BF RID: 5311 RVA: 0x0005C2C0 File Offset: 0x0005A4C0
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(Hide.beginSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				this.animator = this.modelTransform.GetComponent<Animator>();
				this.characterModel = this.modelTransform.GetComponent<CharacterModel>();
				this.hurtboxGroup = this.modelTransform.GetComponent<HurtBoxGroup>();
				if (Hide.smokeEffectPrefab)
				{
					Transform transform = this.modelTransform;
					if (transform)
					{
						this.smokeEffectInstance = UnityEngine.Object.Instantiate<GameObject>(Hide.smokeEffectPrefab, transform);
						ScaleParticleSystemDuration component = this.smokeEffectInstance.GetComponent<ScaleParticleSystemDuration>();
						if (component)
						{
							component.newDuration = component.initialDuration;
						}
					}
				}
			}
			if (this.hurtboxGroup)
			{
				HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter + 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
			this.PlayAnimation("Gesture", "Disappear");
			if (base.characterBody && NetworkServer.active)
			{
				base.characterBody.AddBuff(RoR2Content.Buffs.Cloak);
			}
			this.CreateHiddenEffect(Util.GetCorePosition(base.gameObject));
			if (base.healthComponent)
			{
				base.healthComponent.dontShowHealthbar = true;
			}
			this.hidden = true;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0005C404 File Offset: 0x0005A604
		private void CreateHiddenEffect(Vector3 origin)
		{
			EffectData effectData = new EffectData();
			effectData.origin = origin;
			EffectManager.SpawnEffect(Hide.hideEfffectPrefab, effectData, false);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0002837E File Offset: 0x0002657E
		private void SetPosition(Vector3 newPosition)
		{
			if (base.characterMotor)
			{
				base.characterMotor.Motor.SetPositionAndRotation(newPosition, Quaternion.identity, true);
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0005C42C File Offset: 0x0005A62C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= Hide.hiddenDuration && this.hidden)
			{
				this.Reveal();
			}
			if (base.isAuthority && this.stopwatch > Hide.fullDuration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0005C48C File Offset: 0x0005A68C
		private void Reveal()
		{
			Util.PlaySound(Hide.endSoundString, base.gameObject);
			this.CreateHiddenEffect(Util.GetCorePosition(base.gameObject));
			if (this.modelTransform && Hide.destealthMaterial)
			{
				TemporaryOverlay temporaryOverlay = this.animator.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = 1f;
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = Hide.destealthMaterial;
				temporaryOverlay.inspectorCharacterModel = this.animator.gameObject.GetComponent<CharacterModel>();
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.animateShaderAlpha = true;
			}
			if (this.hurtboxGroup)
			{
				HurtBoxGroup hurtBoxGroup = this.hurtboxGroup;
				int hurtBoxesDeactivatorCounter = hurtBoxGroup.hurtBoxesDeactivatorCounter - 1;
				hurtBoxGroup.hurtBoxesDeactivatorCounter = hurtBoxesDeactivatorCounter;
			}
			if (base.characterMotor)
			{
				base.characterMotor.enabled = true;
			}
			this.PlayAnimation("Gesture", "Appear");
			if (base.characterBody && NetworkServer.active)
			{
				base.characterBody.RemoveBuff(RoR2Content.Buffs.Cloak);
			}
			if (base.healthComponent)
			{
				base.healthComponent.dontShowHealthbar = false;
			}
			if (this.smokeEffectInstance)
			{
				EntityState.Destroy(this.smokeEffectInstance);
			}
			this.hidden = false;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001A8B RID: 6795
		private Transform modelTransform;

		// Token: 0x04001A8C RID: 6796
		public static GameObject hideEfffectPrefab;

		// Token: 0x04001A8D RID: 6797
		public static GameObject smokeEffectPrefab;

		// Token: 0x04001A8E RID: 6798
		public static Material destealthMaterial;

		// Token: 0x04001A8F RID: 6799
		private float stopwatch;

		// Token: 0x04001A90 RID: 6800
		private Vector3 blinkDestination = Vector3.zero;

		// Token: 0x04001A91 RID: 6801
		private Vector3 blinkStart = Vector3.zero;

		// Token: 0x04001A92 RID: 6802
		[Tooltip("the length of time to stay hidden")]
		public static float hiddenDuration = 5f;

		// Token: 0x04001A93 RID: 6803
		[Tooltip("the entire duration of the hidden state (hidden time + time after)")]
		public static float fullDuration = 10f;

		// Token: 0x04001A94 RID: 6804
		public static string beginSoundString;

		// Token: 0x04001A95 RID: 6805
		public static string endSoundString;

		// Token: 0x04001A96 RID: 6806
		private Animator animator;

		// Token: 0x04001A97 RID: 6807
		private CharacterModel characterModel;

		// Token: 0x04001A98 RID: 6808
		private HurtBoxGroup hurtboxGroup;

		// Token: 0x04001A99 RID: 6809
		private bool hidden;

		// Token: 0x04001A9A RID: 6810
		private GameObject smokeEffectInstance;
	}
}
