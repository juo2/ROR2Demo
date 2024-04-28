using System;

namespace EntityStates.AncientWispMonster
{
	// Token: 0x0200049C RID: 1180
	public class FireBomb : BaseState
	{
		// Token: 0x0600152E RID: 5422 RVA: 0x0005DEF8 File Offset: 0x0005C0F8
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = FireBomb.baseDuration / this.attackSpeedStat;
			base.PlayAnimation("Gesture", "FireBomb", "FireBomb.playbackRate", this.duration);
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0005DF2D File Offset: 0x0005C12D
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0000DBF3 File Offset: 0x0000BDF3
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Frozen;
		}

		// Token: 0x04001AFC RID: 6908
		public static float baseDuration = 4f;

		// Token: 0x04001AFD RID: 6909
		private float duration;
	}
}
