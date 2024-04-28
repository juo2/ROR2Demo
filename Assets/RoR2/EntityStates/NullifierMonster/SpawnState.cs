using System;
using RoR2;
using UnityEngine;

namespace EntityStates.NullifierMonster
{
	// Token: 0x02000233 RID: 563
	public class SpawnState : BaseState
	{
		// Token: 0x060009F5 RID: 2549 RVA: 0x00029388 File Offset: 0x00027588
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			if (SpawnState.spawnEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, "PortalEffect", false);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				modelTransform.GetComponent<PrintController>().enabled = true;
			}
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00029403 File Offset: 0x00027603
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000B94 RID: 2964
		public static float duration = 4f;

		// Token: 0x04000B95 RID: 2965
		public static string spawnSoundString;

		// Token: 0x04000B96 RID: 2966
		public static GameObject spawnEffectPrefab;
	}
}
