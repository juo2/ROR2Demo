using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VagrantMonster
{
	// Token: 0x020002E7 RID: 743
	public class SpawnState : BaseState
	{
		// Token: 0x06000D46 RID: 3398 RVA: 0x00037DC0 File Offset: 0x00035FC0
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			if (SpawnState.spawnEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, "SpawnOrigin", false);
			}
			Transform modelTransform = base.GetModelTransform();
			if (modelTransform)
			{
				modelTransform.GetComponent<PrintController>().enabled = true;
			}
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x00037E3B File Offset: 0x0003603B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001033 RID: 4147
		private float stopwatch;

		// Token: 0x04001034 RID: 4148
		public static float duration = 4f;

		// Token: 0x04001035 RID: 4149
		public static string spawnSoundString;

		// Token: 0x04001036 RID: 4150
		public static GameObject spawnEffectPrefab;
	}
}
