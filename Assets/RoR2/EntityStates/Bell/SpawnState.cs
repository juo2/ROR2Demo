using System;
using RoR2;

namespace EntityStates.Bell
{
	// Token: 0x0200045A RID: 1114
	public class SpawnState : BaseState
	{
		// Token: 0x060013E4 RID: 5092 RVA: 0x00058ABA File Offset: 0x00056CBA
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00058AED File Offset: 0x00056CED
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001983 RID: 6531
		public static float duration = 4f;

		// Token: 0x04001984 RID: 6532
		public static string spawnSoundString;
	}
}
