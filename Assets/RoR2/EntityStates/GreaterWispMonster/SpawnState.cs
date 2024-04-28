using System;
using RoR2;

namespace EntityStates.GreaterWispMonster
{
	// Token: 0x02000347 RID: 839
	public class SpawnState : BaseState
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x00040C91 File Offset: 0x0003EE91
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", SpawnState.duration);
			Util.PlaySound(SpawnState.spawnSoundString, base.gameObject);
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00040CC4 File Offset: 0x0003EEC4
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040012C3 RID: 4803
		public static float duration = 4f;

		// Token: 0x040012C4 RID: 4804
		public static string spawnSoundString;
	}
}
