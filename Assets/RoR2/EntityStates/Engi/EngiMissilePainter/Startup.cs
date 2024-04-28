using System;

namespace EntityStates.Engi.EngiMissilePainter
{
	// Token: 0x020003B3 RID: 947
	public class Startup : BaseEngiMissilePainterState
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x0004A467 File Offset: 0x00048667
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = Startup.baseDuration / this.attackSpeedStat;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004A481 File Offset: 0x00048681
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && this.duration <= base.fixedAge)
			{
				this.outer.SetNextState(new Paint());
			}
		}

		// Token: 0x04001561 RID: 5473
		public static float baseDuration;

		// Token: 0x04001562 RID: 5474
		private float duration;
	}
}
