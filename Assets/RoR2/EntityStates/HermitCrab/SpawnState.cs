using System;
using RoR2;
using UnityEngine;

namespace EntityStates.HermitCrab
{
	// Token: 0x0200032A RID: 810
	public class SpawnState : BaseState
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x0003EDAC File Offset: 0x0003CFAC
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			EffectManager.SimpleMuzzleFlash(SpawnState.burrowPrefab, base.gameObject, "BurrowCenter", false);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0003EE00 File Offset: 0x0003D000
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001243 RID: 4675
		private float stopwatch;

		// Token: 0x04001244 RID: 4676
		public static GameObject burrowPrefab;

		// Token: 0x04001245 RID: 4677
		public static float duration = 4f;

		// Token: 0x04001246 RID: 4678
		public static string spawnSoundString;
	}
}
