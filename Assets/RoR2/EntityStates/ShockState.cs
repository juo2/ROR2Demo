using System;
using RoR2;
using UnityEngine;

namespace EntityStates
{
	// Token: 0x020000D4 RID: 212
	public class ShockState : BaseState
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0000FD74 File Offset: 0x0000DF74
		public override void OnEnter()
		{
			base.OnEnter();
			this.animator = base.GetModelAnimator();
			if (base.sfxLocator && base.sfxLocator.barkSound != "")
			{
				Util.PlaySound(base.sfxLocator.barkSound, base.gameObject);
			}
			Util.PlaySound(ShockState.enterSoundString, base.gameObject);
			this.PlayAnimation("Body", (UnityEngine.Random.Range(0, 2) == 0) ? "Hurt1" : "Hurt2");
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				CharacterModel component = modelTransform.GetComponent<CharacterModel>();
				if (component)
				{
					this.temporaryOverlay = base.gameObject.AddComponent<TemporaryOverlay>();
					this.temporaryOverlay.duration = this.shockDuration;
					this.temporaryOverlay.originalMaterial = LegacyResourcesAPI.Load<Material>("Materials/matIsShocked");
					this.temporaryOverlay.AddToCharacerModel(component);
				}
			}
			this.stunVfxInstance = UnityEngine.Object.Instantiate<GameObject>(ShockState.stunVfxPrefab, base.transform);
			this.stunVfxInstance.GetComponent<ScaleParticleSystemDuration>().newDuration = this.shockDuration;
			if (base.characterBody.healthComponent)
			{
				this.healthFraction = base.characterBody.healthComponent.combinedHealthFraction;
			}
			if (base.characterBody)
			{
				base.characterBody.isSprinting = false;
			}
			if (base.characterDirection)
			{
				base.characterDirection.moveVector = base.characterDirection.forward;
			}
			if (base.rigidbodyMotor)
			{
				base.rigidbodyMotor.moveVector = Vector3.zero;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000FF10 File Offset: 0x0000E110
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.shockTimer -= Time.fixedDeltaTime;
			float combinedHealthFraction = base.characterBody.healthComponent.combinedHealthFraction;
			if (this.shockTimer <= 0f)
			{
				this.shockTimer += ShockState.shockInterval;
				this.PlayShockAnimation();
			}
			if (base.fixedAge > this.shockDuration || this.healthFraction - combinedHealthFraction > ShockState.healthFractionToForceExit)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000FF94 File Offset: 0x0000E194
		public override void OnExit()
		{
			if (this.temporaryOverlay)
			{
				EntityState.Destroy(this.temporaryOverlay);
			}
			if (this.stunVfxInstance)
			{
				EntityState.Destroy(this.stunVfxInstance);
			}
			Util.PlaySound(ShockState.exitSoundString, base.gameObject);
			base.OnExit();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000FFE8 File Offset: 0x0000E1E8
		private void PlayShockAnimation()
		{
			string layerName = "Flinch";
			int layerIndex = this.animator.GetLayerIndex(layerName);
			if (layerIndex >= 0)
			{
				this.animator.SetLayerWeight(layerIndex, ShockState.shockStrength);
				this.animator.Play("FlinchStart", layerIndex);
			}
		}

		// Token: 0x040003D7 RID: 983
		public static GameObject stunVfxPrefab;

		// Token: 0x040003D8 RID: 984
		public float shockDuration = 1f;

		// Token: 0x040003D9 RID: 985
		public static float shockInterval = 0.1f;

		// Token: 0x040003DA RID: 986
		public static float shockStrength = 1f;

		// Token: 0x040003DB RID: 987
		public static float healthFractionToForceExit = 0.1f;

		// Token: 0x040003DC RID: 988
		public static string enterSoundString;

		// Token: 0x040003DD RID: 989
		public static string exitSoundString;

		// Token: 0x040003DE RID: 990
		private float shockTimer;

		// Token: 0x040003DF RID: 991
		private Animator animator;

		// Token: 0x040003E0 RID: 992
		private TemporaryOverlay temporaryOverlay;

		// Token: 0x040003E1 RID: 993
		private float healthFraction;

		// Token: 0x040003E2 RID: 994
		private GameObject stunVfxInstance;
	}
}
