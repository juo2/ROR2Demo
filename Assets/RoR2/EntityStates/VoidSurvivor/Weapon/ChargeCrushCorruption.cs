using System;

namespace EntityStates.VoidSurvivor.Weapon
{
	// Token: 0x020000FA RID: 250
	public class ChargeCrushCorruption : ChargeCrushBase
	{
		// Token: 0x0600047B RID: 1147 RVA: 0x00012D24 File Offset: 0x00010F24
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new CrushCorruption());
			}
		}
	}
}
