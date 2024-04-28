using System;

namespace EntityStates.Bandit2
{
	// Token: 0x02000474 RID: 1140
	public class ThrowSmokebomb : BaseState
	{
		// Token: 0x06001461 RID: 5217 RVA: 0x0005AE02 File Offset: 0x00059002
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayAnimation("Gesture, Additive", "ThrowSmokebomb", "ThrowSmokebomb.playbackRate", ThrowSmokebomb.duration);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x0005AE24 File Offset: 0x00059024
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > ThrowSmokebomb.duration)
			{
				this.outer.SetNextState(new StealthMode());
			}
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04001A27 RID: 6695
		public static float duration;
	}
}
