using System;
using RoR2;

namespace EntityStates.Wisp1Monster
{
	// Token: 0x020000DD RID: 221
	public class SpawnState : BaseState
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x00010869 File Offset: 0x0000EA69
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001089C File Offset: 0x0000EA9C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04000404 RID: 1028
		public static float duration = 4f;

		// Token: 0x04000405 RID: 1029
		public static string spawnSoundString;
	}
}
