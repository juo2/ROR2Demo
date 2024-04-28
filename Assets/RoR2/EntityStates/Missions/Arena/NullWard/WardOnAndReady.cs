using System;
using RoR2;

namespace EntityStates.Missions.Arena.NullWard
{
	// Token: 0x02000264 RID: 612
	public class WardOnAndReady : NullWardBaseState
	{
		// Token: 0x06000AC2 RID: 2754 RVA: 0x0002BF18 File Offset: 0x0002A118
		public override void OnEnter()
		{
			base.OnEnter();
			this.sphereZone.Networkradius = NullWardBaseState.wardWaitingRadius;
			this.purchaseInteraction.SetAvailable(true);
			this.childLocator.FindChild("WardOnEffect").gameObject.SetActive(true);
			this.sphereZone.enabled = true;
			Util.PlaySound(WardOnAndReady.soundLoopStartEvent, base.gameObject);
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0002BF7F File Offset: 0x0002A17F
		public override void OnExit()
		{
			Util.PlaySound(WardOnAndReady.soundLoopEndEvent, base.gameObject);
			base.OnExit();
		}

		// Token: 0x04000C47 RID: 3143
		public static string soundLoopStartEvent;

		// Token: 0x04000C48 RID: 3144
		public static string soundLoopEndEvent;
	}
}
