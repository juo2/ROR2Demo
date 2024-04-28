using System;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x02000420 RID: 1056
	public class CallAirstrike1 : CallAirstrikeBase
	{
		// Token: 0x06001302 RID: 4866 RVA: 0x00054C2A File Offset: 0x00052E2A
		public override void OnEnter()
		{
			base.OnEnter();
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00054C34 File Offset: 0x00052E34
		public override void OnExit()
		{
			this.PlayAnimation("Gesture, Override", "CallAirstrike1");
			this.PlayAnimation("Gesture, Additive", "CallAirstrike1");
			base.AddRecoil(0f, 0f, -1f, -1f);
			base.OnExit();
		}
	}
}
