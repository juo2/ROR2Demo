using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000123 RID: 291
	public class SpinBeamWindDown : BaseSpinBeamAttackState
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00016093 File Offset: 0x00014293
		public override void OnEnter()
		{
			base.OnEnter();
			Util.PlaySound(SpinBeamWindDown.enterSoundString, base.gameObject);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000160AC File Offset: 0x000142AC
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= base.duration && base.isAuthority)
			{
				this.outer.SetNextState(new SpinBeamExit());
			}
			base.SetHeadYawRevolutions(SpinBeamWindDown.revolutionsCurve.Evaluate(base.normalizedFixedAge));
		}

		// Token: 0x04000605 RID: 1541
		public static AnimationCurve revolutionsCurve;

		// Token: 0x04000606 RID: 1542
		public static string enterSoundString;
	}
}
