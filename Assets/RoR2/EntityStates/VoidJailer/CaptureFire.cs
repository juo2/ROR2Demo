using System;
using RoR2;

namespace EntityStates.VoidJailer
{
	// Token: 0x02000153 RID: 339
	public class CaptureFire : BaseState
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x0001972C File Offset: 0x0001792C
		public override void OnEnter()
		{
			base.OnEnter();
			CaptureFire.duration /= this.attackSpeedStat;
			Util.PlaySound(CaptureFire.enterSoundString, base.gameObject);
			base.PlayAnimation(CaptureFire.animationLayerName, CaptureFire.animationStateName, CaptureFire.animationPlaybackRateName, CaptureFire.duration);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001977C File Offset: 0x0001797C
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= CaptureFire.duration)
			{
				Capture nextState = new Capture();
				this.outer.SetNextState(nextState);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x000197B6 File Offset: 0x000179B6
		public override void OnExit()
		{
			base.PlayCrossfade(CaptureFire.animationLayerName, CaptureFire.crossfadeStateName, CaptureFire.animationPlaybackRateName, CaptureFire.exitCrossfadeDuration, CaptureFire.exitCrossfadeDuration);
			base.OnExit();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000EE13 File Offset: 0x0000D013
		public override InterruptPriority GetMinimumInterruptPriority()
		{
			return InterruptPriority.PrioritySkill;
		}

		// Token: 0x04000720 RID: 1824
		public static string animationLayerName;

		// Token: 0x04000721 RID: 1825
		public static string animationStateName;

		// Token: 0x04000722 RID: 1826
		public static string animationPlaybackRateName;

		// Token: 0x04000723 RID: 1827
		public static float duration;

		// Token: 0x04000724 RID: 1828
		public static string enterSoundString;

		// Token: 0x04000725 RID: 1829
		public static float exitCrossfadeDuration;

		// Token: 0x04000726 RID: 1830
		public static string crossfadeStateName;
	}
}
