using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000BE RID: 190
	public class FrozenState : BaseState
	{
		// Token: 0x0600035F RID: 863 RVA: 0x0000D998 File Offset: 0x0000BB98
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.sfxLocator && base.sfxLocator.barkSound != "")
			{
				Util.PlaySound(base.sfxLocator.barkSound, base.gameObject);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				CharacterModel component = modelTransform.GetComponent<CharacterModel>();
				if (component)
				{
					this.temporaryOverlay = base.gameObject.AddComponent<TemporaryOverlay>();
					this.temporaryOverlay.duration = this.freezeDuration;
					this.temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matIsFrozen");
					this.temporaryOverlay.AddToCharacerModel(component);
				}
			}
			this.modelAnimator = base.GetModelAnimator();
			if (this.modelAnimator)
			{
				this.modelAnimator.enabled = false;
				this.duration = this.freezeDuration;
				EffectManager.SpawnEffect(FrozenState.frozenEffectPrefab, new EffectData
				{
					origin = base.characterBody.corePosition,
					scale = (base.characterBody ? base.characterBody.radius : 1f)
				}, false);
			}
			if (base.rigidbody && !base.rigidbody.isKinematic)
			{
				base.rigidbody.velocity = Vector3.zero;
				if (base.rigidbodyMotor)
				{
					base.rigidbodyMotor.moveVector = Vector3.zero;
				}
			}
			base.healthComponent.isInFrozenState = true;
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.characterDirection.forward;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public override void OnExit()
		{
			if (this.modelAnimator)
			{
				this.modelAnimator.enabled = true;
			}
			if (this.temporaryOverlay)
			{
				EntityState.Destroy(this.temporaryOverlay);
			}
			EffectManager.SpawnEffect(FrozenState.frozenEffectPrefab, new EffectData
			{
				origin = base.characterBody.corePosition,
				scale = (base.characterBody ? base.characterBody.radius : 1f)
			}, false);
			base.healthComponent.isInFrozenState = false;
			base.OnExit();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000DBCA File Offset: 0x0000BDCA
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x04000366 RID: 870
		private float duration;

		// Token: 0x04000367 RID: 871
		private Animator modelAnimator;

		// Token: 0x04000368 RID: 872
		private TemporaryOverlay temporaryOverlay;

		// Token: 0x04000369 RID: 873
		public float freezeDuration = 0.35f;

		// Token: 0x0400036A RID: 874
		public static GameObject frozenEffectPrefab;

		// Token: 0x0400036B RID: 875
		public static GameObject executeEffectPrefab;
	}
}
