using System;

namespace EntityStates.VoidRaidCrab.Leg
{
	// Token: 0x02000141 RID: 321
	public class JointBroken : BaseLegState
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x000184F1 File Offset: 0x000166F1
		public override void OnEnter()
		{
			base.OnEnter();
			base.legController.shouldRetract = true;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00018505 File Offset: 0x00016705
		public override void OnExit()
		{
			base.OnExit();
			base.legController.shouldRetract = false;
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00018519 File Offset: 0x00016719
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.legController.mainBodyHasEffectiveAuthority && base.legController.DoesJointExist())
			{
				this.outer.SetNextState(new Idle());
			}
		}
	}
}
