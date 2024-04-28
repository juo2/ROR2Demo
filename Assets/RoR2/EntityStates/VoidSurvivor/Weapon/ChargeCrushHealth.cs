using System;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000FB RID: 251
	public class ChargeCrushHealth : ChargeCrushBase
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x00012D5A File Offset: 0x00010F5A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new CrushHealth());
			}
		}
	}
}
