using System;
using RoR2;

namespace EntityStates.Vermin.Weapon
{
	// Token: 0x02000166 RID: 358
	public class TongueLash : BasicMeleeAttack
	{
		// Token: 0x06000641 RID: 1601 RVA: 0x0001AF8F File Offset: 0x0001918F
		protected override void PlayAnimation()
		{
			base.PlayCrossfade("Gesture, Additive", "TongueLash", "TongueLash.playbackRate", this.duration, 0.1f);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00014394 File Offset: 0x00012594
		protected override void AuthorityModifyOverlapAttack(OverlapAttack overlapAttack)
		{
			base.AuthorityModifyOverlapAttack(overlapAttack);
		}
	}
}
