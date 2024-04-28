using System;

namespace EntityStates.CaptainSupplyDrop
{
	// Token: 0x0200040F RID: 1039
	public class EntryState : BaseCaptainSupplyDropState
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0000CF8A File Offset: 0x0000B18A
		protected override bool shouldShowModel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00053CBE File Offset: 0x00051EBE
		public override void OnEnter()
		{
			base.OnEnter();
			this.duration = EntryState.baseDuration;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x00053CD1 File Offset: 0x00051ED1
		public override void FixedUpdate()
		{
			base.FixedUpdate();
			if (base.isAuthority && base.fixedAge >= this.duration)
			{
				this.outer.SetNextState(new HitGroundState());
			}
		}

		// Token: 0x0400182C RID: 6188
		public static float baseDuration;

		// Token: 0x0400182D RID: 6189
		private float duration;
	}
}
