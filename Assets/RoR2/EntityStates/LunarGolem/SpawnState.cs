using System;
using RoR2;
using UnityEngine;

namespace EntityStates.LunarGolem
{
	// Token: 0x020002B9 RID: 697
	public class SpawnState : BaseState
	{
		// Token: 0x06000C5B RID: 3163 RVA: 0x0003417F File Offset: 0x0003237F
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000341B2 File Offset: 0x000323B2
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000F16 RID: 3862
		public static float duration = 1.333f;

		// Token: 0x04000F17 RID: 3863
		public static string spawnSoundString;

		// Token: 0x04000F18 RID: 3864
		public static GameObject spawnEffectPrefab;
	}
}
