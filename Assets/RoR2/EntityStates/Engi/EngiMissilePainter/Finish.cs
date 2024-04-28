using System;

namespace EntityStates.Engi.EngiMissilePainter
{
	// Token: 0x020003B8 RID: 952
	public class Finish : BaseEngiMissilePainterState
	{
		// Token: 0x06001106 RID: 4358 RVA: 0x0004AEB6 File Offset: 0x000490B6
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.isAuthority)
			{
				this.outer.SetNextState(new Idle());
			}
		}
	}
}
