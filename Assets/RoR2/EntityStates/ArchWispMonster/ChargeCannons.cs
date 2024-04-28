using System;
using EntityStates.GreaterWispMonster;

namespace EntityStates.ArchWispMonster
{
	// Token: 0x02000341 RID: 833
	public class ChargeCannons : ChargeCannons
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x000406DC File Offset: 0x0003E8DC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration && base.isAuthority)
			{
				FireCannons nextState = new FireCannons();
				this.outer.SetNextState(nextState);
				return;
			}
		}
	}
}
