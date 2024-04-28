using System;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x02000139 RID: 313
	public class Idle : BaseLegState
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x00017E1A File Offset: 0x0001601A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.legController.mainBodyHasEffectiveAuthority && !base.legController.DoesJointExist())
			{
				this.outer.SetNextState(new JointBroken());
			}
		}
	}
}
