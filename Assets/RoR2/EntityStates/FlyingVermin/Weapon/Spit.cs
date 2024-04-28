using System;

namespace EntityStates.FlyingVermin.Weapon
{
	// Token: 0x02000385 RID: 901
	public class Spit : GenericProjectileBaseState
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x0004719F File Offset: 0x0004539F
		public override void OnEnter()
		{
			base.OnEnter();
			if (base.characterBody)
			{
				base.characterBody.SetAimTimer(5f);
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x000471C4 File Offset: 0x000453C4
		protected override void PlayAnimation(float duration)
		{
			base.PlayAnimation(duration);
			base.PlayAnimation("Gesture, Additive", "Spit", "Spit.playbackRate", duration);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0000B4B7 File Offset: 0x000096B7
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.Skill;
		}
	}
}
