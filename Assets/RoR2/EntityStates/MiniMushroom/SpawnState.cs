using System;
using RoR2;
using UnityEngine;

namespace EntityStates.MiniMushroom
{
	// Token: 0x02000271 RID: 625
	public class SpawnState : BaseState
	{
		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002C890 File Offset: 0x0002AA90
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			EffectManager.SimpleMuzzleFlash(SpawnState.burrowPrefab, base.gameObject, "BurrowCenter", false);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0002C8E4 File Offset: 0x0002AAE4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= SpawnState.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000C75 RID: 3189
		public static GameObject burrowPrefab;

		// Token: 0x04000C76 RID: 3190
		public static float duration = 4f;

		// Token: 0x04000C77 RID: 3191
		public static string spawnSoundString;
	}
}
