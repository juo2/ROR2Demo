using System;

namespace EntityStates.Missions.Moon
{
	// Token: 0x02000243 RID: 579
	public class MoonBatteryComplete : MoonBatteryBaseState
	{
		// Token: 0x06000A46 RID: 2630 RVA: 0x0002AA9B File Offset: 0x00028C9B
		public override void OnEnter()
		{
			base.OnEnter();
			base.FindModelChild("ChargedFX").gameObject.SetActive(true);
		}
	}
}
