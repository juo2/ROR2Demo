using System;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x02000422 RID: 1058
	public class CallAirstrike3 : CallAirstrikeBase
	{
		// Token: 0x06001307 RID: 4871 RVA: 0x00054CDC File Offset: 0x00052EDC
		public override void OnExit()
		{
			this.PlayAnimation("Gesture, Override", "CallAirstrike3");
			this.PlayAnimation("Gesture, Additive", "CallAirstrike3");
			base.AddRecoil(-2f, -2f, -0.5f, 0.5f);
			base.OnExit();
		}
	}
}
