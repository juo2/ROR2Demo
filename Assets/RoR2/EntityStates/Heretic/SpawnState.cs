using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Heretic
{
	// Token: 0x0200032B RID: 811
	public class SpawnState : BaseState
	{
		// Token: 0x06000E8E RID: 3726 RVA: 0x0003EE48 File Offset: 0x0003D048
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			EffectManager.SimpleEffect(SpawnState.effectPrefab, base.characterBody.corePosition, Quaternion.identity, false);
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				CharacterModel component = modelTransform.GetComponent<CharacterModel>();
				if (component && SpawnState.overlayMaterial)
				{
					TemporaryOverlay temporaryOverlay = component.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = SpawnState.overlayDuration;
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = SpawnState.overlayMaterial;
					temporaryOverlay.inspectorCharacterModel = component;
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.animateShaderAlpha = true;
				}
			}
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x0003EF1F File Offset: 0x0003D11F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001247 RID: 4679
		public static float duration = 4f;

		// Token: 0x04001248 RID: 4680
		public static string spawnSoundString;

		// Token: 0x04001249 RID: 4681
		public static GameObject effectPrefab;

		// Token: 0x0400124A RID: 4682
		public static Material overlayMaterial;

		// Token: 0x0400124B RID: 4683
		public static float overlayDuration;
	}
}
