using System;
using RoR2;

namespace EntityStates.VoidJailer.Weapon
{
	// Token: 0x02000157 RID: 343
	public class ExitCapture : BaseState
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x00019DFC File Offset: 0x00017FFC
		public override void OnEnter()
		{
			base.OnEnter();
			ExitCapture.duration /= this.attackSpeedStat;
			Util.PlaySound(ExitCapture.enterSoundString, base.gameObject);
			base.PlayAnimation(ExitCapture.animationLayerName, ExitCapture.animationStateName, ExitCapture.animationPlaybackRateName, ExitCapture.duration);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00019E4B File Offset: 0x0001804B
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= ExitCapture.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x0400074E RID: 1870
		public static string animationLayerName;

		// Token: 0x0400074F RID: 1871
		public static string animationStateName;

		// Token: 0x04000750 RID: 1872
		public static string animationPlaybackRateName;

		// Token: 0x04000751 RID: 1873
		public static float duration;

		// Token: 0x04000752 RID: 1874
		public static string enterSoundString;
	}
}
