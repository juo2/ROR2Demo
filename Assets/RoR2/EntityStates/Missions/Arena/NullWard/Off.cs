using System;

namespace EntityStates.Missions.Arena.NullWard
{
	// Token: 0x02000263 RID: 611
	public class Off : NullWardBaseState
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002BEE0 File Offset: 0x0002A0E0
		public override void OnEnter()
		{
			base.OnEnter();
			this.sphereZone.Networkradius = NullWardBaseState.wardRadiusOff;
			this.purchaseInteraction.SetAvailable(false);
			this.sphereZone.enabled = false;
		}
	}
}
