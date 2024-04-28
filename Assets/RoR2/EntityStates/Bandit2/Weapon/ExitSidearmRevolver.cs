using System;

namespace EntityStates.Bandit2.Weapon
{
	// Token: 0x0200047E RID: 1150
	public class ExitSidearmRevolver : BaseSidearmState
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x0005B864 File Offset: 0x00059A64
		public override string exitAnimationStateName
		{
			get
			{
				return "SideToMain";
			}
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0005B86B File Offset: 0x00059A6B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge > this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0000CF8A File Offset: 0x0000B18A
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Any;
		}
	}
}
