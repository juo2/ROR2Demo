using System;

namespace EntityStates.Turret1
{
	// Token: 0x020003C1 RID: 961
	public class SpawnState : BaseState
	{
		// Token: 0x06001128 RID: 4392 RVA: 0x0004B970 File Offset: 0x00049B70
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.GetModelAnimator())
			{
				base.PlayAnimation("Body", "Spawn", "Spawn.playbackRate", 1.5f);
			}
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0004B99F File Offset: 0x00049B9F
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= SpawnState.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
				return;
			}
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0000E3AD File Offset: 0x0000C5AD
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Death;
		}

		// Token: 0x040015BA RID: 5562
		public static float duration = 4f;
	}
}
