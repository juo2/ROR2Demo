using System;
using RoR2;

namespace EntityStates.Headstompers
{
	// Token: 0x02000331 RID: 817
	public class HeadstompersCooldown : BaseHeadstompersState
	{
		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003F858 File Offset: 0x0003DA58
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = HeadstompersCooldown.baseDuration;
			if (this.body)
			{
				Inventory inventory = this.body.inventory;
				int num = inventory ? inventory.GetItemCount(RoR2Content.Items.FallBoots) : 1;
				if (num > 0)
				{
					this.duration /= (float)num;
				}
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003F8B9 File Offset: 0x0003DAB9
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority)
			{
				this.FixedUpdateAuthority();
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003F8CF File Offset: 0x0003DACF
		private void FixedUpdateAuthority()
		{
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new HeadstompersIdle());
				return;
			}
		}

		// Token: 0x0400126B RID: 4715
		public static float baseDuration = 10f;

		// Token: 0x0400126C RID: 4716
		private float duration;
	}
}
