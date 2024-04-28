using System;

namespace EntityStates.Merc
{
	// Token: 0x02000280 RID: 640
	public class WhirlwindAir : WhirlwindBase
	{
		// Token: 0x06000B4E RID: 2894 RVA: 0x0002F575 File Offset: 0x0002D775
		protected override void PlayAnim()
		{
			base.PlayCrossfade("FullBody, Override", "WhirlwindAir", "Whirlwind.playbackRate", this.duration, 0.1f);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0002F597 File Offset: 0x0002D797
		public override void OnExit()
		{
			base.OnExit();
			this.PlayAnimation("FullBody, Override", "WhirlwindAirExit");
		}
	}
}
