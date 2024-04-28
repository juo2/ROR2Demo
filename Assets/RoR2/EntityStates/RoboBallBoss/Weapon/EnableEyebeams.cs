using System;
using RoR2;

namespace EntityStates.RoboBallBoss.Weapon
{
	// Token: 0x020001E4 RID: 484
	public class EnableEyebeams : BaseState
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x00024930 File Offset: 0x00022B30
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = EnableEyebeams.baseDuration / this.attackSpeedStat;
			Util.PlaySound(EnableEyebeams.soundString, base.gameObject);
			foreach (EntityStateMachine entityStateMachine in base.gameObject.GetComponents<EntityStateMachine>())
			{
				if (entityStateMachine.customName.Contains("EyeBeam"))
				{
					entityStateMachine.SetNextState(new FireSpinningEyeBeam());
				}
			}
		}

		// Token: 0x04000A2F RID: 2607
		public static float baseDuration;

		// Token: 0x04000A30 RID: 2608
		public static string soundString;

		// Token: 0x04000A31 RID: 2609
		private float duration;
	}
}
