using System;
using RoR2;

namespace EntityStates.SurvivorPod.BatteryPanel
{
	// Token: 0x020001BE RID: 446
	public class Opening : BaseBatteryPanelState
	{
		// Token: 0x060007FE RID: 2046 RVA: 0x00021F50 File Offset: 0x00020150
		public override void OnEnter()
		{
			base.OnEnter();
			base.PlayPodAnimation("Additive", "OpenPanel", "OpenPanel.playbackRate", Opening.duration);
			Util.PlaySound(Opening.openSoundString, base.gameObject);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00021F83 File Offset: 0x00020183
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= Opening.duration && base.isAuthority)
			{
				this.outer.SetNextState(new Opened());
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0000EC55 File Offset: 0x0000CE55
		public override void OnExit()
		{
			base.OnExit();
		}

		// Token: 0x04000974 RID: 2420
		public static float duration;

		// Token: 0x04000975 RID: 2421
		public static string openSoundString;
	}
}
