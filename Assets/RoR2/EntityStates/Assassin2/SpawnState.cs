using System;
using RoR2;
using UnityEngine;

namespace EntityStates.Assassin2
{
	// Token: 0x0200048A RID: 1162
	public class SpawnState : BaseState
	{
		// Token: 0x060014C8 RID: 5320 RVA: 0x0005C618 File Offset: 0x0005A818
		public override void OnEnter()
		{
			base.OnEnter();
			this.PlayAnimation("Body", "Spawn");
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			if (SpawnState.spawnEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, "Base", false);
			}
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005C66E File Offset: 0x0005A86E
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			this.stopwatch += Time.fixedDeltaTime;
			if (this.stopwatch >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001A9B RID: 6811
		private float stopwatch;

		// Token: 0x04001A9C RID: 6812
		public static float animDuration = 1.5f;

		// Token: 0x04001A9D RID: 6813
		public static float duration = 4f;

		// Token: 0x04001A9E RID: 6814
		public static string spawnSoundString;

		// Token: 0x04001A9F RID: 6815
		public static GameObject spawnEffectPrefab;
	}
}
