using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Bison
{
	// Token: 0x02000458 RID: 1112
	public class SpawnState : BaseState
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x000588D8 File Offset: 0x00056AD8
		public override void OnEnter()
		{
			base.OnEnter();
			Transform modelTransform = base.GetModelTransform();
			EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, SpawnState.spawnEffectMuzzle, false);
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound("Play_bison_idle_graze", base.gameObject);
			Util.PlaySound("Play_bison_charge_attack_collide", base.gameObject);
			if (modelTransform)
			{
				CharacterModel component = modelTransform.GetComponent<CharacterModel>();
				if (component && SpawnState.snowyMaterial)
				{
					TemporaryOverlay temporaryOverlay = component.gameObject.AddComponent<TemporaryOverlay>();
					temporaryOverlay.duration = SpawnState.snowyOverlayDuration;
					temporaryOverlay.destroyComponentOnEnd = true;
					temporaryOverlay.originalMaterial = SpawnState.snowyMaterial;
					temporaryOverlay.inspectorCharacterModel = component;
					temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
					temporaryOverlay.animateShaderAlpha = true;
				}
			}
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000589BB File Offset: 0x00056BBB
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > SpawnState.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0400197A RID: 6522
		public static GameObject spawnEffectPrefab;

		// Token: 0x0400197B RID: 6523
		public static string spawnEffectMuzzle;

		// Token: 0x0400197C RID: 6524
		public static float duration;

		// Token: 0x0400197D RID: 6525
		public static float snowyOverlayDuration;

		// Token: 0x0400197E RID: 6526
		public static Material snowyMaterial;
	}
}
