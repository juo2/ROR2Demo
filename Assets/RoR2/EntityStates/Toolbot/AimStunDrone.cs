using System;
using RoR2;

namespace EntityStates.Toolbot
{
	// Token: 0x0200018D RID: 397
	public class AimStunDrone : AimGrenade
	{
		// Token: 0x060006EA RID: 1770 RVA: 0x0001E3F4 File Offset: 0x0001C5F4
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(AimStunDrone.enterSoundString, base.gameObject);
			base.PlayAnimation("Gesture, Additive", "PrepBomb", "PrepBomb.playbackRate", this.minimumDuration);
			this.PlayAnimation("Stance, Override", "PutAwayGun");
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001E443 File Offset: 0x0001C643
		public override void OnExit()
		{
			base.OnExit();
			this.outer.SetNextState(new RecoverAimStunDrone());
			Util.PlaySound(AimStunDrone.exitSoundString, base.gameObject);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000899 RID: 2201
		public static string enterSoundString;

		// Token: 0x0400089A RID: 2202
		public static string exitSoundString;
	}
}
