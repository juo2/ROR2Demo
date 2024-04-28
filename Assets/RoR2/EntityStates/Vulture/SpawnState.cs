using System;
using RoR2;

namespace EntityStates.Vulture
{
	// Token: 0x020000E3 RID: 227
	public class SpawnState : BaseState
	{
		// Token: 0x0600041A RID: 1050 RVA: 0x00010F04 File Offset: 0x0000F104
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00010F37 File Offset: 0x0000F137
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000418 RID: 1048
		public static float duration = 4f;

		// Token: 0x04000419 RID: 1049
		public static string spawnSoundString;
	}
}
