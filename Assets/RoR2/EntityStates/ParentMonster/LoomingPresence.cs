using System;
using RoR2;
using RoR2.Navigation;
using UnityEngine;

namespace EntityStates.ParentMonster
{
	// Token: 0x0200022A RID: 554
	public class LoomingPresence : BaseState
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x000281C0 File Offset: 0x000263C0
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(LoomingPresence.beginSoundString, base.gameObject);
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
			if (base.characterMotor)
			{
				base.characterMotor.enabled = false;
			}
			if (base.isAuthority)
			{
				Vector3 vector = base.inputBank.aimDirection * LoomingPresence.blinkDistance;
				this.blinkDestination = base.transform.position;
				this.blinkStart = base.transform.position;
				NodeGraph groundNodes = SceneInfo.instance.groundNodes;
				NodeGraph.NodeIndex nodeIndex = groundNodes.FindClosestNode(base.transform.position + vector, base.characterBody.hullClassification, float.PositiveInfinity);
				groundNodes.GetNodePosition(nodeIndex, out this.blinkDestination);
				this.blinkDestination += base.transform.position - base.characterBody.footPosition;
				vector = this.blinkDestination - this.blinkStart;
				this.CreateBlinkEffect(Util.GetCorePosition(base.gameObject), vector);
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002834C File Offset: 0x0002654C
		private void CreateBlinkEffect(Vector3 origin, Vector3 direction)
		{
			EffectData effectData = new EffectData();
			effectData.rotation = Util.QuaternionSafeLookRotation(direction);
			effectData.origin = origin;
			EffectManager.SpawnEffect(LoomingPresence.blinkPrefab, effectData, true);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002837E File Offset: 0x0002657E
		private void SetPosition(Vector3 newPosition)
		{
			if (base.characterMotor)
			{
				base.characterMotor.Motor.SetPositionAndRotation(newPosition, Quaternion.identity, true);
			}
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x000283A4 File Offset: 0x000265A4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (base.characterMotor && base.characterDirection)
			{
				base.characterMotor.velocity = Vector3.zero;
			}
			this.SetPosition(Vector3.Lerp(this.blinkStart, this.blinkDestination, this.stopwatch / LoomingPresence.duration));
			if (this.stopwatch >= LoomingPresence.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00028438 File Offset: 0x00026638
		public override void OnExit()
		{
			Util.PlaySound(LoomingPresence.endSoundString, base.gameObject);
			this.modelTransform = base.GetModelTransform();
			if (base.characterDirection)
			{
				base.characterDirection.forward = base.GetAimRay().direction;
			}
			if (this.modelTransform && LoomingPresence.destealthMaterial)
			{
				TemporaryOverlay temporaryOverlay = this.modelTransform.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = LoomingPresence.destealthDuration;
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = LoomingPresence.destealthMaterial;
				temporaryOverlay.inspectorCharacterModel = this.modelTransform.gameObject.GetComponent<CharacterModel>();
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
			base.OnExit();
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000B45 RID: 2885
		private Transform modelTransform;

		// Token: 0x04000B46 RID: 2886
		public static GameObject blinkPrefab;

		// Token: 0x04000B47 RID: 2887
		public static Material destealthMaterial;

		// Token: 0x04000B48 RID: 2888
		private float stopwatch;

		// Token: 0x04000B49 RID: 2889
		private Vector3 blinkDestination = Vector3.zero;

		// Token: 0x04000B4A RID: 2890
		private Vector3 blinkStart = Vector3.zero;

		// Token: 0x04000B4B RID: 2891
		public static float duration = 0.3f;

		// Token: 0x04000B4C RID: 2892
		public static float blinkDistance = 25f;

		// Token: 0x04000B4D RID: 2893
		public static string beginSoundString;

		// Token: 0x04000B4E RID: 2894
		public static string endSoundString;

		// Token: 0x04000B4F RID: 2895
		public static float destealthDuration;

		// Token: 0x04000B50 RID: 2896
		private CharacterModel characterModel;

		// Token: 0x04000B51 RID: 2897
		private HurtBoxGroup hurtboxGroup;
	}
}
