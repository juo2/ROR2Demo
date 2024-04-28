using System;

namespace EntityStates.AI.Walker
{
	// Token: 0x020004A9 RID: 1193
	public class Guard : LookBusy
	{
		// Token: 0x06001580 RID: 5504 RVA: 0x0005FB64 File Offset: 0x0005DD64
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			base.fixedAge = 0f;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000026ED File Offset: 0x000008ED
		protected override void PickNewTargetLookDirection()
		{
		}
	}
}
