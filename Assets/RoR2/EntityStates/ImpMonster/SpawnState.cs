using System;
using RoR2;
using UnityEngine;

namespace EntityStates.ImpMonster
{
	// Token: 0x02000312 RID: 786
	public class SpawnState : BaseState
	{
		// Token: 0x06000E0A RID: 3594 RVA: 0x0003C064 File Offset: 0x0003A264
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			if (SpawnState.spawnEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, "Base", false);
			}
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0003C0C4 File Offset: 0x0003A2C4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001175 RID: 4469
		private float stopwatch;

		// Token: 0x04001176 RID: 4470
		public static float duration = 4f;

		// Token: 0x04001177 RID: 4471
		public static string spawnSoundString;

		// Token: 0x04001178 RID: 4472
		public static GameObject spawnEffectPrefab;
	}
}
