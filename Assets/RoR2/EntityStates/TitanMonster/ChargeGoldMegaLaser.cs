using System;

namespace EntityStates.TitanMonster
{
	// Token: 0x0200035C RID: 860
	public class ChargeGoldMegaLaser : ChargeMegaLaser
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x00043C1C File Offset: 0x00041E1C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireGoldMegaLaser nextState = new FireGoldMegaLaser();
				this.outer.SetNextState(nextState);
				return;
			}
		}
	}
}
