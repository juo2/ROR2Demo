using System;

namespace EntityStates.Toolbot
{
	// Token: 0x0200018C RID: 396
	public abstract class AimGrenade : AimThrowableBase
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x0001E3E1 File Offset: 0x0001C5E1
		public override void OnEnter()
		{
			base.OnEnter();
			this.detonationRadius = 7f;
		}
	}
}
