using System;
using RoR2;

namespace EntityStates.Treebot.Weapon
{
	// Token: 0x0200017D RID: 381
	public class AimMortar : AimThrowableBase
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x0001CC4A File Offset: 0x0001AE4A
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(AimMortar.enterSoundString, base.gameObject);
			base.PlayAnimation("Gesture, Additive", "PrepBomb", "PrepBomb.playbackRate", this.minimumDuration);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001CC7E File Offset: 0x0001AE7E
		public override void OnExit()
		{
			base.OnExit();
			this.outer.SetNextState(new FireMortar());
			if (!this.outer.destroying)
			{
				Util.PlaySound(AimMortar.exitSoundString, base.gameObject);
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000832 RID: 2098
		public static string enterSoundString;

		// Token: 0x04000833 RID: 2099
		public static string exitSoundString;
	}
}
