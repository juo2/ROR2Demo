using System;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000121 RID: 289
	public class SpinBeamEnter : BaseSpinBeamAttackState
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x00015FE7 File Offset: 0x000141E7
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= base.duration && base.isAuthority)
			{
				this.outer.SetNextState(new SpinBeamWindUp());
			}
		}
	}
}
