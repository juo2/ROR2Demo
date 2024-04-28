using System;
using RoR2;
using UnityEngine;

namespace EntityStates.VoidRaidCrab
{
	// Token: 0x02000122 RID: 290
	public class SpinBeamWindUp : BaseSpinBeamAttackState
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0001601D File Offset: 0x0001421D
		public override void OnEnter()
		{
			base.OnEnter();
			base.CreateBeamVFXInstance(SpinBeamWindUp.warningLaserPrefab);
			Util.PlaySound(SpinBeamWindUp.enterSoundString, base.gameObject);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00016044 File Offset: 0x00014244
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.fixedAge >= base.duration && base.isAuthority)
			{
				this.outer.SetNextState(new SpinBeamAttack());
			}
			base.SetHeadYawRevolutions(SpinBeamWindUp.revolutionsCurve.Evaluate(base.normalizedFixedAge));
		}

		// Token: 0x04000602 RID: 1538
		public static AnimationCurve revolutionsCurve;

		// Token: 0x04000603 RID: 1539
		public static GameObject warningLaserPrefab;

		// Token: 0x04000604 RID: 1540
		public static string enterSoundString;
	}
}
