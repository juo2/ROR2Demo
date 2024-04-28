using System;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003F6 RID: 1014
	public class ThrowGrenade : GenericProjectileBaseState
	{
		// Token: 0x0600123E RID: 4670 RVA: 0x000513C8 File Offset: 0x0004F5C8
		protected override void PlayAnimation(float duration)
		{
			if (base.GetModelAnimator())
			{
				base.PlayAnimation("Gesture, Additive", "ThrowGrenade", "FireFMJ.playbackRate", duration * 2f);
				base.PlayAnimation("Gesture, Override", "ThrowGrenade", "FireFMJ.playbackRate", duration * 2f);
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}
	}
}
