using System;

namespace EntityStates.Captain.Weapon
{
	// Token: 0x02000421 RID: 1057
	public class CallAirstrike2 : CallAirstrikeBase
	{
		// Token: 0x06001305 RID: 4869 RVA: 0x00054C8C File Offset: 0x00052E8C
		public override void OnExit()
		{
			this.PlayAnimation("Gesture, Override", "CallAirstrike2");
			this.PlayAnimation("Gesture, Additive", "CallAirstrike2");
			base.AddRecoil(0f, 0f, 1f, 1f);
			base.OnExit();
		}
	}
}
