using System;
using RoR2;
using UnityEngine;

namespace EntityStates.GravekeeperBoss
{
	// Token: 0x0200034B RID: 843
	public class SpawnState : BaseState
	{
		// Token: 0x06000F16 RID: 3862 RVA: 0x00041364 File Offset: 0x0003F564
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			EffectManager.SimpleMuzzleFlash(SpawnState.spawnEffectPrefab, base.gameObject, "Root", false);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000413B8 File Offset: 0x0003F5B8
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040012E8 RID: 4840
		public static float duration = 4f;

		// Token: 0x040012E9 RID: 4841
		public static string spawnSoundString;

		// Token: 0x040012EA RID: 4842
		public static GameObject spawnEffectPrefab;
	}
}
