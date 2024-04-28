using System;
using RoR2;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001DB RID: 475
	public class EnterSit : BaseSitState
	{
		// Token: 0x0600087F RID: 2175 RVA: 0x00023F18 File Offset: 0x00022118
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = EnterSit.baseDuration / this.attackSpeedStat;
			Util.PlaySound(EnterSit.soundString, base.gameObject);
			base.PlayCrossfade("Body", "EnterSit", "Sit.playbackRate", this.duration, 0.1f);
			base.modelLocator.normalizeToFloor = true;
			base.modelLocator.modelTransform.GetComponent<AimAnimator>().enabled = true;
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00023F90 File Offset: 0x00022190
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new FindItem());
			}
		}

		// Token: 0x040009FA RID: 2554
		public static float baseDuration;

		// Token: 0x040009FB RID: 2555
		public static string soundString;

		// Token: 0x040009FC RID: 2556
		private float duration;
	}
}
