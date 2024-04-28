using System;

namespace EntityStates.Commando.CommandoWeapon
{
	// Token: 0x020003EA RID: 1002
	public class FireFMJ : GenericProjectileBaseState
	{
		// Token: 0x06001200 RID: 4608 RVA: 0x0004FEF8 File Offset: 0x0004E0F8
		protected override void PlayAnimation(float duration)
		{
			base.PlayAnimation(duration);
			if (base.GetModelAnimator())
			{
				base.PlayAnimation("Gesture, Additive", "FireFMJ", "FireFMJ.playbackRate", duration);
				base.PlayAnimation("Gesture, Override", "FireFMJ", "FireFMJ.playbackRate", duration);
			}
		}
	}
}
