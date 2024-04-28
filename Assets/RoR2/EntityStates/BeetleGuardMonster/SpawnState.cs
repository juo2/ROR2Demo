using System;
using RoR2;

namespace EntityStates.BeetleGuardMonster
{
	// Token: 0x0200046F RID: 1135
	public class SpawnState : BaseState
	{
		// Token: 0x0600144F RID: 5199 RVA: 0x0005AB6C File Offset: 0x00058D6C
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
			base.PlayAnimation("Body", "Spawn1", "Spawn1.playbackRate", SpawnState.duration);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0005AB9F File Offset: 0x00058D9F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x04001A23 RID: 6691
		public static float duration = 4f;

		// Token: 0x04001A24 RID: 6692
		public static string spawnSoundString;
	}
}
