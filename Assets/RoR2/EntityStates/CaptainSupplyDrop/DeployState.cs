using System;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x02000411 RID: 1041
	public class DeployState : BaseCaptainSupplyDropState
	{
		// Token: 0x060012C4 RID: 4804 RVA: 0x00053E80 File Offset: 0x00052080
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = DeployState.baseDuration;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00053E93 File Offset: 0x00052093
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextStateToMain();
			}
		}

		// Token: 0x04001833 RID: 6195
		public static float baseDuration;

		// Token: 0x04001834 RID: 6196
		private float duration;
	}
}
