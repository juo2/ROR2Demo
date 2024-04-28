using System;
using RoR2;

namespace EntityStates.JellyfishMonster
{
	// Token: 0x020002EC RID: 748
	public class SpawnState : BaseState
	{
		// Token: 0x06000D5E RID: 3422 RVA: 0x000386FA File Offset: 0x000368FA
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003872D File Offset: 0x0003692D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001062 RID: 4194
		public static float duration = 4f;

		// Token: 0x04001063 RID: 4195
		public static string spawnSoundString;
	}
}
