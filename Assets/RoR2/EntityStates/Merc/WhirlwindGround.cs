using System;

namespace EntityStates.Merc
{
	// Token: 0x02000282 RID: 642
	public class WhirlwindGround : WhirlwindBase
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x0002F607 File Offset: 0x0002D807
		protected override void PlayAnim()
		{
			base.PlayCrossfade("FullBody, Override", "WhirlwindGround", "Whirlwind.playbackRate", this.duration, 0.1f);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0002F62C File Offset: 0x0002D82C
		public override void OnExit()
		{
			base.OnExit();
			int layerIndex = this.animator.GetLayerIndex("Impact");
			if (layerIndex >= 0)
			{
				this.animator.SetLayerWeight(layerIndex, 3f);
				this.PlayAnimation("Impact", "LightImpact");
			}
		}
	}
}
