using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ImpBossMonster
{
	// Token: 0x02000309 RID: 777
	public class SpawnState : BaseState
	{
		// Token: 0x06000DDB RID: 3547 RVA: 0x0003B22C File Offset: 0x0003942C
		public override void OnEnter()
		{
			base.OnEnter();
			Animator modelAnimator = base.GetModelAnimator();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			if (SpawnState.spawnEffectPrefab)
			{
				EffectData effectData = new EffectData();
				effectData.origin = base.transform.position;
				EffectManager.SpawnEffect(SpawnState.spawnEffectPrefab, effectData, false);
			}
			if (SpawnState.destealthMaterial)
			{
				TemporaryOverlay temporaryOverlay = modelAnimator.gameObject.AddComponent<TemporaryOverlay>();
				temporaryOverlay.duration = 1f;
				temporaryOverlay.destroyComponentOnEnd = true;
				temporaryOverlay.originalMaterial = SpawnState.destealthMaterial;
				temporaryOverlay.inspectorCharacterModel = modelAnimator.gameObject.GetComponent<CharacterModel>();
				temporaryOverlay.alphaCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
				temporaryOverlay.animateShaderAlpha = true;
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003B30A File Offset: 0x0003950A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001133 RID: 4403
		private float stopwatch;

		// Token: 0x04001134 RID: 4404
		public static float duration = 4f;

		// Token: 0x04001135 RID: 4405
		public static string spawnSoundString;

		// Token: 0x04001136 RID: 4406
		public static GameObject spawnEffectPrefab;

		// Token: 0x04001137 RID: 4407
		public static Material destealthMaterial;
	}
}
