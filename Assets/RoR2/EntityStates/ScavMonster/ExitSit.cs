using System;
using RoR2;

namespace EntityStates.ScavMonster
{
	// Token: 0x020001DD RID: 477
	public class ExitSit : BaseSitState
	{
		// Token: 0x06000885 RID: 2181 RVA: 0x00024018 File Offset: 0x00022218
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = ExitSit.baseDuration / this.attackSpeedStat;
			Util.PlaySound(ExitSit.soundString, base.gameObject);
			base.PlayCrossfade("Body", "ExitSit", "Sit.playbackRate", this.duration, 0.1f);
			base.modelLocator.normalizeToFloor = false;
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002407A File Offset: 0x0002227A
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x040009FF RID: 2559
		public static float baseDuration;

		// Token: 0x04000A00 RID: 2560
		public static string soundString;

		// Token: 0x04000A01 RID: 2561
		private float duration;
	}
}
