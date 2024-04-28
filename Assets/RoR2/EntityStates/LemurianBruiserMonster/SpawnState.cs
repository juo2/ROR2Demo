using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LemurianBruiserMonster
{
	// Token: 0x020002D1 RID: 721
	public class SpawnState : EntityState
	{
		// Token: 0x06000CD3 RID: 3283 RVA: 0x00036214 File Offset: 0x00034414
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

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0003627B File Offset: 0x0003447B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000FB8 RID: 4024
		public static float duration = 2f;

		// Token: 0x04000FB9 RID: 4025
		public static string spawnSoundString;

		// Token: 0x04000FBA RID: 4026
		public static GameObject spawnEffectPrefab;
	}
}
