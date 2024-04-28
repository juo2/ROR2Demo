using System;

namespace EntityStates.BrotherMonster
{
	// Token: 0x0200044B RID: 1099
	public class StaggerExit : StaggerBaseState
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00057624 File Offset: 0x00055824
		public override EntityState nextState
		{
			get
			{
				return new GenericCharacterMain();
			}
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0005762B File Offset: 0x0005582B
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayCrossfade("Body", "StaggerExit", "Stagger.playbackRate", this.duration, 0.1f);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x00014F2E File Offset: 0x0001312E
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Pain;
		}
	}
}
