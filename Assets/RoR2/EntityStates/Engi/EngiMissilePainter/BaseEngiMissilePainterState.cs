using System;

namespace EntityStates.Engi.EngiMissilePainter
{
	// Token: 0x020003B2 RID: 946
	public class BaseEngiMissilePainterState : BaseSkillState
	{
		// Token: 0x060010F1 RID: 4337 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}
	}
}
