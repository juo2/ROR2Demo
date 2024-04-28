using System;
using RoR2;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.ImpMonster
{
	// Token: 0x0200030C RID: 780
	public class BlinkState : BaseState
	{
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003B598 File Offset: 0x00039798
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(BlinkState.beginSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform)
			{
				this.animator = this.modelTransform.GetComponent<Animator>();
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
			if (base.characterMotor)
			{
				base.characterMotor.enabled = false;
			}
			Vector3 b = base.inputBank.moveVector * BlinkState.blinkDistance;
			this.blinkDestination = base.transform.position;
			this.blinkStart = base.transform.position;
			NodeGraph groundNodes = SceneInfo.instance.groundNodes;
			NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNode(base.transform.position + b, base.characterBody.hullClassification, float.PositiveInfinity);
			groundNodes.GetNodePosition(nodeIndex, out this.blinkDestination);
			this.blinkDestination += base.transform.position - base.characterBody.footPosition;
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003B718 File Offset: 0x00039918
		private void CreateBlinkEffect(Vector3 origin)
		{
			EffectData effectData = new EffectData();
			effectData.rotation = Util.QuaternionSafeLookRotation(this.blinkDestination - this.blinkStart);
			effectData.origin = origin;
			EffectManager.SpawnEffect(BlinkState.blinkPrefab, effectData, false);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0002837E File Offset: 0x0002657E
		private void SetPosition(Vector3 newPosition)
		{
			if (base.characterMotor)
			{
				base.characterMotor.Motor.SetPositionAndRotation(newPosition, Quaternion.identity, true);
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0003B75C File Offset: 0x0003995C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (base.characterMotor && base.characterDirection)
			{
				base.characterMotor.velocity = Vector3.zero;
			}
			this.SetPosition(Vector3.Lerp(this.blinkStart, this.blinkDestination, this.stopwatch / BlinkState.duration));
			if (this.stopwatch >= BlinkState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0003B7F0 File Offset: 0x000399F0
		public override void OnExit()
		{
			Util.PlaySound(BlinkState.endSoundString, base.gameObject);
			this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject));
			this.modelTransform = base.GetModelTransform();
			if (this.modelTransform && BlinkState.destealthMaterial)
			{
				TemporaryOverlay temporaryOverlay = this.animator.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = 1f;
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = BlinkState.destealthMaterial;
				temporaryOverlay.inspectorCharacterModel = this.animator.gameObject.GetComponent<CharacterModel>();
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.animateShaderAlpha = true;
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
				base.characterMotor.enabled = true;
			}
			this.PlayAnimation("Gesture, Additive", "BlinkEnd");
			base.OnExit();
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001144 RID: 4420
		private Transform modelTransform;

		// Token: 0x04001145 RID: 4421
		public static GameObject blinkPrefab;

		// Token: 0x04001146 RID: 4422
		public static Material destealthMaterial;

		// Token: 0x04001147 RID: 4423
		private float stopwatch;

		// Token: 0x04001148 RID: 4424
		private Vector3 blinkDestination = Vector3.zero;

		// Token: 0x04001149 RID: 4425
		private Vector3 blinkStart = Vector3.zero;

		// Token: 0x0400114A RID: 4426
		public static float duration = 0.3f;

		// Token: 0x0400114B RID: 4427
		public static float blinkDistance = 25f;

		// Token: 0x0400114C RID: 4428
		public static string beginSoundString;

		// Token: 0x0400114D RID: 4429
		public static string endSoundString;

		// Token: 0x0400114E RID: 4430
		private Animator animator;

		// Token: 0x0400114F RID: 4431
		private CharacterModel characterModel;

		// Token: 0x04001150 RID: 4432
		private HurtBoxGroup hurtboxGroup;
	}
}
