using System;

namespace EntityStates.LaserTurbine
{
	// Token: 0x020002DB RID: 731
	public class ReadyState : LaserTurbineBaseState
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x00036C0A File Offset: 0x00034E0A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= ReadyState.baseDuration)
			{
				this.outer.SetNextState(new AimState());
			}
		}

		// Token: 0x04000FE7 RID: 4071
		public static float baseDuration;
	}
}
