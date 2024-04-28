using System;
using RoR2;
using UnityEngine;

namespace EntityStates.NewtMonster
{
	// Token: 0x02000237 RID: 567
	public class SpawnState : EntityState
	{
		// Token: 0x06000A0A RID: 2570 RVA: 0x00029A90 File Offset: 0x00027C90
		public override void OnEnter()
		{
			base.OnEnter();
			base.GetModelAnimator();
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			if (SpawnState.spawnEffectPrefab)
			{
				EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, "SpawnEffectOrigin", false);
			}
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00029AF7 File Offset: 0x00027CF7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000BB8 RID: 3000
		public static float duration = 2f;

		// Token: 0x04000BB9 RID: 3001
		public static string spawnSoundString;

		// Token: 0x04000BBA RID: 3002
		public static GameObject spawnEffectPrefab;
	}
}
